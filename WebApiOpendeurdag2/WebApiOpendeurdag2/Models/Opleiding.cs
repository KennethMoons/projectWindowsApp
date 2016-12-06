using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApiOpendeurdag2.Models
{
    public class Opleiding
    {
        public int OpleidingId { get; set; }
        [Required]
        public String Naam { get; set; }
        public String Type { get; set; }
    }
}