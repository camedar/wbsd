using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;

/// <summary>
/// Summary description for email
/// </summary>
public class Email
{

    private string from = "m.camilo@gmail.com";
    private string password = "L%OnCFAux1";
    private string to = null;
    private string smtp = "smtp.gmail.com";
    private int port = 587;

    public Email()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public bool send(string recipientEmail,string subject,string textMessage)
    {
        try
        {
            MailMessage message = new MailMessage(from, recipientEmail, subject, textMessage);
            message.IsBodyHtml = true;

            SmtpClient client = new SmtpClient(smtp, 587);
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(from,password);
            client.Send(message);

            return true;
        }
        catch (Exception ex)
        {
            Console.Write(ex.Message.ToString());
            return false;
        }
    }
}