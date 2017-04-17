namespace ATPTopTen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddIsFavorite : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Players", "IsFavorite", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Players", "IsFavorite");
        }
    }
}
