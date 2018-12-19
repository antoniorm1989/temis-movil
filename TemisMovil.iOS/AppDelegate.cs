using System;
using System.IO;
using EventKit;
using Foundation;
using Microsoft.WindowsAzure.MobileServices;
using Syncfusion.SfSchedule.XForms.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(TemisMovil.iOS.AppDelegate))]
namespace TemisMovil.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IPlatform
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.SetFlags("FastRenderers_Experimental");
            global::Xamarin.Forms.Forms.Init();
            CurrentPlatform.Init();

            string dbName = "tenant_movil.sqlite";
            string folderPath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "..", "Library");
            string fullPath = Path.Combine(folderPath, dbName);

            SfScheduleRenderer.Init();

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }

        public void SaveEvent(DateTime start, DateTime end, string title, string notes, string location, bool allday)
        {
            AppCalendar.Current.EventStore.RequestAccess(EKEntityType.Event,
                (bool granted, NSError e) =>
                {
                    if (granted)
                    {
                        EKCalendar[] calendars = AppCalendar.Current.EventStore.GetCalendars(EKEntityType.Event);

                        EKEvent newEvent = EKEvent.FromStore(AppCalendar.Current.EventStore);
                        // set the alarm for 10 minutes from now
                        newEvent.AddAlarm(EKAlarm.FromDate((Foundation.NSDate)DateTime.Now.AddMinutes(10)));
                        // make the event start 20 minutes from now and last 30 minutes
                        newEvent.StartDate = (Foundation.NSDate) DateTime.Now.AddMinutes(20);
                        newEvent.EndDate = (Foundation.NSDate) DateTime.Now.AddMinutes(50);
                        newEvent.Title = "Get outside and do some exercise!";
                        newEvent.Notes = "This is your motivational event to go and do 30 minutes of exercise. Super important. Do this.";

                        newEvent.Calendar = AppCalendar.Current.EventStore.DefaultCalendarForNewEvents;

                        AppCalendar.Current.EventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out e);

                        Console.WriteLine("Event Saved, ID: " + newEvent.CalendarItemIdentifier);
                    }
                });
        }

    }
}
