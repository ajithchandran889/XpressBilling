using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Membership.OpenAuth;
using System.IO;

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
                FileUpload inputUpload = (FileUpload)step.ContentTemplateContainer.FindControl("inputUpload");
                string path = "";
                string absolutePath = "";
                if (inputUpload.HasFile)
                {
                    string filename = Path.GetFileName(inputUpload.FileName);
                    path = Server.MapPath("~/Images/user/") + filename;
                    absolutePath = "/Images/user/" + filename;
                    inputUpload.SaveAs(path);
                }
                if (inputUpload.HasFile)
                {
                    string folderPath = "~/Images/Company/" + ddListCompany.SelectedValue + "/User/";
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    if (!System.IO.Directory.Exists(Server.MapPath("~") + "/Images/Company/" + ddListCompany.SelectedValue + "/User/"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~") + "/Images/Company/" + ddListCompany.SelectedValue + "/User/");
                    }
                    path = Server.MapPath(folderPath) + RegisterUser.UserName + "_user_" + timestamp + Path.GetExtension(inputUpload.FileName);
                    absolutePath = folderPath + RegisterUser.UserName + "_user_" + timestamp + Path.GetExtension(inputUpload.FileName); ;
                    inputUpload.SaveAs(path);
                }
                Roles.AddUserToRole(RegisterUser.UserName, ddListUser.SelectedValue);

                XBDataProvider.UserRegistration.SaveAddlUserRegDetails(RegisterUser.UserName, ddListCompany.SelectedValue, null, null, null, null, absolutePath);
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