namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class isReadAdded : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsRead", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "Status");
            DropColumn("dbo.Messages", "IsRead");
            DropColumn("dbo.Contacts", "IsRead");
        }
    }
}
