using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;

namespace XpressBilling.Account
{
    public partial class Register : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!Page.IsPostBack)
            {
                CreateUserWizardStep step = (CreateUserWizardStep)RegisterUser.FindControl("RegisterUserWizardStep");
                if (step != null)
                {
                    DropDownList ddList = (DropDownList)step.ContentTemplateContainer.FindControl("CompanyCode");
                    ddList.DataSource = XBDataProvider.Company.GetAllCompanies();
                    ddList.DataValueField = "ID";
                    ddList.DataTextField = "CompanyCode";
                    ddList.DataBind();
                    ListItem item = new ListItem();
                    item.Text = "Select Company";
                    item.Value = "-1";
                    ddList.Items.Insert(0, item);
                }
            }
            

            RegisterUser.ContinueDestinationPageUrl = Request.QueryString["ReturnUrl"];
        }

        protected void RegisterUser_CreatedUser(object sender, EventArgs e)
        {
            FormsAuthentication.SetAuthCookie(RegisterUser.UserName, createPersistentCookie: false);

            string continueUrl = RegisterUser.ContinueDestinationPageUrl;
            CreateUserWizardStep step = (CreateUserWizardStep)RegisterUser.FindControl("RegisterUserWizardStep");
            if (step != null)
            {
                
                DropDownList ddList = (DropDownList)step.ContentTemplateContainer.FindControl("CompanyCode");
                XBDataProvider.UserRegistration.SaveAddlUserRegDetails(RegisterUser.UserName, ddList.SelectedValue);
            }
            
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        
    }
}