using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    class Newsitem
    {
        public int NewsitemId { get; set; }
        public String Titel { get; set; }
        public String Inhoud { get; set; }
        public String Datum { get; set; }
        public String Uur { get; set; }
        public Campus Campus { get; set; }
        public Opleiding Opleiding { get; set; }
    }
}
