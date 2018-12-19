using System;
using Android.App;
using Firebase.Iid;
using Android.Util;
using System.Threading.Tasks;
using System.Diagnostics;
using Newtonsoft.Json.Linq;
using Microsoft.WindowsAzure.MobileServices;

namespace TemisMovil.Droid
{
    [Service]
    [IntentFilter(new[] { "com.google.firebase.INSTANCE_ID_EVENT" })]
    public class MyFirebaseIIDService : FirebaseInstanceIdService
    {
        const string TAG = "MyFirebaseIIDService";
        public override async void OnTokenRefresh()
        {
            var refreshedToken = FirebaseInstanceId.Instance.Token;
            Log.Debug(TAG, "Refreshed token: " + refreshedToken);
            await SendRegistrationToServerAsync(refreshedToken);
        }

        private async Task SendRegistrationToServerAsync(string token)
        {
            try
            {
                // Formats: https://firebase.google.com/docs/cloud-messaging/concept-options
                // The "notification" format will automatically displayed in the notification center if the
                // app is not in the foreground.
                const string templateBodyFCM =
                "{" +
                "\"notification\" : {" +
                "\"body\" : \"$(messageParam)\"," +
                "\"title\" : \"Temis Legal\"," +
                "\"icon\" : \"myicon\" }" +
                "}";

                var templates = new JObject
                {
                    ["genericMessage"] = new JObject
                    {
                        {
                            "body", templateBodyFCM
                        }
                    }
                };

                var push = TemisMovil.App.MobileService.GetPush();
                await push.RegisterAsync(token, templates);
                // Push object contains installation ID afterwards.
                Log.Debug(TAG, push.InstallationId.ToString());
            }
            catch (Exception ex)
            {
                Log.Debug(TAG, ex.Message);
                Debugger.Break();
            }
        }
    }
}