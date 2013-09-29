namespace Conference.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Sessions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Title = c.String(nullable: false),
                        Abstract = c.String(nullable: false),
                        SpeakerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Speakers", t => t.SpeakerId, cascadeDelete: true)
                .Index(t => t.SpeakerId);
            
            CreateTable(
                "dbo.Speakers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        SessionId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sessions", t => t.SessionId, cascadeDelete: true)
                .Index(t => t.SessionId);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Comments", new[] { "SessionId" });
            DropIndex("dbo.Sessions", new[] { "SpeakerId" });
            DropForeignKey("dbo.Comments", "SessionId", "dbo.Sessions");
            DropForeignKey("dbo.Sessions", "SpeakerId", "dbo.Speakers");
            DropTable("dbo.Comments");
            DropTable("dbo.Speakers");
            DropTable("dbo.Sessions");
        }
    }
}
