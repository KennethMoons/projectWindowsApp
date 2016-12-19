using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class Gebruiker
    {
        public int GebruikerId { get; set; }
        [Required]
        public String Email { get; set; }
        [Required]
        public String Wachtwoord { get; set; }
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Gemeente { get; set; }
        public String Telnr { get; set; }
        public String Rol { get; set; }
        public virtual ICollection<Campus> VoorkeurCampussen { get; set; }
        public virtual ICollection<Opleiding> VoorkeurOpleidingen { get; set; }
    }

    public class GebruikersRollen
    {
        public const string Admin = "admin";
    }
}