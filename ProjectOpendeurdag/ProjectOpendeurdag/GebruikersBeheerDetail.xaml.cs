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
            List<Gebruiker> gebruikerResult = await Api.GetAsync<List<Gebruiker>>();
            gebruikerResult.RemoveAt(0);
            List<VoorkeurOpleiding> voorkeurOpleidingen = await Api.GetAsync<List<VoorkeurOpleiding>>();
            List<VoorkeurCampus> voorkeurCampussen = await Api.GetAsync<List<VoorkeurCampus>>();

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
                        Opleiding opleiding = await Api.GetAsync<Opleiding>(vo.OpleidingId);
                        dg.OpleidingVoorkeuren += opleiding.Naam + " ";
                    }
                }
                foreach (VoorkeurCampus vc in voorkeurCampussen)
                {
                    if (dg.Id == vc.GebruikerId)
                    {
                        Campus campus = await Api.GetAsync<Campus>(vc.CampusId);
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
