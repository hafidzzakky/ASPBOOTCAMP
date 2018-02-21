namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_transaksi3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.KreditDetails", "Jumlah", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.KreditDetails", "Jumlah");
        }
    }
}
