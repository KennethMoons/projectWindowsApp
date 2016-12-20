using System;
using System.Collections.Generic;
using System.IO;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using ProjectOpendeurdag.Models;
using ProjectOpendeurdag.Helpers;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GebruikersBeheerDetail : Page
    {
        List<DisplayGebruiker> displayGebruikers;
        public GebruikersBeheerDetail()
        {
            this.InitializeComponent();
            displayGebruikers = new List<DisplayGebruiker>();
            VulGebruikers();

        }

        public async void VulGebruikers()
        {
            List<Gebruiker> gebruikerResult = await Api.GetAsync<List<Gebruiker>>();

            foreach (Gebruiker g in gebruikerResult)
            {
                DisplayGebruiker displaygebruiker = new DisplayGebruiker();
                displaygebruiker.Adres = g.Adres;
                displaygebruiker.Email = g.Email;
                displaygebruiker.Gemeente = g.Gemeente;
                displaygebruiker.Naam = g.Naam;
                displaygebruiker.Postcode = g.Postcode;
                displaygebruiker.Telnr = g.Telnr;
                displaygebruiker.Id = g.GebruikerId;

                string campussen = "";
                string opleidingen = "";

                if (g.VoorkeurCampussen != null)
                {
                    g.VoorkeurCampussen.ForEach(c => campussen += c.Naam + ", ");
                }
                if (g.VoorkeurOpleidingen != null)
                {
                    g.VoorkeurOpleidingen.ForEach(o => opleidingen += o.Naam + ", ");
                }

                displaygebruiker.CampusVoorkeuren = campussen;
                displaygebruiker.OpleidingVoorkeuren = opleidingen;

                displayGebruikers.Add(displaygebruiker);
            }

            gebruikersList.ItemsSource = displayGebruikers;
        }

        private void Export_Excel_Click(object sender, RoutedEventArgs e)
        {
            SaveFile("excel", ".xlsx", "Excel File");
        }

        private void Export_Click(object sender, RoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
        }

        private void Export_PDF_Click(object sender, RoutedEventArgs e)
        {
            SaveFile("pdf", ".pdf", "Portable Document Format");
        }

        private async void SaveFile(string uri, string type, string typeDescription)
        {
            Stream resp = await Api.GetReportAsync(uri);

            MemoryStream ms = new MemoryStream();

            await resp.CopyToAsync(ms);

            var savePicker = new Windows.Storage.Pickers.FileSavePicker();
            savePicker.SuggestedStartLocation =
                Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add(typeDescription, new List<string>() { type });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "Gebruikers";

            Windows.Storage.StorageFile file = await savePicker.PickSaveFileAsync();
            if (file != null)
            {
                // Prevent updates to the remote version of the file until
                // we finish making changes and call CompleteUpdatesAsync.
                Windows.Storage.CachedFileManager.DeferUpdates(file);
                // write to file
                await Windows.Storage.FileIO.WriteBytesAsync(file, ms.ToArray());
                // Let Windows know that we're finished changing the file so
                // the other app can update the remote version of the file.
                // Completing updates may require Windows to ask for user input.
                Windows.Storage.Provider.FileUpdateStatus status =
                    await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);
                if (status == Windows.Storage.Provider.FileUpdateStatus.Complete)
                {
                    NotificationHandler.show("Success", "Het bestand werd opgeslagen");
                }
                else
                {
                    NotificationHandler.show("Mislukt", "Fout bij opslaan bestand");
                }
            }

        }
    }
}
