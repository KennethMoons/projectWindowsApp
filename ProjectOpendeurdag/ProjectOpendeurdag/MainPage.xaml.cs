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
using Windows.UI.Core;

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
            MyFrame.Navigate(typeof(Newsfeed));
            TitleTextBlock.Text = "Nieuws";

            MyFrame.Navigated += MyFrame_Navigated;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = MyFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = MyFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;
        }

        private void OnBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                e.Handled = true;
                MyFrame.GoBack();
            }
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
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

        private void OptiesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Opties));
        }

        private void NewsitemBeheer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(NewsitemBeheerMain));
        }

        private void InfomomentBeheer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(InfomomentBeheerMain));
        }

        private void GebruikersBeheer_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(GebruikersBeheerMain));
        }
    }
}

