using System;
using CommandLine;

namespace STO_Event_Calendar
{
    class Options
    {
        [Option('d', "daily-tokens")]
        public uint DailyTokens { get; set; }

        [Option('t', "total-tokens")]
        public uint TotalTokens { get; set; }

        [Option('c', "tokens-claimed")]
        public uint TokensClaimed { get; set; }

        [Option('e', "end-date")]
        public string EndDate { get; set; }

        [Option('r', "reset")]
        public float Reset { get; set; }

    }
}