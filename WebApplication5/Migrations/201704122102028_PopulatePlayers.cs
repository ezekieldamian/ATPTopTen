using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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

            var players = GetData();

            foreach (var player in players.Result.ToList())
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

        private async Task<IEnumerable<Player>> GetData()
        {
            var apiBaseAddress = ConfigurationManager.AppSettings["ServiceUrl"];
            var apiKey = ConfigurationManager.AppSettings["Key"];

            var fullUri = apiBaseAddress + "Token/Rankings" + "?token=" + apiKey;

            var client = new HttpClient();

            var response = await client.GetAsync(fullUri);

            var jsonResult = response.Content.ReadAsStringAsync().Result;

            var resultObjects = JsonConvert.DeserializeObject<IEnumerable<Player>>(jsonResult);

            return resultObjects;
        }
    }
}