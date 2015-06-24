namespace ITI.ItSchool.ExerciseContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Exercises",
            //    c => new
            //        {
            //            ExerciseId = c.Int(nullable: false, identity: true),
            //            ExerciseTypeId = c.Int(nullable: false),
            //            AffectedClass = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ExerciseId)
            //    .ForeignKey("dbo.Classes", t => t.AffectedClass, cascadeDelete: true)
            //    .ForeignKey("dbo.ExercisesTypes", t => t.ExerciseTypeId, cascadeDelete: true)
            //    .Index(t => t.ExerciseTypeId)
            //    .Index(t => t.AffectedClass);
            
            //CreateTable(
            //    "dbo.Classes",
            //    c => new
            //        {
            //            ClassId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Description = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.ClassId);
            
            //CreateTable(
            //    "dbo.Users",
            //    c => new
            //        {
            //            UserId = c.Int(nullable: false, identity: true),
            //            FirstName = c.String(nullable: false, maxLength: 45),
            //            LastName = c.String(nullable: false),
            //            Nickname = c.String(nullable: false),
            //            Mail = c.String(nullable: false),
            //            Password = c.String(nullable: false),
            //            ClassId = c.Int(nullable: false),
            //            AvatarId = c.Int(nullable: false),
            //            GroupId = c.Int(nullable: false),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.UserId)
            //    .ForeignKey("dbo.Classes", t => t.ClassId, cascadeDelete: true)
            //    .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
            //    .Index(t => t.ClassId)
            //    .Index(t => t.GroupId);
            
            //CreateTable(
            //    "dbo.Avatars",
            //    c => new
            //        {
            //            AvatarId = c.Int(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            UserId = c.Int(nullable: false),
            //            FootId = c.Int(nullable: false),
            //            LegsId = c.Int(nullable: false),
            //            BodyId = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.AvatarId)
            //    .ForeignKey("dbo.Bodies", t => t.BodyId, cascadeDelete: true)
            //    .ForeignKey("dbo.Feet", t => t.FootId, cascadeDelete: true)
            //    .ForeignKey("dbo.Legs", t => t.LegsId, cascadeDelete: true)
            //    .ForeignKey("dbo.Users", t => t.AvatarId)
            //    .Index(t => t.AvatarId)
            //    .Index(t => t.FootId)
            //    .Index(t => t.LegsId)
            //    .Index(t => t.BodyId);
            
            //CreateTable(
            //    "dbo.Bodies",
            //    c => new
            //        {
            //            BodyId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Link = c.String(maxLength: 512),
            //        })
            //    .PrimaryKey(t => t.BodyId);
            
            //CreateTable(
            //    "dbo.Feet",
            //    c => new
            //        {
            //            FootId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Link = c.String(maxLength: 512),
            //        })
            //    .PrimaryKey(t => t.FootId);
            
            //CreateTable(
            //    "dbo.Legs",
            //    c => new
            //        {
            //            LegsId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Link = c.String(maxLength: 512),
            //        })
            //    .PrimaryKey(t => t.LegsId);
            
            //CreateTable(
            //    "dbo.Groups",
            //    c => new
            //        {
            //            GroupId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.GroupId);
            
            //CreateTable(
            //    "dbo.ExercisesTypes",
            //    c => new
            //        {
            //            ExerciseTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Description = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.ExerciseTypeId);
            
            //CreateTable(
            //    "dbo.Levels",
            //    c => new
            //        {
            //            LevelId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.LevelId);
            
        }
        
        public override void Down()
        {
            //DropForeignKey("dbo.Exercises", "ExerciseTypeId", "dbo.ExercisesTypes");
            //DropForeignKey("dbo.Exercises", "AffectedClass", "dbo.Classes");
            //DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            //DropForeignKey("dbo.Users", "ClassId", "dbo.Classes");
            //DropForeignKey("dbo.Avatars", "AvatarId", "dbo.Users");
            //DropForeignKey("dbo.Avatars", "LegsId", "dbo.Legs");
            //DropForeignKey("dbo.Avatars", "FootId", "dbo.Feet");
            //DropForeignKey("dbo.Avatars", "BodyId", "dbo.Bodies");
            //DropIndex("dbo.Avatars", new[] { "BodyId" });
            //DropIndex("dbo.Avatars", new[] { "LegsId" });
            //DropIndex("dbo.Avatars", new[] { "FootId" });
            //DropIndex("dbo.Avatars", new[] { "AvatarId" });
            //DropIndex("dbo.Users", new[] { "GroupId" });
            //DropIndex("dbo.Users", new[] { "ClassId" });
            //DropIndex("dbo.Exercises", new[] { "AffectedClass" });
            //DropIndex("dbo.Exercises", new[] { "ExerciseTypeId" });
            //DropTable("dbo.Levels");
            //DropTable("dbo.ExercisesTypes");
            //DropTable("dbo.Groups");
            //DropTable("dbo.Legs");
            //DropTable("dbo.Feet");
            //DropTable("dbo.Bodies");
            //DropTable("dbo.Avatars");
            //DropTable("dbo.Users");
            //DropTable("dbo.Classes");
            //DropTable("dbo.Exercises");
        }
    }
}
