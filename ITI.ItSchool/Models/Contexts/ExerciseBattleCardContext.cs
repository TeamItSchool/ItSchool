using ITI.ItSchool.Models.ExerciseEntities;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.PlugExercises;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.Contexts
{
    public class ExerciseBattleCardContext : DbContext
    {
        public ExerciseBattleCardContext()
            : base("ItSchool") 
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<ExerciseBattleCardContext>());
        }

        public DbSet<ExerciseBattleCard> ExerciseBattleCard { get; set; }

        public DbSet<ExerciseType> ExerciseType { get; set; }

        public DbSet<Level> Level { get; set; }
    }
}