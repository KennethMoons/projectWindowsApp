using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using ProjectOpendeurdag.Models;
using Windows.Storage;

namespace ProjectOpendeurdag.Helpers
{
    class Settings
    {
        private const string VoorkeurCampussen = "VoorkeurCampussen";
        private const string VoorkeurOpleidingen = "VoorkeurOpleidingen";

        private static Gebruiker _CurrentGebruiker = null;

        public static readonly ApplicationDataContainer Container = ApplicationData.Current.RoamingSettings;

        static Settings()
        {
            // Add all campussen & opleidingen to voorkeuren
            if (!Container.Values.ContainsKey(VoorkeurCampussen))
            {
                SetVoorkeurCampussen(Api.GetAsync<List<Campus>>().Result);
            }

            if (!Container.Values.ContainsKey(VoorkeurOpleidingen))
            {
                SetVoorkeurOpleidingen(Api.GetAsync<List<Opleiding>>().Result);
            }
        }

        public static void SetCurrentGebruiker(Gebruiker gebruiker)
        {
            _CurrentGebruiker = gebruiker;
        }

        public static Gebruiker GetCurrentGebruiker()
        {
            return _CurrentGebruiker;
        }

        public static bool IsGebruikerLoggedIn()
        {
            return _CurrentGebruiker != null;
        }

        public static bool IsGebruikerAdmin()
        {
            return _CurrentGebruiker != null && GebruikersRollen.Admin.Equals(_CurrentGebruiker.Rol);
        }

        public static List<Campus> GetVoorkeurCampussen()
        {
            if (_CurrentGebruiker != null)
            {
                Container.Values[VoorkeurCampussen] = JsonConvert.SerializeObject(_CurrentGebruiker.VoorkeurCampussen);
            }

            return JsonConvert.DeserializeObject<List<Campus>>(Container.Values[VoorkeurCampussen].ToString());
        }

        public static async void SetVoorkeurCampussen(ICollection<Campus> campussen)
        {
            if (_CurrentGebruiker != null)
            {
                _CurrentGebruiker.VoorkeurCampussen.Clear();
                _CurrentGebruiker.VoorkeurCampussen.AddRange(campussen);

                await Api.PutAsync<Gebruiker>(_CurrentGebruiker.GebruikerId, _CurrentGebruiker);
            }

            Container.Values[VoorkeurCampussen] = JsonConvert.SerializeObject(campussen);
        }

        public static List<Opleiding> GetVoorkeurOpleidingen()
        {
            if (_CurrentGebruiker != null)
            {
                Container.Values[VoorkeurOpleidingen] = JsonConvert.SerializeObject(_CurrentGebruiker.VoorkeurOpleidingen);
            }

            return JsonConvert.DeserializeObject<List<Opleiding>>(Container.Values[VoorkeurOpleidingen].ToString());
        }

        public static async void SetVoorkeurOpleidingen(ICollection<Opleiding> opleidingen)
        {
            if (_CurrentGebruiker != null)
            {
                _CurrentGebruiker.VoorkeurOpleidingen.Clear();
                _CurrentGebruiker.VoorkeurOpleidingen.AddRange(opleidingen);

                await Api.PutAsync<Gebruiker>(_CurrentGebruiker.GebruikerId, _CurrentGebruiker);
            }

            Container.Values[VoorkeurOpleidingen] = JsonConvert.SerializeObject(opleidingen);
        }
    }
}
