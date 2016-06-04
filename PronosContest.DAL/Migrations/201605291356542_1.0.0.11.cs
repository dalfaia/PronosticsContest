namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10011 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "ButsEquipeDomicile", c => c.Int());
            AddColumn("dbo.Matches", "ButsEquipeExterieur", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "ButsEquipeExterieur");
            DropColumn("dbo.Matches", "ButsEquipeDomicile");
        }
    }
}
