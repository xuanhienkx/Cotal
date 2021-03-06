﻿using System;
using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;

namespace Cotal.Core.Common
{
   public class MailHelper
    {
        public static bool SendMail(string toEmail, string subject, string content)
        {
            try
            {
                //From Address
                string FromAddress = "From Email Address";
                string FromAdressTitle = "Email from ASP.NET Core 1.1";
                //To Address
                string ToAddress = "To Email Address";
                string ToAdressTitle = "Microsoft ASP.NET Core";
                string Subject = "Hello World - Sending email using ASP.NET Core 1.1";
                string BodyContent = "ASP.NET Core was previously called ASP.NET 5. It was renamed in January 2016. It supports cross-platform frameworks ( Windows, Linux, Mac ) for building modern cloud-based internet-connected applications like IOT, web apps, and mobile back-end.";

                //Smtp Server
                string SmtpServer = "smtp.gmail.com";
                //Smtp Port Number
                int SmtpPortNumber = 587;

                var mimeMessage = new MimeMessage();
                mimeMessage.From.Add(new MailboxAddress(FromAdressTitle, FromAddress));
                mimeMessage.To.Add(new MailboxAddress(ToAdressTitle, ToAddress));
                mimeMessage.Subject = Subject;
                mimeMessage.Body = new TextPart(TextFormat.Html)
                {
                    Text = BodyContent

                };

                using (var client = new SmtpClient())
                {

                    client.Connect(SmtpServer, SmtpPortNumber, false);
                    // Note: only needed if the SMTP server requires authentication
                    // Error 5.5.1 Authentication 
                    client.Authenticate("From Address Email", "Password");
                    client.Send(mimeMessage);  
                    client.Disconnect(true);

                }
                return true;
            }
            catch  
            {
                return false;
            }
        }
    }  
}
