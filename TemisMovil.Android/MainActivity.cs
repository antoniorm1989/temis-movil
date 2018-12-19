using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Icu.Text;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Java.Util;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.CurrentActivity;
using Plugin.Permissions;
using System;
using System.Globalization;
using Xamarin.Forms;

[assembly: Dependency(typeof(TemisMovil.Droid.MainActivity))]
namespace TemisMovil.Droid
{
    [Activity(Label = "TemisMovil", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, IPlatform
    {

        public void SaveEvent(DateTime start, DateTime end, string title, string notes, string location, bool allday)
        {
            var ctx = Android.App.Application.Context;
            var calendarsUri = CalendarContract.Calendars.ContentUri;

            string[] calendarsProjection = {
                CalendarContract.Calendars.InterfaceConsts.Id,
                CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
                CalendarContract.Calendars.InterfaceConsts.AccountName
            };

            var cursor = ctx.ContentResolver.Query(calendarsUri, calendarsProjection, null, null, null);
            
            while (cursor.MoveToNext())
            {
                Console.WriteLine("ID: " + cursor.GetString(cursor.GetColumnIndex(CalendarContract.Events.InterfaceConsts.Id)));
                Console.WriteLine("DisplayName: " + cursor.GetString(cursor.GetColumnIndex(CalendarContract.Events.InterfaceConsts.CalendarDisplayName)));
                Console.WriteLine("AccountName: " + cursor.GetString(cursor.GetColumnIndex(CalendarContract.Events.InterfaceConsts.AccountName)));
                if(cursor.GetString(cursor.GetColumnIndex(CalendarContract.Events.InterfaceConsts.AccountName)) == "antonio.rosales@uabc.edu.mx")
                {
                    var eventValues = new ContentValues();
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, cursor.GetString(cursor.GetColumnIndex(CalendarContract.Events.InterfaceConsts.Id)));
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, title);
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, "This is an event created from Xamarin.Android");
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(2018, 12, 15, 10, 0));
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(2018, 12, 15, 10, 30));
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.EventLocation, location);
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay, allday);
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, "UTC");
                    eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, "UTC");

                    ctx.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);
                    break;
                }
            }
        }
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);

            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            CurrentPlatform.Init();
            CrossCurrentActivity.Current.Init(this, savedInstanceState);

            /*string dbName = "tenant_movil.sqlite";
            string folderPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string fullPath = Path.Combine(folderPath, dbName);*/

            LoadApplication(new App());

            IsPlayServicesAvailable();
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private bool IsPlayServicesAvailable()
        {
            int resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(this);
            if (resultCode != ConnectionResult.Success)
            {
                if (GoogleApiAvailability.Instance.IsUserResolvableError(resultCode))
                {
                    // In a real project you can give the user a chance to fix the issue.
                    Console.WriteLine($"Error: {GoogleApiAvailability.Instance.GetErrorString(resultCode)}");
                }
                else
                {
                    Console.WriteLine("Error: Play services not supported!");
                    Finish();
                }
                return false;
            }
            else
            {
                Console.WriteLine("Play Services available.");
                return true;
            }
        }

        long GetDateTimeMS(int yr, int month, int day, int hr, int min)
        {
            Java.Util.Calendar c = Java.Util.Calendar.GetInstance(Java.Util.TimeZone.Default);

            c.Set(Java.Util.CalendarField.DayOfMonth, day);
            c.Set(Java.Util.CalendarField.HourOfDay, hr);
            c.Set(Java.Util.CalendarField.Minute, min);
            c.Set(Java.Util.CalendarField.Month, month -1);
            c.Set(Java.Util.CalendarField.Year, yr);

            return c.TimeInMillis;
        }
    }
}
