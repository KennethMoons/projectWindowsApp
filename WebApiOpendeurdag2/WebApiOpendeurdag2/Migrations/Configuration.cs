namespace WebApiOpendeurdag2.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<WebApiOpendeurdag2.Models.WebApiOpendeurdag2Context>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(WebApiOpendeurdag2.Models.WebApiOpendeurdag2Context context)
        {
            context.Gebruikers.AddOrUpdate(new Gebruiker() { GebruikerId = 0, Email = "admin", Wachtwoord = "admin" });
            context.Campus.AddOrUpdate(new Campus[] {
                new Campus() { CampusId = 0, Naam = "Schoonmeersen", Adres = "Valentin Vaerwyckweg 1", Gemeente = "Gent", Postcode = "9000" },
                new Campus() { CampusId = 1, Naam = "Mercator", Adres = "Henleykaai 84", Gemeente = "Gent", Postcode = "9000" },
                new Campus() { CampusId = 2, Naam = "Aalst", Adres = "Arbeidstraat 14", Gemeente = "Aalst", Postcode = "9300" }
            });
            context.Opleidings.AddOrUpdate(new Opleiding[] {
                new Opleiding() {OpleidingId = 0, Naam = "Bedrijfsmanagment", Type = "professionele bachelor" },
                new Opleiding() {OpleidingId = 1, Naam = "Office management", Type = "professionele bachelor" },
                new Opleiding() {OpleidingId = 2, Naam = "Retailmanagement", Type = "professionele bachelor" },
                new Opleiding() {OpleidingId = 3, Naam = "Toegepaste informatica", Type = "professionele bachelor" }
            });
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
