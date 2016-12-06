using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class VoorkeurCampus
    {
        public int VoorkeurCampusId { get; set; }
        public int GebruikerId { get; set; }
        [ForeignKey("GebruikerId")]
        public Gebruiker Gebruiker { get; set; }
        public int CampusId { get; set; }
        [ForeignKey("CampusId")]
        public Campus Campus { get; set; }
    }
}