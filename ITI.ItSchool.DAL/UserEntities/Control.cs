using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;
using ITI.ItSchool.Models.UserEntity;

namespace ITI.ItSchool.Models
{
    public class Control
    {
        [Key]
        public int ControlId { get; set; }

        public int TutorId { get; set; }

        [ForeignKey( "UserId" )]
        public virtual User Tutor { get; set; }

        public int ChildId { get; set; }

        [ForeignKey( "ChildId" )]
        public virtual User Child { get; set; }

    }
}