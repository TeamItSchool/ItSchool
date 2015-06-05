using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Chapter
    {
        [Key, ForeignKey("Grade")]
        public int ChapterId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        public int ThemeId { get; set; }

        [ForeignKey( "ThemeId" )]
        public virtual Theme Theme { get; set; }

        public int GradeId { get; set; }

        [ForeignKey("GradeId")]
        public virtual Grade Grade { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}