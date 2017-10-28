using System;
using System.Linq;
using System.Threading.Tasks;
using Aufnet.Backend.ApiServiceShared.Models;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Aufnet.Backend.Services.Shared
{
    public class SendGridEmailService : IEmailService
    {
        //private readonly IOptions<AuthMessageSenderOptions> _optionsAccessor;

        //public SendGridEmailService( IOptions<AuthMessageSenderOptions> optionsAccessor )
        //{
        //    _optionsAccessor = optionsAccessor;
        //}

        public async Task SendEmailAsync(EmailModel email)
        {
            try
            {
                var myMessage = GetMessage(email);
                //TODO: GET THE SECRET KEY PROPERLY
                var client = new SendGridClient("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");

                await client.SendEmailAsync(myMessage);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
        }

        private SendGridMessage GetMessage( EmailModel emailModel )
        {
            if (string.IsNullOrEmpty(emailModel.ToEmail))
                throw new ArgumentException("model.ToEmail");

            /* MAIL SETTINGS
             * Fill in the relevant information below.
             * ===================================================*/
            // From
            string fromAddress = emailModel.FromEmail;
            // To
            string recipients = emailModel.ToEmail;
            // Subject
            string subject = emailModel.Subject;


            /* CREATE THE MAIL MESSAGE
             * ===================================================*/
            var myMessage = new SendGridMessage();
            recipients = recipients.Replace(",", ";");
            recipients.Split(';').ToList().ForEach(x => myMessage.AddTo(x));
            if (emailModel.CcEmail != null && emailModel.CcEmail.Length > 0)
            {
                emailModel.CcEmail.ToList().ForEach(x => myMessage.AddCc(x));
            }
            if (emailModel.BccEmail != null && emailModel.BccEmail.Length > 0)
            {
                emailModel.BccEmail.ToList().ForEach(x => myMessage.AddCc(x));
            }
            myMessage.From = new EmailAddress(fromAddress, emailModel.FromName);
            myMessage.Subject = subject;


            //if (emailModel.HasTemplate())
            //{
            //    var tmplFile = AppDomain.CurrentDomain.BaseDirectory + "/EmailTemplates/" + emailModel.TemplateName + ".cshtml";

            //    if (tmplFile.Contains("ReimbursementReportEmailNew")) // == @"C:\Projects\Merchant\UnitTests\bin\Debug/EmailTemplates/ReimbursementReportEmailNew.cshtml")
            //    {
            //        //SL Logo
            //        string img = @"C:\Logos\Logo.PNG";
            //        ContentType ctype = new ContentType("image/png");
            //        var attachment = new Attachment(img, ctype);
            //        var linkedResource = new LinkedResource(img, ctype);
            //        linkedResource.ContentId = "SlLogo";
            //        myMessage.AddAttachment(attachment.ContentStream, attachment.Name);
            //        myMessage.EmbedImage(attachment.Name, linkedResource.ContentId);

            //        // Cashpoint Logo
            //        string imgCash = @"C:\Logos\image001.jpg";
            //        ContentType ctypeCash = new ContentType("image/jpeg");
            //        var attachmentCash = new Attachment(imgCash, ctypeCash);
            //        var linkedResourceCash = new LinkedResource(imgCash, ctypeCash);
            //        linkedResourceCash.ContentId = "CashpointLogo";
            //        myMessage.AddAttachment(attachmentCash.ContentStream, attachmentCash.Name);
            //        myMessage.EmbedImage(attachmentCash.Name, linkedResourceCash.ContentId);
            //    }
            //    var templateEngine = new EmailTemplateEngine();
            //    myMessage.Html = templateEngine.RenderTemplate(tmplFile, emailModel);
            //    emailModel.Body = myMessage.Html;
            //}
            if (emailModel.IsHtml)
            {
                myMessage.HtmlContent = emailModel.Body;
            }
            else
            {
                myMessage.PlainTextContent = emailModel.Body;
            }

            ////add attachment
            //if (emailModel.HasAttachment)
            //{
            //    foreach (var d in emailModel.Attachments)
            //    {
            //        myMessage.AddAttachment(d.Value, d.Key);
            //    }
            //}

            return myMessage;
        }
    }
}