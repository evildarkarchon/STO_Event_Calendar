using System;
using Newtonsoft.Json;
using CommandLine;

namespace STO_Event_Calendar
{
    class ConvertJSON
    {
        public static void OutJSON(ref ParserResult<Options> result, Calc DateCalc, bool UseOptions = true)
        {
            Options Opts = null;
            if (result.Tag == ParserResultType.Parsed)
            {
                Opts = ((Parsed<Options>)result).Value;
            }
            else
            {
                UseOptions = false;
            }

            if (UseOptions == true &&
                Opts.DailyTokens == default(uint) &&
                string.IsNullOrEmpty(Opts.EndDate) &&
                Opts.Reset == default(float) &&
                Opts.TokensClaimed == default(uint) &&
                Opts.TotalTokens == default(uint)) { UseOptions = false; }
        }
    }
}