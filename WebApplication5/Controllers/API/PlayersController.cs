using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Newtonsoft.Json;
using WebApplication5.Models;
using WebApplication5.Properties;

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
        [ResponseType(typeof(IEnumerable<Player>))]
        public IHttpActionResult GetPlayers()
        {
            try
            {
                return Ok(dbContext.Players.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET /api/players/1
        [HttpGet]
        [Route("api/players/{rank}")]
        public IHttpActionResult GetPlayer(int rank)
        {
            try
            {
                var player = dbContext.Players.SingleOrDefault(x => x.Rank == rank);

                if (player == null)
                {
                    return NotFound();
                }

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //POST /api/players
        [HttpPost]
        public IHttpActionResult CreatePlayer(Player player)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                if (player == null)
                {
                    return BadRequest("Required parameter Player is null");
                }

                dbContext.Players.Add(player);
                dbContext.SaveChanges();

                return Created(new Uri(Request.RequestUri.ToString()), player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT /api/players/1
        [HttpPut]
        [Route("api/players/{rank}")]
        public IHttpActionResult UpdatePlayer([FromBody]Player player, int rank)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
            
                if (player == null)
                {
                    return BadRequest("Required parameter Player is null");
                }

                var playerInDb = dbContext.Players.SingleOrDefault(x => x.PlayerId == player.PlayerId);

                if (playerInDb == null)
                {
                    return NotFound();
                }

                //todo: add automapper to update all properties automatically
                playerInDb.CareerSummaryHtml = player.CareerSummaryHtml;
                playerInDb.FirstName = player.FirstName;
                playerInDb.LastName = player.LastName;
                playerInDb.IsTie = player.IsTie;
                playerInDb.NatlCode = player.NatlCode;
                playerInDb.PlayerId = player.PlayerId;
                playerInDb.Points = player.Points;
                playerInDb.Rank = player.Rank;

                dbContext.SaveChanges();

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //DELETE /api/players/1
        [HttpDelete]
        [Route("api/players/{rank}")]
        public IHttpActionResult DeletePlayer(int rank)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }

                var playerInDb = dbContext.Players.SingleOrDefault(x => x.Rank == rank);

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
                return BadRequest(ex.Message);
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