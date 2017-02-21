using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UniversityManagementSystem.Models
{
    [Table("Student")]
    public class Student
    {
        public int StudentId { set; get; }

        [Display(Name = "Student RegNo")]
        public string RegNo { set; get; }

        [Required(ErrorMessage = "Student Name can't be empty")]
        [Display(Name = "Student Name")]
        public string Name { set; get; }

        [Required(ErrorMessage = "Student Email can't be empty")]
        [Display(Name = "Student Email")]
        [Remote("CheckStudentEmail", "Student", ErrorMessage = "Email address already exists.")]
        public string Email { set; get; }

        [Required(ErrorMessage = "Student Contact number can't be empty")]
        [Display(Name = "Contact Number")]
        public int ContactNo { set; get; }

        [Required]
        [Display(Name = "Date ")]
        public DateTime Date { set; get; }

        public int DepartmentId { set; get; }
        public virtual Department Department { set; get; }
    }
}