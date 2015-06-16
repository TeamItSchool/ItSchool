using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.Entities;

namespace ITI.ItSchool.Models.Contexts
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext() : base( "ItSchool" ) 
        {
            Database.SetInitializer( new DropCreateDatabaseIfModelChanges<ExerciseContext>() );
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Level> Levels { get; set; }
    }
}