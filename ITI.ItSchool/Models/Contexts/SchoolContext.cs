﻿using System.Data.Entity;

namespace ITI.ItSchool.Models.Contexts
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base( "ItSchool" ) { }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Matter> Matters { get; set; }

        public DbSet<Theme> Themes { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            modelBuilder.HasDefaultSchema( "ItSchool" );
        }
    }
}