using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models.Contexts
{
    public class SocialNetworkContext : DbContext
    {
        public SocialNetworkContext() : base( "ItSchool" ) { }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Status> Statuses { get; set; }

        //protected override void OnModelCreating( DbModelBuilder modelBuilder )
        //{
        //    modelBuilder.HasDefaultSchema( "ItSchool" );
        //}
    }
}