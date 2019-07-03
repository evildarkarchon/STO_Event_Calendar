using System;
using Keyboard;

namespace STO_Event_Calendar
{
    public class STOFactory
    {
        public STO Create(ref Date Dates, ref Options Opts, in Prompts Prompt, in ExceptionMsgs ExceptMsgs)
        {
            Dates.EndDate = Key.Ask(Prompt.EndDate, ExceptMsgs.EndDate);

            Dates.Reset = Key.Ask<float>(Prompt.Reset, ExceptMsgs.EndDate);

            Dates.Needed = Key.Ask<uint>(Prompt.Needed, ExceptMsgs.Needed);

            Dates.Tokens = Key.Ask<uint>(Prompt.Tokens, ExceptMsgs.Tokens);

            Dates.Daily = Key.Ask<uint>(Prompt.Daily, ExceptMsgs.Daily);

            return new STO(ref Dates, ref Opts);
        }

        public STO Create(ref Options Opts, in Prompts Prompt, in ExceptionMsgs ExceptMsgs)
        {
            if (string.IsNullOrEmpty(Opts.EndDate) || Opts.EndDate == default)
            {
                Opts.EndDate = Key.Ask(Prompt.EndDate, ExceptMsgs.EndDate);
            }

            if (Opts.Reset == default(float))
            {
                Opts.Reset = Key.Ask<float>(Prompt.Reset, ExceptMsgs.Reset);
            }

            if (Opts.TokensClaimed == default(uint))
            {
                Opts.TokensClaimed = Key.Ask<uint>(Prompt.Tokens, ExceptMsgs.Tokens);
            }

            if (Opts.TotalTokens == default(uint))
            {
                Opts.TotalTokens = Key.Ask<uint>(Prompt.Needed, ExceptMsgs.Needed);
            }
            if (Opts.DailyTokens == default(uint))
            {
                Opts.DailyTokens = Key.Ask<uint>(Prompt.Daily, ExceptMsgs.Daily);
            }

            return new STO(ref Opts);
        }

        public STO Create(string end, float reset, uint needed, uint tokens, uint daily)
        {
            return new STO(end, reset, needed, tokens, daily);
        }
        
    }
}
