#region usings
using AE.Net.Mail;
using poc_reading_mailbox.Utils;
using System;
using System.Linq;
using System.Text;
#endregion usings

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

string Divider = "=======================================================";

try
{
    var _client = Login();
    _client.SelectMailbox("INBOX");
    var emails = _client.GetMessages(0, 300, false);

    Printer.Print($"Comienza la lectura de {_client.GetMessageCount()} mensajes\n\n", string.Empty, ConsoleColor.Yellow);

    var contador = 1;
    foreach (var item in emails.OrderByDescending(x => x.Date))
    {
        Printer.Print($"{Divider}", $"Mail number {contador}", ConsoleColor.Red);
        Printer.Print("Subject", item.Subject);
        Printer.Print("MessageID", item.MessageID);
        Printer.Print("Sender", item.Sender);
        Printer.Print("From", item.From);
        Printer.Print("AlternateViews", item.AlternateViews);
        Printer.Print("Attachment", item.Attachments.Count != 0);
        Printer.Print("ReplyTo", item.ReplyTo);
        Printer.Print("Bcc", item.Bcc);
        Printer.Print("Cc", item.Cc);
        Printer.Print("To", item.To);
        Printer.Print("Importance", item.Importance);
        Printer.Print("Size", item.Size);
        Printer.Print("Flags", Enum.GetName(typeof(Flags), item.Flags));
        Printer.Print("RawFlags", item.RawFlags);
        Printer.Print("Date", $"{item.Date:dd/MM/yyyy - HH:mm}hs");
        Printer.Print("Uid", item.Uid);

        Console.WriteLine("\n\n");
        contador++;
    }

    Console.ReadKey();
}
catch (Exception ex)
{
    Printer.Print($"{ex.Message}", string.Empty, ConsoleColor.DarkRed);
    Console.ReadKey();
    Environment.Exit(0);
}


static ImapClient Login()
{
    Printer.Print("Ingrese su email", string.Empty, ConsoleColor.Cyan);
    Config.Username = Console.ReadLine();
    Printer.Print("Ingrese su contraseña", string.Empty, ConsoleColor.Cyan);
    Config.Password = Console.ReadLine();
    ImapClient client = new(Config.Host, Config.Username, Config.Password, Config.Method, Config.Port, Config.Secure);
    return client;
}