namespace WebApiOpendeurdag2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NullableCampusOpleiding : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Infomoments", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.Infomoments", "OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.Newsitems", "CampusId", "dbo.Campus");
            DropForeignKey("dbo.Newsitems", "OpleidingId", "dbo.Opleidings");
            DropIndex("dbo.Infomoments", new[] { "CampusId" });
            DropIndex("dbo.Infomoments", new[] { "OpleidingId" });
            DropIndex("dbo.Newsitems", new[] { "CampusId" });
            DropIndex("dbo.Newsitems", new[] { "OpleidingId" });
            RenameColumn(table: "dbo.Infomoments", name: "CampusId", newName: "Campus_CampusId");
            RenameColumn(table: "dbo.Infomoments", name: "OpleidingId", newName: "Opleiding_OpleidingId");
            RenameColumn(table: "dbo.Newsitems", name: "CampusId", newName: "Campus_CampusId");
            RenameColumn(table: "dbo.Newsitems", name: "OpleidingId", newName: "Opleiding_OpleidingId");
            AlterColumn("dbo.Infomoments", "Campus_CampusId", c => c.Int());
            AlterColumn("dbo.Infomoments", "Opleiding_OpleidingId", c => c.Int());
            AlterColumn("dbo.Newsitems", "Campus_CampusId", c => c.Int());
            AlterColumn("dbo.Newsitems", "Opleiding_OpleidingId", c => c.Int());
            CreateIndex("dbo.Infomoments", "Campus_CampusId");
            CreateIndex("dbo.Infomoments", "Opleiding_OpleidingId");
            CreateIndex("dbo.Newsitems", "Campus_CampusId");
            CreateIndex("dbo.Newsitems", "Opleiding_OpleidingId");
            AddForeignKey("dbo.Infomoments", "Campus_CampusId", "dbo.Campus", "CampusId");
            AddForeignKey("dbo.Infomoments", "Opleiding_OpleidingId", "dbo.Opleidings", "OpleidingId");
            AddForeignKey("dbo.Newsitems", "Campus_CampusId", "dbo.Campus", "CampusId");
            AddForeignKey("dbo.Newsitems", "Opleiding_OpleidingId", "dbo.Opleidings", "OpleidingId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Newsitems", "Opleiding_OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.Newsitems", "Campus_CampusId", "dbo.Campus");
            DropForeignKey("dbo.Infomoments", "Opleiding_OpleidingId", "dbo.Opleidings");
            DropForeignKey("dbo.Infomoments", "Campus_CampusId", "dbo.Campus");
            DropIndex("dbo.Newsitems", new[] { "Opleiding_OpleidingId" });
            DropIndex("dbo.Newsitems", new[] { "Campus_CampusId" });
            DropIndex("dbo.Infomoments", new[] { "Opleiding_OpleidingId" });
            DropIndex("dbo.Infomoments", new[] { "Campus_CampusId" });
            AlterColumn("dbo.Newsitems", "Opleiding_OpleidingId", c => c.Int(nullable: false));
            AlterColumn("dbo.Newsitems", "Campus_CampusId", c => c.Int(nullable: false));
            AlterColumn("dbo.Infomoments", "Opleiding_OpleidingId", c => c.Int(nullable: false));
            AlterColumn("dbo.Infomoments", "Campus_CampusId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Newsitems", name: "Opleiding_OpleidingId", newName: "OpleidingId");
            RenameColumn(table: "dbo.Newsitems", name: "Campus_CampusId", newName: "CampusId");
            RenameColumn(table: "dbo.Infomoments", name: "Opleiding_OpleidingId", newName: "OpleidingId");
            RenameColumn(table: "dbo.Infomoments", name: "Campus_CampusId", newName: "CampusId");
            CreateIndex("dbo.Newsitems", "OpleidingId");
            CreateIndex("dbo.Newsitems", "CampusId");
            CreateIndex("dbo.Infomoments", "OpleidingId");
            CreateIndex("dbo.Infomoments", "CampusId");
            AddForeignKey("dbo.Newsitems", "OpleidingId", "dbo.Opleidings", "OpleidingId", cascadeDelete: true);
            AddForeignKey("dbo.Newsitems", "CampusId", "dbo.Campus", "CampusId", cascadeDelete: true);
            AddForeignKey("dbo.Infomoments", "OpleidingId", "dbo.Opleidings", "OpleidingId", cascadeDelete: true);
            AddForeignKey("dbo.Infomoments", "CampusId", "dbo.Campus", "CampusId", cascadeDelete: true);
        }
    }
}
