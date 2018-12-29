using System;

namespace Keyboard
{
    class Key
    {
        public static string Ask(string message, string exceptionmsg = default(string), bool newline = false, bool is_required = true)
        {
            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default(string))
            {
                if (is_required && exceptionmsg != default(string) && !string.IsNullOrEmpty(exceptionmsg)) { throw new ArgumentException(exceptionmsg); }
                else if (is_required && (exceptionmsg == default(string) || string.IsNullOrEmpty(exceptionmsg))) { throw new ArgumentException(exceptionmsg); }
            }

            return Output;
        }
    }
}
