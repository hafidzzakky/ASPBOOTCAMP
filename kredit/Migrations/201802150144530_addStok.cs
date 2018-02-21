namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addStok : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Units", "Stok", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Units", "Stok");
        }
    }
}
