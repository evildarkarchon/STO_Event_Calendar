using System;
using System.Collections.Generic;

namespace STO_Event_Calendar
{
    public abstract class AbsSTO
    {
        public Date Dates { get; protected set; }

        public DateTime Reset { get; protected set; }
        
    }
}