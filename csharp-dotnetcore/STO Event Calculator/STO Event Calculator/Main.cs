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

            Prompts Prompt = new Prompts("Enter the date that the event ends: ",
                "Enter the number of hours until dailies reset: ",
                "Enter the number of tokens needed to complete the event: ",
                "Enter the number of tokens you currently have: ",
                "Enter the number of tokens you get on a daily basis: ");

            ExceptionMsgs ExceptMsgs = new ExceptionMsgs("You must enter a date.",
                "You must enter the number of hours until reset.",
                "You must enter the number of tokens needed to complete the event.",
                "You must enter the number of tokens you currently have.",
                "You must enter the number of tokens you get on a daily basis.");

            result.WithParsed(options =>
           {
               if (options.Quiet == true && options.Json == false && options.PrintJSON == false)
               {
                   options.PrintJSON = true;
                   Console.Error.WriteLine("Neither --json or --print-json were specified, activating --print-json\n");
               }
           });

            UseOptions = result.MapResult(options => {
                if (options.DailyTokens == default && 
                (options.EndDate == default || string.IsNullOrEmpty(options.EndDate)) && 
                options.Reset == default &&
                options.TokensClaimed == default &&
                options.TotalTokens == default) { return false; }
                else { return true; }
            }, _ => { return false; }
             );

            DateCalc = result.MapResult(options => { if (UseOptions == true) { return Factory.Create(ref options, in Prompt, in ExceptMsgs); }
                else { return Factory.Create(ref Dates, ref options, in Prompt, in ExceptMsgs); } }, 
                _ => { return DateCalc; });

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
            });
        }
    }
}
