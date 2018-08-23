using CommandLine;

namespace STO_Event_Calendar
{
    class Options
    {
        [Option('d', "daily-tokens", HelpText = "Amount of tokens you get per reset.")]
        public uint DailyTokens { get; set; }

        [Option('t', "total-tokens", HelpText = "Amount of tokens needed to get the rewards.")]
        public uint TotalTokens { get; set; }

        [Option('c', "tokens-claimed", HelpText = "Amount of tokens you've already claimed.")]
        public uint TokensClaimed { get; set; }

        [Option('e', "end-date", HelpText = "Date that the event ends.")]
        public string EndDate { get; set; }

        [Option('r', "reset", HelpText = "Amount of hours until the daily quests reset.")]
        public float Reset { get; set; }

        [Option('j', "json", HelpText = "Save data as a json file.")] // This feature probably has no real-world use, just implimenting this for fun.
        public bool Json { get; set; }
    }
}