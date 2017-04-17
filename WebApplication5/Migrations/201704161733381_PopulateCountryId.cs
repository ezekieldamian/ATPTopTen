namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCountryId : DbMigration
    {
        public override void Up()
        {
            Sql("update Players set CountryId = (select Id from Countries where Code = Players.NatlCode)");
        }
        
        public override void Down()
        {
            Sql("delete from players.countryId");
        }
    }
}
