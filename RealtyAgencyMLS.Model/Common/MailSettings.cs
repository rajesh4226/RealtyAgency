using System;
using System.Collections.Generic;
using System.Text;

namespace RealtyAgencyMLS.Model.Common
{
    public class MailSettings
    {
        public string Mail { get; set; }
        public string DisplayName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public bool IsTest { get; set; }
        public string TestEmail { get; set; }
    }
}
