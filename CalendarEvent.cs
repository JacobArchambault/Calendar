using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;
using NodaTime;
namespace Calendar
{
    internal class CalendarEvent
    {
        internal string Name { get; set; }
        internal string StartTime { get; set; }
        internal string EndTime { get; set; }
        internal string Location { get; set; }
        internal string Description { get; set; }

    }
}
