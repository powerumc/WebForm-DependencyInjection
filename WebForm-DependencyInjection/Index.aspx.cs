using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Umc.Core;
using WebForm_DependencyInjection.Services;

namespace WebForm_DependencyInjection
{
    [DependencyContract]
    public partial class Index : System.Web.UI.Page
    {
        [DependencyInjection]
        public IEmailService EmailService { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            EmailService.Send("", "");
        }
    }
}