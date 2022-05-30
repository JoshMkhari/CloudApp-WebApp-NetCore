using System;
using System.ComponentModel.DataAnnotations;


namespace _20104681CLDV6211POE.Models
{
    public class CustomerData
    {
        [Required]
        public String CustomerID { get; set; }

        [Required]
        public String CustomerName { get; set; }

        [Required]
        public String CustomerSurname { get; set; }

        [Required]
        public String AddressLineOne { get; set; }

        public String AddressLineTwo { get; set; }

        [Required]
        public String City { get; set; }

        [Required]
        public String PostalCode { get; set; }
        [Required]
        public String JobCardNo { get; set; }
        [Required]
        public String NoOfDays { get; set; }
        [Required]
        public String JobType { get; set; }

        [Required]
        public int StandardFloorBoards { get; set; }
        [Required]
        public int PowerPoints { get; set; }
        [Required]
        public int StandardElectricalWiting { get; set; }
        [Required]
        public String currentEquiptmenID { get; set; }
    }
}
