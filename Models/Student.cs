using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public class Student
    {
        [Key]
        public int RollNo { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Department { get; set; }
    }
}