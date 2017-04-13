using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class HeadToHead
    {
        [Key, Column(Order = 0)]
        public string WinnerId { get; set; }

        [Key, Column(Order = 1)]
        public string OpponentId { get; set; }

        public int? NumberOfWins { get; set; }
    }
}