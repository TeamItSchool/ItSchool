using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ITI.ItSchool.Models
{
    public class DatabaseContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Right> Rights { get; set; }

        public DbSet<Control> Controls { get; set; }

        public DbSet<Belonging> Belongings { get; set; }
    }
}