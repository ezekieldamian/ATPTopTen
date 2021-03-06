﻿using System;
using System.Collections.Generic;

namespace ATPTopTen.Models
{
    public class Player
    {
        public string PlayerId { get; set; }

        public int Rank { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Obsolete]
        public string NatlCode { get; set; }

        public int? CountryId { get; set; }

        public int Points { get; set; }

        public bool IsTie { get; set; }

        public string CareerSummaryHtml { get; set; }

        public Country Country { get; set; }

        public IEnumerable<HeadToHead> HeadToHeads { get; set; }

        public bool IsFavorite { get; set; }
    }
}