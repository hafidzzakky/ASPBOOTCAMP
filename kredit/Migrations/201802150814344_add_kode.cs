namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_kode : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Permohonans", "Kode", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Permohonans", "Kode");
        }
    }
}
