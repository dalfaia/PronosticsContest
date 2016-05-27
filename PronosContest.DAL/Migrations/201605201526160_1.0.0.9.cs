namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1009 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Concours", "DateFin", c => c.DateTime());
            DropColumn("dbo.Concours", "TypePronostic");
            DropColumn("dbo.Concours", "EtatPronostic");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Concours", "EtatPronostic", c => c.Int(nullable: false));
            AddColumn("dbo.Concours", "TypePronostic", c => c.Int(nullable: false));
            AlterColumn("dbo.Concours", "DateFin", c => c.DateTime(nullable: false));
        }
    }
}
