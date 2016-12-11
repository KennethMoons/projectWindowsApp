using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Models
{
    class Opleiding
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

        public Opleiding(string naam, string type, string imageLink)
        {
            this.Naam = naam;
            this.Type = type;
            this.ImageLink = imageLink;
        }
    }

    class OpleidingenLijst
    {
        public static List<Opleiding> getOpleidingen()
        {
            List<Opleiding> opleidingen = new List<Opleiding>();
            Opleiding o1 = new Opleiding("bedrijfsmanagement","professionele bachelor","Assets/opleiding1.PNG");
            Opleiding o2 = new Opleiding("Office management", "professionele bachelor", "Assets/opleiding2.PNG");
            Opleiding o3 = new Opleiding("Retailmanagement", "professionele bachelor", "Assets/opleiding3.PNG");
            Opleiding o4 = new Opleiding("Toegepaste informatica", "professionele bachelor", "Assets/opleiding4.PNG");

            opleidingen.Add(o1);
            opleidingen.Add(o2);
            opleidingen.Add(o3);
            opleidingen.Add(o4);

            return opleidingen;
        }
    }
}
