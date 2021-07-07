using Newtonsoft.Json;
using System;

namespace poc_reading_mailbox.Utils
{
    public static class Printer
    {
        public static void Print(string key, object value, ConsoleColor color = ConsoleColor.Green)
        {
            try
            {
                Console.ForegroundColor = color;
                Console.Write($"{key}:  ");
                Console.ResetColor();

                if (value?.GetType().Name == "String")
                {
                    Console.Write($"{value}\n");
                }
                else
                {
                    var data = JsonConvert.SerializeObject(value, Formatting.Indented);
                    Console.Write($"{data}\n");
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write($"  ERROR trying to print {key} value  \n");
                Console.ResetColor();
            }
        }
    }
}