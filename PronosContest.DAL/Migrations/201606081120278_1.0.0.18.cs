namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10018 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "ButsPenaltiesEquipeDomicile", c => c.Int());
            AddColumn("dbo.Matches", "ButsPenaltiesEquipeExterieur", c => c.Int());
            AddColumn("dbo.Pronostics", "ButsPenaltiesEquipeDomicile", c => c.Int(nullable: false));
            AddColumn("dbo.Pronostics", "ButsPenaltiesEquipeExterieur", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pronostics", "ButsPenaltiesEquipeExterieur");
            DropColumn("dbo.Pronostics", "ButsPenaltiesEquipeDomicile");
            DropColumn("dbo.Matches", "ButsPenaltiesEquipeExterieur");
            DropColumn("dbo.Matches", "ButsPenaltiesEquipeDomicile");
        }
    }
}
