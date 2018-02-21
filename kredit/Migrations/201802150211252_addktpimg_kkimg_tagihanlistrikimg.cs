namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addktpimg_kkimg_tagihanlistrikimg : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "ktpimg", c => c.String());
            AddColumn("dbo.AspNetUsers", "kkimg", c => c.String());
            AddColumn("dbo.AspNetUsers", "tagihanlistrikimg", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "tagihanlistrikimg");
            DropColumn("dbo.AspNetUsers", "kkimg");
            DropColumn("dbo.AspNetUsers", "ktpimg");
        }
    }
}
