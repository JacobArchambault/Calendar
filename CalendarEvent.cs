using System;
using System.Collections.Generic;
using System.DirectoryServices;
using System.Text;

namespace Calendar
{
    internal class CalendarEvent
    {
        internal string Name { get; set; }
        internal DateTime StartTime { get; set; }
        internal DateTime EndTime { get; set; }
        internal string Location { get; set; }
        internal string Description { get; set; }

    }
}
