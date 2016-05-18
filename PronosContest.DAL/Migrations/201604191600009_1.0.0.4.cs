namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1004 : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Equipes", name: "PhaseGroupe_ID", newName: "PhaseGroupeID");
            RenameIndex(table: "dbo.Equipes", name: "IX_PhaseGroupe_ID", newName: "IX_PhaseGroupeID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Equipes", name: "IX_PhaseGroupeID", newName: "IX_PhaseGroupe_ID");
            RenameColumn(table: "dbo.Equipes", name: "PhaseGroupeID", newName: "PhaseGroupe_ID");
        }
    }
}
