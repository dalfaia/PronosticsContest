namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10010 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CompteUtilisateurs", "Concours_ID", "dbo.Concours");
            DropIndex("dbo.CompteUtilisateurs", new[] { "Concours_ID" });
            CreateTable(
                "dbo.ConcoursCompteUtilisateurs",
                c => new
                    {
                        CompteUtilisateurID = c.Int(nullable: false),
                        ConcoursID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => new { t.CompteUtilisateurID, t.ConcoursID })
                .ForeignKey("dbo.CompteUtilisateurs", t => t.CompteUtilisateurID)
                .ForeignKey("dbo.Concours", t => t.ConcoursID)
                .Index(t => t.CompteUtilisateurID)
                .Index(t => t.ConcoursID);
            
            DropColumn("dbo.CompteUtilisateurs", "Concours_ID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CompteUtilisateurs", "Concours_ID", c => c.Int());
            DropForeignKey("dbo.ConcoursCompteUtilisateurs", "ConcoursID", "dbo.Concours");
            DropForeignKey("dbo.ConcoursCompteUtilisateurs", "CompteUtilisateurID", "dbo.CompteUtilisateurs");
            DropIndex("dbo.ConcoursCompteUtilisateurs", new[] { "ConcoursID" });
            DropIndex("dbo.ConcoursCompteUtilisateurs", new[] { "CompteUtilisateurID" });
            DropTable("dbo.ConcoursCompteUtilisateurs");
            CreateIndex("dbo.CompteUtilisateurs", "Concours_ID");
            AddForeignKey("dbo.CompteUtilisateurs", "Concours_ID", "dbo.Concours", "ID");
        }
    }
}
