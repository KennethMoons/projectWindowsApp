﻿using System;
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

            // Set initial state for top checkboxes
            if (VoorkeurCampussen.Count == 0)
            {
                AlleCampussen.IsChecked = false;
            }
            else if (VoorkeurCampussen.Count == Campussen.Count)
            {
                AlleCampussen.IsChecked = true;
            }
            else
            {
                AlleCampussen.IsChecked = null;
            }

            if (VoorkeurOpleidingen.Count == 0)
            {
                AlleOpleidingen.IsChecked = false;
            }
            else if (VoorkeurOpleidingen.Count == Opleidingen.Count)
            {
                AlleOpleidingen.IsChecked = true;
            }
            else
            {
                AlleOpleidingen.IsChecked = null;
            }

            // Listen to changes in preferences
            VoorkeurCampussen.CollectionChanged += VoorkeurCampussen_CollectionChanged;
            VoorkeurOpleidingen.CollectionChanged += VoorkeurOpleidingen_CollectionChanged;

            // Listen to changes in top checkboxes
            AlleCampussen.Checked += AlleCampussen_Checked;
            AlleCampussen.Unchecked += AlleCampussen_Unchecked;
            AlleOpleidingen.Checked += AlleOpleidingen_Checked;
            AlleOpleidingen.Unchecked += AlleOpleidingen_Unchecked;

            // Create states for each campus/opleiding and bind them
            campussen.ForEach(c => VoorkeurCampussenState.Add(new ToggleState<Campus>(VoorkeurCampussen, c)));
            opleidingen.ForEach(o => VoorkeurOpleidingenState.Add(new ToggleState<Opleiding>(VoorkeurOpleidingen, o)));
        }

        private void VoorkeurCampussen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveCampussen();
        }

        private void VoorkeurOpleidingen_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            SaveOpleidingen();
        }

        private void AlleCampussen_Checked(object sender, RoutedEventArgs e)
        {
            // Pause listening for changes
            VoorkeurCampussen.CollectionChanged -= VoorkeurCampussen_CollectionChanged;

            VoorkeurCampussen.Clear();
            Campussen.ToList().ForEach(c => VoorkeurCampussen.Add(c));

            SaveCampussen();

            // Resume listening for changes
            VoorkeurCampussen.CollectionChanged += VoorkeurCampussen_CollectionChanged;
        }

        private void AlleCampussen_Unchecked(object sender, RoutedEventArgs e)
        {
            VoorkeurCampussen.Clear();
        }

        private void AlleOpleidingen_Checked(object sender, RoutedEventArgs e)
        {
            // Pause listening for changes
            VoorkeurOpleidingen.CollectionChanged -= VoorkeurOpleidingen_CollectionChanged;

            VoorkeurOpleidingen.Clear();
            Opleidingen.ToList().ForEach(o => VoorkeurOpleidingen.Add(o));

            SaveOpleidingen();

            // Resume listening for changes
            VoorkeurOpleidingen.CollectionChanged += VoorkeurOpleidingen_CollectionChanged;
        }

        private void AlleOpleidingen_Unchecked(object sender, RoutedEventArgs e)
        {
            VoorkeurOpleidingen.Clear();
        }

        private async void SaveCampussen()
        {
            if (Gebruiker != null)
            {
                Gebruiker.VoorkeurCampussen.Clear();
                Gebruiker.VoorkeurCampussen.AddRange(VoorkeurCampussen);

                await Api.PutAsync<Gebruiker>(Gebruiker.GebruikerId, Gebruiker);
            }
        }

        private async void SaveOpleidingen()
        {
            if (Gebruiker != null)
            {
                Gebruiker.VoorkeurOpleidingen.Clear();
                Gebruiker.VoorkeurOpleidingen.AddRange(VoorkeurOpleidingen);

                await Api.PutAsync<Gebruiker>(Gebruiker.GebruikerId, Gebruiker);
            }
        }

        public class ToggleState<T> : INotifyPropertyChanged
        {
            private ObservableCollection<T> _Collection;

            public event PropertyChangedEventHandler PropertyChanged;

            public ToggleState(ObservableCollection<T> collection, T inner)
            {
                _Collection = collection;
                _Collection.CollectionChanged += _Collection_CollectionChanged;
                Inner = inner;
            }

            private void _Collection_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
            {
                if (PropertyChanged != null)
                {
                    // Notify UI that property might have changed
                    PropertyChanged(this, new PropertyChangedEventArgs("IsChecked"));
                }
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
