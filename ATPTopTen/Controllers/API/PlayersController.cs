using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Newtonsoft.Json;
using ATPTopTen.DataTransferObjects;
using ATPTopTen.HelperClasses;
using ATPTopTen.Models;
using ATPTopTen.Properties;

namespace ATPTopTen.Controllers.API
{
    public class PlayersController : ApiController
    {
        private readonly ApplicationDbContext dbContext;

        public PlayersController()
        {
            //todo: dependency injection!!
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //dispose dbcontext appropriately
            dbContext.Dispose();
        }

        /// <summary>
        /// GET ATP Top Ten players.
        /// </summary>
        /// <returns></returns>
        // GET /api/players
        [HttpGet]
        [ResponseType(typeof(IEnumerable<PlayerDto>))]
        public IHttpActionResult GetPlayers()
        {
            try
            {
                //get player data and eager load country too!!
                var players = dbContext.Players
                    .Include(x => x.Country).ToList();

                foreach (var player in players)
                {
                    //load head to heads
                    player.HeadToHeads =
                        dbContext.HeadToHead.Where(x => x.WinnerId == player.PlayerId).ToList();
                }

                //convert to dto and return
                return Ok(players.Select(Mapper.Map<Player, PlayerDto>));
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        /// <summary>
        /// Get ATP Top Ten Player by Player Id.
        /// Player Id is a string and it's the player's unique identifier.
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        // GET /api/players/ABCD
        [HttpGet]
        [Route("api/players/{playerId}")]
        public IHttpActionResult GetPlayerByPlayerId(string playerId)
        {
            try
            {
                //get player data and eager load country too
                var player = dbContext.Players
                    .Include(x => x.Country)
                    .SingleOrDefault(x => x.PlayerId == playerId);

                //return if not found
                if (player == null)
                {
                    return NotFound();
                }

                //get head to head data
                player.HeadToHeads =
                    dbContext.HeadToHead.Where(x => x.WinnerId == player.PlayerId).ToList();

                //convert to dto and return
                return Ok(Mapper.Map<Player, PlayerDto>(player));
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        /// <summary>
        /// Get ATP Top Ten Player by Rank
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        // GET /api/players/1
        [HttpGet]
        [Route("api/players/rank/{rank}")]
        public IHttpActionResult GetPlayerByRank(int rank)
        {
            try
            {
                //get player data and country
                var player = dbContext.Players
                    .Include(x => x.Country)
                    .SingleOrDefault(x => x.Rank == rank);

                //return if not found
                if (player == null)
                {
                    return NotFound();
                }

                //get head to head data
                player.HeadToHeads =
                    dbContext.HeadToHead.Where(x => x.WinnerId == player.PlayerId).ToList();

                //convert to dto and return
                return Ok(Mapper.Map<Player, PlayerDto>(player));
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        /// <summary>
        /// Insert an ATP Top Ten player to the list.
        /// </summary>
        /// <param name="playerDto"></param>
        /// <returns></returns>
        //POST /api/players
        [HttpPost]
        public IHttpActionResult CreatePlayer(PlayerDto playerDto)
        {
            try
            {
                //return if not valid request
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                //return if player is null
                if (playerDto == null)
                {
                    return BadRequest("Required parameter Player is null");
                }

                //convert dto into player
                var player = Mapper.Map<PlayerDto, Player>(playerDto);

                //add to local db and save changes
                dbContext.Players.Add(player);
                dbContext.SaveChanges();

                //return created object
                return Created(new Uri(Request.RequestUri.ToString()), playerDto);
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        /// <summary>
        /// Update ATP Top Ten Player info.
        /// </summary>
        /// <param name="playerDto"></param>
        /// <param name="playerId"></param>
        /// <returns></returns>
        // PUT /api/players/ABCD
        [HttpPut]
        [Route("api/players/{playerId}")]
        public IHttpActionResult UpdatePlayer([FromBody]PlayerDto playerDto, string playerId)
        {
            try
            {
                //return if bad request
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                //return if player is null
                if (playerDto == null)
                {
                    return BadRequest("Required parameter Player is null");
                }

                //get player and country info
                var playerInDb = dbContext.Players.Include(x => x.Country)
                    .SingleOrDefault(x => x.PlayerId == playerId);

                //todo: create if not found?
                //return if no player found
                if (playerInDb == null)
                {
                    return NotFound();
                }

                //update all fields with automapper
                Mapper.Map(playerDto, playerInDb);

                //save to db
                dbContext.SaveChanges();

                //return updated dto
                return Ok(playerDto);
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        /// <summary>
        /// Delete ATP Top Ten player from the list
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        //DELETE /api/players/ABCD
        [HttpDelete]
        [Route("api/players/{playerId}")]
        public IHttpActionResult DeletePlayer(string playerId)
        {
            try
            {
                //return if not valid state
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                //get player record in db
                var playerInDb = dbContext.Players.SingleOrDefault(x => x.PlayerId == playerId);

                //return if not found
                if (playerInDb == null)
                {
                    return NotFound();
                }

                //remove from db and save changes
                dbContext.Players.Remove(playerInDb);
                dbContext.SaveChanges();

                //return ok if no errors
                return Ok();
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        [Route("api/players/headtohead/{player1id}/{player2Id}")]
        public IHttpActionResult GetHeadToHeadData(string player1Id, string player2Id)
        {
            try
            {
                var winner = dbContext.Players.ToList().SingleOrDefault(x => x.PlayerId == player1Id);
                var opponent = dbContext.Players.ToList().SingleOrDefault(x => x.PlayerId == player2Id);

                //return if not found
                if (winner == null || opponent == null)
                {
                    //todo: make this check on the client side!!!
                    return NotFound();
                }

                if (winner == opponent)
                {
                    //todo: make this check on the client side!!!
                    return BadRequest();
                }

                //get head to head data
                winner.HeadToHeads = dbContext.HeadToHead.Where(x => x.WinnerId == winner.PlayerId).ToList();
                opponent.HeadToHeads = dbContext.HeadToHead.Where(x => x.WinnerId == opponent.PlayerId).ToList();

                //player first name and last name
                var player1Fullname = winner.FirstName + " " + winner.LastName;
                var player2FullName = opponent.FirstName + " " + opponent.LastName;

                //get winner data
                var player1Wins = winner.HeadToHeads.FirstOrDefault(x => x.OpponentId == player2Id)?.NumberOfWins.ToString() ?? "-";
                var player2Wins = opponent.HeadToHeads.FirstOrDefault(x => x.OpponentId == player1Id)?.NumberOfWins.ToString() ?? "-";

                var returnString = $"{player1Fullname} ({player1Wins}) Vs. {player2FullName} ({player2Wins})";

                //convert to dto and return
                return Ok(returnString);
            }
            catch (Exception ex)
            {
                //return innermost exception if any
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        /// <summary>
        /// Get initial ATP Top Ten player info to insert in database.
        /// If ATP service is down, get saved data in resources.
        /// </summary>
        /// <returns></returns>
        // GET /api/players/initialData
        [HttpGet]
        [Route("api/players/initialData")]
        public async Task<IEnumerable<Player>> GetInitialPlayersData()
        {
            try
            {
                //try to get player data from ATP web service
                return await GetPlayerDataFromATP();
            }
            catch
            {
                try
                {
                    //if failed, read from saved resource
                    return GetPlayerDataFromResource();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while getting player data from resource.", ex);
                }
            }
        }

        /// <summary>
        /// Get JSON player data from resource (to be used only if ATP service is down).
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Player> GetPlayerDataFromResource()
        {
            var players = Resources.PlayersJsonString;
            var resultObjects = JsonConvert.DeserializeObject<IEnumerable<Player>>(players);

            return resultObjects;
        }

        /// <summary>
        /// Read player data from ATP web service.
        /// </summary>
        /// <returns></returns>
        // ReSharper disable once InconsistentNaming
        private static async Task<IEnumerable<Player>> GetPlayerDataFromATP()
        {
            //todo: refactor this (move to logic layer)

            //testing
            //throw new Exception("Service Down");

            var apiBaseAddress = ConfigurationManager.AppSettings["ServiceUrl"];
            var apiKey = ConfigurationManager.AppSettings["Key"];

            var fullUri = apiBaseAddress + "Token/Rankings" + "?token=" + apiKey;

            var client = new HttpClient();

            var response = await client.GetAsync(fullUri);

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var resultObjects = JsonConvert.DeserializeObject<IEnumerable<Player>>(jsonResult);

            return resultObjects;
        }
    }
}