using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ITI.ItSchool.Models.Contexts
{
    public class AvatarContext : DbContext
    {
        public AvatarContext() : base( "ItSchool" ) { }

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet<Clothe> Clothes { get; set; }

        public DbSet<Eye> Eyes { get; set; }

        public DbSet<Hair> Hairs { get; set; }

        public DbSet<Mouth> Mouths { get; set; }

        public DbSet<Nose> Noses { get; set; }

        //protected override void OnModelCreating( DbModelBuilder modelBuilder )
        //{
        //    modelBuilder.HasDefaultSchema( "ItSchool" );
        //}
    }
}