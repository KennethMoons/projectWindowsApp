using Newtonsoft.Json;
using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using ProjectOpendeurdag.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfomomentBeheerMain : Page
    {
        ObservableCollection<Infomoment> InfomomentList = new ObservableCollection<Infomoment>();
        public InfomomentBeheerMain()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Infomoment> infomomentenResult = await Api.GetAsync<List<Infomoment>>();
            foreach (Infomoment i in infomomentenResult)
            {
                InfomomentList.Add(i);
            }

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfomomentBeheerAddOne));
        }

        private void Infomoment_click(object sender, ItemClickEventArgs e)
        {
            Infomoment infomoment = e.ClickedItem as Infomoment;
            Frame.Navigate(typeof(InfomomentBeheerUpdate), infomoment);
        }
    }
}
