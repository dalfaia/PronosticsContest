namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10020 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pronostics", "IsNouveauProno", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pronostics", "IsNouveauProno");
        }
    }
}
