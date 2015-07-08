using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models.ExerciseEntities
{
    [Table( "ExercisesResults" )]
    public class ExercisesResults
    {
        [Key]
        public int ExerciseResultsId { get; set; }

        [ForeignKey( "ExerciseResultsId" )]
        public virtual ExerciseAffectation ExerciseAffectation { get; set; }

        public string Description { get; set; }
    }
}
