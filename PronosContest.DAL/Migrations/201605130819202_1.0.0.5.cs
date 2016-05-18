namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1005 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Concours",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypePronostic = c.Int(nullable: false),
                        EtatPronostic = c.Int(nullable: false),
                        EtatConcours = c.Int(nullable: false),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                        CompetitionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Competitions", t => t.CompetitionID)
                .Index(t => t.CompetitionID);
            
            CreateTable(
                "dbo.Pronostics",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypePronostic = c.Int(nullable: false),
                        EtatPronostic = c.Int(nullable: false),
                        DateCreation = c.DateTime(nullable: false),
                        CompteUtilisateurID = c.Int(nullable: false),
                        MatchID = c.Int(nullable: false),
                        ConcoursID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.CompteUtilisateurs", t => t.CompteUtilisateurID)
                .ForeignKey("dbo.Concours", t => t.ConcoursID)
                .ForeignKey("dbo.Matches", t => t.MatchID)
                .Index(t => t.CompteUtilisateurID)
                .Index(t => t.MatchID)
                .Index(t => t.ConcoursID);
            
            AddColumn("dbo.CompteUtilisateurs", "Concours_ID", c => c.Int());
            CreateIndex("dbo.CompteUtilisateurs", "Concours_ID");
            AddForeignKey("dbo.CompteUtilisateurs", "Concours_ID", "dbo.Concours", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pronostics", "MatchID", "dbo.Matches");
            DropForeignKey("dbo.Pronostics", "ConcoursID", "dbo.Concours");
            DropForeignKey("dbo.Pronostics", "CompteUtilisateurID", "dbo.CompteUtilisateurs");
            DropForeignKey("dbo.CompteUtilisateurs", "Concours_ID", "dbo.Concours");
            DropForeignKey("dbo.Concours", "CompetitionID", "dbo.Competitions");
            DropIndex("dbo.Pronostics", new[] { "ConcoursID" });
            DropIndex("dbo.Pronostics", new[] { "MatchID" });
            DropIndex("dbo.Pronostics", new[] { "CompteUtilisateurID" });
            DropIndex("dbo.Concours", new[] { "CompetitionID" });
            DropIndex("dbo.CompteUtilisateurs", new[] { "Concours_ID" });
            DropColumn("dbo.CompteUtilisateurs", "Concours_ID");
            DropTable("dbo.Pronostics");
            DropTable("dbo.Concours");
        }
    }
}
