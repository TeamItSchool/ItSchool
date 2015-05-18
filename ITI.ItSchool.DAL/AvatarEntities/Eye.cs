using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Eye
    {
        [Key]
        public int EyeId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        public string Link { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}