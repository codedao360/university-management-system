using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{

    [Table("Semester")]
    public class Semester
    {
        public int SemesterId { set; get; }

        [Display(Name = "Semester Name")]
        public string Name { set; get; }
    }
}