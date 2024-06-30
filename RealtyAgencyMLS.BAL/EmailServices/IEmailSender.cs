using RealtyAgencyMLS.Model.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RealtyAgencyMLS.BAL.EmailServices
{
    public interface IEmailSender
    {
        Task<bool> SendEmal(EmailTemplates template, MailRequest appUser);
    }
}
