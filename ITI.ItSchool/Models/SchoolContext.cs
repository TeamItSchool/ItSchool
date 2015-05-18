using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class SchoolContext : DbContext
    {
        public SchoolContext() : base( "ItSchool" ) { }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Matter> Matters { get; set; }

        public DbSet<Theme> Themes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema( "ItSchool" );
        }
    }
}