using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using ITI.ItSchool.Models.UserEntity;


namespace ITI.ItSchool.Models
{
    public class UserContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Control> Controls { get; set; }

        public DbSet<Group> Groups { get; set; }

        public DbSet<Right> Rights { get; set; }
    }
}