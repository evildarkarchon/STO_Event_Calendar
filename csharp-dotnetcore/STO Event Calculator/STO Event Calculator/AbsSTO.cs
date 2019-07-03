using System;
using System.Collections.Generic;

namespace STO_Event_Calendar
{
    public abstract class AbsSTO
    {
        public Date Dates { get; set; }

        public DateTime Reset { get; protected set; }
        public TimeSpan DaysNeeded { get; protected set; }
        public TimeSpan EndDiff { get; protected set; }
        public DateTime End { get; protected set; }
        public Dictionary<string, uint> AllTokens { get; protected set; }
        public DateTime Now { get; }
        public string OutPath { get; set; }

        protected DateTime _End;

        protected AbsSTO() { Now = DateTime.Now; }

        protected struct JSONInfo
        {
            public DateTime Now { get; set; }
            public DateTime Reset { get; set; }
            public uint DaysNeeded { get; set; }
            public uint EndDiff { get; set; }
            public DateTime End { get; set; }
            public Dictionary<string, uint> AllTokens { get; set; }
            public DateTime FinalDay { get; set; }
            public DateTime DateNeeded { get; set; }
        }
        protected JSONInfo? JSON { get; set; }
        protected string JSONOut { get; set; }
    }
    /*
    public abstract class AbsSTOFactory
    {
        public abstract STO Create(ref Date Dates, ref Options Opts);
        public abstract STO Create(ref Options Opts);
        public abstract STO Create(string end, float reset, uint needed, uint tokens, uint daily);
    }
    */
}