using System;
using System.ComponentModel.DataAnnotations;

namespace _20104681CLDV6211POE.Models
{
    public class EmployeeData
    {
        //EmployeeNo
        //EmployeeName
        //EmployeeSurname
        [Required]
        public String EmployeeNo { get; set; }

        [Required]
        public String EmployeeName { get; set; }

        [Required]
        public String EmployeeSurname { get; set; }

        public String JobCardNo { get; set; }

    }
}
