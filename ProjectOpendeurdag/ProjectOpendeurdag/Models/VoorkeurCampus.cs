using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    class VoorkeurCampus
    {
        public int VoorkeurCampusId { get; set; }
        public int GebruikerId { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public int CampusId { get; set; }
        public Campus Campus { get; set; }
    }
}
