using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace STO_Event_Calendar
{
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
            if (JSON != null)
            {
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

            if (o.Reset == 0)
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

            if (o.Json || o.PrintJSON)
            {
                JSON = new JSONInfo
                {
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

        public STO(ref Date dates, ref Options o)
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
            
            Dates = dates;
            
            if (dates.Reset > 0)
            {
                Reset = Now + TimeSpan.FromHours(dates.Reset);
            }
            else
            {
                Reset = Now;
                throw new ArgumentException("Reset time must be greater than 0.");
            }
            

            AllTokens = new Dictionary<string, uint>()
            {
                { "TokensNeeded", dates.Needed },
                { "TokensClaimed", dates.Tokens },
                { "DailyTokens", dates.Daily }
            };

            float _dn = (dates.Needed - dates.Tokens) / dates.Daily;
            DaysNeeded = TimeSpan.FromDays(Math.Ceiling(_dn));
            
            if (o != default(Options))
            {
                OutPath = o.JsonPath;

                if (o.Json || o.PrintJSON)
                {
                    JSON = new JSONInfo
                    {
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
            else { JSON = null; }
        }
    }
}
