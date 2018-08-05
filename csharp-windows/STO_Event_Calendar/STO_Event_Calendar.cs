using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Reflection;
using CommandLine;

namespace STO_Event_Calendar
{
    class Options {
      [Option('d', "daily-tokens")]
      public uint DailyTokens {get; set;}

      [Option('t', "total-tokens")]
      public uint TotalTokens {get; set;}

      [Option('c', "tokens-claimed")]
      public uint TokensClaimed {get; set;}

      [Option('e', "end-date")]
      public string EndDate {get; set;}

      [Option('r', "reset")]
      public float Reset {get; set;}

    }

    class STO_Event_Calendar
    {

        public static void Main(string[] args)
        {
            ParserResult<Options> result = Parser.Default.ParseArguments<Options>(args);
            /*if (result.Tag == ParserResultType.Parsed)
            {
                var options = ((Parsed<Options>)result).Value;
            }
            else
            {
                var errors = ((NotParsed<Options>)result).Errors;
            }*/
#pragma warning disable CS0168 // Variable is declared but never used

            void end(string d)
            {
                DateTimeOffset Now = DateTimeOffset.Now;

                if (DateTimeOffset.TryParse(d, out DateTimeOffset End))
                {
                    TimeSpan EndDiff = End - Now;
                    
                    if (EndDiff.Days > 1)
                    {
                        Console.WriteLine("There are {0} days until the event ends.", EndDiff.Days);
                    }
                    else if (EndDiff.Days == 1)
                    {
                        Console.WriteLine("There is 1 day until the event ends.");
                    }
                    else if (EndDiff.Days < 1 && EndDiff.Hours > 1)
                    {
                        Console.WriteLine("There are {0} hours until the event ends", EndDiff.Hours);
                    }
                    else if (EndDiff.Days < 1 && EndDiff.Hours == 1)
                    {
                        Console.WriteLine("There is 1 hour until the event ends.");
                    }
                    else if (EndDiff.Days < 1 && EndDiff.Hours < 1 && EndDiff.Minutes >= 1)
                    {
                        Console.WriteLine("There are {0} minutes until the event ends.", EndDiff.Minutes);
                    }
                    else if (EndDiff.Days < 1 && EndDiff.Hours < 1 && EndDiff.Minutes == 1)
                    {
                        Console.WriteLine("There is only 1 minute until the event ends.");
                    }
                    else if (EndDiff.Days < 1 && EndDiff.Hours < 1 && EndDiff.Minutes < 1 && EndDiff.Seconds < 1)
                    {
                        Console.WriteLine("The event is over, sorry.");
                    }
                }
                else
                {
                    Console.WriteLine("Unable to parse date value.");
                }
            }

            result.WithParsed<Options>(options => end(options.EndDate)).WithNotParsed(errors => {
                Console.Write("Input the date the event ends: ");
                end(Console.ReadLine());
            } );

#pragma warning restore CS0168 // Variable is declared but never used
        }
    }
}
