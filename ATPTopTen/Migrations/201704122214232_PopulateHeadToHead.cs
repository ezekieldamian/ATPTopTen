namespace ATPTopTen.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class PopulateHeadToHead : DbMigration
    {
        public override void Up()
        {
            Sql("INSERT into HeadToHeads VALUES ('D643', 'MC10', 7)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'W367', 5)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'N409', 3)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'N552', 5)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'R975', 3)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'F324', 4)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'MC65', 2)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'BA47', 2)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'TB69', 0)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'D643', 7)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'W367', 4)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'N409', 0)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'N552', 3)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'R975', 0)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'F324', 6)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'MC65', 3)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'BA47', 2)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'TB69', 1)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'D643', 4)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'MC10', 4)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'N409', 8)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'N552', NULL)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'R975', 5)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'F324', 3)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'MC65', 4)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'BA47', 5)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'TB69', 2)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'D643', 3)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'MC10', 4)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'W367', 3)");
            Sql("INSERT into HeadToHeads VALUES ('D643', 'N552', 5)");

            Sql("INSERT into HeadToHeads VALUES ('MC10', 'R975', 0)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'F324', 3)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'MC65', 5)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'BA47', 6)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'TB69', 3)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'D643', 6)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'MC10', 4)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'W367', 3)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'N409', 2)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'R975', 6)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'F324', 7)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'MC65', 7)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'BA47', 2)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'TB69', 3)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'D643', 4)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'MC10', 3)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'W367', 3)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'N409', 2)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'N552', 3)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'F324', 4)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'MC65', 5)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'BA47', 5)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'TB69', 5)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'D643', 3)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'MC10', 5)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'W367', 5)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'N409', 4)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'N552', 3)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'R975', 2)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'MC65', 7)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'BA47', 5)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'TB69', 2)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'D643', 7)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'MC10', 5)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'W367', 3)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'N409', 6)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'N552', 3)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'R975', 2)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'F324', 1)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'BA47', 4)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'TB69', 7)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'D643', 6)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'MC10', 5)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'W367', 8)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'N409', 4)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'N552', 1)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'R975', 3)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'F324', 1)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'MC65', 4)");

            Sql("INSERT into HeadToHeads VALUES ('D643', 'TB69', 8)");
            Sql("INSERT into HeadToHeads VALUES ('MC10', 'D643', 5)");
            Sql("INSERT into HeadToHeads VALUES ('W367', 'MC10', 8)");
            Sql("INSERT into HeadToHeads VALUES ('N409', 'W367', 0)");
            Sql("INSERT into HeadToHeads VALUES ('N552', 'N409', 6)");
            Sql("INSERT into HeadToHeads VALUES ('R975', 'N552', 2)");
            Sql("INSERT into HeadToHeads VALUES ('F324', 'R975', 6)");
            Sql("INSERT into HeadToHeads VALUES ('MC65', 'F324', 3)");
            Sql("INSERT into HeadToHeads VALUES ('BA47', 'MC65', 3)");
            Sql("INSERT into HeadToHeads VALUES ('TB69', 'BA47', 0)");
        }

        public override void Down()
        {
            Sql("DELETE from HeadToHeads");
        }
    }
}