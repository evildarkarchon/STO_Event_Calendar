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

            Calc DateCalc;

            Date Dates;

            if (result.Tag == ParserResultType.Parsed)
            {
                DateCalc = new Calc(((Parsed<Options>)result).Value);
            }
            else
            {
                Console.Write("Enter the Date that the event ends: ");
                Dates.EndDate = Console.ReadLine();

                if (String.IsNullOrEmpty(Dates.EndDate))
                {
                    throw new ArgumentException("You must enter a date.");
                }

                Console.Write("Enter the number of hours until the dailies reset: ");
                Dates.Reset = float.Parse(Console.ReadLine());

                if (String.IsNullOrEmpty(Dates.Reset.ToString()))
                {
                    throw new ArgumentException("You must enter the number of hours until reset.");
                }

                Console.Write("Enter the number of tokens needed to complete the event: ");
                Dates.Needed = uint.Parse(Console.ReadLine());

                if (String.IsNullOrEmpty(Dates.Needed.ToString()))
                {
                    throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                }

                Console.Write("Enter the number of tokens you currently have: ");
                Dates.Tokens = uint.Parse(Console.ReadLine());

                if (String.IsNullOrEmpty(Dates.Tokens.ToString()))
                {
                    throw new ArgumentException("You must enter the number of tokens you currently have.");
                }

                Console.Write("Enter the number of tokens you get on a daily basis: ");
                Dates.Daily = uint.Parse(Console.ReadLine());

                if (String.IsNullOrEmpty(Dates.Daily.ToString()))
                {
                    throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                }

                DateCalc = new Calc(Dates);
            }

            
            void end(TimeSpan n)
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

            end(DateCalc.EndDiff);

#pragma warning restore CS0168 // Variable is declared but never used
#pragma warning restore CS8321 // Local function is declared but never used
        }
    }
}
