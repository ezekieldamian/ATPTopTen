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
        /// Get ViewModels of the ATP Top Ten players list, sorted by Rank.
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

            //create list of viewmodels
            var listOfPlayerViewModels = new List<PlayerViewModel>();

            foreach (var player in players)
            {
                //eager loading head to heads
                player.HeadToHeads = dbContext.HeadToHead
                    .Where(x => x.WinnerId == player.PlayerId);

                //create view model
                var playerViewModel = new PlayerViewModel()
                {
                    Player = player
                };

                //add to list
                listOfPlayerViewModels.Add(playerViewModel);
            }

            //return list of viewmodels
            return View(listOfPlayerViewModels);
        }

        /// <summary>
        /// Get player details of ATP Top Ten player by rank
        /// </summary>
        /// <param name="rank"></param>
        /// <returns></returns>
        //[Route("player/rank/{rank:regex(\\d{1,2}):range(1,10)}")]
        [Route("player/rank/{rank}")]
        public ActionResult PlayerDetailsByRank(int rank)
        {
            //get players list and country where rank = rank
            var player = dbContext.Players.Include(x => x.Country)
                .FirstOrDefault(x => x.Rank == rank);

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
                Player = player
            };

            //return viewmodel
            return View(playerViewModel);
        }

        public ActionResult AddToFavorites(int rank)
        {
            //get players list and country where rank = rank
            var player = dbContext.Players.Include(x => x.Country)
                .FirstOrDefault(x => x.Rank == rank);

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
                Player = player
            };

            //reload page
            //todo: call this with javascript!!
            return View("PlayerDetailsByRank", playerViewModel);
        }
    }
}