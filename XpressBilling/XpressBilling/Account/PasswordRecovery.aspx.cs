using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.Text.RegularExpressions;
namespace XpressBilling.Account
{
    public partial class PasswordRecovery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected bool IsValidEmail(string strIn)
        {
            // Return true if strIn is in valid e-mail format.
            return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        protected void PasswordRecoveryVerifyingUser(object sender, LoginCancelEventArgs e)
        {
            if (!IsValidEmail(PasswordRecovery1.UserName))
            {
                PasswordRecovery1.UserNameInstructionText = "You must enter a valid e-mail address.";
                e.Cancel = true;
            }
            if (PasswordRecovery1.UserName.Length > 0)
            {
                PasswordRecovery1.UserName = Membership.GetUserNameByEmail(PasswordRecovery1.UserName);
            }
        }
    }
}