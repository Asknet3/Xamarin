using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Android.Locations;

namespace SimpleService
{
    [Service]
    public class LocationService : Service, ILocationListener
    {
        public override StartCommandResult OnStartCommand(Intent intent, StartCommandFlags flags, int startId)
        {
            #region  ===== Gestione Location =====
            // Creo un'istanza di LocationManager che permette di interagire con il servizio di localizzazione del Sistema
            LocationManager locMgr;
            locMgr = GetSystemService(Context.LocationService) as LocationManager;

            Criteria locationCriteria = new Criteria();
            locationCriteria.Accuracy = Accuracy.Fine;
            locationCriteria.PowerRequirement = Power.High;
            string provider = locMgr.GetBestProvider(locationCriteria, true);

            //string provider = LocationManager.GpsProvider;
            if (provider != null)
            {
                locMgr.RequestLocationUpdates(provider, 2000, 0.2f, this);
            }
            else
            {
                Toast.MakeText(this.ApplicationContext, "No location providers available", ToastLength.Long).Show();
            }
            #endregion============================

            return StartCommandResult.NotSticky;
        }

        public override IBinder OnBind(Intent intent)
        {
            // This example isn't of a bound service, so we just return NULL.
            return null;
        }


        #region Implementazione di ILocationListener 
        public void OnLocationChanged(Location location)
        {
            //MainActivity _activity = new MainActivity();
            
            //Android.Views.LayoutInflater infl = (Android.Views.LayoutInflater)GetSystemService(Context.LayoutInflaterService);
            //Android.Views.View layView = infl.Inflate (_activity.Resource.Layout.Main, null);

            //TextView lbl_lat = layView.FindViewById<TextView>(Resource.Id.lbl_lat);
            //TextView lbl_lon = layView.FindViewById<TextView>(Resource.Id.lbl_lon);

            //layView.FindViewById<TextView>(Resource.Id.lbl_lat).Text = location.Latitude.ToString();
            //layView.FindViewById<TextView>(Resource.Id.lbl_lon).Text = location.Longitude.ToString();

            //Toast.MakeText(this, "Latitudine: " + location.Latitude + " Longitudine: " + location.Longitude, ToastLength.Long).Show();

            // Calcola distanza tra location settata e quella attuale.
            // Se mi trovo nei pressi della location settata, spengo il WiFi
            double distanza = CalcolaDistanza(location.Latitude, location.Longitude);

            Toast.MakeText(this, "Distanza dall'ufficio: " + distanza + " metri", ToastLength.Long).Show();

            var myWifi = (Android.Net.Wifi.WifiManager)GetSystemService(WifiService);
            if (distanza < 90)  // sono nel raggio di 100 metri
            {
                // Abilito il wifi
                if(myWifi.WifiState != Android.Net.WifiState.Enabled)
                    myWifi.SetWifiEnabled(true);
            }
            else //Stacco il wifi
                if (myWifi.WifiState != Android.Net.WifiState.Disabled)
                    myWifi.SetWifiEnabled(false);

        }

        public void OnProviderDisabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider)
        {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, [GeneratedEnum] Availability status, Bundle extras)
        {
            throw new NotImplementedException();
        }


        public double CalcolaDistanza(Double endLatitude, Double endLongitude)  // Distanza in metri
        {
            Double startLatitude = 45.4666981;
            Double startLongitude = 9.151868;

            if (startLatitude == endLatitude && startLongitude == endLongitude)
                return 0.0;

            var theta = startLongitude - endLongitude;

            var distance = Math.Sin(deg2rad(startLatitude)) * Math.Sin(deg2rad(endLatitude)) +
                           Math.Cos(deg2rad(startLatitude)) * Math.Cos(deg2rad(endLatitude)) *
                           Math.Cos(deg2rad(theta));

            distance = Math.Acos(distance);
            if (double.IsNaN(distance))
                return 0.0;

            distance = rad2deg(distance);
            distance = distance * 60.0 * 1.1515 * 1609.344;

            return (distance);
        }

        private static double deg2rad(double deg)
        {
            return (deg * Math.PI / 180.0);
        }

        private static double rad2deg(double rad)
        {
            return (rad / Math.PI * 180.0);
        }
        #endregion
    }
}