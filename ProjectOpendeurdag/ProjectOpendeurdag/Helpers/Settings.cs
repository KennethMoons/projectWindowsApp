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

            SyncVoorkeurCampussen();
            SyncVoorkeurOpleidingen();
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
            return JsonConvert.DeserializeObject<List<Campus>>(Container.Values[VoorkeurCampussen].ToString());
        }

        public static void SetVoorkeurCampussen(ICollection<Campus> campussen)
        {
            Container.Values[VoorkeurCampussen] = JsonConvert.SerializeObject(campussen);
            SyncVoorkeurCampussen();
        }

        private static async void SyncVoorkeurCampussen()
        {
            if (_CurrentGebruiker != null)
            {
                var voorkeurCampussen = GetVoorkeurCampussen();

                if (!_CurrentGebruiker.VoorkeurCampussen.SequenceEqual(voorkeurCampussen))
                {
                    _CurrentGebruiker.VoorkeurCampussen.Clear();
                    _CurrentGebruiker.VoorkeurCampussen.AddRange(voorkeurCampussen);

                    await Api.PutAsync<Gebruiker>(_CurrentGebruiker.GebruikerId, _CurrentGebruiker);
                }
            }
        }

        private static async void SyncVoorkeurOpleidingen()
        {
            if (_CurrentGebruiker != null)
            {
                var voorkeurOpleidingen = GetVoorkeurOpleidingen();

                if (!_CurrentGebruiker.VoorkeurOpleidingen.SequenceEqual(voorkeurOpleidingen))
                {
                    _CurrentGebruiker.VoorkeurOpleidingen.Clear();
                    _CurrentGebruiker.VoorkeurOpleidingen.AddRange(voorkeurOpleidingen);

                    await Api.PutAsync<Gebruiker>(_CurrentGebruiker.GebruikerId, _CurrentGebruiker);
                }
            }
        }

        public static List<Opleiding> GetVoorkeurOpleidingen()
        {
            return JsonConvert.DeserializeObject<List<Opleiding>>(Container.Values[VoorkeurOpleidingen].ToString());
        }

        public static void SetVoorkeurOpleidingen(ICollection<Opleiding> opleidingen)
        {
            Container.Values[VoorkeurOpleidingen] = JsonConvert.SerializeObject(opleidingen);
            SyncVoorkeurOpleidingen();
        }
    }
}
