namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1006 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pronostics", "ButsEquipeDomicile", c => c.Int(nullable: false));
            AddColumn("dbo.Pronostics", "ButsEquipeExterieur", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pronostics", "ButsEquipeExterieur");
            DropColumn("dbo.Pronostics", "ButsEquipeDomicile");
        }
    }
}
