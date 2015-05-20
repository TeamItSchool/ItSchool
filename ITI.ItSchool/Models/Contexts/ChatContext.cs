using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITI.ItSchool.Models.ChatEntities;

namespace ITI.ItSchool.Models.Contexts
{
    public class ChatContext : DbContext
    {
        public ChatContext() : base( "ItSchool" ) { }

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Message> Messages { get; set; }
    }
}