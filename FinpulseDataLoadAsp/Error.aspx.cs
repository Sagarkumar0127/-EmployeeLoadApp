using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace iSHARE
{
    public partial class Error : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            lblTime.Text = DateTime.Now.ToString();
            lblMessage.Text = "An Unexpected Error Occured while performing this operation.  " +
"Please Try again later. If problem persists Please contact administrator  ";
            if (Request.QueryString["Message"] + "" != "")
                lblMessage.Text = Request.QueryString["Message"] + "";
        }
    }
}