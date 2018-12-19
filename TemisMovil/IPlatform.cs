using System;

namespace TemisMovil
{
    public interface IPlatform
    {
        void SaveEvent(DateTime start, DateTime end, string title, string notes, string location, bool allday);
    }
}
