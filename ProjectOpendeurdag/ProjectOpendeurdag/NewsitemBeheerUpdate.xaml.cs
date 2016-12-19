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
using ProjectOpendeurdag.Helpers;

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

        private async void CampussenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Campus> campussen = new List<Campus>();
            Campus nullCampus = new NullCampus();
            campussen.Add(nullCampus);
            campussen.AddRange(await Api.GetAsync<List<Campus>>());
            CampussenComboBox.ItemsSource = campussen;
            CampussenComboBox.SelectedItem = newsitem.Campus != null ? newsitem.Campus : nullCampus;
        }

        private async void OpleidingenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Opleiding> opleidingen = new List<Opleiding>();
            Opleiding nullOpleiding = new NullOpleiding();
            opleidingen.Add(nullOpleiding);
            opleidingen.AddRange(await Api.GetAsync<List<Opleiding>>());
            OpleidingenComboBox.ItemsSource = opleidingen;
            OpleidingenComboBox.SelectedItem = newsitem.Opleiding != null ? newsitem.Opleiding : nullOpleiding;
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            newsitem = e.Parameter as Newsitem;
            titel.Text = newsitem.Titel;
            beschrijving.Text = newsitem.Inhoud;
            Datum.Date = DateTime.Parse(newsitem.Datum);
            Tijd.Time = TimeSpan.Parse(newsitem.Uur);
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            newsitem.Titel = titel.Text;
            newsitem.Inhoud = beschrijving.Text;
            newsitem.Datum = Datum.Date.ToString().Split(' ')[0];
            newsitem.Uur = Tijd.Time.ToString();

            var campus = CampussenComboBox.SelectedItem as Campus;
            var opleiding = OpleidingenComboBox.SelectedItem as Opleiding;

            newsitem.Campus = campus is NullCampus ? null : campus;
            newsitem.Opleiding = opleiding is NullOpleiding ? null : opleiding;

            await Api.PutAsync<Newsitem>(newsitem.NewsitemId, newsitem);

            Frame.GoBack();
        }

        private async void Delete_Click(object sender, RoutedEventArgs e)
        {
            await Api.DeleteAsync<Newsitem>(newsitem.NewsitemId);

            Frame.GoBack();
        }
    }
}
