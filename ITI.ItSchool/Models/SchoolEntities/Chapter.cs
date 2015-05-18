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
        [Key]
        public int ChapterId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        public int ThemeId { get; set; }

        [ForeignKey( "ThemeId" )]
        public Theme Theme { get; set; }

        [ForeignKey( "GradeId" )]
        public int GradeId { get; set; }

        public Grade Grade { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}