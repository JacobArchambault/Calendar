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
        internal LocalTime StartTime { get; set; }
        internal LocalTime EndTime { get; set; }
        internal string Location { get; set; }
        internal string Description { get; set; }

    }
}
