namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddHeadToHead : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.HeadToHeads",
                c => new
                {
                    WinnerId = c.String(nullable: false, maxLength: 128),
                    OpponentId = c.String(nullable: false, maxLength: 128),
                    NumberOfWins = c.Int(nullable: true),
                })
                .PrimaryKey(t => new { t.WinnerId, t.OpponentId });
        }

        public override void Down()
        {
            DropTable("dbo.HeadToHeads");
        }
    }
}