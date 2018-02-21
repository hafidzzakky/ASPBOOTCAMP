namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_statussurvey : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permohonans", "statussurvey", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permohonans", "statussurvey");
        }
    }
}
