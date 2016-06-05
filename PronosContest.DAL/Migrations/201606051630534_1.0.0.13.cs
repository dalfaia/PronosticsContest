namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10013 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "NumeroMatch", c => c.Int(nullable: false));
            AddColumn("dbo.Matches", "MatchVainqueurDomicileID", c => c.Int());
            AddColumn("dbo.Matches", "MatchVainqueurExterieurID", c => c.Int());
            AddColumn("dbo.Matches", "EquipePossibleDomicile", c => c.String());
            AddColumn("dbo.Matches", "EquipePossibleExterieur", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "EquipePossibleExterieur");
            DropColumn("dbo.Matches", "EquipePossibleDomicile");
            DropColumn("dbo.Matches", "MatchVainqueurExterieurID");
            DropColumn("dbo.Matches", "MatchVainqueurDomicileID");
            DropColumn("dbo.Matches", "NumeroMatch");
        }
    }
}
