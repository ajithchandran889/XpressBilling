using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Login : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RegisterHyperLink.NavigateUrl = "Register";

            var returnUrl = HttpUtility.UrlEncode(Request.QueryString["ReturnUrl"]);
            if (!String.IsNullOrEmpty(returnUrl))
            {
                RegisterHyperLink.NavigateUrl += "?ReturnUrl=" + returnUrl;
            }
        }

        //protected void UserLoggedIn(object sender, EventArgs e)
        //{
        //    if (Membership.ValidateUser(LoginCtrl.UserName, LoginCtrl.Password))
        //    {
        //        FormsAuthentication.SetAuthCookie(LoginCtrl.UserName, true); 
        //    }
        //}
    }
}