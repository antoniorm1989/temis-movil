using EventKit;

namespace TemisMovil.iOS
{
    // <summary>
    /// Singleton class for Application wide objects. In this sample, we use it to
    /// maintain a single instance of the EKEventStore.
    /// </summary>
    public class AppCalendar
    {
        public static AppCalendar Current
        {
            get { return current; }
        }
        private static readonly AppCalendar current;

        public EKEventStore EventStore
        {
            get { return eventStore; }
        }
        protected EKEventStore eventStore;

        static AppCalendar()
        {
            current = new AppCalendar();
        }
        protected AppCalendar()
        {
            eventStore = new EKEventStore();
        }
    }
}