using System;
using System.ComponentModel.DataAnnotations;


namespace _20104681CLDV6211POE.Models
{
    public class RateData
    {
        [Required]
        public int Rate { get; set; }

        [Required]
        public String JobType { get; set; }
    }
}
