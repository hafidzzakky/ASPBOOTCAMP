namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_transaksi2 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.KreditDetails", name: "Unit_Idunit", newName: "Idunit");
            RenameIndex(table: "dbo.KreditDetails", name: "IX_Unit_Idunit", newName: "IX_Idunit");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.KreditDetails", name: "IX_Idunit", newName: "IX_Unit_Idunit");
            RenameColumn(table: "dbo.KreditDetails", name: "Idunit", newName: "Unit_Idunit");
        }
    }
}
