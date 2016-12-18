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
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Opties : Page
    {
        public Gebruiker Gebruiker { get; set; }
        public ObservableCollection<Campus> Campussen { get; set; }
        public ObservableCollection<Opleiding> Opleidingen { get; set; }
        public ObservableCollection<Campus> VoorkeurCampussen { get; set; }
        public ObservableCollection<Opleiding> VoorkeurOpleidingen { get; set; }
        public ObservableCollection<ToggleState<Campus>> VoorkeurCampussenState { get; set; }
        public ObservableCollection<ToggleState<Opleiding>> VoorkeurOpleidingenState { get; set; }

        public Opties()
        {
            this.InitializeComponent();
            this.DataContext = this;
            // Alle campussen/opleidingen
            this.Campussen = new ObservableCollection<Campus>();
            this.Opleidingen = new ObservableCollection<Opleiding>();
            // Geselecteerde campussen/opleidingen
            this.VoorkeurCampussen = new ObservableCollection<Campus>();
            this.VoorkeurOpleidingen = new ObservableCollection<Opleiding>();
            // Toggle state voor checkboxes
            this.VoorkeurCampussenState = new ObservableCollection<ToggleState<Campus>>();
            this.VoorkeurOpleidingenState = new ObservableCollection<ToggleState<Opleiding>>();

            InitializeData();
        }

        private async void InitializeData()
        {
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();
            List<Opleiding> opleidingen = await Api.GetAsync<List<Opleiding>>();

            // Add all campussen/opleidingen to collection
            campussen.ForEach(c => Campussen.Add(c));
            opleidingen.ForEach(o => Opleidingen.Add(o));

            // Get current user
            Gebruiker = await Api.GetAsync<Gebruiker>("current");

            // Set preferences if user is logged in
            if (Gebruiker != null)
            {
                if (Gebruiker.VoorkeurCampussen != null)
                {
                    VoorkeurCampussen.Clear();
                    Gebruiker.VoorkeurCampussen.ForEach(c => VoorkeurCampussen.Add(c));
                }
                if (Gebruiker.VoorkeurOpleidingen != null)
                {
                    VoorkeurOpleidingen.Clear();
                    Gebruiker.VoorkeurOpleidingen.ForEach(o => VoorkeurOpleidingen.Add(o));
                }
            }

            // Listen to changes in preferences
            VoorkeurCampussen.CollectionChanged += VoorkeurCampussen_CollectionChanged;
            VoorkeurOpleidingen.CollectionChanged += VoorkeurOpleidingen_CollectionChanged;

            // Create states for each campus/opleiding and bind them
            campussen.ForEach(c => VoorkeurCampussenState.Add(new ToggleState<Campus>(VoorkeurCampussen, c)));
            opleidingen.ForEach(o => VoorkeurOpleidingenState.Add(new ToggleState<Opleiding>(VoorkeurOpleidingen, o)));
        }

        private async void VoorkeurCampussen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Gebruiker != null)
            {
                Gebruiker.VoorkeurCampussen.Clear();
                Gebruiker.VoorkeurCampussen.AddRange(VoorkeurCampussen);

                await Api.PutAsync<Gebruiker>(Gebruiker.GebruikerId, Gebruiker);
            }
        }

        private async void VoorkeurOpleidingen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (Gebruiker != null)
            {
                Gebruiker.VoorkeurOpleidingen.Clear();
                Gebruiker.VoorkeurOpleidingen.AddRange(VoorkeurOpleidingen);

                await Api.PutAsync<Gebruiker>(Gebruiker.GebruikerId, Gebruiker);
            }
        }

        public class ToggleState<T>
        {
            private ObservableCollection<T> _Collection;

            public ToggleState(ObservableCollection<T> collection, T inner)
            {
                _Collection = collection;
                Inner = inner;
            }

            public T Inner { get; set; }

            public bool IsChecked
            {
                get
                {
                    return _Collection.Contains(Inner);
                }
                set
                {
                    if (value == false && _Collection.Contains(Inner))
                    {
                        _Collection.Remove(Inner);
                    }
                    else if (value == true && !_Collection.Contains(Inner))
                    {
                        _Collection.Add(Inner);
                    }
                }
            }
        }
    }
}
