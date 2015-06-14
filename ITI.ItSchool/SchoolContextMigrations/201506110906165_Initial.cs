namespace ITI.ItSchool.SchoolContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Chapters",
            //    c => new
            //        {
            //            ChapterId = c.Int(nullable: false),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            ThemeId = c.Int(nullable: false),
            //            GradeId = c.Int(nullable: false),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.ChapterId)
            //    .ForeignKey("dbo.Grades", t => t.ChapterId)
            //    .ForeignKey("dbo.Themes", t => t.ThemeId, cascadeDelete: true)
            //    .Index(t => t.ChapterId)
            //    .Index(t => t.ThemeId);
            
            //CreateTable(
            //    "dbo.Games",
            //    c => new
            //        {
            //            GameId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 200),
            //            ChapterId = c.Int(nullable: false),
            //            LevelId = c.Int(nullable: false),
            //            ExerciseTypeId = c.Int(nullable: false),
            //            Data = c.String(maxLength: 100),
            //        })
            //    .PrimaryKey(t => t.GameId)
            //    .ForeignKey("dbo.Chapters", t => t.ChapterId, cascadeDelete: true)
            //    .ForeignKey("dbo.ExerciseTypes", t => t.ExerciseTypeId, cascadeDelete: true)
            //    .ForeignKey("dbo.Levels", t => t.LevelId, cascadeDelete: true)
            //    .Index(t => t.ChapterId)
            //    .Index(t => t.LevelId)
            //    .Index(t => t.ExerciseTypeId);
            
            //CreateTable(
            //    "dbo.ExerciseTypes",
            //    c => new
            //        {
            //            ExerciseTypeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
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
            
            //CreateTable(
            //    "dbo.Grades",
            //    c => new
            //        {
            //            GradeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.GradeId);
            
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
            //            GradeId = c.Int(nullable: false),
            //            AvatarId = c.Int(nullable: false),
            //            GroupId = c.Int(nullable: false),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.UserId)
            //    .ForeignKey("dbo.Grades", t => t.GradeId, cascadeDelete: true)
            //    .ForeignKey("dbo.Groups", t => t.GroupId, cascadeDelete: true)
            //    .Index(t => t.GradeId)
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
            
            CreateTable(
                "dbo.Matters",
                c => new
                    {
                        MatterId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        Remarks = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.MatterId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Chapters", "ThemeId", "dbo.Themes");
            DropForeignKey("dbo.Themes", "MatterId", "dbo.Matters");
            DropForeignKey("dbo.Chapters", "ChapterId", "dbo.Grades");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Users", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Avatars", "AvatarId", "dbo.Users");
            DropForeignKey("dbo.Avatars", "LegsId", "dbo.Legs");
            DropForeignKey("dbo.Avatars", "FootId", "dbo.Feet");
            DropForeignKey("dbo.Avatars", "BodyId", "dbo.Bodies");
            DropForeignKey("dbo.Games", "LevelId", "dbo.Levels");
            DropForeignKey("dbo.Games", "ExerciseTypeId", "dbo.ExerciseTypes");
            DropForeignKey("dbo.Games", "ChapterId", "dbo.Chapters");
            DropIndex("dbo.Themes", new[] { "MatterId" });
            DropIndex("dbo.Avatars", new[] { "BodyId" });
            DropIndex("dbo.Avatars", new[] { "LegsId" });
            DropIndex("dbo.Avatars", new[] { "FootId" });
            DropIndex("dbo.Avatars", new[] { "AvatarId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.Users", new[] { "GradeId" });
            DropIndex("dbo.Games", new[] { "ExerciseTypeId" });
            DropIndex("dbo.Games", new[] { "LevelId" });
            DropIndex("dbo.Games", new[] { "ChapterId" });
            DropIndex("dbo.Chapters", new[] { "ThemeId" });
            DropIndex("dbo.Chapters", new[] { "ChapterId" });
            DropTable("dbo.Matters");
            DropTable("dbo.Themes");
            DropTable("dbo.Groups");
            DropTable("dbo.Legs");
            DropTable("dbo.Feet");
            DropTable("dbo.Bodies");
            DropTable("dbo.Avatars");
            DropTable("dbo.Users");
            DropTable("dbo.Grades");
            DropTable("dbo.Levels");
            DropTable("dbo.ExerciseTypes");
            DropTable("dbo.Games");
            DropTable("dbo.Chapters");
        }
    }
}
