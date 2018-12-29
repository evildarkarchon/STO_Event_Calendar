using System;
using Keyboard;

namespace STO_Event_Calendar
{
    public class STOFactory : AbsSTOFactory
    {
        public override STO Create(ref Date Dates)
        {
            Dates.EndDate = Key.Ask("Enter the date that the event ends: ", "You must enter a date.");

            Dates.Reset = float.Parse(Key.Ask("Enter the number of hours until dailies reset: ", "You must enter the number of hours until reset."));

            if (Dates.Reset == default(float))
            {
                throw new ArgumentException("You must enter the number of hours until reset.");
            }

            Dates.Needed = uint.Parse(Key.Ask("Enter the number of tokens needed to complete the event: ", "You must enter the number of tokens needed to complete the event."));

            if (Dates.Needed == default(uint))
            {
                throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
            }

            Dates.Tokens = uint.Parse(Key.Ask("Enter the number of tokens you currently have: ", "You must enter the number of tokens you currently have."));

            if (Dates.Tokens == default(uint))
            {
                throw new ArgumentException("You must enter the number of tokens you currently have.");
            }

            Dates.Daily = uint.Parse(Key.Ask("Enter the number of tokens you get on a daily basis: ", "You must enter the number of tokens you get on a daily basis."));

            if (Dates.Daily == default(uint))
            {
                throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
            }

            return new STO(ref Dates);
        }

        public override STO Create(ref Options Opts)
        {
            if (string.IsNullOrEmpty(Opts.EndDate))
            {
                Opts.EndDate = Key.Ask("Enter the date that the event ends: ", "You must enter a date.");

                if (string.IsNullOrEmpty(Opts.EndDate))
                {
                    throw new ArgumentException("You must enter the date that the event ends.");
                }
            }

            if (Opts.Reset == default(float))
            {
                Opts.Reset = float.Parse(Key.Ask("Enter the number of hours until dailies reset: ", "You must enter the number of hours until reset."));

                if (Opts.Reset == default(float))
                {
                    throw new ArgumentException("You must enter the number of hours until reset.");
                }
            }

            if (Opts.TokensClaimed == default(uint))
            {
                Opts.TokensClaimed = uint.Parse(Key.Ask("Enter the number of tokens you currently have: ", "You must enter the number of tokens you currently have."));

                if (Opts.TokensClaimed == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you currently have.");
                }
            }

            if (Opts.TotalTokens == default(uint))
            {
                Opts.TotalTokens = uint.Parse(Key.Ask("Enter the number of tokens needed to complete the event: ", "You must enter the number of tokens needed to complete the event"));

                if (Opts.TotalTokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens needed to complete the event.");
                }
            }

            if (Opts.DailyTokens == default(uint))
            {
                Opts.DailyTokens = uint.Parse(Key.Ask("Enter the number of tokens you can claim on a daily basis: ", "You must enter the number of tokens you get on a daily basis."));

                if (Opts.DailyTokens == default(uint))
                {
                    throw new ArgumentException("You must enter the number of tokens you get on a daily basis.");
                }
            }

            return new STO(ref Opts);
        }

        public override STO Create(string end, float reset, uint needed, uint tokens, uint daily)
        {
            return new STO(end, reset, needed, tokens, daily);
        }
    }
}
