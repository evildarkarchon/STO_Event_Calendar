using System.IO;
using CommandLine;
namespace STO_Event_Calendar
{
    public class Options
    {
        [Option('d', "daily-tokens", HelpText = "Amount of tokens you get per reset.")]
        public uint DailyTokens { get; set; }

    }
}

