using Entities.Responses;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using ElasticEmail.Api;
using ElasticEmail.Client;
using ElasticEmail.Model;

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
            SmtpClient SmtpServer = new SmtpClient(_from);

            mail.From = new MailAddress(_from);
            mail.To.Add(_to);
            mail.Subject = _subject;
            mail.Body = _body;

            SmtpServer.Port = 2525;
            SmtpServer.Credentials = new System.Net.NetworkCredential("vucao005@gmail.com", "14CCAAC2FD6545F88BC86588631D17D66BF2");
            SmtpServer.EnableSsl = true;
            SmtpServer.UseDefaultCredentials = true;
            SmtpServer.Send(mail);*/
            //MessageBox.Show("mail Send");
            #endregion
            /*try
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
            }*/
            Configuration config = new Configuration();
            // Configure API key authorization: apikey
            config.ApiKey.Add("X-ElasticEmail-ApiKey", "089DE984E2B0219FB6BCD411FD4B783CA700107A2D34E4C57891EDDC1CCEACD55751FA13E57E213C5207A6271B07FFDF");
            var apiInstance = new EmailsApi(config);
            var to = new List<string>();
            to.Add(_to);
            var recipients = new TransactionalRecipient(to: to);
            EmailTransactionalMessageData emailData = new EmailTransactionalMessageData(recipients: recipients);
            emailData.Content = new EmailContent();
            emailData.Content.Body = new List<BodyPart>();
            BodyPart htmlBodyPart = new BodyPart();
            htmlBodyPart.ContentType = BodyContentType.HTML;
            htmlBodyPart.Charset = "utf-8";
            htmlBodyPart.Content = _body;
            BodyPart plainTextBodyPart = new BodyPart();
            plainTextBodyPart.ContentType = BodyContentType.PlainText;
            plainTextBodyPart.Charset = "utf-8";
            plainTextBodyPart.Content = _body;
            emailData.Content.Body.Add(htmlBodyPart);
            emailData.Content.Body.Add(plainTextBodyPart);
            emailData.Content.From = _from;
            emailData.Content.Subject = _subject;
            try
            {
                apiInstance.EmailsTransactionalPost(emailData);
                return new BaseResponse();
            }
            catch (Exception ex)
            {
                return new BaseResponse(false, ex.Message);
                throw;
            }
        }
    }
}
