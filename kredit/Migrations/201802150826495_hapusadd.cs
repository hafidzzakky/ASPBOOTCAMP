namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hapusadd : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Permohonans", "Kode");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Permohonans", "Kode", c => c.Int(nullable: false));
        }
    }
}
