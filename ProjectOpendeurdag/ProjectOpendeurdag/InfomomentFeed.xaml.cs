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
    public sealed partial class InfomomentFeed : Page
    {
        ObservableCollection<Infomoment> InfomomentList = new ObservableCollection<Infomoment>();

        public InfomomentFeed()
        {
            this.InitializeComponent();
            getInfomoments();
        }
        public async void getInfomoments()
        {
            List<Infomoment> infomomentsResult = await Api.GetAsync<List<Infomoment>>();
            List<Infomoment> infomomenten = new List<Infomoment>();
            //List<VoorkeurCampus> voorkeurCampusItemsDb = await Api.GetAsync<List<VoorkeurCampus>>();
            //List<VoorkeurCampus> voorkeurcampussen = new List<VoorkeurCampus>();
            //var roamingSettings = ApplicationData.Current.RoamingSettings;
            //foreach (VoorkeurCampus vc in voorkeurCampusItemsDb)
            //{
            //    if (vc.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
            //    {
            //        voorkeurcampussen.Add(vc);
            //    }
            //}
            //List<VoorkeurOpleiding> voorkeurOpleidingenDb = await Api.GetAsync<List<VoorkeurOpleiding>>();
            //List<VoorkeurOpleiding> voorkeuropleidingen = new List<VoorkeurOpleiding>();
            //foreach (VoorkeurOpleiding vo in voorkeurOpleidingenDb)
            //{
            //    if (vo.GebruikerId == Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()))
            //    {
            //        voorkeuropleidingen.Add(vo);
            //    }
            //}


            foreach (Infomoment i in infomomentsResult)
            {
                if (i.CampusId == 4 && i.OpleidingId == 5)
                {
                    infomomenten.Add(i);
                }
            }
            //foreach (VoorkeurCampus vc in voorkeurcampussen)
            //{
            //    foreach (Infomoment i in infomomentsResult)
            //    {
            //        if (i.CampusId == vc.CampusId)
            //        {
            //            infomomenten.Add(i);
            //        }
            //    }
            //}
            //foreach (VoorkeurOpleiding vo in voorkeuropleidingen)
            //{
            //    foreach (Infomoment i in infomomentsResult)
            //    {
            //        if (i.OpleidingId == vo.OpleidingId)
            //        {
            //            infomomenten.Add(i);
            //        }
            //    }
            //}
            foreach (Infomoment i in infomomenten)
                InfomomentList.Add(i);
        }

        private void Infomoment_click(object sender, ItemClickEventArgs e)
        {
            Infomoment i = e.ClickedItem as Infomoment;
            Frame.Navigate(typeof(InfomomentfeedDetail), i);
        }
    }
}
