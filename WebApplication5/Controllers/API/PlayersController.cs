using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using AutoMapper;
using Newtonsoft.Json;
using WebApplication5.DataTransferObjects;
using WebApplication5.HelperClasses;
using WebApplication5.Models;
using WebApplication5.Properties;
using System.Data.Entity;

namespace WebApplication5.Controllers.API
{
    public class PlayersController : ApiController
    {
        private readonly ApplicationDbContext dbContext;

        public PlayersController()
        {
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }

        // GET /api/players
        [HttpGet]
        [ResponseType(typeof(IEnumerable<PlayerDto>))]
        public IHttpActionResult GetPlayers()
        {
            try
            {
                return Ok(dbContext.Players.Include(x=>x.Country)
                    .ToList().Select(Mapper.Map<Player, PlayerDto>));
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        // GET /api/players/ABCD
        [HttpGet]
        [Route("api/players/{playerId}")]
        public IHttpActionResult GetPlayerByPlayerId(string playerId)
        {
            try
            {
                var player = dbContext.Players.Include(x => x.Country)
                    .SingleOrDefault(x => x.PlayerId == playerId);

                if (player == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<Player, PlayerDto>(player));
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        // GET /api/players/1
        [HttpGet]
        [Route("api/players/rank/{rank}")]
        public IHttpActionResult GetPlayerByRank(int rank)
        {
            try
            {
                var player = dbContext.Players.Include(x => x.Country)
                    .SingleOrDefault(x => x.Rank == rank);

                if (player == null)
                {
                    return NotFound();
                }

                return Ok(Mapper.Map<Player, PlayerDto>(player));
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        //POST /api/players
        [HttpPost]
        public IHttpActionResult CreatePlayer(PlayerDto playerDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (playerDto == null)
                {
                    return BadRequest("Required parameter Player is null");
                }

                var player = Mapper.Map<PlayerDto, Player>(playerDto);

                dbContext.Players.Add(player);
                dbContext.SaveChanges();

                return Created(new Uri(Request.RequestUri.ToString()), playerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        // PUT /api/players/ABCD
        [HttpPut]
        [Route("api/players/{playerId}")]
        public IHttpActionResult UpdatePlayer([FromBody]PlayerDto playerDto, string playerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (playerDto == null)
                {
                    return BadRequest("Required parameter Player is null");
                }

                var playerInDb = dbContext.Players.Include(x => x.Country)
                    .SingleOrDefault(x => x.PlayerId == playerId);

                if (playerInDb == null)
                {
                    return NotFound();
                }

                //update all fields with automapper
                Mapper.Map(playerDto, playerInDb);

                dbContext.SaveChanges();

                return Ok(playerDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        //DELETE /api/players/ABCD
        [HttpDelete]
        [Route("api/players/{playerId}")]
        public IHttpActionResult DeletePlayer(string playerId)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var playerInDb = dbContext.Players.SingleOrDefault(x => x.PlayerId == playerId);

                if (playerInDb == null)
                {
                    return NotFound();
                }

                dbContext.Players.Remove(playerInDb);
                dbContext.SaveChanges();

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ExceptionHelper.FindInnermostException(ex).Message);
            }
        }

        // GET /api/players/initialData
        [HttpGet]
        [Route("api/players/initialData")]
        public async Task<IEnumerable<Player>> GetInitialPlayersData()
        {
            try
            {
                return await GetPlayerDataFromATP();
            }
            catch
            {
                try
                {
                    return GetPlayerDataFromResource();
                }
                catch (Exception ex)
                {
                    throw new Exception("Error while getting player data from resource.", ex);
                }
            }
        }

        private static IEnumerable<Player> GetPlayerDataFromResource()
        {
            var players = Resources.PlayersJsonString;
            var resultObjects = JsonConvert.DeserializeObject<IEnumerable<Player>>(players);

            return resultObjects;
        }

        //todo: refactor this
        // ReSharper disable once InconsistentNaming
        private static async Task<IEnumerable<Player>> GetPlayerDataFromATP()
        {
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