namespace WebApplication5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MakeNumberOfWinsNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.HeadToHeads", "NumberOfWins", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.HeadToHeads", "NumberOfWins", c => c.Int(nullable: false));
        }
    }
}
