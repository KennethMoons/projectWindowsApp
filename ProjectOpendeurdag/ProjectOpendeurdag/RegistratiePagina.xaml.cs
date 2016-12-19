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
using Newtonsoft.Json;
using System.Net.Http;
using ProjectOpendeurdag.Helpers;
using System.ComponentModel.DataAnnotations;
using Prism.Windows.Validation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistratiePagina : Page
    {
        public RegistratieModel Registratie { get; set; }


        public RegistratiePagina()
        {
            this.InitializeComponent();
            this.Loaded += RegistratiePagina_Loaded;
        }

        private void RegistratiePagina_Loaded(object sender, RoutedEventArgs e)
        {
            Registratie = DataContext as RegistratieModel;
            Registratie.ErrorsChanged += Registratie_ErrorsChanged;
        }

        private void Registratie_ErrorsChanged(object sender, System.ComponentModel.DataErrorsChangedEventArgs e)
        {
            ErrorList.ItemsSource = Registratie.Errors.Errors.Values.SelectMany(x => x);
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            if (Registratie.ValidateProperties())
            {
                Gebruiker gebruiker = new Gebruiker();

                gebruiker.Email = Registratie.Email;
                gebruiker.Wachtwoord = Registratie.Wachtwoord;
                gebruiker.Naam = Registratie.Voornaam + " " + Registratie.Naam;
                gebruiker.Adres = Registratie.Adres;
                gebruiker.Postcode = Registratie.Postcode;
                gebruiker.Gemeente = Registratie.Gemeente;
                gebruiker.Telnr = Registratie.Telefoon;

                try
                {
                    await Api.PostAsync<Gebruiker>(gebruiker);
                    await Api.Login(gebruiker.Email, gebruiker.Adres);
                }
                catch (Exception)
                {
                    // Something went wrong
                }


                Frame.GoBack();
            }
        }
    }

    public class RegistratieModel : ValidatableBindableBase
    {
        private string email;
        private string wachtwoord;
        private string naam;
        private string voornaam;
        private string adres;
        private string postcode;
        private string gemeente;
        private string telefoon;

        [Required]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Gelieve een geldig email adres in te geven")]
        public string Email
        {
            get { return email; }
            set { SetProperty(ref email, value); }
        }
        [Required]
        public string Wachtwoord
        {
            get { return wachtwoord; }
            set { SetProperty(ref wachtwoord, value); }
        }
        public string Naam
        {
            get { return naam; }
            set { SetProperty(ref naam, value); }
        }
        public string Voornaam
        {
            get { return voornaam; }
            set { SetProperty(ref voornaam, value); }
        }
        public string Adres
        {
            get { return adres; }
            set { SetProperty(ref adres, value); }
        }
        [RegularExpression(@"\d{4}", ErrorMessage = "Gelieve een geldige postcode in te geven")]
        public string Postcode
        {
            get { return postcode; }
            set { SetProperty(ref postcode, value); }
        }
        public string Gemeente
        {
            get { return gemeente; }
            set { SetProperty(ref gemeente, value); }
        }
        public string Telefoon
        {
            get { return telefoon; }
            set { SetProperty(ref telefoon, value); }
        }
    }
}
