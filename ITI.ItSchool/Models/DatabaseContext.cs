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

        public DbSet<Conversation> Conversations { get; set; }

        public DbSet<Message> Messages { get; set; }

        public DbSet<Friendship> Friendships { get; set; }

        public DbSet<Status> Statuses { get; set; }

        public DbSet<Exercise> Exercises { get; set; }

        public DbSet<Level> Levels { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        public DbSet<Grade> Grades { get; set; }

        public DbSet<Chapter> Chapters { get; set; }

        public DbSet<Theme> Themes { get; set; }

        public DbSet<Matter> Matters { get; set; }

        public DbSet<Hair> Hairs { get; set; }

        public DbSet<Mouth> Mouthes { get; set; }

        public DbSet<Nose> Noses { get; set; }

        public DbSet<Eye> Eyes { get; set; }

        public DbSet<Avatar> Avatars { get; set; }

        public DbSet Clothes { get; set; }
    }
}