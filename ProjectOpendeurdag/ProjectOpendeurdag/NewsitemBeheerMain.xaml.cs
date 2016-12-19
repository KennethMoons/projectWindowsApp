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
using System.Collections.ObjectModel;
using ProjectOpendeurdag.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsitemBeheerMain : Page
    {
        ObservableCollection<Newsitem> NewsfeedList = new ObservableCollection<Newsitem>();
        public NewsitemBeheerMain()
        {
            this.InitializeComponent();
        }
        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            List<Newsitem> newsitemsResult = await Api.GetAsync<List<Newsitem>>();
            foreach (Newsitem n in newsitemsResult)
            {
                NewsfeedList.Add(n);
            }
        }

        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsitemBeheerAddOne));
        }

        private void NewsItem_click(object sender, ItemClickEventArgs e)
        {
            Newsitem newsitem = e.ClickedItem as Newsitem;
            Frame.Navigate(typeof(NewsitemBeheerUpdate), newsitem);
        }
    }
}
