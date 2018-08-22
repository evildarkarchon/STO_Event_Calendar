using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
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
            
            Date Dates;

            Calc CalcDateCalc(bool UseOptions = true)
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

                if (UseOptions == true && Opts != null && string.IsNullOrEmpty(Opts.EndDate))
                {
                    Console.Write("Enter the Date that the event ends: ");
                    Opts.EndDate = Console.ReadLine();

                    if (string.IsNullOrEmpty(Opts.EndDate))
                    {
                        throw new ArgumentException("You must enter a date.");
                    }
                }
                else if (UseOptions == true && Opts != null && Opts.Reset == default(float) && result.Tag == ParserResultType.Parsed)
                {
                    Console.Write("Enter the number of hours until the dailies reset: ");
                    Opts.Reset = float.Parse(Console.ReadLine());

                    if (Opts.Reset == default(float))
                    {
                        throw new ArgumentException("You must enter the number of hours until reset.");
                    }
                }
                else if (UseOptions == true && Opts != null && result.Tag == ParserResultType.Parsed && Opts.TotalTokens == default(uint))
                {
                    Console.Write("Enter the number of tokens needed to complete the event: ");
                    Opts.TotalTokens = uint.Parse(Console.ReadLine());

                    if (Opts.TotalTokens == default(uint))
                    {
                        throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                    }
                }
                else if (UseOptions == true && Opts != null && result.Tag == ParserResultType.Parsed && Opts.TokensClaimed == default(uint))
                {
                    Console.Write("Enter the number of tokens you currently have: ");
                    Opts.TokensClaimed = uint.Parse(Console.ReadLine());

                    if (Opts.TokensClaimed == default(uint))
                    {
                        throw new ArgumentException("You must enter the number of tokens you currently have.");
                    }
                }
                else if (UseOptions == true && Opts != null && result.Tag == ParserResultType.Parsed && Opts.DailyTokens == default(uint))
                {
                    Console.Write("Enter the number of tokens you get on a daily basis: ");
                    Opts.DailyTokens = uint.Parse(Console.ReadLine());

                    if (Opts.DailyTokens == default(uint))
                    {
                        throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                    }
                }

                if (UseOptions == true && Opts != null)
                    {
                    return new Calc(Opts);
                }
                else if (UseOptions == false && result.Tag == ParserResultType.Parsed)
                {
                    Console.Write("Enter the Date that the event ends: ");
                    Dates.EndDate = Console.ReadLine();

                    if (string.IsNullOrEmpty(Dates.EndDate))
                    {
                        throw new ArgumentException("You must enter a date.");
                    }

                    Console.Write("Enter the number of hours until the dailies reset: ");
                    Dates.Reset = float.Parse(Console.ReadLine());

                    if (Dates.Reset == default(float))
                    {
                        throw new ArgumentException("You must enter the number of hours until reset.");
                    }

                    Console.Write("Enter the number of tokens needed to complete the event: ");
                    Dates.Needed = uint.Parse(Console.ReadLine());

                    if (Dates.Needed == default(uint))
                    {
                        throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                    }

                    Console.Write("Enter the number of tokens you currently have: ");
                    Dates.Tokens = uint.Parse(Console.ReadLine());

                    if (Dates.Tokens == default(uint))
                    {
                        throw new ArgumentException("You must enter the number of tokens you currently have.");
                    }

                    Console.Write("Enter the number of tokens you get on a daily basis: ");
                    Dates.Daily = uint.Parse(Console.ReadLine());

                    if (Dates.Daily == default(uint))
                    {
                        throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                    }

                    return new Calc(Dates);
                }
                else
                {
                    //string end, float reset, uint needed, uint tokens, uint daily
                    return new Calc("8/30/2018", 20.0F, 40, 20, 10);
                }
            }

            Calc DateCalc = CalcDateCalc();
                       
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
