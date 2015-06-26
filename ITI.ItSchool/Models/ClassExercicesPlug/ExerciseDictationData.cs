using ITI.ItSchool.Models.ExerciseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models.ClassExercicesPlug
{
    public class ExerciseDictationData
    {
        public Level Level { get; set; }

        public ICollection<int> UsersIds { get; set; }

        public string Text { get; set; }

        public string AudioData { get; set; }
    }
}
