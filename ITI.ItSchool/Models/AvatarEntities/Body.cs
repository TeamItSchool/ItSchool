using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.ItSchool.Models.AvatarEntities
{
    public class Body
    {
        [Key]
        public int BodyId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(512)]
        public string Link { get; set; }
    }
}