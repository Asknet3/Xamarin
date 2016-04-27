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
    [IntentFilter(new[] { Android.Content.Intent.ActionBootCompleted }, 
        Categories = new[] { Android.Content.Intent.CategoryHome}
    )]
    public class ReceiveBoot : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            //Toast.MakeText(context, “Received intent!”, ToastLength.Short).Show();

            if ((intent.Action != null) &&
            (intent.Action ==
            Android.Content.Intent.ActionBootCompleted))
            { // Start the service or activity
              //context.ApplicationContext.StartService(new Intent(context, typeof(MainActivity)));
                //Android.Content.Intent start = new Android.Content.Intent(context, typeof(SimpleService)); 
                context.ApplicationContext.StartService(new Intent(context, typeof(SimpleService))); // my service name is SimpleService

                //start.AddFlags(ActivityFlags.NewTask);
                //start.AddFlags(ActivityFlags.FromBackground);
                //context.ApplicationContext.StartService(start);
            }
        }
    }
}