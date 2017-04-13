using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;
using WebApplication5.Models;
using WebApplication5.ViewModel;

namespace WebApplication5.Controllers
{
    public class PlayerController : Controller
    {
        private ApplicationDbContext dbContext;

        public PlayerController()
        {
            dbContext = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            dbContext.Dispose();
        }
        
        public ActionResult Index(int? pageIndex, string sortBy)
        {
            if (!pageIndex.HasValue)
            {
                pageIndex = 1;
            }

            if (string.IsNullOrWhiteSpace(sortBy))
            {
                sortBy = "Rank";
            }

            var players = dbContext.Players.SortBy(sortBy);
            
            return View(players);
        }

        //[Route("player/rank/{position:regex(\\d{1,2}):range(1,10)}")]
        [Route("player/rank/{position}")]
        public ActionResult ByRank(int? position)
        {
            if (!position.HasValue)
            {
                position = 1;
            }

            var player = dbContext.Players.FirstOrDefault(x => x.Rank == position.Value);

            if (player == null)
            {
                return HttpNotFound();
            }

            var playerViewModel = new PlayerViewModel()
            {
                Player = player
            };

            return View(playerViewModel);
        }
    }
}