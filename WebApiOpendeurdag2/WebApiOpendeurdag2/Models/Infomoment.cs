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
        public virtual Campus Campus { get; set; }
        public virtual Opleiding Opleiding { get; set; }
    }
}