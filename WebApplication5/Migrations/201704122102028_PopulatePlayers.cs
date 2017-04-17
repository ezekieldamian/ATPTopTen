using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebApplication5.Controllers.API;
using WebApplication5.Models;

namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

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
                sqlCommand += "'" + player.NatlCode + "', ";
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