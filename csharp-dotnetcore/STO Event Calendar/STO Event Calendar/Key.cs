using System;

namespace Keyboard
{
    class Key
    {
        public static string Ask(string message)
        {
            Console.Write(message);

            return Console.ReadLine();
        }
        public static string Ask(string message, string exceptionmsg)
        {
            Console.Write(message);

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default) { throw new ArgumentException(exceptionmsg); }

            return Output;
        }
        public static string Ask(string message, bool newline)
        {
            if (newline == default) { newline = false; }

            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            return Console.ReadLine();
        }
        public static string Ask(string message, string exceptionmsg, bool newline)
        {
            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default) { throw new ArgumentException(exceptionmsg); }

            return Output;
        }

        public static object AskObj(string message)
        {
            Console.Write(message);

            object Output = Console.ReadLine();

            return Output;
        }
        public static object AskObj(string message, string exceptionmsg)
        {
            Console.Write(message);

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default) { throw new ArgumentException(exceptionmsg); }

            return Output;
        }
        public static object AskObj(string message, bool newline)
        {
            if (newline == default) { newline = false; }

            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            object Output = Console.ReadLine();

            return Output;
        }
        public static object AskObj(string message, string exceptionmsg, bool newline)
        {
            if (newline == default) { newline = false; }

            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default) { throw new ArgumentException(exceptionmsg); }

            return Output;
        }

        public static T Ask<T>(string message)
        {
            Console.Write(message);

            return (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
        }
        public static T Ask<T>(string message, string exceptionmsg)
        {
            Console.Write(message);

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default) { throw new ArgumentException(exceptionmsg); }

            return (T)Convert.ChangeType(Output, typeof(T));
        }
        public static T Ask<T>(string message, bool newline)
        {
            if (newline == default) { newline = false; }

            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            return (T)Convert.ChangeType(Console.ReadLine(), typeof(T));
        }
        public static T Ask<T>(string message, string exceptionmsg, bool newline)
        {
            if (newline == default) { newline = false; }

            if (newline == true) { Console.WriteLine(message); }
            else { Console.Write(message); }

            string Output = Console.ReadLine();

            if (string.IsNullOrEmpty(Output) || Output == default) { throw new ArgumentException(exceptionmsg); }

            return (T)Convert.ChangeType(Output, typeof(T));
        }
    }
}
