namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1002 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.PhaseGroupes", new[] { "Competition_ID" });
            DropIndex("dbo.PhaseFinales", new[] { "Competition_ID" });
            RenameColumn(table: "dbo.PhaseGroupes", name: "Competition_ID", newName: "CompetitionID");
            RenameColumn(table: "dbo.PhaseFinales", name: "Competition_ID", newName: "CompetitionID");
            AddColumn("dbo.PhaseGroupes", "Lettre", c => c.String());
            AddColumn("dbo.Equipes", "PhaseFinale_ID", c => c.Int());
            AlterColumn("dbo.PhaseGroupes", "CompetitionID", c => c.Int(nullable: false));
            AlterColumn("dbo.PhaseFinales", "CompetitionID", c => c.Int(nullable: false));
            CreateIndex("dbo.PhaseGroupes", "CompetitionID");
            CreateIndex("dbo.Equipes", "PhaseFinale_ID");
            CreateIndex("dbo.PhaseFinales", "CompetitionID");
            AddForeignKey("dbo.Equipes", "PhaseFinale_ID", "dbo.PhaseFinales", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Equipes", "PhaseFinale_ID", "dbo.PhaseFinales");
            DropIndex("dbo.PhaseFinales", new[] { "CompetitionID" });
            DropIndex("dbo.Equipes", new[] { "PhaseFinale_ID" });
            DropIndex("dbo.PhaseGroupes", new[] { "CompetitionID" });
            AlterColumn("dbo.PhaseFinales", "CompetitionID", c => c.Int());
            AlterColumn("dbo.PhaseGroupes", "CompetitionID", c => c.Int());
            DropColumn("dbo.Equipes", "PhaseFinale_ID");
            DropColumn("dbo.PhaseGroupes", "Lettre");
            RenameColumn(table: "dbo.PhaseFinales", name: "CompetitionID", newName: "Competition_ID");
            RenameColumn(table: "dbo.PhaseGroupes", name: "CompetitionID", newName: "Competition_ID");
            CreateIndex("dbo.PhaseFinales", "Competition_ID");
            CreateIndex("dbo.PhaseGroupes", "Competition_ID");
        }
    }
}
