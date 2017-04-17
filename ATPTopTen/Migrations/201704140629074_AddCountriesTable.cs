namespace ATPTopTen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountriesTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                    "dbo.Countries",
                    c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.String(),
                        Name = c.String(),
                        FlagCSS = c.String(),
                    })
                .PrimaryKey(t => t.Id);

        }

        public override void Down()
        {
            DropTable("dbo.Countries");
        }
    }
}
