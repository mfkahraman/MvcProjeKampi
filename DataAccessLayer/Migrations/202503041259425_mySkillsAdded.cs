namespace DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class mySkillsAdded : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MySkills",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FullName = c.String(),
                        Description = c.String(),
                        SkillName = c.String(),
                        Percentage = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MySkills");
        }
    }
}
