# SMTP_Email_Sent
Simple Mail Transfer Protocol

 using (var client = new SmtpClient())
                    {
                        await client.ConnectAsync("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                        await client.AuthenticateAsync("officework144923@gmail.com", ""); // App password
                        await client.SendAsync(message);
                        await client.DisconnectAsync(true);
                    }


                    ************** Inside Controller this app password postion add Emaill pass code 

                    System.Net.Mail.SmtpClient       => this package use
