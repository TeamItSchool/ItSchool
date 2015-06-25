namespace ITI.ItSchool.ExerciseContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenamingCorrectlyAffectation : DbMigration
    {
        public override void Up()
        {
            //DropForeignKey("dbo.ExercisesAffections", "ExerciseId", "dbo.Exercises");
            //DropForeignKey("dbo.ExercisesAffections", "UserId", "dbo.Users");
            //DropIndex("dbo.ExercisesAffections", new[] { "UserId" });
            //DropIndex("dbo.ExercisesAffections", new[] { "ExerciseId" });
            //CreateTable(
            //    "dbo.ExercisesAffectations",
            //    c => new
            //        {
            //            ExerciseAffectationId = c.Int(nullable: false, identity: true),
            //            UserId = c.Int(nullable: false),
            //            ExerciseId = c.Int(nullable: false),
            //            CreationDate = c.DateTime(nullable: false),
            //            FirstViewDate = c.DateTime(nullable: false),
            //            EndDate = c.DateTime(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ExerciseAffectationId)
            //    .ForeignKey("dbo.Exercises", t => t.ExerciseId, cascadeDelete: true)
            //    .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.UserId)
            //    .Index(t => t.ExerciseId);
            
            //DropTable("dbo.ExercisesAffections");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ExercisesAffections",
                c => new
                    {
                        ExerciseAffectionId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        ExerciseId = c.Int(nullable: false),
                        CreationDate = c.DateTime(nullable: false),
                        FirstViewDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ExerciseAffectionId);
            
            DropForeignKey("dbo.ExercisesAffectations", "UserId", "dbo.Users");
            DropForeignKey("dbo.ExercisesAffectations", "ExerciseId", "dbo.Exercises");
            DropIndex("dbo.ExercisesAffectations", new[] { "ExerciseId" });
            DropIndex("dbo.ExercisesAffectations", new[] { "UserId" });
            DropTable("dbo.ExercisesAffectations");
            CreateIndex("dbo.ExercisesAffections", "ExerciseId");
            CreateIndex("dbo.ExercisesAffections", "UserId");
            AddForeignKey("dbo.ExercisesAffections", "UserId", "dbo.Users", "UserId", cascadeDelete: true);
            AddForeignKey("dbo.ExercisesAffections", "ExerciseId", "dbo.Exercises", "ExerciseId", cascadeDelete: true);
        }
    }
}
