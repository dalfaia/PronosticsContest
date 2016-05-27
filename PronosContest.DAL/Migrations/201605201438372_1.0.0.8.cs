namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1008 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concours", "CompteUtilisateurID", c => c.Int(nullable: false));
            CreateIndex("dbo.Concours", "CompteUtilisateurID");
            AddForeignKey("dbo.Concours", "CompteUtilisateurID", "dbo.CompteUtilisateurs", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Concours", "CompteUtilisateurID", "dbo.CompteUtilisateurs");
            DropIndex("dbo.Concours", new[] { "CompteUtilisateurID" });
            DropColumn("dbo.Concours", "CompteUtilisateurID");
        }
    }
}
