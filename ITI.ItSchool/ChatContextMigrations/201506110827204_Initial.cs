namespace ITI.ItSchool.ChatContextMigrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Conversations",
                c => new
                    {
                        ConversationId = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 45),
                        FirstUserId = c.Int(nullable: false),
                        SecondUserId = c.Int(nullable: false),
                        Remarks = c.String(maxLength: 200),
                    })
                .PrimaryKey(t => t.ConversationId)
                .ForeignKey("dbo.Users", t => t.FirstUserId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.SecondUserId, cascadeDelete: true)
                .Index(t => t.FirstUserId)
                .Index(t => t.SecondUserId);
            
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
            //    "dbo.Grades",
            //    c => new
            //        {
            //            GradeId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.GradeId);
            
            //CreateTable(
            //    "dbo.Groups",
            //    c => new
            //        {
            //            GroupId = c.Int(nullable: false, identity: true),
            //            Name = c.String(nullable: false, maxLength: 45),
            //            Remarks = c.String(maxLength: 200),
            //        })
            //    .PrimaryKey(t => t.GroupId);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        MessageId = c.Int(nullable: false, identity: true),
                        ConversationId = c.Int(nullable: false),
                        MsgDate = c.DateTime(nullable: false),
                        Content = c.String(nullable: false, maxLength: 255),
                    })
                .PrimaryKey(t => t.MessageId)
                .ForeignKey("dbo.Conversations", t => t.ConversationId, cascadeDelete: true)
                .Index(t => t.ConversationId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messages", "ConversationId", "dbo.Conversations");
            DropForeignKey("dbo.Conversations", "SecondUserId", "dbo.Users");
            DropForeignKey("dbo.Conversations", "FirstUserId", "dbo.Users");
            DropForeignKey("dbo.Users", "GroupId", "dbo.Groups");
            DropForeignKey("dbo.Users", "GradeId", "dbo.Grades");
            DropForeignKey("dbo.Avatars", "AvatarId", "dbo.Users");
            DropForeignKey("dbo.Avatars", "LegsId", "dbo.Legs");
            DropForeignKey("dbo.Avatars", "FootId", "dbo.Feet");
            DropForeignKey("dbo.Avatars", "BodyId", "dbo.Bodies");
            DropIndex("dbo.Messages", new[] { "ConversationId" });
            DropIndex("dbo.Avatars", new[] { "BodyId" });
            DropIndex("dbo.Avatars", new[] { "LegsId" });
            DropIndex("dbo.Avatars", new[] { "FootId" });
            DropIndex("dbo.Avatars", new[] { "AvatarId" });
            DropIndex("dbo.Users", new[] { "GroupId" });
            DropIndex("dbo.Users", new[] { "GradeId" });
            DropIndex("dbo.Conversations", new[] { "SecondUserId" });
            DropIndex("dbo.Conversations", new[] { "FirstUserId" });
            DropTable("dbo.Messages");
            DropTable("dbo.Groups");
            DropTable("dbo.Grades");
            DropTable("dbo.Legs");
            DropTable("dbo.Feet");
            DropTable("dbo.Bodies");
            DropTable("dbo.Avatars");
            DropTable("dbo.Users");
            DropTable("dbo.Conversations");
        }
    }
}
