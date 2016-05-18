namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1007 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.CompteUtilisateurs", "Adresse_Ligne1", c => c.String(maxLength: 50, unicode: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.CompteUtilisateurs", "Adresse_Ligne1", c => c.String(nullable: false, maxLength: 50, unicode: false));
        }
    }
}
