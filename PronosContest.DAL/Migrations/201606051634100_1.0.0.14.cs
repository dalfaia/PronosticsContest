namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10014 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Matches", new[] { "EquipeAID" });
            DropIndex("dbo.Matches", new[] { "EquipeBID" });
            AlterColumn("dbo.Matches", "EquipeAID", c => c.Int());
            AlterColumn("dbo.Matches", "EquipeBID", c => c.Int());
            CreateIndex("dbo.Matches", "EquipeAID");
            CreateIndex("dbo.Matches", "EquipeBID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Matches", new[] { "EquipeBID" });
            DropIndex("dbo.Matches", new[] { "EquipeAID" });
            AlterColumn("dbo.Matches", "EquipeBID", c => c.Int(nullable: false));
            AlterColumn("dbo.Matches", "EquipeAID", c => c.Int(nullable: false));
            CreateIndex("dbo.Matches", "EquipeBID");
            CreateIndex("dbo.Matches", "EquipeAID");
        }
    }
}
