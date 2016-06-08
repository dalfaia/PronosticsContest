namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10016 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pronostics", "EquipeAID", c => c.Int());
            AddColumn("dbo.Pronostics", "EquipeBID", c => c.Int());
            CreateIndex("dbo.Pronostics", "EquipeAID");
            CreateIndex("dbo.Pronostics", "EquipeBID");
            AddForeignKey("dbo.Pronostics", "EquipeAID", "dbo.Equipes", "ID");
            AddForeignKey("dbo.Pronostics", "EquipeBID", "dbo.Equipes", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pronostics", "EquipeBID", "dbo.Equipes");
            DropForeignKey("dbo.Pronostics", "EquipeAID", "dbo.Equipes");
            DropIndex("dbo.Pronostics", new[] { "EquipeBID" });
            DropIndex("dbo.Pronostics", new[] { "EquipeAID" });
            DropColumn("dbo.Pronostics", "EquipeBID");
            DropColumn("dbo.Pronostics", "EquipeAID");
        }
    }
}
