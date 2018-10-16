using System;
using System.Collections.Generic;
using STO_Event_Calendar;

namespace STO_Event_Calendar_Old
{
    class Calc
    {
        private DateTime _End;
        private Date Dates;

        public DateTime Reset { get; }
        public TimeSpan DaysNeeded { get; }
        public TimeSpan EndDiff { get; }
        public DateTime End { get { return _End; } }
        public Dictionary<string, uint> AllTokens { get; }
        public DateTime Now { get; } = DateTime.Now;
        public string OutPath { get; set; }

        public DateTime DateNeeded()
        {
            return Now + DaysNeeded;
        }

        public DateTime FinalDay()
        {
            DateTime Day = _End - DaysNeeded;
            return Day;
        }

        public Calc(string end, float reset, uint needed, uint tokens, uint daily)
        {
            if (DateTime.TryParse(end, out _End))
            {
                EndDiff = _End - Now;
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
            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", needed },
                { "TokensClaimed", tokens },
                { "DailyTokens", daily }
            };

            float _dn = (needed - tokens) / daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
        }

        public Calc(ref Options o)
        {
            if (DateTime.TryParse(o.EndDate, out _End))
            {
                EndDiff = _End - Now;
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

            //AllTokens["TokensNeeded"] = o.TotalTokens;
            //AllTokens["TokensClaimed"] = o.TokensClaimed;
            //AllTokens["DailyTokens"] = o.DailyTokens;
            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", o.TotalTokens },
                { "TokensClaimed", o.TokensClaimed },
                { "DailyTokens", o.DailyTokens }
            };

            float _dn = (o.TotalTokens - o.TokensClaimed) / o.DailyTokens;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
            OutPath = o.JsonPath;
        }

        public Calc(ref Date dates)
        {
            if (DateTime.TryParse(dates.EndDate, out _End))
            {
                EndDiff = _End - Now;
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

            //AllTokens["TokensNeeded"] = dates.Needed;
            //AllTokens["TokensClaimed"] = dates.Tokens;
            //AllTokens["DailyTokens"] = dates.Daily;
            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", dates.Needed },
                { "TokensClaimed", dates.Tokens },
                { "DailyTokens", dates.Daily }
            };

            float _dn = (dates.Needed - dates.Tokens) / dates.Daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
        }
    }
}