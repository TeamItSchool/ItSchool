﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace ITI.ItSchool.Models.Contexts
{
    public class GameContext : DbContext
    {
        public GameContext() : base( "ItSchool" ) 
        {
            Database.SetInitializer( new DropCreateDatabaseIfModelChanges<GameContext>() );
        }

        public DbSet<ExerciseType> ExerciseTypes { get; set; }

        public DbSet<Game> Games { get; set; }

        public DbSet<Level> Levels { get; set; }
    }
}