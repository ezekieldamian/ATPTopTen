using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using ATPTopTen.Models;
using ATPTopTen.ViewModel;
using Microsoft.Ajax.Utilities;

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
        public ActionResult TopTenList(int? pageIndex, string sortBy, string player1Id, string player2Id)
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

            //initialize player 1 and 2
            var player1 = player1Id.IsNullOrWhiteSpace()
                //if null, get the first in the list
                ? players.FirstOrDefault()
                //if not null, get the first in the list that matches player 1 id
                : players.FirstOrDefault(x => x.PlayerId == player1Id);

            var player2 = player2Id.IsNullOrWhiteSpace()
                //if null, get the first in the list that is not player 1
                ? players.FirstOrDefault(x => x.PlayerId != player1.PlayerId)
                //if not null, get the first in the list that matches player 2 id
                : players.FirstOrDefault(x => x.PlayerId == player2Id);

            //create view model
            var playerListViewModel = new PlayerListViewModel()
            {
                Players = new List<Player>(),
                Head1Player = player1,
                Head2Player = player2
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
        /// <param name="rank"></param>
        /// <returns></returns>
        //[Route("player/rank/{rank:regex(\\d{1,2}):range(1,10)}")]
        [Route("player/rank/{rank}")]
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
    }
}