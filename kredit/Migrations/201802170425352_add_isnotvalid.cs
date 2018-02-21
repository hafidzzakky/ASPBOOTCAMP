namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_isnotvalid : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permohonans", "IsnotValid", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permohonans", "IsnotValid");
        }
    }
}
