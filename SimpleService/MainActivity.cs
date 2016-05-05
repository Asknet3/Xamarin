
using Android.App;
using Android.Content;
using Android.Widget;
using Android.OS;


namespace SimpleService
{
    [Activity(Label = "SimpleService", MainLauncher = true, Icon = "@drawable/icon",UiOptions = Android.Content.PM.UiOptions.None, LaunchMode = Android.Content.PM.LaunchMode.SingleInstance)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            var manager = (ActivityManager)GetSystemService(ActivityService);
            System.Collections.Generic.IList<ActivityManager.RunningServiceInfo> list = manager.GetRunningServices(int.MaxValue);

            // Controllo se il servizio è già presente e avviato
            bool serviceExist = false;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].Service.PackageName == "SimpleService.SimpleService")
                {
                    serviceExist = true;
                }
            }

            Button btn_start = FindViewById<Button>(Resource.Id.btn_start);
            Button btn_stop = FindViewById<Button>(Resource.Id.btn_stop);
            

            Intent service = new Intent(this, typeof(SimpleService));
            Intent locationService = new Intent(this, typeof(LocationService));

            if (serviceExist)
            {
                btn_start.Enabled = false;
                btn_stop.Enabled = true;
            }
            else
            {
                btn_start.Enabled = true;
                btn_stop.Enabled = false;
            }

            btn_start.Click += delegate
            {
                StartService(service);
                StartService(locationService); // Faccio partire il servizio per la localizzazione
                btn_start.Enabled = !btn_start.Enabled;
                btn_stop.Enabled = !btn_stop.Enabled;
            };

            btn_stop.Click += delegate
            {
                StopService(service);
                StopService(locationService); // Fermo il servizio per la localizzazione
                btn_start.Enabled = !btn_start.Enabled;
                btn_stop.Enabled = !btn_stop.Enabled;
            };
        }
    }
}


