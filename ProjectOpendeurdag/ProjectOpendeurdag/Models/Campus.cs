using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    class Campus
    {
        public int CampusId { get; set; }
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Gemeente { get; set; }
    }
}
