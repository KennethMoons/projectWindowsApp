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
using Windows.UI.Text;
using Windows.Storage.Streams;

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
            List<Campus> campussen = new List<Campus>();
            Campus nullCampus = new NullCampus();
            campussen.Add(nullCampus);
            campussen.AddRange(await Api.GetAsync<List<Campus>>());
            CampussenComboBox.ItemsSource = campussen;
            CampussenComboBox.SelectedItem = nullCampus;
        }

        private async void OpleidingenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Opleiding> opleidingen = new List<Opleiding>();
            Opleiding nullOpleiding = new NullOpleiding();
            opleidingen.Add(nullOpleiding);
            opleidingen.AddRange(await Api.GetAsync<List<Opleiding>>());
            OpleidingenComboBox.ItemsSource = opleidingen;
            OpleidingenComboBox.SelectedItem = nullOpleiding;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            Infomoment infomoment = new Infomoment();

            infomoment.Titel = titel.Text;
            infomoment.Beschrijving = beschrijving.Text;
            infomoment.Datum = Datum.Date.ToString().Split(' ')[0];
            infomoment.Uur = Tijd.Time.ToString();
            
            var campus = CampussenComboBox.SelectedValue as Campus;
            var opleiding = OpleidingenComboBox.SelectedValue as Opleiding;

            infomoment.Campus = campus is NullCampus ? null : campus;
            infomoment.Opleiding = opleiding is NullOpleiding ? null : opleiding;

            await Api.PostAsync<Infomoment>(infomoment);

            Frame.GoBack();
        }
    }
}
