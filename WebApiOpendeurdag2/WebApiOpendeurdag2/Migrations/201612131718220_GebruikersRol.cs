namespace WebApiOpendeurdag2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GebruikersRol : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Gebruikers", "Rol", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Gebruikers", "Rol");
        }
    }
}
