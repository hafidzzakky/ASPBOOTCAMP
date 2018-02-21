namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permohonans", "Kode", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permohonans", "Kode");
        }
    }
}
