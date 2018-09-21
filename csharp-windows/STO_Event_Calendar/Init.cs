using System;
using CommandLine;
using System.Collections.Generic;

namespace STO_Event_Calendar
{
    struct Date
    {
        public string EndDate;
        public float Reset;
        public uint Needed;
        public uint Tokens;
        public uint Daily;
    }

    class STO_Event_Calendar
    {
        public static void Main(string[] args)
        {
            ParserResult<Options> result = Parser.Default.ParseArguments<Options>(args);
            
            /*
            if (result.Tag == ParserResultType.Parsed)
            {
                var options = ((Parsed<Options>)result).Value;
            }
            else
            {
                var errors = ((NotParsed<Options>)result).Errors;
            }
            */
            
            Calc DateCalc = Comp.CalcDateCalc(ref result);
                       
            DateTime DateNeeded = DateCalc.DateNeeded();
            DateTime FinalDay = DateCalc.FinalDay();
            
            void AnnounceEnd(TimeSpan n)
            {
                if (n.Days >= 1)
                {
                    if (n.Days > 1) { Console.WriteLine("There are {0} days until the event ends.", n.Days); }
                    else if (n.Days == 1) { Console.WriteLine("There is 1 day until the event ends."); }
                }
                else if (n.Days < 1)
                {
                    if (n.Hours >= 1)
                    {
                        if (n.Hours > 1) { Console.WriteLine("There are {0} hours until the event ends", n.Hours); }
                        else if (n.Hours == 1) { Console.WriteLine("There is 1 hour until the event ends."); }
                    }
                    else if (n.Hours < 1)
                    {
                        if (n.Minutes > 1) { Console.WriteLine("There are {0} minutes until the event ends.", n.Minutes); }
                        else if (n.Minutes == 1) { Console.WriteLine("There is only 1 minute until the event ends."); }
                        else if (n.Minutes < 1 && n.Seconds < 1) { Console.WriteLine("The event is over, sorry."); }
                    }
                }
            }
            
            void Announce()
            {
                Console.WriteLine("Todays Date: {0}", DateCalc.Now.ToShortDateString());
                Console.WriteLine("Daily Quests will be available on {0} at {1}", DateCalc.Reset.ToShortDateString(), DateCalc.Reset.ToShortTimeString());
                Console.WriteLine("Estimated date of completion: {0}", DateCalc.DateNeeded().ToShortDateString());
                Console.WriteLine("Days needed to complete the event: {0}", DateCalc.DaysNeeded.Days);
                AnnounceEnd(DateCalc.EndDiff);

                if (FinalDay < DateCalc.Now) { Console.WriteLine("There is no way to complete this event, sorry."); }
                else if (FinalDay.Day == DateCalc.Now.Day) { Console.WriteLine("You have to do dailies every day to be able to get enough tokens."); }
                Console.WriteLine("The last day to start the event is: {0}", FinalDay.ToShortDateString());
                Console.WriteLine("The event ends on {0}", DateCalc.End.ToShortDateString());
            }

            result.WithParsed(options =>
            {
                if (!options.Quiet) { Announce(); }
            }
            );

            //JSONInfo JSON = ConvertJSON.CalcJSONInfo(ref DateCalc);
            Nullable<JSONInfo> JSON = null;
            if (result.Tag == ParserResultType.Parsed)
            {
                if (((Parsed<Options>)result).Value.Json == true || ((Parsed<Options>)result).Value.PrintJSON == true) {
                    JSON = result.MapResult((Options options) => { return ConvertJSON.CalcJSONInfo(ref DateCalc); }, (IEnumerable<CommandLine.Error> err) => { return ConvertJSON.CalcJSONInfo(ref DateCalc); });
                }
            }
            result.WithParsed(options =>
            {
                if (options.Json) { ConvertJSON.OutJSON(ref JSON, DateCalc.OutPath); }
                if (options.PrintJSON) { ConvertJSON.PrintJSON(ref JSON); }
            }
            );
        }
    }
}