using System;
using System.Collections.Generic;
using ATPTopTen.Models;

namespace ATPTopTen.DataTransferObjects
{
    public class PlayerDto
    {
        public string PlayerId { get; set; }

        public int Rank { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Points { get; set; }

        public bool IsTie { get; set; }
        public string CareerSummaryHtml { get; set; }

        public int? CountryId { get; set; }

        public CountryDto Country { get; set; }

        public IEnumerable<HeadToHeadDto> HeadToHeads { get; set; }
    }
}