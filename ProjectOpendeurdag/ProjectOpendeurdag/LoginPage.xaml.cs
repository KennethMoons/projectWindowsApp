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
using ProjectOpendeurdag.Helpers;
using Prism.Windows.Validation;
using System.ComponentModel.DataAnnotations;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        public LoginModel LoginModel { get; set; }
        public LoginPage()
        {
            this.InitializeComponent();
            this.Loaded += LoginPage_Loaded;
        }

        private void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            LoginModel = DataContext as LoginModel;
            LoginModel.ErrorsChanged += LoginModel_ErrorsChanged;
            // Focus email
            Email.Focus(FocusState.Programmatic);
        }

        private void LoginModel_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            var errors = LoginModel.Errors.Errors.Values.SelectMany(x => x);

            ErrorList.ItemsSource = errors;

            if (errors.Count() > 0)
            {
                ErrorList.Visibility = Visibility.Visible;
            }
            else
            {
                ErrorList.Visibility = Visibility.Collapsed;
            }
        }

        private void SignUp_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(RegistratiePagina));
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            TryLogin();
        }

        private void Password_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                TryLogin();
            }
        }

        private async void TryLogin()
        {
            if (LoginModel.ValidateProperties())
            {
                var gebruiker = await Api.Login(LoginModel.Email, LoginModel.Password);

                if (gebruiker == null)
                {
                    LoginModel.SetAllErrors(new Dictionary<string, ReadOnlyCollection<string>>() { { "Email", new ReadOnlyCollection<string>(new string[] { "Combinatie email en wachtwoord is verkeerd" }) } });
                }
                else
                {
                    Frame.Navigate(typeof(MainPage));
                }
            }
        }
    }

    public class LoginModel : ValidatableBindableBase
    {
        private string email;
        private string password;

        [Required(ErrorMessage = "Gelieve een email adres in te geven")]
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }

        [Required(ErrorMessage = "Gelieve een wachtwoord in te vullen")]
        public string Password
        {
            get { return password; }
            set { SetProperty(ref password, value); }
        }
    }
}
