using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.SchoolEntities
{
    [Table("Matters")]
    public class Matter
    {
        [Key]
        public int MatterId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}