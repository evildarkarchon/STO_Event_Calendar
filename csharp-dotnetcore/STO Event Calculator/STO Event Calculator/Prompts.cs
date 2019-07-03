using System;
using System.Collections.Generic;
using System.Text;

namespace STO_Event_Calendar
{
    /*
    public struct Prompts
    {
       public string EndDate;
       public string Reset;
       public string Needed;
       public string Tokens;
       public string Daily;
    }

    public struct ExceptionMsgs
    {
       public string EndDate;
       public string Reset;
       public string Needed;
       public string Tokens;
       public string Daily;
    }
    */
    public readonly struct Prompts
    {
        public string EndDate { get; }
        public string Reset { get; }
        public string Needed { get; }
        public string Tokens { get; }
        public string Daily { get; }

        public Prompts(string ED, string R, string N, string T, string D)
        {
            EndDate = ED;
            Reset = R;
            Needed = N;
            Tokens = T;
            Daily = D;
        }
    }

    public readonly struct ExceptionMsgs
    {
        public string EndDate { get; }
        public string Reset { get; }
        public string Needed { get; }
        public string Tokens { get; }
        public string Daily { get; }

        public ExceptionMsgs(string ED, string R, string N, string T, string D)
        {
            EndDate = ED;
            Reset = R;
            Needed = N;
            Tokens = T;
            Daily = D;
        }
    }
}
