namespace kredit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class delktpimg_kkimg_tagihanlistrikimg : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "ktpimg");
            DropColumn("dbo.AspNetUsers", "kkimg");
            DropColumn("dbo.AspNetUsers", "tagihanlistrikimg");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "tagihanlistrikimg", c => c.String());
            AddColumn("dbo.AspNetUsers", "kkimg", c => c.String());
            AddColumn("dbo.AspNetUsers", "ktpimg", c => c.String());
        }
    }
}
