using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using ProjectOpendeurdag.Models;
using ProjectOpendeurdag.Helpers;

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
            //List<VoorkeurOpleiding> voorkeurOpleidingen = await Api.GetAsync<List<VoorkeurOpleiding>>();
            //List<VoorkeurCampus> voorkeurCampussen = await Api.GetAsync<List<VoorkeurCampus>>();

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
            gebruikersList.ItemsSource = displayGebruikers;
        }

        private async void Export_Excel_Click(object sender, RoutedEventArgs e)
        {
            Stream resp = await Api.GetReportAsync("excel");
            // did not find how to open Excel
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private async void Export_PDF_Click(object sender, RoutedEventArgs e)
        {
            Stream resp = await Api.GetReportAsync("pdf");
            Windows.System.LauncherOptions options = new Windows.System.LauncherOptions();
            options.ContentType = "application/pdf";
            string file = Path.GetTempFileName();

            MemoryStream ms = new MemoryStream();
            await resp.CopyToAsync(ms);
            ms.WriteTo(new FileStream(file, FileMode.Create | FileMode.Open));

            await Windows.System.Launcher.LaunchUriAsync(new Uri(file), options);
        }
    }
}
