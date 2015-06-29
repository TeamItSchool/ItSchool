namespace ITI.ItSchool.ExerciseBattleCardContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExerciseBattleCard",
                c => new
                    {
                        ExerciseBattleCardId = c.Int(nullable: false),
                        Name = c.String(maxLength: 50),
                        LevelId = c.Int(nullable: false),
                        ChapterId = c.Int(nullable: false),
                        Choice = c.String(),
                    })
                .PrimaryKey(t => t.ExerciseBattleCardId)
                .ForeignKey("dbo.Chapters", t => t.ChapterId, cascadeDelete: true)
                .ForeignKey("dbo.Exercises", t => t.ExerciseBattleCardId)
                .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
                .Index(t => t.ExerciseBattleCardId)
                .Index(t => t.LevelId)
                .Index(t => t.ChapterId);
            
            //CreateTable(
            //    "dbo.Chapters",
            //    c => new
            //        {
            //            ChapterId = c.Int(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            ThemeId = c.Int(nullable: false),
            //            ClassId = c.Int(nullable: false),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.ChapterId)
            //    .ForeignKey("dbo.Classes", t => t.ChapterId)
            //    .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
            //    .Index(t => t.ChapterId)
            //    .Index(t => t.ThemeId);
            
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
            //    "dbo.Exercises",
            //    c => new
            //        {
            //            ExerciseId = c.Int(nullable: false, identity: true),
            //            ExerciseTypeId = c.Int(nullable: false),
            //            Chapter_ChapterId = c.Int(),
            //        })
            //    .PrimaryKey(t => t.ExerciseId)
            //    .ForeignKey("dbo.ExercisesTypes", t => t.ExerciseTypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.Chapters", t => t.Chapter_ChapterId)
            //    .Index(t => t.ExerciseTypeId)
            //    .Index(t => t.Chapter_ChapterId);
            
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
            //    "dbo.Themes",
            //    c => new
            //        {
            //            ThemeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 200),
            //            MatterId = c.Int(nullable: false),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.ThemeId)
            //    .ForeignKey("dbo.Matters", t => t.MatterId, cascadeDelete: true)
            //    .Index(t => t.MatterId);
            
            //CreateTable(
            //    "dbo.Matters",
            //    c => new
            //        {
            //            MatterId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.MatterId);
            
            CreateTable(
                "dbo.Levels",
                c => new
                    {
                        LevelId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Remarks = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.LevelId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExerciseBattleCard", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.ExerciseBattleCard", "ExerciseBattleCardId", "dbo.Exercises");
            DropForeignKey("dbo.ExerciseBattleCard", "ChapterId", "dbo.Chapters");
            DropForeignKey("dbo.Chapters", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "MatterId", "dbo.Matters");
            DropForeignKey("dbo.Exercises", "Chapter_ChapterId", "dbo.Chapters");
            DropForeignKey("dbo.Exercises", "ExerciseTypeId", "dbo.ExercisesTypes");
            DropForeignKey("dbo.Chapters", "ChapterId", "dbo.Classes");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Users", "ClassId", "dbo.Classes");
            DropForeignKey("dbo.Avatars", "AvatarId", "dbo.Users");
            DropForeignKey("dbo.Avatars", "LegsId", "dbo.Legs");
            DropForeignKey("dbo.Avatars", "FootId", "dbo.Feet");
            DropForeignKey("dbo.Avatars", "BodyId", "dbo.Bodies");
            DropIndex("dbo.Themes", new[] { "MatterId" });
            DropIndex("dbo.Exercises", new[] { "Chapter_ChapterId" });
            DropIndex("dbo.Exercises", new[] { "ExerciseTypeId" });
            DropIndex("dbo.Avatars", new[] { "BodyId" });
            DropIndex("dbo.Avatars", new[] { "LegsId" });
            DropIndex("dbo.Avatars", new[] { "FootId" });
            DropIndex("dbo.Avatars", new[] { "AvatarId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.Users", new[] { "ClassId" });
            DropIndex("dbo.Chapters", new[] { "ThemeId" });
            DropIndex("dbo.Chapters", new[] { "ChapterId" });
            DropIndex("dbo.ExerciseBattleCard", new[] { "ChapterId" });
            DropIndex("dbo.ExerciseBattleCard", new[] { "LevelId" });
            DropIndex("dbo.ExerciseBattleCard", new[] { "ExerciseBattleCardId" });
            DropTable("dbo.Levels");
            DropTable("dbo.Matters");
            DropTable("dbo.Themes");
            DropTable("dbo.ExercisesTypes");
            DropTable("dbo.Exercises");
            DropTable("dbo.Groups");
            DropTable("dbo.Legs");
            DropTable("dbo.Feet");
            DropTable("dbo.Bodies");
            DropTable("dbo.Avatars");
            DropTable("dbo.Users");
            DropTable("dbo.Classes");
            DropTable("dbo.Chapters");
            DropTable("dbo.ExerciseBattleCard");
        }
    }
}
