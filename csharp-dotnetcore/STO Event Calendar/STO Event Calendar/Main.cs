using System;
using CommandLine;

namespace STO_Event_Calendar
{
    class STO_Event_Calendar
    {
        static void Main(string[] args)
        {
            ParserResult<Options> result = Parser.Default.ParseArguments<Options>(args);
            STOFactory Factory = new STOFactory();
            Date Dates = new Date();
            DateTime End = DateTime.Now + TimeSpan.FromDays(20);
            STO DateCalc = Factory.Create(End.ToString(), 20.0f, 20u, 20u, 10u);
            bool UseOptions = new bool();
            //Options Opts = new Options();

            /*
            if (result.Tag == ParserResultType.Parsed) {
                if (Opts.Quiet == true && Opts.Json == false && Opts.PrintJSON == false) {
                    Opts.PrintJSON = true;
                    Console.Error.WriteLine("Neither --json or --print-json were specified, activating --print-json\n");
                }
            }
            */

            result.WithParsed(options =>
           {
               if (options.Quiet == true && options.Json == false && options.PrintJSON == false)
               {
                   options.PrintJSON = true;
                   Console.Error.WriteLine("Neither --json or --print-json were specified, activating --print-json\n");
               }
           });

            UseOptions = result.MapResult(options => {
                if (options.DailyTokens == default(uint) && 
                (options.EndDate == default(string) || string.IsNullOrEmpty(options.EndDate)) && 
                options.Reset == default(float) &&
                options.TokensClaimed == default(uint) &&
                options.TotalTokens == default(uint)) { return false; }
                else { return true; }
            }, _ => { return false; }
             );

            DateCalc = result.MapResult(options => { if (UseOptions == true) { return Factory.Create(ref options); }
                else { return Factory.Create(ref Dates, ref options); } }, 
                _ => { return DateCalc; });

            /*
            if (result.Tag == ParserResultType.Parsed)
            {
                Opts = ((Parsed<Options>)result).Value;
            }

            if (result.Tag == ParserResultType.Parsed &&
                Opts.DailyTokens == default(uint) &&
                (string.IsNullOrEmpty(Opts.EndDate) || Opts.EndDate == default(string)) &&
                Opts.Reset == default(float) &&
                Opts.TokensClaimed == default(uint) &&
                Opts.TotalTokens == default(uint))
            {
                UseOptions = false;
            }
            else if (result.Tag == ParserResultType.Parsed)
            {
                UseOptions = true;
            }

            if (UseOptions == true && result.Tag == ParserResultType.Parsed)
            {
                DateCalc = Factory.Create(ref Opts);
            }
            else if (UseOptions == false && result.Tag == ParserResultType.Parsed)
            {
                DateCalc = Factory.Create(ref Dates, ref Opts);
            }
            */

            if (DateCalc != default(STO))
            {
                DateTime DateNeeded = DateCalc.DateNeeded();
                DateTime FinalDay = DateCalc.FinalDay();

                result.WithParsed(options =>
                {
                    if (!options.Quiet)
                    {
                        Print.Announce(DateCalc, FinalDay);
                        Print.AnnounceEnd(DateCalc.EndDiff);
                    }
                    if (options.Json) { DateCalc.WriteJSON(); }
                    if (options.PrintJSON) { DateCalc.PrintJSON(); }
                }
                );
            }
            
        }
    }
}
