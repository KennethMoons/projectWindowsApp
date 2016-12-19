using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
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
using ProjectOpendeurdag.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GebruikersBeheerMain : Page
    {
        public GebruikersBeheerMain()
        {
            this.InitializeComponent();
            getAantalGebruikers();
        }

        private void BekijkLijst(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GebruikersBeheerDetail));
        }

        public async void getAantalGebruikers()
        {
            List<Gebruiker> gebruikers = await Api.GetAsync<List<Gebruiker>>();
            int aantal = gebruikers.Count - 1;
            aantalStudenten.Text = aantal.ToString();
        }
        public async void getCampusVoorkeurInfo()
        {
        }
        public async void getOpleidingvoorkeurInfo()
        {

        }
    }
}
