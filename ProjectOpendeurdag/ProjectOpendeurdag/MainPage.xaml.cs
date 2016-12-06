using ProjectOpendeurdag.Models;
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
            BackButton.Visibility = Visibility.Collapsed;
            MyFrame.Navigate(typeof(Newsfeed));
            TitleTextBlock.Text = "news feed";
            NewsFeedLbi.IsSelected = true;
            frame = MyFrame;
            
        }

        private void HamburgerButton_Click(object sender, RoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            if (MyFrame.CanGoBack)
            {
                MyFrame.GoBack();
            }
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (NewsFeedLbi.IsSelected)
            {
                BackButton.Visibility = Visibility.Collapsed;
                MyFrame.Navigate(typeof(Newsfeed));
                TitleTextBlock.Text = "News feed";
            }
            else if (OpleidingenLbi.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Opleidingen));
                TitleTextBlock.Text = "Opleidingen";
            }
            else if (CampussenLbi.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Campussen));
                TitleTextBlock.Text = "Campussen";
            }
            else if (InfoMomentenLbi.IsSelected)
            {
                BackButton.Visibility = Visibility.Visible;
                MyFrame.Navigate(typeof(Infomomenten));
                TitleTextBlock.Text = "Infomomenten";
            }
        }

        private void AdminButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(AdminPage));
        }

        private void AanmeldenButton_Click(object sender, RoutedEventArgs e)
        {
            MyFrame.Navigate(typeof(LoginPage));
        }
    }
}
       
