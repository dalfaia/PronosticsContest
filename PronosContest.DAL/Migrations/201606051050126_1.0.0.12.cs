namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10012 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Equipes", "ShortName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Equipes", "ShortName");
        }
    }
}
