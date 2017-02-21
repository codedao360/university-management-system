using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{

    [Table("Enrollment")]
    public class Enrollment
    {
        public int EnrollmentId { set; get; }

        [Required(ErrorMessage = "Student Id can't be empty")]
     
        public int StudentId { set; get; }

        [Required(ErrorMessage = "Student Course can't be empty")]
        [Display(Name = "Select Course")]
        public int CourseId { set; get; }

        [Required(ErrorMessage = "Date can't be empty")]
        [Display(Name = "Date")]
        public DateTime Date { set; get; }

        public int? GradeLetterId { set; get; }
        public virtual GradeLetter GradeLetter { set; get; }
        public virtual Student Student { set; get; }
        public virtual Course Course { set; get; }
    }
}