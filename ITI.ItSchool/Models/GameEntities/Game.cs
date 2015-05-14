using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Game
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Chapter { get; set; }

        public int Level { get; set; }

        public int Exercise { get; set; }

        public int ExerciseType { get; set; }

        public string Data { get; set; }

        public string Remarks { get; set; }
    }
}