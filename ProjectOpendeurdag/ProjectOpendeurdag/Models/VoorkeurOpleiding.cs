using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    class VoorkeurOpleiding
    {
        public int VoorkeurOpleidingId { get; set; }
        public int GebruikerId { get; set; }
        public Gebruiker Gebruiker { get; set; }
        public int OpleidingId { get; set; }
        public Opleiding Opleiding { get; set; }
    }
}
