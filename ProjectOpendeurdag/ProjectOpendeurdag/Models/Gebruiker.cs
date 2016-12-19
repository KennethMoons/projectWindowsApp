using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }
        public String Email { get; set; }
        public String Wachtwoord { get; set; }
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Gemeente { get; set; }
        public String Telnr { get; set; }
        public String Rol { get; set; }
        public List<Campus> VoorkeurCampussen { get; set; }
        public List<Opleiding> VoorkeurOpleidingen { get; set; }

    }
    class GebruikersRollen
    {
        public const String Admin = "admin";
    }
}
