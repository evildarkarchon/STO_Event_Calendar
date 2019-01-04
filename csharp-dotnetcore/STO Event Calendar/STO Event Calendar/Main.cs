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

            Prompts Prompt = new Prompts()
            {
                EndDate = "Enter the date that the event ends: ",
                Reset = "Enter the number of hours until dailies reset: ",
                Needed = "Enter the number of tokens needed to complete the event: ",
                Tokens = "Enter the number of tokens you currently have: ",
                Daily = "Enter the number of tokens you get on a daily basis: "
            };

            ExceptionMsgs ExceptMsgs = new ExceptionMsgs()
            {
                EndDate = "You must enter a date.",
                Reset = "You must enter the number of hours until reset.",
                Needed = "You must enter the number of tokens needed to complete the event.",
                Tokens = "You must enter the number of tokens you currently have.",
                Daily = "You must enter the number of tokens you get on a daily basis."
            };

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

            DateCalc = result.MapResult(options => { if (UseOptions == true) { return Factory.Create(ref options, in Prompt, in ExceptMsgs); }
                else { return Factory.Create(ref Dates, ref options, in Prompt, in ExceptMsgs); } }, 
                _ => { return DateCalc; });

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
