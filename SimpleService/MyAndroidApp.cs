using Android.App;
using Android.Content;
using Android.OS;


namespace SimpleService
{
    /* Affinchè il servizio parta in background e non venga aperta alcuna applicazione è necessario 
     * NON avviare la MainActivity.cs ma creare una nuova Android class Application (come questa)  */
    [Application]
    public class MyAndroidApp : Application
    {
        public static Context AppContext;

        public void OnCreate(Bundle bundle)
        {
            AppContext = this.ApplicationContext;
            
            // Faccio partire il servizio per le notifiche
            StartService(new Intent(AppContext, typeof(SimpleService)));

            // Faccio partire il servizio per la localizzazione
            StartService(new Intent(AppContext, typeof(LocationService)));
        }
    }
}