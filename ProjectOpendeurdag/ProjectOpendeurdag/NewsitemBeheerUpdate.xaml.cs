﻿using Newtonsoft.Json;
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
    public sealed partial class NewsitemBeheerUpdate : Page
    {
        private Newsitem newsitem;
        public NewsitemBeheerUpdate()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            newsitem = e.Parameter as Newsitem;
            titel.Text = newsitem.Titel;
            beschrijving.Text = newsitem.Inhoud;
            Datum.Date = new DateTime(Int32.Parse(newsitem.Datum.Split('/')[2]), Int32.Parse(newsitem.Datum.Split('/')[1]), Int32.Parse(newsitem.Datum.Split('/')[0]));
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
            OpleidingenComboBox.SelectedIndex = newsitem.OpleidingId - 1;
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
            CampussenComboBox.SelectedIndex = newsitem.CampusId - 1;
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            var client = new HttpClient();
            await client.DeleteAsync("http://localhost:51399/api/Newsitems/" + newsitem.NewsitemId);
            Frame.GoBack();
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            newsitem.Titel = titel.Text;
            newsitem.Inhoud = beschrijving.Text;
            newsitem.CampusId = CampussenComboBox.SelectedIndex + 1;
            newsitem.Datum = Datum.Date.ToString().Split(' ')[0];
            newsitem.OpleidingId = OpleidingenComboBox.SelectedIndex + 1;
            newsitem.Uur = Tijd.Time.ToString();
            var client = new HttpClient();
            var newsitemtJson = JsonConvert.SerializeObject(newsitem);
            var httpContent = new StringContent(newsitemtJson);
            httpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");
            await client.PutAsync("http://localhost:51399/api/Newsitems/" + newsitem.NewsitemId, httpContent);
            Frame.GoBack();
        }
    }
}
