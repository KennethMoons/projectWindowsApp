using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class Campus
    {
        public int CampusId { get; set; }
        [Required]
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Gemeente { get; set; }
    }
}