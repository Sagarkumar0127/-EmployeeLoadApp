using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace FinpulseDataLoadAsp
{
    public partial class BasePage : System.Web.UI.Page
    {
       

        protected void Page_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            Logger.LogErrorToServer(Logger.LoggerType.Error, UserIdentity.CurrentUser, ex);
            Server.ClearError();
            Response.Redirect("Error.aspx?Message=" + ex.Message);
        }
    }
}
 