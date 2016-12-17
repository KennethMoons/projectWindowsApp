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
        }
        public async void getNewsItems()
        {
            List<Newsitem> newsitemsResult = await Api.GetAsync<List<Newsitem>>();
            List<Newsitem> newsItems = new List<Newsitem>();
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
            List<VoorkeurOpleiding> voorkeurOpleidingenDb = await Api.GetAsync<List<VoorkeurOpleiding>>();
            List<VoorkeurOpleiding> voorkeuropleidingen = new List<VoorkeurOpleiding>();
            foreach (VoorkeurOpleiding vo in voorkeurOpleidingenDb)
            {
                if (vo.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
                {
                    voorkeuropleidingen.Add(vo);
                }
            }


            foreach (Newsitem n in newsitemsResult)
            {
                if (n.CampusId == 4 && n.OpleidingId == 5)
                {
                    newsItems.Add(n);
                }
            }
            foreach (VoorkeurCampus vc in voorkeurcampussen)
            {
                foreach (Newsitem n in newsitemsResult)
                {
                    if (n.CampusId == vc.CampusId)
                    {
                        newsItems.Add(n);
                    }
                }
            }
            foreach (VoorkeurOpleiding vo in voorkeuropleidingen)
            {
                foreach (Newsitem n in newsitemsResult)
                {
                    if (n.OpleidingId == vo.OpleidingId)
                    {
                        newsItems.Add(n);
                    }
                }
            }
            foreach (Newsitem n in newsItems)
                NewsfeedList.Add(n);
        }

        private void NewsItem_click(object sender, ItemClickEventArgs e)
        {
            Newsitem n = e.ClickedItem as Newsitem;
            Frame.Navigate(typeof(NewsfeedDetail), n);
        }
    }
}
