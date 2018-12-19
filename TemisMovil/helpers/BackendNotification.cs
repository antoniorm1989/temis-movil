using Microsoft.Azure.NotificationHubs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace TemisMovil.helpers
{
    public class BackendNotification
    {
        public static async Task<NotificationOutcome> SendNotificationAsync(string message, string installationId)
        {
            // Get the settings for the server project.
            //https://github.com/Krumelur/XamUAzureNotificationHub/commit/288609d5f60f05e532b5ec504d507b47cee8d11d#diff-e0e92162ab005e57ece56b872008c40c

            // The name of the Notification Hub from the overview page.
            string notificationHubName = "TemisNotificationPush";
            // Use "DefaultFullSharedAccessSignature" from the portal's Access Policies.
            string notificationHubConnection = "Endpoint=sb://temisnotificationpush.servicebus.windows.net/;SharedAccessKeyName=DefaultFullSharedAccessSignature;SharedAccessKey=v6viUPjushc3wcxFNHGpNXI0oy6T2q92kTDXgI7dUC4=";
            // Create a new Notification Hub client.
            var hub = NotificationHubClient.CreateClientFromConnectionString(notificationHubConnection, notificationHubName, enableTestSend: true);

            // Sending the message so that all template registrations that contain "messageParam"
            // will receive the notifications. This includes APNS, GCM, WNS, and MPNS template registrations.
            var templateParams = new Dictionary<string, string>
            {
                ["messageParam"] = message
            };

            // Send the push notification and log the results.
            NotificationOutcome result = null;
            if (string.IsNullOrWhiteSpace(installationId))
            {
                result = await hub.SendTemplateNotificationAsync(templateParams).ConfigureAwait(false);
            }
            else
            {
                result = await hub.SendTemplateNotificationAsync(templateParams, "$InstallationId:{" + installationId + "}").ConfigureAwait(false);
            }
            return result;
        }

    }
}
