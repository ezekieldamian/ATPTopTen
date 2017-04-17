namespace ATPTopTen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DeleteFlagCssFromCountry : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Countries", "FlagCSS");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Countries", "FlagCSS", c => c.String());
        }
    }
}
