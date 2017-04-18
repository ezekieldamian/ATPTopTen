using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ATPTopTen.Models;
using ATPTopTen.ViewModel;

namespace ATPTopTen.Controllers
{
    public class PlayerController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public PlayerController()
        {
            //todo: dependency injection!! (unit testing)
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            //dispose of db context correctly
            dbContext.Dispose();
        }

        /// <summary>
        /// Get the ATP Top Ten players list, sorted by Rank.
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="sortBy"></param>
        /// <returns></returns>
        public ActionResult TopTenList(int? pageIndex, string sortBy)
        {
            //todo: add pagination
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            //sort by Rank by default
            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Rank";
            }

            //get players (include country too) and sort by Rank (default)
            var players = dbContext.Players.Include(x => x.Country).SortBy(sortBy);

            //create view model
            var playerListViewModel = new PlayerListViewModel()
            {
                Players = new List<Player>()
            };

            foreach (var player in players)
            {
                //eager loading head to heads
                player.HeadToHeads = dbContext.HeadToHead
                    .Where(x => x.WinnerId == player.PlayerId);

                //add to list
                playerListViewModel.Players.Add(player);
            }

            //return list of viewmodels
            return View(playerListViewModel);
        }

        /// <summary>
        /// Get player details of ATP Top Ten player by rank
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="rank"></param>
        /// <returns></returns>
        //[Route("player/rank/{rank:regex(\\d{1,2}):range(1,10)}")]
        [System.Web.Mvc.Route("player/rank/{rank}")]
        public ActionResult PlayerDetailsByRank(int rank)
        {
            //get players list and country where rank = rank
            var players = dbContext.Players.Include(x => x.Country);
            var player = players.FirstOrDefault(x => x.Rank == rank);

            //return if not found
            if (player == null)
            {
                return HttpNotFound();
            }

            //eager loading head to heads
            player.HeadToHeads = dbContext.HeadToHead.Where(x => x.WinnerId == player.PlayerId);

            //create viewmodel
            var playerViewModel = new PlayerViewModel()
            {
                Players = players.ToList(),
                Player = player
            };

            //return viewmodel
            return View(playerViewModel);
        }

        /// <summary>
        /// Save Favorite setting, then reload the page
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        public ActionResult AddToFavorites(int rank)
        {
            //get players list and country where rank = rank
            var players = dbContext.Players.Include(x => x.Country);
            var player = players.FirstOrDefault(x => x.Rank == rank);

            //return if not found
            if (player == null)
            {
                return HttpNotFound();
            }

            //eager loading head to heads
            player.HeadToHeads = dbContext.HeadToHead.Where(x => x.WinnerId == player.PlayerId);

            //update favorite status
            player.IsFavorite = !player.IsFavorite;
            dbContext.SaveChanges();

            //create viewmodel
            var playerViewModel = new PlayerViewModel()
            {
                Players = players.ToList(),
                Player = player
            };

            //reload page
            //todo: call this with javascript!!
            return View("PlayerDetailsByRank", playerViewModel);
        }

        /// <summary>
        /// Get list of players needed for the Head To Head view
        /// </summary>
        /// <returns></returns>
        public ActionResult _HeadToHead()
        {
            //get players (include country too) and sort by Rank (default)
            var players = dbContext.Players.Include(x => x.Country);

            //create view model
            var playerListViewModel = new PlayerListViewModel()
            {
                Players = new List<Player>()
            };

            foreach (var player in players)
            {
                //eager loading head to heads
                player.HeadToHeads = dbContext.HeadToHead
                    .Where(x => x.WinnerId == player.PlayerId);

                //add to list
                playerListViewModel.Players.Add(player);
            }

            //return list of viewmodels
            return View(playerListViewModel);
        }

        //public ActionResult ChangeHeadLeft(string playerId)
        //{
        //    //get players list and country where PlayerId = playerId
        //    var player = dbContext.Players.Include(x => x.Country)
        //        .FirstOrDefault(x => x.PlayerId == playerId);

        //    //return if not found
        //    if (player == null)
        //    {
        //        return HttpNotFound();
        //    }

        //    //eager loading head to heads
        //    player.HeadToHeads = dbContext.HeadToHead.Where(x => x.WinnerId == player.PlayerId);

        //    //create viewmodel
        //    var playerViewModel = new PlayerViewModel()
        //    {
        //        Player = player
        //    };

        //    //reload page
        //    //todo: call this with javascript!!
        //    return View("_HeadToHead", playerViewModel);
        //}
    }
}