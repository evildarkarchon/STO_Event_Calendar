using System;

namespace STO_Event_Calendar
{
    class Calc
    {
        public TimeSpan EndDiff;
        public DateTimeOffset End, Reset;
        public DateTimeOffset Now = DateTimeOffset.Now;
        public uint Needed, Tokens, Daily;
        private Options o;
        private Date Dates;
        public uint DaysNeeded;

        public DateTimeOffset DateNeeded()
        {
            TimeSpan Days = TimeSpan.FromDays(EndDiff.Days);
            return Now + Days;
        }

        public DateTimeOffset FinalDay()
        {
            TimeSpan Days = TimeSpan.FromDays(DaysNeeded);
            DateTimeOffset Day = End - Days;
            return Day;
        }

        public Calc(string end, float reset, uint needed, uint tokens, uint daily)
        {
            if (DateTimeOffset.TryParse(end, out DateTimeOffset End))
            {
                EndDiff = End - Now;
            }
            else
            {
                throw new FormatException("Unable to parse date.");
            }

            Reset = Now + TimeSpan.FromHours(reset);
            Needed = needed;
            Tokens = tokens;
            Daily = daily;

            float _dn = (needed - tokens) / daily;
            DaysNeeded = (uint)TimeSpan.FromDays(Math.Ceiling(_dn)).Days;
        }

        public Calc(Options o)
        {
            if (DateTimeOffset.TryParse(o.EndDate, out DateTimeOffset End))
            {
                EndDiff = End - Now;
            }
            else
            {
                throw new FormatException("Unable to parse date.");
            }

            Reset = Now + TimeSpan.FromHours(o.Reset);

            float _dn = (o.TotalTokens - o.TokensClaimed) / o.DailyTokens;
            DaysNeeded = (uint)TimeSpan.FromDays(Math.Ceiling(_dn)).Days;
        }

        public Calc(Date dates)
        {
            if (DateTimeOffset.TryParse(dates.EndDate, out DateTimeOffset End))
            {
                EndDiff = End - Now;
            }
            else
            {
                throw new FormatException("Unable to parse date.");
            }

            Reset = Now + TimeSpan.FromHours(dates.Reset);
            Dates = dates;

            float _dn = (dates.Needed - dates.Tokens) / dates.Daily;
            DaysNeeded = (uint)TimeSpan.FromDays(Math.Ceiling(_dn)).Days;
        }
    }
}