namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init_permohonan2 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Permohonans",
                c => new
                    {
                        Idapprove = c.String(nullable: false, maxLength: 128),
                        ktpimg = c.String(),
                        kkimg = c.String(),
                        skkerjaimg = c.String(),
                        tagihanlistrikimg = c.String(),
                        Isapproved = c.String(),
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
                .PrimaryKey(t => t.Idapprove)
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
            DropForeignKey("dbo.Permohonans", "UpdatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Permohonans", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Permohonans", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Permohonans", "ApprovedBy_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Permohonans", new[] { "UpdatedBy_Id" });
            DropIndex("dbo.Permohonans", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Permohonans", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Permohonans", new[] { "ApprovedBy_Id" });
            DropTable("dbo.Permohonans");
        }
    }
}
