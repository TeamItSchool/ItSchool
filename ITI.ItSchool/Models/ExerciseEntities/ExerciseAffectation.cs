using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models.ExerciseEntities
{
    [Table( "ExercisesAffectations" )]
    public class ExerciseAffectation
    {
        [Key]
        public int ExerciseAffectationId { get; set; }

        public int UserId { get; set; }

        public User User { get; set; }

        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime FirstViewDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
