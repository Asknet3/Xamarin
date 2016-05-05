using System;
using Android.App;
using Android.Content;
using Android.OS;


namespace SimpleService
{
    [Service]
    public class SimpleService : Service
    {
        System.Timers.Timer timer1;
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            #region ===== Invia notifica =====
            timer1 = new System.Timers.Timer();
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(NotificaTemporizzata);
            timer1.Interval = 8000; // in miliseconds
            timer1.Enabled = true;
            #endregion =======================

            return StartCommandResult.NotSticky;
        }


        public override void OnDestroy()
        {
            base.OnDestroy();
            timer1.Dispose();
            timer1 = null;
            //this.ApplicationContext.StopService(new Intent(this, typeof(SimpleService)));
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