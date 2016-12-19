using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectOpendeurdag.Helpers
{
    static class PageTitles
    {
        private static Dictionary<Type, string> Titles = new Dictionary<Type, string>()
        {
            { typeof(Opties), "Instellingen" },
            { typeof(NewsitemBeheerMain), "Nieuwsbeheer" },
            { typeof(NewsitemBeheerAddOne), "Toevoegen artikel" },
            { typeof(NewsitemBeheerUpdate), "Bewerken artikel" },
            { typeof(InfomomentBeheerMain), "Infomomentbeheer" },
            { typeof(InfomomentBeheerAddOne), "Toevoegen infomoment" },
            { typeof(InfomomentBeheerUpdate), "Bewerken infomoment" },
            { typeof(GebruikersBeheerMain), "Gebruikersbeheer" },
            { typeof(GebruikersBeheerDetail), "Gebruiker" },
            { typeof(Newsfeed), "Nieuws" },
            { typeof(NewsfeedDetail), "Artikel" },
            { typeof(Campussen), "Campussen" },
            { typeof(Opleidingen), "Opleidingen" },
            { typeof(InfomomentFeed), "Infomomenten" },
            { typeof(InfomomentfeedDetail), "Infomoment" },
        };

        public static string Get(Type pageType)
        {
            string title = null;
            return Titles.TryGetValue(pageType, out title) ? title : "";
        }
    }
}
