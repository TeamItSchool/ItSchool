using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.SchoolEntities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.PlugExercises
{
    [Table( "ExerciseDictations" )]
    public class ExerciseDictation
    {
        public int ExerciseDictationId { get; set; }

        [MaxLength( 50 )]
        public string Name { get; set; }

        public int ExerciseTypeId { get; set; }

        [ForeignKey("ExerciseTypeId")]
        public virtual ExerciseType ExerciseType { get; set; }

        public int LevelId { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        public int ChapterId { get; set; }

        [ForeignKey( "ChapterId" )]
        public virtual Chapter Chapter { get; set; }

        [MaxLength]
        public string Text { get; set; }

        public string AudioData { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}