namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeCountryCodePrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Countries");
            AlterColumn("dbo.Countries", "Code", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.Countries", "Code");
            DropColumn("dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "Id", c => c.Int(nullable: false, identity: true));
            DropPrimaryKey("dbo.Countries");
            AlterColumn("dbo.Countries", "Code", c => c.String());
            AddPrimaryKey("dbo.Countries", "Id");
        }
    }
}
