using System;
using System.ComponentModel.DataAnnotations;

namespace _20104681CLDV6211POE.Models
{
    public class JobCardData
    {
        //EmployeeNo
        //EmployeeName
        //EmployeeSurname
        [Required]
        public String JobCardNo { get; set; }

        [Required]
        public String NoOfDays { get; set; }

        [Required]
        public String EmployeesAssigned { get; set; }

        public String EmployeeNo { get; set; }

        [Required]
        public String EmployeeName { get; set; }

        [Required]
        public String EmployeeSurname { get; set; }
        [Required]
        public String CustomerName { get; set; }

        [Required]
        public String CustomerSurname { get; set; }

        [Required]
        public String AddressLineOne { get; set; }

        public String AddressLineTwo { get; set; }
        public String AddressLine { get; set; }

        [Required]
        public String City { get; set; }
        [Required]
        public String PostalCode { get; set; }
        public String JobType { get; set; }

        public String FloorBoards { get; set; }
        public String Rate { get; set; }
        public String SubTotal { get; set; }
        public String Vat { get; set; }
        public String Total { get; set; }
        public String EquptmentID { get; set; }
        public String PowerPoints { get; set; }
        public String Sew { get; set; }
        public String Stairs { get; set; }
    }
}
