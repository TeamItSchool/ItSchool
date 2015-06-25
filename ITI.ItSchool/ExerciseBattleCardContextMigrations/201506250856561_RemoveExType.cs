namespace ITI.ItSchool.ExerciseBattleCardContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemoveExType : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExerciseBattleCard", "ExerciseTypeId", "dbo.ExercisesTypes");
            DropIndex("dbo.ExerciseBattleCard", new[] { "ExerciseTypeId" });
            DropColumn("dbo.ExerciseBattleCard", "ExerciseTypeId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExerciseBattleCard", "ExerciseTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.ExerciseBattleCard", "ExerciseTypeId");
            AddForeignKey("dbo.ExerciseBattleCard", "ExerciseTypeId", "dbo.ExercisesTypes", "ExerciseTypeId", cascadeDelete: true);
        }
    }
}
