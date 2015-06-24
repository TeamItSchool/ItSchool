namespace ITI.ItSchool.ExerciseClozeContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedId : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Exercises", new[] { "AffectedClass" });
            DropPrimaryKey("dbo.ExerciseCloze");
            AlterColumn("dbo.ExerciseCloze", "ExerciseClozeId", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.ExerciseCloze", "ExerciseClozeId");
            CreateIndex("dbo.ExerciseCloze", "ExerciseClozeId");
            AddForeignKey("dbo.ExerciseCloze", "ExerciseClozeId", "dbo.Exercises", "ExerciseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Exercises", "AffectedClass", c => c.Int(nullable: false));
            DropForeignKey("dbo.ExerciseCloze", "ExerciseClozeId", "dbo.Exercises");
            DropIndex("dbo.ExerciseCloze", new[] { "ExerciseClozeId" });
            DropPrimaryKey("dbo.ExerciseCloze");
            AlterColumn("dbo.ExerciseCloze", "ExerciseClozeId", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ExerciseCloze", "ExerciseClozeId");
            CreateIndex("dbo.Exercises", "AffectedClass");
            AddForeignKey("dbo.Exercises", "AffectedClass", "dbo.Classes", "ClassId", cascadeDelete: true);
        }
    }
}
