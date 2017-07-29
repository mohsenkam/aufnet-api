using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Aufnet.Backend.ApiServiceShared.Models
{
    public class EmailModel
    {
        public EmailModel()
        {
            EmailRefId = Guid.NewGuid().ToString();
        }

        public string EmailRefId { get; set; }

        public virtual bool IsHtml
        {
            get { return false; }
        }



        public virtual string FromEmail
        {
            get { return "core@aufnet.com.au"; }
        }

        public virtual string[] CcEmail
        {
            get { return null; }
        }

        public virtual string[] BccEmail
        {
            get { return null; }
        }

        public virtual String FromName { get { return "Aufnet Core Service"; } }

        public string ToEmail { get; set; }
        public string ToEmailName { get; set; }
        public string Subject { get; set; }

        //for rendering the body
        public virtual string TemplateName
        {
            get { return null; }
        }
        public virtual string Body
        {
            get; set;
        }

        public bool HasTemplate()
        {
            return string.IsNullOrEmpty(Body);
        }


        private IDictionary<string, MemoryStream> _attachments;
        private IList<string> _attachmentFileNames;
        //[JsonIgnore]
        public IDictionary<string, MemoryStream> Attachments { get { return _attachments; } }
        public IList<string> AttachedFileNames { get { return _attachmentFileNames; } }

        public bool HasAttachment { get { return Attachments != null && Attachments.Count > 0; } }
        public void ClearAttachment()
        {
            _attachments = null;
        }
        public void AddAttachment( MemoryStream stream, string attachmentName, string fileName = null )
        {
            if (Attachments == null)
            {
                _attachments = new Dictionary<string, MemoryStream>();
                _attachmentFileNames = new List<string>();
            }


            _attachments.Add(attachmentName, stream);

            if (!string.IsNullOrEmpty(fileName))
                _attachmentFileNames.Add(fileName);
        }

        public void AddAttachment( string filepath, string attachmentName )
        {
            var ms = new MemoryStream();
            using (var stream = File.OpenRead(filepath))
            {
                stream.CopyTo(ms);
            }
            ms.Position = 0;
            AddAttachment(ms, attachmentName, new FileInfo(filepath).Name);
        }


        public string UnsubscribeLink { get { return "https://aufnet.com.au/Email/Unsubscribe/" + EmailRefId; } }

        //public string RenderBody()
        //{
        //    if (!this.HasTemplate()) return Body;

        //    var tmplFile = AppDomain.CurrentDomain.BaseDirectory + "/EmailTemplates/" + this.TemplateName + ".cshtml";
        //    var templateEngine = new EmailTemplateEngine();
        //    var html = templateEngine.RenderTemplate(tmplFile, this);

        //    return html;
        //}
    }
}
