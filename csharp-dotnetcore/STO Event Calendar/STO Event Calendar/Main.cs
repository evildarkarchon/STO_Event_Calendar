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
            STO DateCalc = default(STO);
            bool UseOptions = new bool();
            Options Opts = new Options();

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
                DateCalc = Factory.Create(ref Dates);
            }
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
