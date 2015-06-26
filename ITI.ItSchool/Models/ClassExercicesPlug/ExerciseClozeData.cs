using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.SchoolEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.ClassExercicesPlug
{
    public class ExerciseClozeData
    {
        public string Text { get; set; }

        public string HiddenWords { get; set; }

        public Level Level { get; set; }

        public Chapter Chapter { get; set; }

        public ICollection<int> UsersIds { get; set; }
    }
}