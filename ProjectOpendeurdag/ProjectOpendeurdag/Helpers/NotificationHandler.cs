using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Toolkit.Uwp.Notifications;
using Windows.UI.Notifications;

namespace ProjectOpendeurdag.Helpers
{
    class NotificationHandler
    {



        public static void show(string title, string message)
        {
            ToastContent content = new ToastContent()
            {
                Launch = "notification",
                Visual = new ToastVisual()
                {
                    BindingGeneric = new ToastBindingGeneric()
                    {
                        Children =
                        {
                            new AdaptiveText()
                            {
                                Text = title
                            },
                            new AdaptiveText()
                            {
                                Text = message
                            }
                        }
                    }
                }

            };

            ToastNotification notification = new ToastNotification(content.GetXml());
            ToastNotificationManager.CreateToastNotifier().Show(notification);
        }
    }


}
