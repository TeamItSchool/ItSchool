using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class Game
    {
        [Key]
        public int GameId { get; set; }

        [Required]
        [MinLength( 3 )]
        [MaxLength( 200 )]
        public string Name { get; set; }

        [ForeignKey( "ChapterId" )]
        public int ChapterId { get; set; }

        public Chapter Chapter { get; set; }

        [ForeignKey( "LevelId" )]
        public int LevelId { get; set; }

        public Level Level { get; set; }

        [ForeignKey( "ExerciseId" )]
        public int ExerciseId { get; set; }

        public Exercise Exercise { get; set; }

        [ForeignKey( "ExerciseType" )]
        public int ExerciseTypeId { get; set; }

        public ExerciseType ExerciseType { get; set; }

        [MaxLength( 100 )]
        public string Data { get; set; }

        [MaxLength( 200 )]
        public string Remarks { get; set; }
    }
}