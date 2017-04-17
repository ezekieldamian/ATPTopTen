using System.Data.Entity.Migrations;
using System.Linq;
using ATPTopTen.Controllers.API;

namespace ATPTopTen.Migrations
{
    public partial class PopulatePlayers : DbMigration
    {
        public override void Up()
        {
            Sql("Delete from players");

            var api = new PlayersController();

            //Debugger.Launch();

            var players = api.GetInitialPlayersData().Result;

            foreach (var player in players.ToList())
            {
                var sqlCommand = "INSERT into Players VALUES (";

                sqlCommand += "'" + player.PlayerId + "', ";
                sqlCommand += player.Rank + ", ";
                sqlCommand += "'" + player.FirstName + "', ";
                sqlCommand += "'" + player.LastName + "', ";
                sqlCommand += "'" + player.NatlCode + "', "; //need it there for old migrations
                sqlCommand += player.Points + ", ";
                sqlCommand += "'" + player.IsTie + "') ";

                Sql(sqlCommand);
            }
        }

        public override void Down()
        {
            Sql("Delete from players");
        }
    }
}