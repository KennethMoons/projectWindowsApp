namespace WebApiOpendeurdag2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GebruikersVoorkeuren : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.VoorkeurCampus", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.VoorkeurCampus", "GebruikerId", "dbo.Gebruikers");
            DropForeignKey("dbo.VoorkeurOpleidings", "GebruikerId", "dbo.Gebruikers");
            DropForeignKey("dbo.VoorkeurOpleidings", "OpleidingId", "dbo.Opleidings");
            DropIndex("dbo.VoorkeurCampus", new[] { "GebruikerId" });
            DropIndex("dbo.VoorkeurCampus", new[] { "CampusId" });
            DropIndex("dbo.VoorkeurOpleidings", new[] { "GebruikerId" });
            DropIndex("dbo.VoorkeurOpleidings", new[] { "OpleidingId" });
            CreateTable(
                "dbo.VoorkeurCampussen",
                c => new
                    {
                        GebruikerId = c.Int(nullable: false),
                        CampusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GebruikerId, t.CampusId })
                .ForeignKey("dbo.Gebruikers", t => t.GebruikerId, cascadeDelete: true)
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true)
                .Index(t => t.GebruikerId)
                .Index(t => t.CampusId);
            
            CreateTable(
                "dbo.VoorkeurOpleidingen",
                c => new
                    {
                        GebruikerId = c.Int(nullable: false),
                        OpleidingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.GebruikerId, t.OpleidingId })
                .ForeignKey("dbo.Gebruikers", t => t.GebruikerId, cascadeDelete: true)
                .ForeignKey("dbo.Opleidings", t => t.OpleidingId, cascadeDelete: true)
                .Index(t => t.GebruikerId)
                .Index(t => t.OpleidingId);
            
            DropTable("dbo.VoorkeurCampus");
            DropTable("dbo.VoorkeurOpleidings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.VoorkeurOpleidings",
                c => new
                    {
                        VoorkeurOpleidingId = c.Int(nullable: false, identity: true),
                        GebruikerId = c.Int(nullable: false),
                        OpleidingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoorkeurOpleidingId);
            
            CreateTable(
                "dbo.VoorkeurCampus",
                c => new
                    {
                        VoorkeurCampusId = c.Int(nullable: false, identity: true),
                        GebruikerId = c.Int(nullable: false),
                        CampusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoorkeurCampusId);
            
            DropForeignKey("dbo.VoorkeurOpleidingen", "OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.VoorkeurOpleidingen", "GebruikerId", "dbo.Gebruikers");
            DropForeignKey("dbo.VoorkeurCampussen", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.VoorkeurCampussen", "GebruikerId", "dbo.Gebruikers");
            DropIndex("dbo.VoorkeurOpleidingen", new[] { "OpleidingId" });
            DropIndex("dbo.VoorkeurOpleidingen", new[] { "GebruikerId" });
            DropIndex("dbo.VoorkeurCampussen", new[] { "CampusId" });
            DropIndex("dbo.VoorkeurCampussen", new[] { "GebruikerId" });
            DropTable("dbo.VoorkeurOpleidingen");
            DropTable("dbo.VoorkeurCampussen");
            CreateIndex("dbo.VoorkeurOpleidings", "OpleidingId");
            CreateIndex("dbo.VoorkeurOpleidings", "GebruikerId");
            CreateIndex("dbo.VoorkeurCampus", "CampusId");
            CreateIndex("dbo.VoorkeurCampus", "GebruikerId");
            AddForeignKey("dbo.VoorkeurOpleidings", "OpleidingId", "dbo.Opleidings", "OpleidingId", cascadeDelete: true);
            AddForeignKey("dbo.VoorkeurOpleidings", "GebruikerId", "dbo.Gebruikers", "GebruikerId", cascadeDelete: true);
            AddForeignKey("dbo.VoorkeurCampus", "GebruikerId", "dbo.Gebruikers", "GebruikerId", cascadeDelete: true);
            AddForeignKey("dbo.VoorkeurCampus", "CampusId", "dbo.Campus", "CampusId", cascadeDelete: true);
        }
    }
}
