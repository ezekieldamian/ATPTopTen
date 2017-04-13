using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class Player
    {
        public string PlayerId { get; set; }

        public int Rank { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string NatlCode { get; set; }

        public int Points { get; set; }

        public bool IsTie { get; set; }
    }
}