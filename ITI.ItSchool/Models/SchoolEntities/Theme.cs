using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.SchoolEntities
{
    public class Theme
    {
        [Key]
        public int ThemeId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 200 )]
        public string Name { get; set; }

        public int MatterId { get; set; }

        [ForeignKey("MatterId")]
        public Matter Matter { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}