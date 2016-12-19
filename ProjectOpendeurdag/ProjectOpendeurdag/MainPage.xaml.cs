﻿using ProjectOpendeurdag.Models;
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
using ProjectOpendeurdag.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();

            ToggleLoginButtons();

            MyFrame.Navigated += MyFrame_Navigated;

            SystemNavigationManager.GetForCurrentView().BackRequested += OnBackRequested;
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = MyFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            // Nagivate to newsfeed by default
            MyFrame.Navigate(typeof(Newsfeed));
        }

        private void ToggleLoginButtons()
        {
            bool isLoggedIn = Settings.IsGebruikerLoggedIn();
            bool isAdmin = Settings.IsGebruikerLoggedIn();

            LoginButton.Visibility = !isLoggedIn ? Visibility.Visible : Visibility.Collapsed;
            UserButton.Visibility = isLoggedIn && !isAdmin ? Visibility.Visible : Visibility.Collapsed;
            AdminButton.Visibility = isLoggedIn && isAdmin ? Visibility.Visible : Visibility.Collapsed;
        }

        private void MyFrame_Navigated(object sender, NavigationEventArgs e)
        {
            SystemNavigationManager.GetForCurrentView().AppViewBackButtonVisibility = MyFrame.CanGoBack ? AppViewBackButtonVisibility.Visible : AppViewBackButtonVisibility.Collapsed;

            // Update title 
            TitleTextBlock.Text = PageTitles.Get(e.SourcePageType);

            // Update selected item in navigation list
            if (e.SourcePageType.Equals(typeof(Newsfeed)))
            {
                NavigationList.SelectedItem = NewsFeedLbi;
            }
            else if (e.SourcePageType.Equals(typeof(Opleidingen)))
            {
                NavigationList.SelectedItem = OpleidingenLbi;
            }
            else if (e.SourcePageType.Equals(typeof(Campussen)))
            {
                NavigationList.SelectedItem = CampussenLbi;
            }
            else if (e.SourcePageType.Equals(typeof(InfomomentFeed)))
            {
                NavigationList.SelectedItem = InfoMomentenLbi;
            }
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
            }
            else if (OpleidingenLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(Opleidingen));
            }
            else if (CampussenLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(Campussen));
            }
            else if (InfoMomentenLbi.IsSelected)
            {
                MyFrame.Navigate(typeof(InfomomentFeed));
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

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(LoginPage));
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Api.Logout();
            ToggleLoginButtons();
        }
    }
}

