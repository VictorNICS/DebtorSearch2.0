using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace NICS.System.Notifications
{
    
    public class SmtpMessage : IExtensibleDataObject
    {
        private ExtensionDataObject extensionDataField;

        private string[] AttachmentsField;

        private string[] BccField;

        private string BodyField;

        private string[] CCField;

        private string FromField;

        private bool IsBodyHtmlField;

        private string ReplyToAddressField;

        private string SenderAddressField;

        private string SubjectField;

        private string[] ToField;

        public ExtensionDataObject ExtensionData
        {
            get
            {
                return this.extensionDataField;
            }
            set
            {
                this.extensionDataField = value;
            }
        }

    
        public string[] Attachments
        {
            get
            {
                return this.AttachmentsField;
            }
            set
            {
                this.AttachmentsField = value;
            }
        }

     
        public string[] Bcc
        {
            get
            {
                return this.BccField;
            }
            set
            {
                this.BccField = value;
            }
        }

    
        public string Body
        {
            get
            {
                return this.BodyField;
            }
            set
            {
                this.BodyField = value;
            }
        }

    
        public string[] CC
        {
            get
            {
                return this.CCField;
            }
            set
            {
                this.CCField = value;
            }
        }

     
        public string From
        {
            get
            {
                return this.FromField;
            }
            set
            {
                this.FromField = value;
            }
        }

      
        public bool IsBodyHtml
        {
            get
            {
                return this.IsBodyHtmlField;
            }
            set
            {
                this.IsBodyHtmlField = value;
            }
        }

        
        public string ReplyToAddress
        {
            get
            {
                return this.ReplyToAddressField;
            }
            set
            {
                this.ReplyToAddressField = value;
            }
        }

        public string SenderAddress
        {
            get
            {
                return this.SenderAddressField;
            }
            set
            {
                this.SenderAddressField = value;
            }
        }

     
        public string Subject
        {
            get
            {
                return this.SubjectField;
            }
            set
            {
                this.SubjectField = value;
            }
        }

        public string[] To
        {
            get
            {
                return this.ToField;
            }
            set
            {
                this.ToField = value;
            }
        }
    }

}