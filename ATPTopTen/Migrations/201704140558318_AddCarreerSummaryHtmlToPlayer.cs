namespace ATPTopTen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCarreerSummaryHtmlToPlayer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "CareerSummaryHtml", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "CareerSummaryHtml");
        }
    }
}
