namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_transaksi : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.KreditDetails",
                c => new
                    {
                        IdKreditDetail = c.Int(nullable: false, identity: true),
                        IdKredit = c.Int(nullable: false),
                        IdApprove = c.Int(nullable: false),
                        DP = c.Int(nullable: false),
                        JumlahPinjam = c.Int(nullable: false),
                        Angsuran = c.Int(nullable: false),
                        Periode = c.Int(nullable: false),
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
                .PrimaryKey(t => t.IdKreditDetail)
                .ForeignKey("dbo.AspNetUsers", t => t.ApprovedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.Permohonans", t => t.IdApprove, cascadeDelete: true)
                .ForeignKey("dbo.TransaksiKredits", t => t.IdKredit, cascadeDelete: true)
                .ForeignKey("dbo.Units", t => t.Unit_Idunit)
                .ForeignKey("dbo.AspNetUsers", t => t.UpdatedBy_Id)
                .Index(t => t.IdKredit)
                .Index(t => t.IdApprove)
                .Index(t => t.ApprovedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.Unit_Idunit)
                .Index(t => t.UpdatedBy_Id);
            
            CreateTable(
                "dbo.TransaksiKredits",
                c => new
                    {
                        IdKredit = c.Int(nullable: false, identity: true),
                        IdApprove = c.Int(nullable: false),
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
                .PrimaryKey(t => t.IdKredit)
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
            DropForeignKey("dbo.KreditDetails", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KreditDetails", "Unit_Idunit", "dbo.Units");
            DropForeignKey("dbo.KreditDetails", "IdKredit", "dbo.TransaksiKredits");
            DropForeignKey("dbo.TransaksiKredits", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransaksiKredits", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransaksiKredits", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TransaksiKredits", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KreditDetails", "IdApprove", "dbo.Permohonans");
            DropForeignKey("dbo.KreditDetails", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KreditDetails", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.KreditDetails", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TransaksiKredits", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.TransaksiKredits", new[] { "DeletedBy_Id" });
            DropIndex("dbo.TransaksiKredits", new[] { "CreatedBy_Id" });
            DropIndex("dbo.TransaksiKredits", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.KreditDetails", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.KreditDetails", new[] { "Unit_Idunit" });
            DropIndex("dbo.KreditDetails", new[] { "DeletedBy_Id" });
            DropIndex("dbo.KreditDetails", new[] { "CreatedBy_Id" });
            DropIndex("dbo.KreditDetails", new[] { "ApprovedBy_Id" });
            DropIndex("dbo.KreditDetails", new[] { "IdApprove" });
            DropIndex("dbo.KreditDetails", new[] { "IdKredit" });
            DropTable("dbo.TransaksiKredits");
            DropTable("dbo.KreditDetails");
        }
    }
}
