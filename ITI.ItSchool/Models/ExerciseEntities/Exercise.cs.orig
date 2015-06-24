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
    [Table( "Exercises" )]
    public class Exercise
    {
        public int ExerciseId { get; set; }

        public int ExerciseTypeId { get; set; }

        [ForeignKey("ExerciseTypeId")]
        public virtual ExerciseType ExerciseType { get; set; }
    }
}