using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using ITI.ItSchool.Models.UserEntities;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace ITI.ItSchool.Models.Contexts
{
    public class UserContext : DbContext
    {
        public UserContext() : base("ItSchool") { }

        public DbSet<User> Users { get; set; }

        public DbSet<Control> Controls { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Right> Rights { get; set; }
    }
}