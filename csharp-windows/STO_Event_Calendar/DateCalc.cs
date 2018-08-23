using System;

namespace STO_Event_Calendar
{
    class Calc
    {
        public TimeSpan EndDiff;
#pragma warning disable CS0649
        public DateTime End, Reset;
        public DateTime Now = DateTime.Now;
        public uint Needed, Tokens, Daily;
        private Date Dates;
        public TimeSpan DaysNeeded;

        public DateTime DateNeeded()
        {
            return Now + DaysNeeded;
        }

        public DateTime FinalDay()
        {
            DateTime Day = End - DaysNeeded;
            return Day;
        }

        public Calc(string end, float reset, uint needed, uint tokens, uint daily)
        {
            if (DateTime.TryParse(end, out End))
            {
                EndDiff = End - Now;
            }
            else
            {
                throw new FormatException("Unable to parse date.");
            }
            if (reset > 0)
            {
                Reset = Now + TimeSpan.FromHours(reset);
            }
            else
            {
                Reset = Now;
                throw new ArgumentException("Reset time must be greater than 0.");
            }
            Needed = needed;
            Tokens = tokens;
            Daily = daily;

            float _dn = (needed - tokens) / daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
        }

        public Calc(ref Options o)
        {
            if (DateTime.TryParse(o.EndDate, out End))
            {
                EndDiff = End - Now;
            }
            else
            {
                throw new FormatException("Unable to parse date.");
            }
            Reset = Now + TimeSpan.FromHours(o.Reset);

            if (o.Reset > 0)
            {
                Reset = Now + TimeSpan.FromHours(o.Reset);
            }
            else
            {
                Reset = Now;
                throw new ArgumentException("Reset time must be greater than 0.");
            }

            float _dn = (o.TotalTokens - o.TokensClaimed) / o.DailyTokens;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
        }

        public Calc(ref Date dates)
        {
            if (DateTime.TryParse(dates.EndDate, out End))
            {
                EndDiff = End - Now;
            }
            else
            {
                throw new FormatException("Unable to parse date.");
            }

            if (Dates.Reset > 0)
            {
                Reset = Now + TimeSpan.FromHours(dates.Reset);
            }
            else
            {
                Reset = Now;
                throw new ArgumentException("Reset time must be greater than 0.");
            }
            Dates = dates;

            float _dn = (dates.Needed - dates.Tokens) / dates.Daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
        }
    }
}