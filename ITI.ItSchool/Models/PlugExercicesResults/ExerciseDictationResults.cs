using ITI.ItSchool.Models.ExerciseEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models.PlugExercicesResults
{
    [Table("ExerciseDictationResults")]
    public class ExerciseDictationResults
    {
        public int ExerciseDictationResultsId { get; set; }

        [ForeignKey( "ExerciseDictationResultsId" )]
        public virtual ExercisesResults ExercisesResults { get; set; }

        public string Name { get; set; }
        public string SubmittedText { get; set; }

        public string Remarks { get; set; }

        public string Mark { get; set; }

        public int Points { get; set; }
    }
}
