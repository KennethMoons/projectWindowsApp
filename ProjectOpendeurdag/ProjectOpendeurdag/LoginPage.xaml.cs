using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.Security.Credentials;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginPage()
        {
            this.InitializeComponent();
            //TitleTextBlock.Text = "log in";
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistratiePagina));
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            var email = UserName.Text.Trim();
            var password = PassWord.Password.Trim();

            var gebruiker = await Api.Login(email, password);

            if (gebruiker != null)
            {
                ApplicationData.Current.RoamingSettings.Values["gebruikerId"] = gebruiker.GebruikerId;
                Api.SetCredentials(email, password);
                Frame.Navigate(typeof(MainPage));
                return;
            }

            MessageDialog showDialog = new MessageDialog("aanmelden niet gelukt");
            showDialog.Commands.Add(new UICommand("Ok") { Id = 0 });
            showDialog.DefaultCommandIndex = 0;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 0)
            {
                Frame.Navigate(typeof(LoginPage));
            }
        }

        private void UserName_GotFocus(object sender, RoutedEventArgs e)
        {

        }

    }
}
