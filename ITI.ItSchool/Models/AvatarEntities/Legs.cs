using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.AvatarEntities
{
    public class Legs
    {
        [Key]
        public int LegsId { get; set; }

        [Required]
        [MinLength(3)]
        [MaxLength(45)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Link { get; set; }

    }
}