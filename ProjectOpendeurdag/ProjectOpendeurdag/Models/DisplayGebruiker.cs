using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    class DisplayGebruiker
    {
        public int Id { get; set; }
        public String Email { get; set; }
        public String Wachtwoord { get; set; }
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Gemeente { get; set; }
        public String Telnr { get; set; }
        public String CampusVoorkeuren { get; set; }
        public String OpleidingVoorkeuren { get; set; }
    }
}
