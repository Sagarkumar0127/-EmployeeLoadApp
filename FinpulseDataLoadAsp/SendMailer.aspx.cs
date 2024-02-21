using FinpulseDataLoadModule.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinpulseDataLoadAsp
{
    public partial class SendMailer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            UpdateMailerInfo();
        }
        protected void UpdateMailerInfo()
        {
            var info = MailerInfo.GetMailerSentStatus(MailerInfoConstants.EmployeeMailer);
            bSentBy.InnerText = info.SentBy;
            bSentOn.InnerText = info.SentOnFormatted == null ? "Info Not Available" : info.SentOnFormatted;
        }

        protected bool IsMailerRepeated()
        {

            var info = MailerInfo.GetMailerSentStatus(MailerInfoConstants.EmployeeMailer);
            bool returnValue = false;
            if (info.SentOn.HasValue)
            {
                if (info.SentOn.Value.ToString("dd-MM-yyyy") == DateTime.Now.ToString("dd-MM-yyyy"))
                {
                    UpdateMailerInfo();
                    ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Mailer has been triggered today, if you wish to resend the mail, pls set the [LastSentOn] column value as null for the respected item in the table [MailerSentStatus]')", true);
                    returnValue = true;
                }
            }

            return returnValue;

        }

        protected void btnSendMailer_Click(object sender, EventArgs e)
        {
            if (IsMailerRepeated())
            {
                return;
            }

            ActVsBdgMailer.GenerateMailer();

            MailerInfo.UpdateMailerStatus(MailerInfoConstants.EmployeeMailer); // New Line
            UpdateMailerInfo();  // New Line
            ClientScript.RegisterStartupScript(this.GetType(), "Error", "alert('Mailer Sent Successfully!')", true);


        }
    }
}