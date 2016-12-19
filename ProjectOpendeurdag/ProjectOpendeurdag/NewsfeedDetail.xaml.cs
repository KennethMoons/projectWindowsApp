using NotificationsExtensions.ToastContent;
using ProjectOpendeurdag.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Notifications;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProjectOpendeurdag.Helpers;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjectOpendeurdag
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsfeedDetail : Page
    {
        private Newsitem newsitem = new Newsitem();
        public NewsfeedDetail()
        {
            this.InitializeComponent();
        }
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            newsitem = e.Parameter as Newsitem;
            titel.Text = newsitem.Titel;
            beschrijving.Text = newsitem.Inhoud;
            datum.Text = newsitem.Datum;
            tijd.Text = newsitem.Uur;
            List<Opleiding> opleidingen = await Api.GetAsync<List<Opleiding>>();
            List<Campus> campussen = await Api.GetAsync<List<Campus>>();

            foreach (Opleiding o in opleidingen)
            {
                if (o == newsitem.Opleiding)
                {
                    opleiding.Text = o.Naam;
                }
            }
            foreach (Campus c in campussen)
            {
                if (c == newsitem.Campus)
                {
                    campus.Text = c.Naam;
                }
            }
        }

        private async void agenda_button_Click(object sender, RoutedEventArgs e)
        {
            Appointment a = setAfspraak();
            await Add(a);
        }

        private async void reminder_button_Click(object sender, RoutedEventArgs e)
        {
            MessageDialog showDialog = new MessageDialog("wenst u een reminder een half uur voor de afspraak ?");
            showDialog.Commands.Add(new UICommand("Ok") { Id = 0 });
            showDialog.Commands.Add(new UICommand("Cancel") { Id = 1 });
            showDialog.DefaultCommandIndex = 1;
            var result = await showDialog.ShowAsync();
            if ((int)result.Id == 1)
            {
                Frame.Navigate(typeof(Newsfeed));
            }
            if ((int)result.Id == 0)
            {
                String[] datumFormat = datum.Text.Split('/');
                String[] tijdformat = tijd.Text.Split(':');
                DateTime datumNotification = new DateTime(Int32.Parse(datumFormat[2]), Int32.Parse(datumFormat[0]), Int32.Parse(datumFormat[1]), Int32.Parse(tijdformat[0]), Int32.Parse(tijdformat[1]), Int32.Parse(tijdformat[2]));
                datumNotification.AddMinutes(-30);
                IToastText02 toast = ToastContentFactory.CreateToastText02();
                toast.TextHeading.Text = newsitem.Titel; ;
                toast.TextBodyWrap.Text = newsitem.Inhoud;
                ScheduledToastNotification toastNotification = new ScheduledToastNotification(toast.GetXml(), datumNotification);
                toastNotification.Id = "id";
                ToastNotificationManager.CreateToastNotifier().AddToSchedule(toastNotification);
                Frame.Navigate(typeof(Newsfeed));
            }
        }

        private void cancel_button_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(Newsfeed));
        }
        public Appointment setAfspraak()
        {
            String[] datumFormat = datum.Text.Split('/');
            String[] tijdformat = tijd.Text.Split(':');
            var appointment = new Appointment();
            appointment.StartTime = new DateTime(Int32.Parse(datumFormat[2]), Int32.Parse(datumFormat[0]), Int32.Parse(datumFormat[1]), Int32.Parse(tijdformat[0]), Int32.Parse(tijdformat[1]), Int32.Parse(tijdformat[2]));
            appointment.Duration = TimeSpan.FromHours(4);
            appointment.Subject = titel.Text;
            appointment.Location = campus.Text;
            appointment.Details = beschrijving.Text;
            return appointment;
        }



        public async static Task<string> Add(Appointment appt)
        {
            var selection = new Rect(new Point(Window.Current.Bounds.Width / 2, Window.Current.Bounds.Height / 2), new Size());
            return await Add(appt, selection);
        }
        public async static Task<string> Add(Appointment appt, Rect selection)
        {
            var id = await AppointmentManager.ShowAddAppointmentAsync(appt, selection, Placement.Default);
            AddAppointmentId(id);

            return id;
        }

        public static void AddAppointmentId(string appointmentId)
        {
            if (String.IsNullOrEmpty(appointmentId))
            {
                return;
            }

            string ids = ApplicationData.Current.RoamingSettings.Values["AppointmentIds"] as string;
            if (String.IsNullOrEmpty(ids))
            {
                ids = appointmentId;
            }
            else
            {
                ids += ";" + appointmentId;
            }

            ApplicationData.Current.RoamingSettings.Values["AppointmentIds"] = ids;
        }
    }
}
