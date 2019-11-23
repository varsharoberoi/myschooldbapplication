using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace myschooldbapplication.Models
{
    public partial class AdTypeFee
    {
        public AdTypeFee()
        {
            Admissiopay = new HashSet<Admissiopay>();
        }
        [JsonProperty]
        public int AdTId { get; set; }
        [JsonProperty]
        public string Term { get; set; }
        [JsonProperty]
        public int? TermAmount { get; set; }
        [JsonProperty]
       
        [DataType(DataType.Date)]
        public DateTime? TermDate { get; set; }
        [JsonProperty]
        public int? AdYear { get; set; }
        [JsonProperty]
        public int? FTId { get; set; }

        public FeeType feetype { get; set; }
        [JsonProperty]
        public ICollection<Admissiopay> Admissiopay { get; set; }
    }
}
