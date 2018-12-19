using Android.App;
using Firebase.Messaging;
using System;
using Xamarin.Forms;

namespace TemisMovil.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.MESSAGING_EVENT" })]
    public class MyFirebaseMessagingService : FirebaseMessagingService
    {
        public override void OnMessageReceived(RemoteMessage message)
        {
            base.OnMessageReceived(message);
            Console.WriteLine("Received: " + message);
            // Android supports different message payloads. To use the code below it must be something like this (you can paste this into Azure test send window):
            // {
            // "notification" : {
            // "body" : "The body",
            // "title" : "The title",
            // "icon" : "myicon
            // }
            // }
            try
            {
                var msg = message.GetNotification().Body;
                MessagingCenter.Send<object, string>(this, TemisMovil.App.NotificationReceivedKey, msg);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error extracting message: " + ex);
            }
        }
    }
}