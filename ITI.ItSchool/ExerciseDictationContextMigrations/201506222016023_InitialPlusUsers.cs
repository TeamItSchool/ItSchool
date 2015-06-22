namespace ITI.ItSchool.ExerciseDictationContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialPlusUsers : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "ExerciseDictation_ExerciseDictationId", c => c.Int());
            CreateIndex("dbo.Users", "ExerciseDictation_ExerciseDictationId");
            AddForeignKey("dbo.Users", "ExerciseDictation_ExerciseDictationId", "dbo.ExerciseDictations", "ExerciseDictationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ExerciseDictation_ExerciseDictationId", "dbo.ExerciseDictations");
            DropIndex("dbo.Users", new[] { "ExerciseDictation_ExerciseDictationId" });
            DropColumn("dbo.Users", "ExerciseDictation_ExerciseDictationId");
        }
    }
}
