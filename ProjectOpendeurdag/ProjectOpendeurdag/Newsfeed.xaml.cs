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
    public sealed partial class Newsfeed : Page
    {
        ObservableCollection<Newsitem> NewsfeedList = new ObservableCollection<Newsitem>();

        public Newsfeed()
        {
            this.InitializeComponent();
            GetNewsItems();
        }
        public async void GetNewsItems()
        {
            var newsitemsResult = await Api.GetAsync<List<Newsitem>>();
            var voorkeurCampussen = Settings.GetVoorkeurCampussen();
            var voorkeurOpleidingen = Settings.GetVoorkeurOpleidingen();

            newsitemsResult.Where(n =>
            {
                // Ensure campus & opleiding are either not set or in voorkeuren
                var voorkeurCampus = n.Campus != null ? voorkeurCampussen.Contains(n.Campus) : true;
                var voorkeurOpleiding = n.Opleiding != null ? voorkeurOpleidingen.Contains(n.Opleiding) : true;
                return voorkeurCampus && voorkeurOpleiding;
            }).ToList().ForEach(n =>
            {
                // Add all news items to list
                NewsfeedList.Add(n);
            });
        }

        private void NewsItem_click(object sender, ItemClickEventArgs e)
        {
            Newsitem n = e.ClickedItem as Newsitem;
            Frame.Navigate(typeof(NewsfeedDetail), n);
        }
    }
}
