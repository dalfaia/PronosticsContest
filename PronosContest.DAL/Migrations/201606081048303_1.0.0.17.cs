namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10017 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Concours", "DateLimiteSaisie", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Concours", "DateLimiteSaisie");
        }
    }
}
