﻿using ITI.ItSchool.Models.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.SchoolEntities
{
    [Table("Chapters")]
    public class Chapter
    {
        [Key, ForeignKey("Class")]
        public int ChapterId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        
        public int ThemeId { get; set; }

        [ForeignKey("ThemeId")]
        public virtual Theme Theme { get; set; }

        public int ClassId { get; set; }

        [ForeignKey("ClassId")]
        public virtual Class Class { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }

        public virtual ICollection<Exercise> Exercises { get; set; }
    }
}