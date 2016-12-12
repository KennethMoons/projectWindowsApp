using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
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
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        Frame frame;
        
        public MainPage()
        {
            this.InitializeComponent();
            var roamingSettings = ApplicationData.Current.RoamingSettings;

            if (Int32.Parse(roamingSettings.Values["gebruikerId"].ToString()) != 1)
            {
                AdminButton.Visibility = Visibility.Collapsed;
            }

            NavigationList.SelectedItem = NewsFeedLbi;
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                Debug.WriteLine("Going back");
                MyFrame.GoBack();
            }
            else
            {
                Debug.WriteLine("Cant go back");
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewsFeedLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(Newsfeed));
                TitleTextBlock.Text = "Nieuws";
            }
            else if (OpleidingenLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(Opleidingen));
                TitleTextBlock.Text = "Opleidingen";
            }
            else if (CampussenLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(Campussen));
                TitleTextBlock.Text = "Campussen";
            }
            else if (InfoMomentenLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(InfomomentFeed));
                TitleTextBlock.Text = "Infomomenten";
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AdminPage));
        }

        private void OptiesButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Opties));
        }
    }
}
       
