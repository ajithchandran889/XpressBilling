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
                if(!XBDataProvider.Company.IsCompanyexist())
                {
                    Response.Redirect("RegisterCompany");
                }
                CreateUserWizardStep step = (CreateUserWizardStep)RegisterUser.FindControl("RegisterUserWizardStep");
                if (step != null)
                {
                    DropDownList ddList = (DropDownList)step.ContentTemplateContainer.FindControl("CompanyCode");
                    ddList.DataSource = XBDataProvider.Company.GetAllCompanies();
                    ddList.DataValueField = "CompanyCode";
                    ddList.DataTextField = "CompanyCode";
                    ddList.DataBind();
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
                
                DropDownList ddListCompany = (DropDownList)step.ContentTemplateContainer.FindControl("CompanyCode");
                DropDownList ddListUser = (DropDownList)step.ContentTemplateContainer.FindControl("UserType");
                Roles.AddUserToRole(RegisterUser.UserName, ddListUser.SelectedValue);
                
                XBDataProvider.UserRegistration.SaveAddlUserRegDetails(RegisterUser.UserName, ddListCompany.SelectedValue);
                Session["CompanyCode"] = ddListCompany.SelectedValue;
            }
            
            if (!OpenAuth.IsLocalUrl(continueUrl))
            {
                continueUrl = "~/";
            }
            Response.Redirect(continueUrl);
        }

        
    }
}