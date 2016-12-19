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
using Prism.Mvvm;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GebruikersBeheerMain : Page
    {
        public GebruikersModel Gebruikers { get; set; }

        public GebruikersBeheerMain()
        {
            this.InitializeComponent();
            this.Loaded += GebruikersBeheerMain_Loaded;
        }

        private async void GebruikersBeheerMain_Loaded(object sender, RoutedEventArgs e)
        {
            Gebruikers = DataContext as GebruikersModel;

            var gebruikers = await Api.GetAsync<List<Gebruiker>>();

            Gebruikers.Aantal = gebruikers.Count;

            var campussen = gebruikers
                .SelectMany(g => g.VoorkeurCampussen)
                .GroupBy(c => c)
                .OrderBy(g => g.Count())
                .Reverse()
                .Select(g => g.Key);

            Gebruikers.PopulairsteCampus = campussen.FirstOrDefault();
            Gebruikers.MinstPopulaireCampus = campussen.LastOrDefault();

            var opleidingen = gebruikers
                .SelectMany(g => g.VoorkeurOpleidingen)
                .GroupBy(c => c)
                .OrderBy(g => g.Count())
                .Reverse()
                .Select(g => g.Key);

            Gebruikers.PopulairsteOpleiding = opleidingen.FirstOrDefault();
            Gebruikers.MinstPopulaireOpleiding = opleidingen.LastOrDefault();
        }

        private void BekijkLijst(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GebruikersBeheerDetail));
        }
    }
    public class GebruikersModel : BindableBase
    {
        private int aantal;
        private Campus populairsteCampus;
        private Campus minstPopulaireCampus;
        private Opleiding populairsteOpleiding;
        private Opleiding minstPopulaireOpleiding;

        public int Aantal
        {
            get { return aantal; }
            set { SetProperty(ref aantal, value); }
        }
        public Opleiding PopulairsteOpleiding
        {
            get { return populairsteOpleiding; }
            set { SetProperty(ref populairsteOpleiding, value); }
        }
        public Opleiding MinstPopulaireOpleiding
        {
            get { return minstPopulaireOpleiding; }
            set { SetProperty(ref minstPopulaireOpleiding, value); }
        }
        public Campus PopulairsteCampus
        {
            get { return populairsteCampus; }
            set { SetProperty(ref populairsteCampus, value); }
        }
        public Campus MinstPopulaireCampus
        {
            get { return minstPopulaireCampus; }
            set { SetProperty(ref minstPopulaireCampus, value); }
        }

    }
}
