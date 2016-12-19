using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class InfomomentFeed : Page
    {
        ObservableCollection<Infomoment> InfomomentList = new ObservableCollection<Infomoment>();

        public InfomomentFeed()
        {
            this.InitializeComponent();
            this.Loaded += InfomomentFeed_Loaded;
        }

        private async void InfomomentFeed_Loaded(object sender, RoutedEventArgs e)
        {
            var newsitems = await Api.GetAsync<List<Infomoment>>();
            var voorkeurCampussen = Settings.GetVoorkeurCampussen();
            var voorkeurOpleidingen = Settings.GetVoorkeurOpleidingen();

            newsitems.Where(i =>
            {
                // Ensure campus & opleiding are either not set or in voorkeuren
                var voorkeurCampus = i.Campus != null ? voorkeurCampussen.Contains(i.Campus) : true;
                var voorkeurOpleiding = i.Opleiding != null ? voorkeurOpleidingen.Contains(i.Opleiding) : true;
                return voorkeurCampus && voorkeurOpleiding;
            }).ToList().ForEach(i =>
            {
                // Add all info items to list
                InfomomentList.Add(i);
            });
        }

        private void Infomoment_click(object sender, ItemClickEventArgs e)
        {
            Infomoment i = e.ClickedItem as Infomoment;
            Frame.Navigate(typeof(InfomomentfeedDetail), i);
        }
    }
}
