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
            try
            {
                string userName = (LoginCtrl.FindControl("UserName") as TextBox).Text;
                MembershipUser user = Membership.GetUser(userName);
                if(user!=null)
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(user.UserName);
            }
            catch(Exception ex)
            {

            }
           
        }

        protected void LoginCtrl_LoginError(object sender, EventArgs e)
        {
            string userName = (LoginCtrl.FindControl("UserName") as TextBox).Text;
            MembershipUser user = Membership.GetUser(userName);
            Label FailureTextLbl = (LoginCtrl.FindControl("failureMessage") as Label);
            if (user == null)
            {
                FailureTextLbl.Text = "Invalid Password or User Name. Please try again.";
            }
            else
            {
                if (!user.IsApproved)
                    FailureTextLbl.Text = "Your account has not yet been approved by an administrator.";
                else if (user.IsLockedOut)
                    FailureTextLbl.Text = "You have been locked out, please try again in 10 minutes.";
                else
                    FailureTextLbl.Text = "Invalid Password or User Name. Please try again.";
            }
        }
    }
}