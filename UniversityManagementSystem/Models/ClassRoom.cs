using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace UniversityManagementSystem.Models
{
    [Table("ClassRoom")]
    public class ClassRoom
    {
        public int ClassRoomId { set; get; }
        public string RoomNo { set; get; }
    }
}