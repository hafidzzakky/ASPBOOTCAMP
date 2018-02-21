namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_Kategori_Unit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        Idkategori = c.String(nullable: false, maxLength: 128),
                        Nama_kategori = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Updated = c.DateTimeOffset(nullable: false, precision: 7),
                        Deleted = c.DateTimeOffset(nullable: false, precision: 7),
                        Approved = c.DateTimeOffset(nullable: false, precision: 7),
                        ApprovedBy_Id = c.String(maxLength: 128),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        Unit_Idunit = c.String(maxLength: 128),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Idkategori)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.Units", t => t.Unit_Idunit)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.Unit_Idunit)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.Units",
                c => new
                    {
                        Idunit = c.String(nullable: false, maxLength: 128),
                        Idkategori = c.String(),
                        Namabarang = c.String(),
                        Harga = c.Int(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        Created = c.DateTimeOffset(nullable: false, precision: 7),
                        Updated = c.DateTimeOffset(nullable: false, precision: 7),
                        Deleted = c.DateTimeOffset(nullable: false, precision: 7),
                        Approved = c.DateTimeOffset(nullable: false, precision: 7),
                        ApprovedBy_Id = c.String(maxLength: 128),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        UpdatedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Idunit)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.UpdatedBy_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Categories", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "Unit_Idunit", "dbo.Units");
            DropForeignKey("dbo.Units", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Units", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Units", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Units", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Categories", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Units", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Units", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Units", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Units", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.Categories", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Categories", new[] { "Unit_Idunit" });
            DropIndex("dbo.Categories", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Categories", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Categories", new[] { "ApprovedBy_Id" });
            DropTable("dbo.Units");
            DropTable("dbo.Categories");
        }
    }
}
