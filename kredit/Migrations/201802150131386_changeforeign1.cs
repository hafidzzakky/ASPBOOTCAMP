namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeforeign1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Categories", "Unit_Idunit", "dbo.Units");
            DropIndex("dbo.Categories", new[] { "Unit_Idunit" });
            AlterColumn("dbo.Units", "Idkategori", c => c.String(maxLength: 128));
            CreateIndex("dbo.Units", "Idkategori");
            AddForeignKey("dbo.Units", "Idkategori", "dbo.Categories", "Idkategori");
            DropColumn("dbo.Categories", "Unit_Idunit");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "Unit_Idunit", c => c.String(maxLength: 128));
            DropForeignKey("dbo.Units", "Idkategori", "dbo.Categories");
            DropIndex("dbo.Units", new[] { "Idkategori" });
            AlterColumn("dbo.Units", "Idkategori", c => c.String());
            CreateIndex("dbo.Categories", "Unit_Idunit");
            AddForeignKey("dbo.Categories", "Unit_Idunit", "dbo.Units", "Idunit");
        }
    }
}
