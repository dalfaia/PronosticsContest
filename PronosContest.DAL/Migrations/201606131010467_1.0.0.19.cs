namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10019 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "InformationsMatchURL", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Matches", "InformationsMatchURL");
        }
    }
}
