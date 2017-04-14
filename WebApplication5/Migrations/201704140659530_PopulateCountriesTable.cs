namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PopulateCountriesTable : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT into Countries (Name, Code) VALUES ('Croatia', 'CRO')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Serbia', 'SRB')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Switzerland', 'SUI')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Great Britain', 'GBR')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Spain', 'ESP')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Japan', 'JPN')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Canada', 'CAN')");
            Sql("INSERT into Countries (Name, Code) VALUES ('France', 'FRA')");
            Sql("INSERT into Countries (Name, Code) VALUES ('Austria', 'AUT')");            
        }

        public override void Down()
        {
            Sql("DELETE from Countries");
        }
    }
}
