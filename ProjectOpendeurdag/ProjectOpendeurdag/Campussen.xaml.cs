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
using ProjectOpendeurdag.Helpers;

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
            this.Loaded += Campussen_Loaded;
        }

        private async void Campussen_Loaded(object sender, RoutedEventArgs e)
        {
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();

            campussen.ForEach(c =>
            {
                c.ImageLink = String.Format("Assets/campus{0}.jpeg", c.CampusId);
                CampusList.Add(c);
            });
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
