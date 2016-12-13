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
using Newtonsoft.Json;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsitemBeheerAddOne : Page
    {
        public NewsitemBeheerAddOne()
        {
            this.InitializeComponent();
        }

        private async void OpleidingenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Opleiding> opleidingen = await Api.GetAsync<List<Opleiding>>();
            List<String> waarden = new List<string>();
            foreach(Opleiding o in opleidingen)
            {
                waarden.Add(o.Naam);
            }
            OpleidingenComboBox.ItemsSource = waarden;
        }

        private async void CampussenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();
            List<String> waarden = new List<string>();
            foreach (Campus c in campussen)
            {
                waarden.Add(c.Naam);
            }
            CampussenComboBox.ItemsSource = waarden;
        }

        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Newsitem newsitem = new Newsitem();
            newsitem.CampusId = CampussenComboBox.SelectedIndex + 1;
            newsitem.Datum = Datum.Date.ToString().Split(' ')[0];
            newsitem.Inhoud = beschrijving.Text;
            newsitem.OpleidingId = OpleidingenComboBox.SelectedIndex + 1;
            newsitem.Titel = titel.Text;
            newsitem.Uur = Tijd.Time.ToString();
            await Api.PostAsync<Newsitem>(newsitem);
            Frame.GoBack();
        }

        private void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            Frame.GoBack();
        }
    }
}
