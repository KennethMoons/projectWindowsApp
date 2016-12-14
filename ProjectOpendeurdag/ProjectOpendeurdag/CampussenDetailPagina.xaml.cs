using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CampussenDetailPagina : Page
    {
        public CampussenDetailPagina()
        {
            this.InitializeComponent();
            setMap();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            Campus campusDb = e.Parameter as Campus;
            campus.Text = campusDb.Naam;
            adres.Text = campusDb.Adres;
            postcode.Text = campusDb.Postcode;
            gemeente.Text = campusDb.Gemeente;
            afbeelding.Source = new BitmapImage(new Uri("ms-appx:///Assets/campus1.jpg"));
            setWeather();
        }

        public void setMap()
        {
            myMap.MapServiceToken = "XbmpHWrIxFyALSPnOSJU~qgAEyQ_VVi9VmNXRPCA11w~Aubuvc2ot5nI5pvz2mF4869NWp_k-Vv9c_jxDOMOyayZIF7emI_Y-ApfJl-jMcLj";
            BasicGeoposition positionSchool = new BasicGeoposition();
            positionSchool.Latitude = 51.031359;
            positionSchool.Longitude = 3.706420;

            MapIcon mapIcon1 = new MapIcon();
            mapIcon1.Location = new Geopoint(positionSchool);
            mapIcon1.NormalizedAnchorPoint = new Point(0.5, 1.0);
            mapIcon1.Title = "campus schoonmeersen";
            mapIcon1.ZIndex = 0;

            myMap.MapElements.Add(mapIcon1);

            myMap.Center = new Geopoint(positionSchool);
            myMap.ZoomLevel = 15;
        }

        private async void myMap_MapDoubleTapped(MapControl sender, MapInputEventArgs args)
        {

            var uriSchool = new Uri(@"bingmaps:? cp = 51.031359~-3.706420 & lvl = 15");
            var launcherOptions = new Windows.System.LauncherOptions();
            launcherOptions.TargetApplicationPackageFamilyName = "Microsoft.WindowsMaps_8wekyb3d8bbwe";
            var success = await Windows.System.Launcher.LaunchUriAsync(uriSchool, launcherOptions);
        }

        public async void setWeather()
        {
            RootObject myWeather = await WeatherProxy.getWeather();
            weer.Text = "het is momenteel " + ((int)myWeather.main.temp).ToString() + " graden";
        }
    }
}
