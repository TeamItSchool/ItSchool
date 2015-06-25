namespace ITI.ItSchool.ExerciseDictationContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExerciseDictations", "ExerciseTypeId", "dbo.ExercisesTypes");
            DropIndex("dbo.ExerciseDictations", new[] { "ExerciseTypeId" });
            DropColumn("dbo.ExerciseDictations", "ExerciseTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExerciseDictations", "ExerciseTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExerciseDictations", "ExerciseTypeId");
            AddForeignKey("dbo.ExerciseDictations", "ExerciseTypeId", "dbo.ExercisesTypes", "ExerciseTypeId", cascadeDelete: true);
        }
    }
}
