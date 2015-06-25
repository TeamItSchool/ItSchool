namespace ITI.ItSchool.ExerciseClozeContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExerciseCloze", "ExerciseTypeId", "dbo.ExercisesTypes");
            DropIndex("dbo.ExerciseCloze", new[] { "ExerciseTypeId" });
            DropColumn("dbo.ExerciseCloze", "ExerciseTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExerciseCloze", "ExerciseTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExerciseCloze", "ExerciseTypeId");
            AddForeignKey("dbo.ExerciseCloze", "ExerciseTypeId", "dbo.ExercisesTypes", "ExerciseTypeId", cascadeDelete: true);
        }
    }
}
