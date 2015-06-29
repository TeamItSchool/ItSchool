using ITI.ItSchool.Models.ExerciseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.ClassExercicesPlug
{
    public class ExerciseBattleCardData
    {
        public Level Level { get; set; }

        public ICollection<int> UsersIds { get; set; }

        public string ChoiceData { get; set; }
    }
}