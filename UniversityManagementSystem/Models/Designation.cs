using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    [Table("Designation")]
    public class Designation
    {
        public int DesignationId { set; get; }
        public string Name { set; get; }
    }
}