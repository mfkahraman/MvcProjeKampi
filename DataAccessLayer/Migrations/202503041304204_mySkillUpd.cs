namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mySkillUpd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.MySkills", "FullName");
            DropColumn("dbo.MySkills", "Description");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MySkills", "Description", c => c.String());
            AddColumn("dbo.MySkills", "FullName", c => c.String());
        }
    }
}
