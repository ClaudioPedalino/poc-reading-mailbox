#region usings
using AE.Net.Mail;
using poc_reading_mailbox.Utils;
using System;
using System.Linq;
using System.Security;
using System.Text;
#endregion usings

Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

string Divider = "=======================================================";

try
{
    var _client = Login();
    _client.SelectMailbox("INBOX");
    var emails = _client.GetMessages(0, 300, false);

    Utils.Print($"\n{Divider}{Divider}\nComienza la lectura de {_client.GetMessageCount()} mensajes\n\n", string.Empty, ConsoleColor.Yellow);

    var contador = 1;
    foreach (var item in emails.OrderByDescending(x => x.Date))
    {
        Utils.Print($"{Divider}", $"Mail number {contador}", ConsoleColor.Red);
        Utils.Print("Subject", item.Subject);
        Utils.Print("MessageID", item.MessageID);
        Utils.Print("Sender", item.Sender);
        Utils.Print("From", item.From);
        Utils.Print("AlternateViews", item.AlternateViews);
        Utils.Print("Attachment", item.Attachments.Count != 0);
        Utils.Print("ReplyTo", item.ReplyTo);
        Utils.Print("Bcc", item.Bcc);
        Utils.Print("Cc", item.Cc);
        Utils.Print("To", item.To);
        Utils.Print("Importance", item.Importance);
        Utils.Print("Size", item.Size);
        Utils.Print("Flags", Enum.GetName(typeof(Flags), item.Flags));
        Utils.Print("RawFlags", item.RawFlags);
        Utils.Print("Date", $"{item.Date:dd/MM/yyyy - HH:mm}hs");
        Utils.Print("Uid", item.Uid);

        Console.WriteLine("\n\n");
        contador++;
    }

    Console.ReadKey();
}
catch (Exception ex)
{
    Utils.Print($"{ex.Message}", string.Empty, ConsoleColor.DarkRed);
    Console.ReadKey();
    Environment.Exit(0);
}


static ImapClient Login()
{
    Utils.Print("Ingrese su email", string.Empty, ConsoleColor.Cyan);
    Config.Username = Console.ReadLine();
    Utils.Print("Ingrese su contraseña", string.Empty, ConsoleColor.Cyan);
    Config.Password = Utils.MaskPasswordString();
    ImapClient client = new(Config.Host, Config.Username, Config.Password, Config.Method, Config.Port, Config.Secure);
    return client;
}

