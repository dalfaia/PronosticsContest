namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CompteUtilisateurs",
                c => new
                    {
                        UtilisateurID = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false, maxLength: 256, unicode: false),
                        Prenom = c.String(maxLength: 256, unicode: false),
                        Nom = c.String(),
                        IdentifiantConsole = c.String(nullable: false, maxLength: 8000, unicode: false),
                        Password = c.Binary(nullable: false),
                        Adresse_Ligne1 = c.String(nullable: false, maxLength: 50, unicode: false),
                        Adresse_Ligne2 = c.String(maxLength: 50, unicode: false),
                        Adresse_Ligne3 = c.String(maxLength: 50, unicode: false),
                        Adresse_CodePostal = c.String(maxLength: 50, unicode: false),
                        Adresse_Ville = c.String(maxLength: 50, unicode: false),
                        Adresse_Pays = c.String(maxLength: 50, unicode: false),
                    })
                .PrimaryKey(t => t.UtilisateurID);
            
            CreateTable(
                "dbo.Tests",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256, unicode: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Tests");
            DropTable("dbo.CompteUtilisateurs");
        }
    }
}
