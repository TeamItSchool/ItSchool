using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.ItSchool.Models.UserEntities
{
    public class Control
    {
        [Key]
        public int ControlId { get; set; }

        public int? TutorId { get; set; }

        [ForeignKey( "TutorId" )]
        public virtual User Tutor { get; set; }

        public int ChildId { get; set; }

        [ForeignKey( "ChildId" )]
        public virtual User Child { get; set; }

    }
}