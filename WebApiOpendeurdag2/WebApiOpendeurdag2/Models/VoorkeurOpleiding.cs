using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class VoorkeurOpleiding
    {
        public int VoorkeurOpleidingId { get; set; }
        public int GebruikerId { get; set; }
        [ForeignKey("GebruikerId")]
        public Gebruiker Gebruiker { get; set; }
        public int OpleidingId { get; set; }
        [ForeignKey("OpleidingId")]
        public Opleiding Opleiding { get; set; }
    }
}