using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using Umc.Core;

namespace WebForm_DependencyInjection.Services
{
    public interface IEmailService
    {
        bool Send(string to, string contents);
    }

    [DependencyContract(typeof(IEmailService))]
    public class EmailService : IEmailService
    {
        public bool Send(string to, string contents)
        {
            HttpContext.Current.Response.Write("Send email.");
            return true;
        }
    }
}