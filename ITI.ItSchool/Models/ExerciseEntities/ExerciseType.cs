using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ITI.ItSchool.Models.ExercisesEntities
{
    public class ExerciseType
    {
        [Key]
        public int ExerciseTypeId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}