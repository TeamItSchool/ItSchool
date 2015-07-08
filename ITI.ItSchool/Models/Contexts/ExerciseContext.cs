using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITI.ItSchool.Models.ExercisesEntities;
using ITI.ItSchool.Models.Entities;
using ITI.ItSchool.Models.ExerciseEntities;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace ITI.ItSchool.Models.Contexts
{
    public class ExerciseContext : DbContext
    {
        public ExerciseContext() : base( "ItSchool" ) 
        {
            Database.SetInitializer( new DropCreateDatabaseIfModelChanges<ExerciseContext>() );
        }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        public DbSet<ExerciseAffectation> ExercisesAffectations { get; set; }

        public DbSet<ExercisesResults> ExercisesResults { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ExerciseAffectation>()
                .HasRequired(u => u.User)
                .WithMany()
                .HasForeignKey( u => u.UserId )
                .WillCascadeOnDelete(false);
        }
    }
}