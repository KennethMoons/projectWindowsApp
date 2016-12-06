using Newtonsoft.Json;
using ProjectOpendeurdag.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfomomentBeheerAddOne : Page
    {
        public InfomomentBeheerAddOne()
        {
            this.InitializeComponent();
        }

        private async void CampussenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var JsonResponse = await client.GetStringAsync("http://localhost:51399/api/Campus");
            List<Campus> campussen = JsonConvert.DeserializeObject<List<Campus>>(JsonResponse);
            List<String> waarden = new List<string>();
            foreach (Campus c in campussen)
            {
                waarden.Add(c.Naam);
            }
            CampussenComboBox.ItemsSource = waarden;
            CampussenComboBox.SelectedIndex = 3;
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Infomoment infomoment = new Infomoment();
            infomoment.Titel = titel.Text;
            infomoment.Beschrijving = beschrijving.Text;
            infomoment.CampusId = CampussenComboBox.SelectedIndex + 1;
            infomoment.Datum = Datum.Date.ToString().Split(' ')[0];
            infomoment.OpleidingId = OpleidingenComboBox.SelectedIndex + 1;
            infomoment.Uur = Tijd.Time.ToString();
            var infomomentJson = JsonConvert.SerializeObject(infomoment);
            var client = new HttpClient();
            var httpContent = new StringContent(infomomentJson);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            await client.PostAsync("http://localhost:51399/api/Infomoments", httpContent);
            Frame.GoBack();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }

        private async void OpleidingenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            HttpClient client = new HttpClient();
            var JsonResponse = await client.GetStringAsync("http://localhost:51399/api/Opleidings");
            List<Opleiding> opleidingen = JsonConvert.DeserializeObject<List<Opleiding>>(JsonResponse);
            List<String> waarden = new List<string>();
            foreach (Opleiding o in opleidingen)
            {
                waarden.Add(o.Naam);
            }
            OpleidingenComboBox.ItemsSource = waarden;
            OpleidingenComboBox.SelectedIndex = 4;
        }
    }
}
