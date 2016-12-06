namespace WebApiOpendeurdag2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Campus",
                c => new
                    {
                        CampusId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Adres = c.String(),
                        Postcode = c.String(),
                        Gemeente = c.String(),
                    })
                .PrimaryKey(t => t.CampusId);
            
            CreateTable(
                "dbo.Gebruikers",
                c => new
                    {
                        GebruikerId = c.Int(nullable: false, identity: true),
                        Email = c.String(nullable: false),
                        Wachtwoord = c.String(nullable: false),
                        Naam = c.String(),
                        Adres = c.String(),
                        Postcode = c.String(),
                        Gemeente = c.String(),
                        Telnr = c.String(),
                    })
                .PrimaryKey(t => t.GebruikerId);
            
            CreateTable(
                "dbo.Infomoments",
                c => new
                    {
                        InfomomentId = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false),
                        Beschrijving = c.String(),
                        Datum = c.String(),
                        Uur = c.String(),
                        CampusId = c.Int(nullable: false),
                        OpleidingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.InfomomentId)
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true)
                .ForeignKey("dbo.Opleidings", t => t.OpleidingId, cascadeDelete: true)
                .Index(t => t.CampusId)
                .Index(t => t.OpleidingId);
            
            CreateTable(
                "dbo.Opleidings",
                c => new
                    {
                        OpleidingId = c.Int(nullable: false, identity: true),
                        Naam = c.String(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.OpleidingId);
            
            CreateTable(
                "dbo.Newsitems",
                c => new
                    {
                        NewsitemId = c.Int(nullable: false, identity: true),
                        Titel = c.String(nullable: false),
                        Inhoud = c.String(),
                        Datum = c.String(),
                        Uur = c.String(),
                        CampusId = c.Int(nullable: false),
                        OpleidingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NewsitemId)
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true)
                .ForeignKey("dbo.Opleidings", t => t.OpleidingId, cascadeDelete: true)
                .Index(t => t.CampusId)
                .Index(t => t.OpleidingId);
            
            CreateTable(
                "dbo.VoorkeurCampus",
                c => new
                    {
                        VoorkeurCampusId = c.Int(nullable: false, identity: true),
                        GebruikerId = c.Int(nullable: false),
                        CampusId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoorkeurCampusId)
                .ForeignKey("dbo.Campus", t => t.CampusId, cascadeDelete: true)
                .ForeignKey("dbo.Gebruikers", t => t.GebruikerId, cascadeDelete: true)
                .Index(t => t.GebruikerId)
                .Index(t => t.CampusId);
            
            CreateTable(
                "dbo.VoorkeurOpleidings",
                c => new
                    {
                        VoorkeurOpleidingId = c.Int(nullable: false, identity: true),
                        GebruikerId = c.Int(nullable: false),
                        OpleidingId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.VoorkeurOpleidingId)
                .ForeignKey("dbo.Gebruikers", t => t.GebruikerId, cascadeDelete: true)
                .ForeignKey("dbo.Opleidings", t => t.OpleidingId, cascadeDelete: true)
                .Index(t => t.GebruikerId)
                .Index(t => t.OpleidingId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.VoorkeurOpleidings", "OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.VoorkeurOpleidings", "GebruikerId", "dbo.Gebruikers");
            DropForeignKey("dbo.VoorkeurCampus", "GebruikerId", "dbo.Gebruikers");
            DropForeignKey("dbo.VoorkeurCampus", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.Newsitems", "OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.Newsitems", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.Infomoments", "OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.Infomoments", "CampusId", "dbo.Campus");
            DropIndex("dbo.VoorkeurOpleidings", new[] { "OpleidingId" });
            DropIndex("dbo.VoorkeurOpleidings", new[] { "GebruikerId" });
            DropIndex("dbo.VoorkeurCampus", new[] { "CampusId" });
            DropIndex("dbo.VoorkeurCampus", new[] { "GebruikerId" });
            DropIndex("dbo.Newsitems", new[] { "OpleidingId" });
            DropIndex("dbo.Newsitems", new[] { "CampusId" });
            DropIndex("dbo.Infomoments", new[] { "OpleidingId" });
            DropIndex("dbo.Infomoments", new[] { "CampusId" });
            DropTable("dbo.VoorkeurOpleidings");
            DropTable("dbo.VoorkeurCampus");
            DropTable("dbo.Newsitems");
            DropTable("dbo.Opleidings");
            DropTable("dbo.Infomoments");
            DropTable("dbo.Gebruikers");
            DropTable("dbo.Campus");
        }
    }
}
