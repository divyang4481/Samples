namespace CodeFirstSample.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAttributes : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Blogs", "Name", c => c.String(nullable: false, maxLength: 300));
            AlterColumn("dbo.Users", "DisplayName", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DisplayName", c => c.String());
            AlterColumn("dbo.Blogs", "Name", c => c.String());
        }
    }
}
