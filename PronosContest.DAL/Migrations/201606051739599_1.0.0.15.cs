namespace PronosContest.DAL.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _10015 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Matches", "EquipePossibleDomicile_Libelle", c => c.String());
            AddColumn("dbo.Matches", "EquipePossibleDomicile_Place", c => c.Int());
            AddColumn("dbo.Matches", "EquipePossibleDomicile_Groupes", c => c.String());
            AddColumn("dbo.Matches", "EquipePossibleExterieur_Libelle", c => c.String());
            AddColumn("dbo.Matches", "EquipePossibleExterieur_Place", c => c.Int());
            AddColumn("dbo.Matches", "EquipePossibleExterieur_Groupes", c => c.String());
            DropColumn("dbo.Matches", "EquipePossibleDomicile");
            DropColumn("dbo.Matches", "EquipePossibleExterieur");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Matches", "EquipePossibleExterieur", c => c.String());
            AddColumn("dbo.Matches", "EquipePossibleDomicile", c => c.String());
            DropColumn("dbo.Matches", "EquipePossibleExterieur_Groupes");
            DropColumn("dbo.Matches", "EquipePossibleExterieur_Place");
            DropColumn("dbo.Matches", "EquipePossibleExterieur_Libelle");
            DropColumn("dbo.Matches", "EquipePossibleDomicile_Groupes");
            DropColumn("dbo.Matches", "EquipePossibleDomicile_Place");
            DropColumn("dbo.Matches", "EquipePossibleDomicile_Libelle");
        }
    }
}
