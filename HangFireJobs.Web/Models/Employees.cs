using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HangFireJobs.Web.Models
{
    [Table("Employee", Schema = "HumanResources")]
    public class Employees
    {
        [Key]
        public int BusinessEntityID { get; set; }
        public string NationalIDNumber { get; set; }
        public string JobTitle { get; set; }
        public string Gender { get; set; }
        public DateTime HireDate { get; set; }
        public bool CurrentFlag { get; set; }
    }
}
