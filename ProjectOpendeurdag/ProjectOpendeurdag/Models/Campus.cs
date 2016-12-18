using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    public class Campus
    {
        public int CampusId { get; set; }
        public String Naam { get; set; }
        public String Adres { get; set; }
        public String Postcode { get; set; }
        public String Gemeente { get; set; }
        public String ImageLink { get; set; }

        public override bool Equals(object obj)
        {
            var item = obj as Campus;

            if (item == null)
            {
                return false;
            }

            return this.CampusId.Equals(item.CampusId);
        }

        public override int GetHashCode()
        {
            return this.CampusId.GetHashCode();
        }
    }
}
