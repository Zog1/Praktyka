using CarRental.Services.Interfaces;
using CarRental.Services.Models.User;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;

namespace CarRental.Services.Models.Email_Templates
{
    public class EmailService : IEmailServices
    {
        /// <summary>
        /// Send email with link to set password
        /// </summary>
        /// <param name="createUserDto"></param>
        /// <returns>return true if email send correct</returns>
        [Obsolete]
        public bool EmailAfterRegistration(CreateUserDto createUserDto)
        {
            string subject = "Rent Car Service";
            string data = createUserDto.FirstName;
            string htmlBody = @"
                        <html lang=""en"">    
                         <body style='width:720px'>  
                           <h2>Dear " + createUserDto.FirstName + @",</h2> <p style='font-family: Arial,sans-serif'>You have been registered in the service where you can rent a car.
                             <br>
                             Please see the information below about your login</p>
                            <h2>Login: " + createUserDto.Email + @"
                             </h2>
                             <p>That's your temporary password, you can change your password followed this link.</p>
                              <div style='text-align:center'><a href='http://localhost:3000/set-password/" + createUserDto.CodeOfVerification + @"' style='font-size:30px'>Change Password</a></div>
                              <p style='font-family: Arial,sans-serif'>We appreciate that you are with us and using service<br>Have a nice day,<br>Car Rental Service</p>
                            <img src=""cid:WinLogo"" />
                                    </body>
                                         </html>";
            string messageBody = string.Format(htmlBody, data);
            AlternateView alternateViewHtml = AlternateView.CreateAlternateViewFromString(htmlBody, Encoding.UTF8, MediaTypeNames.Text.Html);
            MailMessage mailMessage = new MailMessage("kucherbogdan2000@gmail.com", createUserDto.Email, subject, messageBody);
            mailMessage.AlternateViews.Add(alternateViewHtml);
            using (SmtpClient smpt = new SmtpClient("smtp.gmail.com", 587))
            {
                smpt.EnableSsl = true;
                smpt.DeliveryMethod = SmtpDeliveryMethod.Network;
                smpt.UseDefaultCredentials = false;
                smpt.Credentials = new NetworkCredential("kucherbogdan2000@gmail.com", "basket2009");
                MailMessage message = new MailMessage();
                message.To.Add(createUserDto.Email);
                message.From = new MailAddress("kucherbogdan2000@gmail.com");
                message.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;
                message.Subject = "Car Renting";
                message.Body = "Something";
                smpt.Send(mailMessage);
            }
            return true;
        }
    }
}
