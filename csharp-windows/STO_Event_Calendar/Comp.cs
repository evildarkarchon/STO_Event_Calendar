using System;
using CommandLine;

namespace STO_Event_Calendar
{
    class Comp
    {
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
                return new Calc(ref Opts);
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
    }
}