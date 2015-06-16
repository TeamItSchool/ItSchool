using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.SchoolEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.Entities
{
    public class Exercise
    {
        [Key]
        public int ExerciseId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 200 )]
        public string Name { get; set; }

        public int ChapterId { get; set; }

        [ForeignKey("ChapterId")]
        public virtual Chapter Chapter { get; set; }
        
        public int LevelId { get; set; }

        [ForeignKey("LevelId")]
        public Level Level { get; set; }

        
        public int ExerciseTypeId { get; set; }

        [ForeignKey("ExerciseTypeId")]
        public virtual ExerciseType ExerciseType { get; set; }

        [MaxLength( 100 )]
        public string Data { get; set; }
    }
}