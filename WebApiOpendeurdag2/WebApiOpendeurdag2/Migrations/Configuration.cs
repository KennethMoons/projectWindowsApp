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
            context.Gebruikers.AddOrUpdate(new Gebruiker[] {
                new Gebruiker() { GebruikerId = 1, Email = "admin", Wachtwoord = "admin", Rol = GebruikersRollen.Admin },
                new Gebruiker() { GebruikerId = 2, Email = "user", Wachtwoord = "user" }
            });

            context.Campus.AddOrUpdate(new Campus[] {
                new Campus() { CampusId = 1, Naam = "Schoonmeersen", Adres = "Valentin Vaerwyckweg 1", Gemeente = "Gent", Postcode = "9000" },
                new Campus() { CampusId = 2, Naam = "Mercator", Adres = "Henleykaai 84", Gemeente = "Gent", Postcode = "9000" },
                new Campus() { CampusId = 3, Naam = "Aalst", Adres = "Arbeidstraat 14", Gemeente = "Aalst", Postcode = "9300" }
            });

            context.Opleidings.AddOrUpdate(new Opleiding[] {
                new Opleiding() {OpleidingId = 1, Naam = "Bedrijfsmanagment", Type = "professionele bachelor" },
                new Opleiding() {OpleidingId = 2, Naam = "Office management", Type = "professionele bachelor" },
                new Opleiding() {OpleidingId = 3, Naam = "Retailmanagement", Type = "professionele bachelor" },
                new Opleiding() {OpleidingId = 4, Naam = "Toegepaste informatica", Type = "professionele bachelor" }
            });
        }
    }
}
