using System;
using CommandLine;

namespace STO_Event_Calendar
{
    public struct Date
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
            STOFactory Factory = new STOFactory();
            Date Dates = default(Date);
            STO DateCalc = default(STO);
            bool UseOptions = default(bool);
            Options Opts = default(Options);

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

            if (UseOptions == true)
            {
                DateCalc = Factory.Create(ref Opts);
            }
            else if (UseOptions == false)
            {
                DateCalc = Factory.Create(ref Dates);
            }

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