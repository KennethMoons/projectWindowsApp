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
using System.Collections.ObjectModel;
using System.Diagnostics;
using ProjectOpendeurdag.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Opleidingen : Page
    {
        public ObservableCollection<Opleiding> OpleidingCollection = new ObservableCollection<Opleiding>();

        public Opleidingen()
        {
            this.InitializeComponent();

            GetOpleidingen();
        }

        public async void GetOpleidingen()
        {
            var opleidingen = await Api.GetAsync<List<Opleiding>>();

            opleidingen.ForEach(o =>
            {
                int id = opleidingen.IndexOf(o) + 1;
                o.ImageLink = String.Format("Assets/opleiding{0}.PNG", id);
                OpleidingCollection.Add(o);
            });
        }
    }
}
