using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Util;
using System.Threading;

namespace SimpleService
{
    [Service]
    public class SimpleService : Service
    {
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            System.Timers.Timer timer1;

            timer1 = new System.Timers.Timer();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(NotificaTemporizzata);
            timer1.Interval = 8000; // in miliseconds
            timer1.Enabled = true;

            return StartCommandResult.NotSticky;
        }



        public override void OnDestroy()
        {
           
        }

        public override IBinder OnBind(Intent intent)
        {
            // This example isn't of a bound service, so we just return NULL.
            return null;
        }


        private void NotificaTemporizzata(object sender, EventArgs e)
        {
            CreaNotifica();
        }


        public void CreaNotifica()
        {
            // Instantiate the builder and set notification elements:
            Notification.Builder builder = new Notification.Builder(this)
                .SetContentTitle("Sample Notification")
                .SetContentText("Time: " + DateTime.Now)
                .SetDefaults(NotificationDefaults.Sound)
                .SetSmallIcon(Resource.Drawable.Icon);

            builder.SetPriority((int)NotificationPriority.High);

            // Build the notification:
            Notification notification = builder.Build();

            // Get the notification manager:
            NotificationManager notificationManager =
                GetSystemService(Context.NotificationService) as NotificationManager;

            // Publish the notification:
            const int notificationId = 0;
            notificationManager.Notify(notificationId, notification);
        }

    }
}