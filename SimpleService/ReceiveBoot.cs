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


namespace SimpleService
{
    [BroadcastReceiver]
    [IntentFilter(new[] { Intent.ActionBootCompleted }, Categories = new[] { Intent.CategoryHome})]
    public class ReceiveBoot : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //Toast.MakeText(context, “Received intent!”, ToastLength.Short).Show();

            if ((intent.Action != null) && (intent.Action == Intent.ActionBootCompleted))
            { 
                // Avvio il servizio
                context.StartService(new Intent(context, typeof(SimpleService))); // SimpleService è il nome del mio servizio
            }

           
        }
    }
}