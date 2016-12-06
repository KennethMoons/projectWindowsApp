using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class Infomoment
    {
        public int InfomomentId { get; set; }
        [Required]
        public String Titel { get; set; }
        public String Beschrijving { get; set; }
        public String Datum { get; set; }
        public String Uur { get; set; }
        public int CampusId { get; set; }
        [ForeignKey("CampusId")]
        public Campus Campus { get; set; }
        public int OpleidingId { get; set; }
        [ForeignKey("OpleidingId")]
        public Opleiding Opleiding { get; set; }
    }
}