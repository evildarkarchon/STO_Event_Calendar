using System;
using CommandLine;

namespace STO_Event_Calendar
{
    class Comp
    {
        static string Ask(string message, string exceptionmsg)
        {
            Console.Write(message);
            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output))
            {
                throw new ArgumentException(exceptionmsg);
            }
            else
            {
                return Output;
            }
        }

        public static Calc CalcDateCalc(ref ParserResult<Options> result, bool UseOptions = true)
        {
            Date Dates;

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
                //Console.Write("Enter the Date that the event ends: ");
                //Opts.EndDate = Console.ReadLine();

                //if (string.IsNullOrEmpty(Opts.EndDate))
                //{
                //    throw new ArgumentException("You must enter a date.");
                //}
                Opts.EndDate = Ask("Enter the date that the event ends: ", "You must enter a date.");
            }
            else if (UseOptions == true && Opts != null && Opts.Reset == default(float) && result.Tag == ParserResultType.Parsed)
            {
                //Console.Write("Enter the number of hours until the dailies reset: ");
                //Opts.Reset = float.Parse(Console.ReadLine());

                Opts.Reset = float.Parse(Ask("Enter the number of hours until dailies reset: ", "You must enter the number of hours until reset."));

                if (Opts.Reset == default(float))
                {
                    throw new ArgumentException("You must enter the number of hours until reset.");
                }
            }
            else if (UseOptions == true && Opts != null && result.Tag == ParserResultType.Parsed && Opts.TotalTokens == default(uint))
            {
                //Console.Write("Enter the number of tokens needed to complete the event: ");
                //Opts.TotalTokens = uint.Parse(Console.ReadLine());

                Opts.TotalTokens = uint.Parse(Ask("Enter the number of tokens needed to complete the event: ", "You must enter the number of tokens needed to complete the event"));

                if (Opts.TotalTokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                }
            }
            else if (UseOptions == true && Opts != null && result.Tag == ParserResultType.Parsed && Opts.TokensClaimed == default(uint))
            {
                //Console.Write("Enter the number of tokens you currently have: ");
                //Opts.TokensClaimed = uint.Parse(Console.ReadLine());

                Opts.TokensClaimed = uint.Parse(Ask("Enter the number of tokens you currently have: ", "You must enter the number of tokens you currently have."));

                if (Opts.TokensClaimed == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you currently have.");
                }
            }
            else if (UseOptions == true && Opts != null && result.Tag == ParserResultType.Parsed && Opts.DailyTokens == default(uint))
            {
                //Console.Write("Enter the number of tokens you get on a daily basis: ");
                //Opts.DailyTokens = uint.Parse(Console.ReadLine());

                Opts.DailyTokens = uint.Parse(Ask("Enter the number of tokens you can claim on a daily basis: ", "You must enter the number of tokens you get on a daily basis."));

                if (Opts.DailyTokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                }
            }

            if (UseOptions == true && Opts != null)
            {
                return new Calc(ref Opts);
            }
            else if (UseOptions == false && result.Tag == ParserResultType.Parsed)
            {
                Dates.EndDate = Ask("Enter the date that the event ends: ", "You must enter a date.");
                
                Dates.Reset = float.Parse(Ask("Enter the number of hours until dailies reset: ", "You must enter the number of hours until reset."));

                if (Dates.Reset == default(float))
                {
                    throw new ArgumentException("You must enter the number of hours until reset.");
                }

                Dates.Needed = uint.Parse(Ask("Enter the number of tokens needed to complete the event: ", "You must enter the number of tokens needed to complete the event."));

                if (Dates.Needed == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                }
                
                Dates.Tokens = uint.Parse(Ask("Enter the number of tokens you currently have: ", "You must enter the number of tokens you currently have."));

                if (Dates.Tokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you currently have.");
                }
                
                Dates.Daily = uint.Parse(Ask("Enter the number of tokens you get on a daily basis: ", "You must enter the number of tokens you get on a daily basis."));

                if (Dates.Daily == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                }

                return new Calc(ref Dates);
            }
            else
            {
                //string end, float reset, uint needed, uint tokens, uint daily
                //This section does absolutely nothing except for satiating IntelliSense.
                return new Calc((DateTime.Now + TimeSpan.FromDays(30)).ToString(), 20.0F, 40, 20, 10);
            }
        }
    }
}