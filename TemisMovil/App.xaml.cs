using Microsoft.WindowsAzure.MobileServices;
using System;
using TemisMovil.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TemisMovil
{
    public partial class App : Application
    {
        //public static string DatabaseLocation = string.Empty;
        public static MobileServiceClient MobileService = new MobileServiceClient("https://temismovil.azurewebsites.net");
        public static User user = new User();
        public const string NotificationReceivedKey = "NotificationReceived";

        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new LoginPage());

            Xamarin.Forms.PlatformConfiguration.iOSSpecific.Page.SetUseSafeArea(this, true); // para que no interfiera la bocina en pantalla dispositivos nuevos
        }

       /* public App(string databaseLocation)
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

            DatabaseLocation = databaseLocation;
        }*/

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
