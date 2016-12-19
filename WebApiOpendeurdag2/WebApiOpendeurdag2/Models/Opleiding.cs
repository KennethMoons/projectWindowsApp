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
        public override bool Equals(object obj)
        {
            Opleiding item = obj as Opleiding;

            if (item == null)
            {
                return false;
            }

            return this.OpleidingId.Equals(item.OpleidingId);
        }

        public override int GetHashCode()
        {
            return this.OpleidingId.GetHashCode();
        }
    }
}