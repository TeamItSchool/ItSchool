using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITI.ItSchool.Models.AvatarEntities;

namespace ITI.ItSchool.Models.Contexts
{
    public class AvatarContext : DbContext
    {
        public AvatarContext() : base( "ItSchool" ) { }

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet<Body> Bodies { get; set; }

        public DbSet<Foot> Feet { get; set; }

        public DbSet<Legs> Legs { get; set; }

    }
}