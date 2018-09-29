using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using CommandLine;

namespace STO_Event_Calendar
{
    public class Key
    {
        public static string Ask(string message, string exceptionmsg)
        {
            Console.Write(message);
            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output))
            {
                throw new ArgumentException(exceptionmsg);
            }
            else
            {
                return Output;
            }
        }
    }

    public abstract class AbsSTO
    {
        public Date Dates { get; protected set; }

        public DateTime Reset { get; protected set; }
        public TimeSpan DaysNeeded { get; protected set; }
        public TimeSpan EndDiff { get; protected set; }
        public DateTime End { get; protected set; }
        public Dictionary<string, uint> AllTokens { get; protected set; }
        public DateTime Now { get; }
        public string OutPath { get; set; }

        protected DateTime _End;

        protected AbsSTO() { Now = DateTime.Now; }

        public struct JSONInfo
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
        public JSONInfo? JSON { get; protected set; }
        public string JSONOut { get; protected set; }
    }

    public class STO : AbsSTO
    {
        public DateTime DateNeeded()
        {
            return Now + DaysNeeded;
        }

        public DateTime FinalDay()
        {
            DateTime Day = End - DaysNeeded;
            return Day;
        }

        public void WriteJSON()
        {
            if (JSON != null) { File.WriteAllText(OutPath, JsonConvert.SerializeObject(JSON, Formatting.Indented)); }
            else { throw new ArgumentNullException("Somehow WriteJSON got called while the JSON variable was null."); }
        }

        public void PrintJSON()
        {
            if (JSON != null) {
                //string output = JsonConvert.SerializeObject(JSON, Formatting.Indented);
                Console.WriteLine("Here's the raw data (in JSON form):");
                Console.WriteLine(JSONOut);
            }
            else { throw new ArgumentNullException("Somehow PrintJSON got called while the JSON variable was null."); }
        }

        public STO(string end, float reset, uint needed, uint tokens, uint daily)
        {
            if (DateTime.TryParse(end, out _End))
            {
                End = _End;
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
            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", needed },
                { "TokensClaimed", tokens },
                { "DailyTokens", daily }
            };

            float _dn = (needed - tokens) / daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
            JSON = null;
        }

        public STO(ref Options o)
        {
            if (DateTime.TryParse(o.EndDate, out _End))
            {
                End = _End;
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
            
            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", o.TotalTokens },
                { "TokensClaimed", o.TokensClaimed },
                { "DailyTokens", o.DailyTokens }
            };

            float _dn = (o.TotalTokens - o.TokensClaimed) / o.DailyTokens;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));

            OutPath = o.JsonPath;

            if (o.Json || o.PrintJSON) {
                JSON = new JSONInfo {
                    Now = Now,
                    Reset = Reset,
                    DaysNeeded = (uint)DaysNeeded.Days,
                    EndDiff = (uint)EndDiff.Days,
                    End = End,
                    AllTokens = AllTokens,
                    FinalDay = FinalDay(),
                    DateNeeded = DateNeeded()
                };
                JSONOut = JsonConvert.SerializeObject(JSON, Formatting.Indented);
            }
            else { JSON = null; }
        }

        public STO(ref Date dates)
        {
            if (DateTime.TryParse(dates.EndDate, out _End))
            {
                End = _End;
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
            
            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", dates.Needed },
                { "TokensClaimed", dates.Tokens },
                { "DailyTokens", dates.Daily }
            };

            float _dn = (dates.Needed - dates.Tokens) / dates.Daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
            JSON = null;
        }
    }


    public abstract class AbsSTOFactory {
        public abstract STO Create(ref Date Dates);
        public abstract STO Create(ref Options Opts);
        public abstract STO Create(string end, float reset, uint needed, uint tokens, uint daily);
    }

    public class STOFactory : AbsSTOFactory
    {
        public override STO Create(ref Date Dates)
        {
            Dates.EndDate = Key.Ask("Enter the date that the event ends: ", "You must enter a date.");

            Dates.Reset = float.Parse(Key.Ask("Enter the number of hours until dailies reset: ", "You must enter the number of hours until reset."));

            if (Dates.Reset == default(float))
            {
                throw new ArgumentException("You must enter the number of hours until reset.");
            }

            Dates.Needed = uint.Parse(Key.Ask("Enter the number of tokens needed to complete the event: ", "You must enter the number of tokens needed to complete the event."));

            if (Dates.Needed == default(uint))
            {
                throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
            }

            Dates.Tokens = uint.Parse(Key.Ask("Enter the number of tokens you currently have: ", "You must enter the number of tokens you currently have."));

            if (Dates.Tokens == default(uint))
            {
                throw new ArgumentException("You must enter the number of tokens you currently have.");
            }

            Dates.Daily = uint.Parse(Key.Ask("Enter the number of tokens you get on a daily basis: ", "You must enter the number of tokens you get on a daily basis."));

            if (Dates.Daily == default(uint))
            {
                throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
            }

            return new STO(ref Dates);
        }

        public override STO Create(ref Options Opts)
        {
            if (string.IsNullOrEmpty(Opts.EndDate))
            {
                Opts.EndDate = Key.Ask("Enter the date that the event ends: ", "You must enter a date.");

                if (string.IsNullOrEmpty(Opts.EndDate))
                {
                    throw new ArgumentException("You must enter the date that the event ends.");
                }
            }

            if (Opts.Reset == default(float))
            {
                Opts.Reset = float.Parse(Key.Ask("Enter the number of hours until dailies reset: ", "You must enter the number of hours until reset."));

                if (Opts.Reset == default(float))
                {
                    throw new ArgumentException("You must enter the number of hours until reset.");
                }
            }

            if (Opts.TokensClaimed == default(uint))
            {
                Opts.TokensClaimed = uint.Parse(Key.Ask("Enter the number of tokens you currently have: ", "You must enter the number of tokens you currently have."));

                if (Opts.TokensClaimed == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you currently have.");
                }
            }

            if (Opts.TotalTokens == default(uint))
            {
                Opts.TotalTokens = uint.Parse(Key.Ask("Enter the number of tokens needed to complete the event: ", "You must enter the number of tokens needed to complete the event"));

                if (Opts.TotalTokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                }
            }

            if (Opts.DailyTokens == default(uint))
            {
                Opts.DailyTokens = uint.Parse(Key.Ask("Enter the number of tokens you can claim on a daily basis: ", "You must enter the number of tokens you get on a daily basis."));

                if (Opts.DailyTokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                }
            }

            return new STO(ref Opts);
        }

        public override STO Create(string end, float reset, uint needed, uint tokens, uint daily)
        {
            return new STO(end, reset, needed, tokens, daily);
        }
    }
}
