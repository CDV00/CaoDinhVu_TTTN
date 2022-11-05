using Entities.Responses;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace CaoDinhVu.BLL.Services.Implementations
{
    public class MailService: IMailService
    {
        public async Task<BaseResponse> SeedMail(string _from, string _to, string _toUser, string _subject, string _body)
        {
            #region test sendMail sptp server
            /*MailMessage message = new MailMessage(
                from: _from,
                to: _to,
                subject: _subject,
                body: _body
            );
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.IsBodyHtml = true;
            message.ReplyToList.Add(new MailAddress(_from));
            message.Sender = new MailAddress(_from);
            using var smtpClient = new SmtpClient("localhost");*/
            //await smtpClient.SendMailAsync(message);
            #endregion
            #region test sendMaid Gmait smtp
            /*MailMessage mail = new MailMessage();
           SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

           mail.From = new MailAddress("vucao005@gmail.com");
           mail.To.Add("caodinhvu00@gmail.com");
           mail.Subject = "Test Mail";
           mail.Body = "This is for testing SMTP mail from GMAIL";

           SmtpServer.Port = 587;
           SmtpServer.Credentials = new System.Net.NetworkCredential("vucao005@gmail.com", "01699148170");
           SmtpServer.EnableSsl = true;
           SmtpServer.UseDefaultCredentials = true;
           SmtpServer.Send(mail);
           //MessageBox.Show("mail Send");*/
            #endregion
            try
            {
                #region SendMail SENDGRID       
                Environment.SetEnvironmentVariable("SENDGRID_API_KEY", "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");
                var apiKey = Environment.GetEnvironmentVariable("SENDGRID_API_KEY");
                var client = new SendGridClient(apiKey);
                var from = new EmailAddress(_from, "Thiet bi dien tu Shop");
                var subject = _subject;
                var to = new EmailAddress(_to, _toUser);
                var plainTextContent = "and easy to do anywhere, even with";
                var htmlContent = _body;
                var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
                var response = await client.SendEmailAsync(msg);
                #endregion
                return new BaseResponse(true);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new BaseResponse(false, ex.Message);
            }
        }
    }
}
