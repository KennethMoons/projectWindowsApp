using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    public class Opleiding
    {
        public int OpleidingId { get; set; }
        private String naam;
        private String type;
        private String imageLink;

        public string Naam
        {
            get
            {
                return naam;
            }

            set
            {
                naam = value;
            }
        }

        public string Type
        {
            get
            {
                return type;
            }

            set
            {
                type = value;
            }
        }

        public string ImageLink
        {
            get
            {
                return imageLink;
            }

            set
            {
                imageLink = value;
            }
        }

        public override bool Equals(object obj)
        {
            var item = obj as Opleiding;

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

    public class NullOpleiding : Opleiding
    {
        public NullOpleiding()
        {
            Naam = "Geen";
        }
    }
}
