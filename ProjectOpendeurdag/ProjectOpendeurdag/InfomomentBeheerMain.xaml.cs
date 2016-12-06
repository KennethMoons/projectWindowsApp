using Newtonsoft.Json;
using ProjectOpendeurdag.Models;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class InfomomentBeheerMain : Page
    {
        public InfomomentBeheerMain()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HttpClient client = new HttpClient();
            var JsonResponse = await client.GetStringAsync("http://localhost:51399/api/Infomoments");
            var infomomentenResult = JsonConvert.DeserializeObject<List<Infomoment>>(JsonResponse);
            infomomentenList.ItemsSource = infomomentenResult;

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfomomentBeheerAddOne));
        }

        private void infomomenten_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Infomoment infomoment = infomomentenList.SelectedItem as Infomoment;
            Frame.Navigate(typeof(InfomomentBeheerUpdate), infomoment);
        }
    }
}
