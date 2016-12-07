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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class RegistratiePagina : Page
    {
        public RegistratiePagina()
        {
            this.InitializeComponent();
        }

        private async void Submit_Click(object sender, RoutedEventArgs e)
        {
            Gebruiker gebruiker = new Gebruiker();
            gebruiker.Wachtwoord = TxtPwd.Password;
            gebruiker.Naam = VoornaamAddr.Text + " " + NameAddr.Text;
            gebruiker.Adres = TxtAddr.Text;
            gebruiker.Postcode = TxtPostcode.Text;
            gebruiker.Gemeente = TxtGemeente.Text;
            gebruiker.Email = TxtEmail.Text;
            gebruiker.Telnr = TxtTelefoon.Text;
            await Api.PostAsync<Gebruiker>(gebruiker);
            Frame.GoBack();
        }

        private void Txt_GotFocus(object sender, RoutedEventArgs e)
        {

        }

        private void pwd_GotFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
