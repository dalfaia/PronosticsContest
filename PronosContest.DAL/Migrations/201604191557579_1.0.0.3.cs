namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1003 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Matches", "Equipe_ID", "dbo.Equipes");
            DropForeignKey("dbo.Equipes", "PhaseFinale_ID", "dbo.PhaseFinales");
            DropIndex("dbo.Equipes", new[] { "PhaseFinale_ID" });
            DropIndex("dbo.Matches", new[] { "Equipe_ID" });
            RenameColumn(table: "dbo.Matches", name: "PhaseGroupe_ID", newName: "PhaseGroupeID");
            RenameColumn(table: "dbo.Matches", name: "PhaseFinale_ID", newName: "PhaseFinaleID");
            RenameIndex(table: "dbo.Matches", name: "IX_PhaseGroupe_ID", newName: "IX_PhaseGroupeID");
            RenameIndex(table: "dbo.Matches", name: "IX_PhaseFinale_ID", newName: "IX_PhaseFinaleID");
            DropColumn("dbo.Equipes", "PhaseFinale_ID");
            DropColumn("dbo.Matches", "Equipe_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "Equipe_ID", c => c.Int());
            AddColumn("dbo.Equipes", "PhaseFinale_ID", c => c.Int());
            RenameIndex(table: "dbo.Matches", name: "IX_PhaseFinaleID", newName: "IX_PhaseFinale_ID");
            RenameIndex(table: "dbo.Matches", name: "IX_PhaseGroupeID", newName: "IX_PhaseGroupe_ID");
            RenameColumn(table: "dbo.Matches", name: "PhaseFinaleID", newName: "PhaseFinale_ID");
            RenameColumn(table: "dbo.Matches", name: "PhaseGroupeID", newName: "PhaseGroupe_ID");
            CreateIndex("dbo.Matches", "Equipe_ID");
            CreateIndex("dbo.Equipes", "PhaseFinale_ID");
            AddForeignKey("dbo.Equipes", "PhaseFinale_ID", "dbo.PhaseFinales", "ID");
            AddForeignKey("dbo.Matches", "Equipe_ID", "dbo.Equipes", "ID");
        }
    }
}
