using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ITI.ItSchool.Models
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base( "ItSchool" ) { }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Message> Messages { get; set; }

        protected override void OnModelCreating( DbModelBuilder modelBuilder )
        {
            modelBuilder.HasDefaultSchema( "ItSchool" );
        }
    }
}