using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using WebApplication5.DataTransferObjects;
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
        public IEnumerable<Player> GetPlayers()
        {
            return dbContext.Players.ToList();
        }

        // GET /api/players/1
        public Player GetPlayer(int rank)
        {
            var player = dbContext.Players.SingleOrDefault(x => x.Rank == rank);

            if (player == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            return player;
        }

        //POST /api/players
        [HttpPost]
        public Player CreatePlayer(Player player)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            dbContext.Players.Add(player);
            dbContext.SaveChanges();

            return player;
        }

        // PUT /api/players/1
        [HttpPut]
        public void UpdatePlayer(Player player, int rank)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var playerInDb = dbContext.Players.SingleOrDefault(x => x.PlayerId == player.PlayerId);

            if (playerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            playerInDb.Rank = player.Rank;
            dbContext.SaveChanges();
        }

        //DELETE /api/players/1
        [HttpDelete]
        public void DeletePlayer(int rank)
        {
            if (!ModelState.IsValid)
            {
                throw new HttpResponseException(HttpStatusCode.BadRequest);
            }

            var playerInDb = dbContext.Players.SingleOrDefault(x => x.Rank == rank);

            if (playerInDb == null)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }

            dbContext.Players.Remove(playerInDb);
            dbContext.SaveChanges();
        }

        // GET /api/players/initialData
        [HttpGet]
        [Route("api/players/initialData")]
        public async Task<IEnumerable<Player>> GetInitialPlayersData()
        {
            try
            {
                var players = await GetPlayerDataFromATP();

                return players;
            }
            catch
            {
                var players = GetPlayerDataFromResource();

                return players;
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