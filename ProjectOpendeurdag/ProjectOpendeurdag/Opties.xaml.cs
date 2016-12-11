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
using Windows.Storage;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Opties : Page
    {
        public Opties()
        {
            this.InitializeComponent();
            vulCampussen();
            vulOpleidingen();
        }
        public async void vulCampussen()
        {
            List<VoorkeurCampus> voorkeurCampusItemsDb = await Api.GetAsync<List<VoorkeurCampus>>();
            List<VoorkeurCampus> voorkeurcampussen = new List<VoorkeurCampus>();
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            foreach (VoorkeurCampus vc in voorkeurCampusItemsDb)
            {
                if (vc.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
                {
                    voorkeurcampussen.Add(vc);
                }
            }
            foreach (VoorkeurCampus vc in voorkeurcampussen)
            {
                Campus c = await Api.GetAsync<Campus>(vc.CampusId);
                vc.Campus = c;
            }
            campussenList.ItemsSource = voorkeurcampussen;
        }

        public async void vulOpleidingen()
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            List<VoorkeurOpleiding> voorkeurOpleidingenDb = await Api.GetAsync<List<VoorkeurOpleiding>>();
            List<VoorkeurOpleiding> voorkeuropleidingen = new List<VoorkeurOpleiding>();
            foreach (VoorkeurOpleiding vo in voorkeurOpleidingenDb)
            {
                if (vo.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
                {
                    voorkeuropleidingen.Add(vo);
                }
            }
            foreach (VoorkeurOpleiding vo in voorkeuropleidingen)
            {
                Opleiding o = await Api.GetAsync<Opleiding>(vo.OpleidingId);
                vo.Opleiding = o;
            }
            opleidingenLijst.ItemsSource = voorkeuropleidingen;
        }

        private async void CampussenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            List<VoorkeurCampus> voorkeurCampusItemsDb = await Api.GetAsync<List<VoorkeurCampus>>();
            List<VoorkeurCampus> voorkeurcampussen = new List<VoorkeurCampus>();
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            foreach (VoorkeurCampus vc in voorkeurCampusItemsDb)
            {
                if (vc.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
                {
                    voorkeurcampussen.Add(vc);
                }
            }
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();
            for (int i = 0; i < campussen.Count; i++)
            {
                for (int y = 0; y < voorkeurcampussen.Count; y++)
                {
                    if (voorkeurcampussen[y].CampusId == campussen[i].CampusId)
                    {
                        campussen.RemoveAt(i);
                    }
                }
            }
            List<String> waarden = new List<string>();
            campussen.RemoveAt(campussen.Count - 1);
            foreach (Campus c in campussen)
            {
                waarden.Add(c.Naam);
            }
            CampussenComboBox.ItemsSource = waarden;
        }

        private async void VoegCampusToeBtn_Click(object sender, RoutedEventArgs e)
        {
            VoorkeurCampus voorkeurcampus = new VoorkeurCampus();
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            voorkeurcampus.GebruikerId = Int32.Parse(roamingSettings.Values["gebruikerId"].ToString());
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();
            foreach (Campus c in campussen)
            {
                if (c.Naam == CampussenComboBox.SelectedItem.ToString())
                    voorkeurcampus.CampusId = c.CampusId;
            }
            await Api.PostAsync<VoorkeurCampus>(voorkeurcampus);
            Frame.Navigate(typeof(Opties));
        }

        private async void OpleidingenComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            List<VoorkeurOpleiding> voorkeurOpleidingenDb = await Api.GetAsync<List<VoorkeurOpleiding>>();
            List<VoorkeurOpleiding> voorkeuropleidingen = new List<VoorkeurOpleiding>();
            foreach (VoorkeurOpleiding vo in voorkeurOpleidingenDb)
            {
                if (vo.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
                {
                    voorkeuropleidingen.Add(vo);
                }
            }
            List<Opleiding> opleidingen = await Api.GetAsync<List<Opleiding>>();

            for (int i = 0; i < opleidingen.Count; i++)
            {
                for (int y = 0; y < voorkeuropleidingen.Count; y++)
                {
                    if (voorkeuropleidingen[y].OpleidingId == opleidingen[i].OpleidingId)
                    {
                        opleidingen.RemoveAt(i);
                    }
                }
            }
            List<String> waarden = new List<string>();
            opleidingen.RemoveAt(opleidingen.Count - 1);
            foreach (Opleiding o in opleidingen)
            {
                waarden.Add(o.Naam);
            }
            OpleidingenComboBox.ItemsSource = waarden;
        }

        private async void DeleteCampusBtn_Click(object sender, RoutedEventArgs e)
        {
            VoorkeurCampus voorkeurCampus = campussenList.SelectedItem as VoorkeurCampus;
            await Api.DeleteAsync<VoorkeurCampus>(voorkeurCampus.VoorkeurCampusId);
            Frame.Navigate(typeof(Opties));
        }

        private async void DeleteOpleidingBtn_Click(object sender, RoutedEventArgs e)
        {
            VoorkeurOpleiding voorkeurOpleiding = opleidingenLijst.SelectedItem as VoorkeurOpleiding;
            await Api.DeleteAsync<VoorkeurOpleiding>(voorkeurOpleiding.VoorkeurOpleidingId);
            Frame.Navigate(typeof(Opties));
        }

        private async void VoegOpleidingToeComboBox_Click(object sender, RoutedEventArgs e)
        {
            VoorkeurOpleiding voorkeuropleiding = new VoorkeurOpleiding();
            var roamingSettings = ApplicationData.Current.RoamingSettings;
            voorkeuropleiding.GebruikerId = Int32.Parse(roamingSettings.Values["gebruikerId"].ToString());
            Opleiding opleiding = OpleidingenComboBox.SelectedItem as Opleiding;
            List<Opleiding> opleidingen = await Api.GetAsync<List<Opleiding>>();
            foreach (Opleiding o in opleidingen)
            {
                if (o.Naam == OpleidingenComboBox.SelectedItem.ToString())
                    voorkeuropleiding.OpleidingId = o.OpleidingId;
            }
            await Api.PostAsync<VoorkeurOpleiding>(voorkeuropleiding);
            Frame.Navigate(typeof(Opties));
        }
    }
}
