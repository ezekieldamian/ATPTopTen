namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryToPlayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "CountryId", c => c.Int());
            CreateIndex("dbo.Players", "CountryId");
            AddForeignKey("dbo.Players", "CountryId", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Players", "CountryId", "dbo.Countries");
            DropIndex("dbo.Players", new[] { "CountryId" });
            DropColumn("dbo.Players", "CountryId");
        }
    }
}
