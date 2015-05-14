using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ITI.ItSchool.Models
{
    public class SNContext : DbContext
    {
        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Status> Statuses { get; set; }
    }
}