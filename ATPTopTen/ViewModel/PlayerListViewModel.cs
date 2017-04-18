using System.Collections.Generic;
using ATPTopTen.Models;

namespace ATPTopTen.ViewModel
{
    public class PlayerListViewModel : ViewModelBase
    {
        public List<Player> Players { get; set; }

        public Player Head1Player { get; set; }

        public Player Head2Player { get; set; }
    }
}