using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.UserEntities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ITI.ItSchool.Models.ExerciseEntities
{
    [Table( "ExercisesAffections" )]
    public class ExerciseAffection
    {
        [Key]
        public int ExerciseAffectionId { get; set; }

        public int UserId { get; set; }

        [ForeignKey( "UserId" )]
        public virtual User User { get; set; }

        public int ExerciseId { get; set; }

        [ForeignKey( "ExerciseId" )]
        public virtual Exercise Exercise { get; set; }

        public DateTime CreationDate { get; set; }

        public DateTime FirstViewDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}
