using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
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
    }
}