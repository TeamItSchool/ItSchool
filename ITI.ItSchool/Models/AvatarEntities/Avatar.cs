using ITI.ItSchool.Models.AvatarEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Avatar
    {
        [Key]
        public int AvatarId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        public int FootId { get; set; }

        [ForeignKey( "FootId" )]
        public virtual Foot Feet { get; set; }

        public int LegsId { get; set; }

        [ForeignKey( "LegsId" )]
        public virtual Legs Legs { get; set; }

        public int BodyId { get; set; }

        [ForeignKey("BodyId")]
        public virtual Body Body { get; set; }


    }
}