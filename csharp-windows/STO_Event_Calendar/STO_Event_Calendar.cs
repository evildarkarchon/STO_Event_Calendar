using System;
using CommandLine;

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
#pragma warning disable CS0168 // Variable is declared but never used
#pragma warning disable CS8321 // Local function is declared but never used
            
            Calc DateCalc = Comp.CalcDateCalc(result);
                       
            DateTime DateNeeded = DateCalc.DateNeeded();
            DateTime FinalDay = DateCalc.FinalDay();
            
            void AnnounceEnd(TimeSpan n)
            {          
                if (n.Days > 1)
                {
                    Console.WriteLine("There are {0} days until the event ends.", n.Days);
                }
                else if (n.Days == 1)
                {
                    Console.WriteLine("There is 1 day until the event ends.");
                }
                else if (n.Days < 1 && n.Hours > 1)
                {
                    Console.WriteLine("There are {0} hours until the event ends", n.Hours);
                }
                else if (n.Days < 1 && n.Hours == 1)
                {
                    Console.WriteLine("There is 1 hour until the event ends.");
                }
                else if (n.Days < 1 && n.Hours < 1 && n.Minutes >= 1)
                {
                    Console.WriteLine("There are {0} minutes until the event ends.", n.Minutes);
                }
                else if (n.Days < 1 && n.Hours < 1 && n.Minutes == 1)
                {
                    Console.WriteLine("There is only 1 minute until the event ends.");
                }
                else if (n.Days < 1 && n.Hours < 1 && n.Minutes < 1 && n.Seconds < 1)
                {
                    Console.WriteLine("The event is over, sorry.");
                }
            }

            void Announce()
            {
                Console.WriteLine("Todays Date: {0}", DateTime.Now.ToShortDateString());
                Console.WriteLine("Daily Quests will be available on {0} at {1}", DateCalc.Reset.ToShortDateString(), DateCalc.Reset.ToShortTimeString());
                Console.WriteLine("Estimated date of completion: {0}", DateCalc.DateNeeded().ToShortDateString());
                Console.WriteLine("Days needed to complete the event: {0}", DateCalc.DaysNeeded.Days);
                AnnounceEnd(DateCalc.EndDiff);

                if (FinalDay < DateTime.Now)
                {
                    Console.WriteLine("There is no way to complete this event, sorry.");
                }
                else if (FinalDay.Day == DateTime.Now.Day)
                {
                    Console.WriteLine("You have to do dailies every day to be able to get enough tokens to finish the event.");
                }

                Console.WriteLine("The last day to start the event is: {0}", FinalDay.ToShortDateString());
                Console.WriteLine("The event ends on {0}", DateCalc.End.ToShortDateString());
            }

            result.WithParsed<Options>(options => Announce());

#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore CS8321 // Local function is declared but never used
        }
    }
}
