namespace ITI.ItSchool.ExerciseContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddingExercisesResults : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExercisesResults",
                c => new
                    {
                        ExerciseResultsId = c.Int(nullable: false),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseResultsId)
                .ForeignKey("dbo.ExercisesAffectations", t => t.ExerciseResultsId)
                .Index(t => t.ExerciseResultsId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExercisesResults", "ExerciseResultsId", "dbo.ExercisesAffectations");
            DropIndex("dbo.ExercisesResults", new[] { "ExerciseResultsId" });
            DropTable("dbo.ExercisesResults");
        }
    }
}
