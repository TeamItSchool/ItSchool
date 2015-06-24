using ITI.ItSchool.Models.Entities;
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
    [Table("ExerciseBattleCard")]
    public class ExerciseBattleCard
    {
        public int ExerciseBattleCardId { get; set; }

        [ForeignKey("ExerciseBattleCardId")]
        public virtual Exercise Exercise { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public int ExerciseTypeId { get; set; }

        [ForeignKey("ExerciseTypeId")]
        public virtual ExerciseType ExerciseType { get; set; }

        public int LevelId { get; set; }

        [ForeignKey("LevelId")]
        public virtual Level Level { get; set; }

        public int ChapterId { get; set; }

        [ForeignKey("ChapterId")]
        public virtual Chapter Chapter { get; set; }

        [MaxLength]
        public string Choice { get; set; }

    }
}