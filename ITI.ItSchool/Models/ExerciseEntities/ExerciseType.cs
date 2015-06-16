using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ITI.ItSchool.Models.ExercisesEntities
{
    [Table( "ExercisesTypes" )]
    public class ExerciseType
    {
        [Key]
        public int ExerciseTypeId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 45 )]
        public string Name { get; set; }

        [MaxLength( 200 )]
        public string Description { get; set; }
    }
}