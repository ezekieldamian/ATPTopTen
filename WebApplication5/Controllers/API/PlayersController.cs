using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Newtonsoft.Json;
using WebApplication5.Models;

namespace WebApplication5.Controllers.API
{
    public class PlayersController : ApiController
    {
        private ApplicationDbContext dbContext;

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
        public IEnumerable<Player> GetInitialPlayersData()
        {
            var players = GetDataFromATP();

            return players.Result;
        }

        //todo: refactor this
        private async Task<IEnumerable<Player>> GetDataFromATP()
        {
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