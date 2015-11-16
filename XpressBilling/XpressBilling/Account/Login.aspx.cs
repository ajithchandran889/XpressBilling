using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginCtrl_LoggingIn(object sender, LoginCancelEventArgs e)
        {
            string userName = (LoginCtrl.FindControl("UserName") as TextBox).Text;
            MembershipUser user = Membership.GetUser(userName);
            Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(user.UserName);
        }
    }
}