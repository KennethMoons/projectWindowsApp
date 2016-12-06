using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProjectOpendeurdag.Models;
using System.Net.Http;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GebruikersBeheerDetail : Page
    {
        List<DisplayGebruiker> displayGebruikers;
        public GebruikersBeheerDetail()
        {
            this.InitializeComponent();
            displayGebruikers = new List<DisplayGebruiker>();
            VulGebruikers();
            
        }

        public async void VulGebruikers()
        {
            HttpClient client = new HttpClient();
            var JsonResponse = await client.GetStringAsync("http://localhost:51399/api/Gebruikers");
            List<Gebruiker> gebruikerResult = JsonConvert.DeserializeObject<List<Gebruiker>>(JsonResponse);
            gebruikerResult.RemoveAt(0);
            HttpClient client1 = new HttpClient();
            var JsonResponse1 = await client1.GetStringAsync("http://localhost:51399/api/VoorkeurOpleidings");
            List<VoorkeurOpleiding> voorkeurOpleidingen = JsonConvert.DeserializeObject<List<VoorkeurOpleiding>>(JsonResponse1);
            HttpClient client2 = new HttpClient();
            var JsonResponse2 = await client2.GetStringAsync("http://localhost:51399/api/VoorkeurCampus");
            List<VoorkeurCampus> voorkeurCampussen = JsonConvert.DeserializeObject<List<VoorkeurCampus>>(JsonResponse2);

            foreach (Gebruiker g in gebruikerResult)
            {
                DisplayGebruiker displaygebruiker = new DisplayGebruiker();
                displaygebruiker.Adres = g.Adres;
                displaygebruiker.Email = g.Email;
                displaygebruiker.Gemeente = g.Gemeente;
                displaygebruiker.Naam = g.Naam;
                displaygebruiker.Postcode = g.Postcode;
                displaygebruiker.Telnr = g.Telnr;
                displaygebruiker.Id = g.GebruikerId;
                displayGebruikers.Add(displaygebruiker);   
            }
            foreach (DisplayGebruiker dg in displayGebruikers)
            {
                foreach (VoorkeurOpleiding vo in voorkeurOpleidingen)
                {
                    if (dg.Id == vo.GebruikerId)
                    {
                        HttpClient client3 = new HttpClient();
                        var JsonResponse3 = await client3.GetStringAsync("http://localhost:51399/api/Opleidings/" + vo.OpleidingId);
                        Opleiding opleiding = JsonConvert.DeserializeObject<Opleiding>(JsonResponse3);
                        dg.OpleidingVoorkeuren += opleiding.Naam + " ";
                    }
                }
                foreach (VoorkeurCampus vc in voorkeurCampussen)
                {
                    if (dg.Id == vc.GebruikerId)
                    {
                        HttpClient client3 = new HttpClient();
                        var JsonResponse3 = await client3.GetStringAsync("http://localhost:51399/api/Campus/" + vc.CampusId);
                        Campus campus = JsonConvert.DeserializeObject<Campus>(JsonResponse3);
                        dg.CampusVoorkeuren += campus.Naam + " ";

                    }
                }
            }
            gebruikersList.ItemsSource = displayGebruikers;
        }
        

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
    }
}
