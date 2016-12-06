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
using System.Net.Http;
using Newtonsoft.Json;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsitemBeheerMain : Page
    {
        public NewsitemBeheerMain()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            HttpClient client = new HttpClient();
            var JsonResponse = await client.GetStringAsync("http://localhost:51399/api/Newsitems");
            var newsitemsResult = JsonConvert.DeserializeObject<List<Newsitem>>(JsonResponse);
            newsitemList.ItemsSource = newsitemsResult;

        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsitemBeheerAddOne));
        }

        private void newsitemList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Newsitem newsitem = newsitemList.SelectedItem as Newsitem;
            Frame.Navigate(typeof(NewsitemBeheerUpdate), newsitem);
        }
    }
}
