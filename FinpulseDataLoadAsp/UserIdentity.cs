using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

 
    public class UserIdentity
    {
        public static string CurrentUser
        {
            get
            {

                string userid = HttpContext.Current.User.Identity.Name;
                string[] userids = userid.Split('\\');
                if (userids.Length == 2)
                {
                    userid = userids[1];
                }
              
                return userid;
            }
        }
    }
 
