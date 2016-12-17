using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Campussen : Page
    {
        ObservableCollection<Campus> CampusList = new ObservableCollection<Campus>();
        public Campussen()
        {
            this.InitializeComponent();
            vulCampussen();
        }

        public async void vulCampussen()
        {
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();
            campussen.RemoveAt(campussen.Count - 1);
            campussen[0].ImageLink = "Assets/campus1.jpg";
            campussen[1].ImageLink = "Assets/campus2.jpg";
            campussen[2].ImageLink = "Assets/campus3.jpeg";
            foreach (Campus c in campussen)
                CampusList.Add(c);
        }

        private void Campus_click(object sender, ItemClickEventArgs e)
        {
            Campus c = e.ClickedItem as Campus;
            if (c.CampusId == 1)
            {
                Frame.Navigate(typeof(CampussenDetailPagina), c);
            }
        }
    }
}
