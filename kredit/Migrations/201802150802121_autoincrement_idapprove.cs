namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class autoincrement_idapprove : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Permohonans");
            AlterColumn("dbo.Permohonans", "IdApprove", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Permohonans", "IdApprove");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.Permohonans");
            AlterColumn("dbo.Permohonans", "IdApprove", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Permohonans", "Idapprove");
        }
    }
}
