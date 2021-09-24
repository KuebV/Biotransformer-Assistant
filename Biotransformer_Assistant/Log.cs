using System;
using System.Collections.Generic;
using System.Text;

namespace Biotransformer_Assistant
{
    public class Log
    {
        public static void Info(string Message)
        {
            Write("\n[Info]: ", ConsoleColor.Cyan);
            Console.Write(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Success(string Message)
        {
            Write("\n[✓]: ", ConsoleColor.Green);
            Console.Write(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Debug(string Message)
        {
            Write("\n[Debug]: ", ConsoleColor.Blue);
            Console.Write(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string Message)
        {
            Write("\n[Error]: ", ConsoleColor.Red);
            Console.Write(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Warning(string Message)
        {
            Write("\n[Warning]: ", ConsoleColor.Yellow);
            Console.Write(Message);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void WriteLine(string text, ConsoleColor? TextColor = null, bool stayColored = false)
        {
            if (text == null) return;
            if (TextColor != null)
            {
                Console.ForegroundColor = TextColor.Value;
            }
            Console.WriteLine(text);

            if (!stayColored)
                Console.ForegroundColor = ConsoleColor.White;

        }

        public static void Write(string text, ConsoleColor? TextColor = null, bool stayColored = false)
        {
            if (text == null) return;
            if (TextColor != null)
            {
                Console.ForegroundColor = TextColor.Value;
            }
            Console.Write(text);

            if (!stayColored)
                Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
