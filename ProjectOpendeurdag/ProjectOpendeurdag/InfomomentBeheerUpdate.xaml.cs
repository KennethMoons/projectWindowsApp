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
    public sealed partial class InfomomentBeheerUpdate : Page
    {
        private Infomoment infomoment;
        public InfomomentBeheerUpdate()
        {
            this.InitializeComponent();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            infomoment = e.Parameter as Infomoment;
            titel.Text = infomoment.Titel;
            beschrijving.Text = infomoment.Beschrijving;
            Datum.Date = new DateTime(Int32.Parse(infomoment.Datum.Split('/')[2]), Int32.Parse(infomoment.Datum.Split('/')[1]), Int32.Parse(infomoment.Datum.Split('/')[0]));
        }
        private async void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            infomoment.Titel = titel.Text;
            infomoment.Beschrijving = beschrijving.Text;
            //infomoment.CampusId = CampussenComboBox.SelectedIndex + 1;
            infomoment.Datum = Datum.Date.ToString().Split(' ')[0];
            //infomoment.OpleidingId = OpleidingenComboBox.SelectedIndex + 1;
            infomoment.Uur = Tijd.Time.ToString();
            await Api.PutAsync<Infomoment>(infomoment.InfomomentId, infomoment);
            Frame.GoBack();
        }

        private async void AppBarButton_Click_1(object sender, RoutedEventArgs e)
        {
            await Api.DeleteAsync<Infomoment>(infomoment.InfomomentId);
            Frame.GoBack();
        }

        private async void OpleidingenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<Opleiding> opleidingen = await Api.GetAsync<List<Opleiding>>();
            List<String> waarden = new List<string>();
            foreach (Opleiding o in opleidingen)
            {
                waarden.Add(o.Naam);
            }
            OpleidingenComboBox.ItemsSource = waarden;
            //OpleidingenComboBox.SelectedIndex = infomoment.OpleidingId -1;
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
            //CampussenComboBox.SelectedIndex = infomoment.CampusId - 1;
        }
    }
}
