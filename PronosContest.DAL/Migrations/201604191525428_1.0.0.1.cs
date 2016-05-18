namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1001 : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.CompteUtilisateurs");
            CreateTable(
                "dbo.Competitions",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(nullable: false, maxLength: 256, unicode: false),
                        TypeSport = c.Int(nullable: false),
                        DateDebut = c.DateTime(nullable: false),
                        DateFin = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PhaseGroupes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Competition_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Competitions", t => t.Competition_ID)
                .Index(t => t.Competition_ID);
            
            CreateTable(
                "dbo.Equipes",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Libelle = c.String(),
                        Logo = c.String(),
                        PhaseGroupe_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.PhaseGroupes", t => t.PhaseGroupe_ID)
                .Index(t => t.PhaseGroupe_ID);
            
            CreateTable(
                "dbo.Matches",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EquipeAID = c.Int(nullable: false),
                        EquipeBID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Stade = c.String(),
                        Equipe_ID = c.Int(),
                        PhaseGroupe_ID = c.Int(),
                        PhaseFinale_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Equipes", t => t.EquipeAID)
                .ForeignKey("dbo.Equipes", t => t.EquipeBID)
                .ForeignKey("dbo.Equipes", t => t.Equipe_ID)
                .ForeignKey("dbo.PhaseGroupes", t => t.PhaseGroupe_ID)
                .ForeignKey("dbo.PhaseFinales", t => t.PhaseFinale_ID)
                .Index(t => t.EquipeAID)
                .Index(t => t.EquipeBID)
                .Index(t => t.Equipe_ID)
                .Index(t => t.PhaseGroupe_ID)
                .Index(t => t.PhaseFinale_ID);
            
            CreateTable(
                "dbo.PhaseFinales",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypePhaseFinale = c.Int(nullable: false),
                        Competition_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Competitions", t => t.Competition_ID)
                .Index(t => t.Competition_ID);

			DropColumn("dbo.CompteUtilisateurs", "UtilisateurID");
			DropColumn("dbo.CompteUtilisateurs", "IdentifiantConsole");
			AddColumn("dbo.CompteUtilisateurs", "ID", c => c.Int(nullable: false, identity: true));
            AddColumn("dbo.CompteUtilisateurs", "Role", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.CompteUtilisateurs", "ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompteUtilisateurs", "IdentifiantConsole", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AddColumn("dbo.CompteUtilisateurs", "UtilisateurID", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.PhaseFinales", "Competition_ID", "dbo.Competitions");
            DropForeignKey("dbo.Matches", "PhaseFinale_ID", "dbo.PhaseFinales");
            DropForeignKey("dbo.PhaseGroupes", "Competition_ID", "dbo.Competitions");
            DropForeignKey("dbo.Matches", "PhaseGroupe_ID", "dbo.PhaseGroupes");
            DropForeignKey("dbo.Equipes", "PhaseGroupe_ID", "dbo.PhaseGroupes");
            DropForeignKey("dbo.Matches", "Equipe_ID", "dbo.Equipes");
            DropForeignKey("dbo.Matches", "EquipeBID", "dbo.Equipes");
            DropForeignKey("dbo.Matches", "EquipeAID", "dbo.Equipes");
            DropIndex("dbo.PhaseFinales", new[] { "Competition_ID" });
            DropIndex("dbo.Matches", new[] { "PhaseFinale_ID" });
            DropIndex("dbo.Matches", new[] { "PhaseGroupe_ID" });
            DropIndex("dbo.Matches", new[] { "Equipe_ID" });
            DropIndex("dbo.Matches", new[] { "EquipeBID" });
            DropIndex("dbo.Matches", new[] { "EquipeAID" });
            DropIndex("dbo.Equipes", new[] { "PhaseGroupe_ID" });
            DropIndex("dbo.PhaseGroupes", new[] { "Competition_ID" });
            DropPrimaryKey("dbo.CompteUtilisateurs");
            DropColumn("dbo.CompteUtilisateurs", "Role");
            DropColumn("dbo.CompteUtilisateurs", "ID");
            DropTable("dbo.PhaseFinales");
            DropTable("dbo.Matches");
            DropTable("dbo.Equipes");
            DropTable("dbo.PhaseGroupes");
            DropTable("dbo.Competitions");
            AddPrimaryKey("dbo.CompteUtilisateurs", "UtilisateurID");
        }
    }
}
