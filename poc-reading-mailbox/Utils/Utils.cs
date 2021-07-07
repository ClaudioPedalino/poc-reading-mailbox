using Newtonsoft.Json;
using System;
using System.Security;

namespace poc_reading_mailbox.Utils
{
    public static class Utils
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

        public static string MaskPasswordString()
        {
            SecureString password = new();
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey(true);
                if (!char.IsControl(keyInfo.KeyChar))
                {
                    password.AppendChar(keyInfo.KeyChar);
                    Console.Write("*");
                }
                else if (keyInfo.Key == ConsoleKey.Backspace && password.Length > 0)
                {
                    password.RemoveAt(password.Length - 1);
                    Console.Write("\b \b");
                }
            } while (keyInfo.Key != ConsoleKey.Enter);
            {
                return new System.Net.NetworkCredential(string.Empty, password).Password;
            }
        }
    }
}