using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class AddUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtLocation = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());

                Location.DataSource = dtLocation;
                Location.DataValueField = "LocationCode";
                Location.DataTextField = "Name";
                Location.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Location";
                item.Value = "";
                Location.Items.Insert(0, item);
                ListItem itemAll = new ListItem();
                itemAll.Text = "All";
                itemAll.Value = "1";
                Location.Items.Insert(0, itemAll);

                DefLocation.DataSource = dtLocation;
                DefLocation.DataValueField = "LocationCode";
                DefLocation.DataTextField = "Name";
                DefLocation.DataBind();
                ListItem itemdefloc = new ListItem();
                itemdefloc.Text = "Select Location";
                itemdefloc.Value = "0";
                DefLocation.Items.Insert(0, item);
                DataTable dtCompany = XBDataProvider.Company.GetAllCompanyCode();
                ddlCompany.DataSource = dtCompany;
                ddlCompany.DataValueField = "CompanyCode";
                ddlCompany.DataTextField = "Name";
                ddlCompany.DataBind();
                ListItem itemCompany = new ListItem();
                itemCompany.Text = "Select Company";
                itemCompany.Value = "0";
                ddlCompany.Items.Insert(0, itemCompany);
                DataTable dtEmployee = XBDataProvider.Employee.GetAllEmployee(Session["CompanyCode"].ToString());
                ddlEmployeeId.DataSource = dtEmployee;
                ddlEmployeeId.DataValueField = "EmployeeCode";
                ddlEmployeeId.DataTextField = "Name";
                ddlEmployeeId.DataBind();
                ListItem itemEmployee = new ListItem();
                itemEmployee.Text = "Select Employee";
                itemEmployee.Value = "0";
                ddlEmployeeId.Items.Insert(0, itemEmployee);
                string id = Request.QueryString["ID"];
                if (id != null)
                {
                    SetUserDetails(id); 
                }
                else
                {
                    UserId.Value = "0";
                }
            }
        }

        public void SetUserDetails(string id)
        {
            DataTable userDetails = XBDataProvider.User.GetUserById(id);
            if (userDetails.Rows.Count > 0)
            {
                DataRow row = userDetails.Rows[0];
                UserId.Value = row["Id"].ToString(); ;
                UserName.Text = row["UserName"].ToString();
                UserName.ReadOnly = true;
                Email.Text = row["Email"].ToString();
                Email.ReadOnly = true;
                ddlEmployeeId.SelectedValue = row["EmployeeId"].ToString();
                ddlCompany.SelectedValue = row["CompanyCode"].ToString();
                ddlCompany.Enabled = false;
                Location.SelectedValue = row["LocationCode"].ToString();
                DefLocation.SelectedValue = row["DefaultLocation"].ToString();
                string status = Convert.ToBoolean(row["Status"].ToString()) ? "1" : "0";
                Status.SelectedValue = status;
                UserType.SelectedValue = row["UserType"].ToString();
                Password.ReadOnly = true;
                ConfPassword.ReadOnly = true;
                Password.Attributes.Add("class", Password.Attributes["class"].ToString().Replace("required", ""));
                ConfPassword.Attributes.Add("class", ConfPassword.Attributes["class"].ToString().Replace("required", ""));
                if (row["path"].ToString() == null || row["path"].ToString() == "")
                {
                    imgPreview.ImageUrl = "/Images/user/preview.png";
                }
                else
                    imgPreview.ImageUrl = HttpUtility.HtmlDecode(row["path"].ToString());
                inputUpload.Attributes.Clear();
            }
            
        }

        [WebMethod]
        public static List<string> GatAllCompanies()
        {
           List<string> result = new List<string>();
           DataTable dtTable= XBDataProvider.Company.GetAllCompanyCode();
           DataRow row = null;
           for (int index = 0; index < dtTable.Rows.Count;index++ )
           {
               row = dtTable.Rows[index];
               result.Add(row["CompanyCode"].ToString());
           }
           return result;
        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                string path = "";
                string absolutePath = "";
     
                if (inputUpload.HasFile)
                {
                    string folderPath = "~/Images/Company/" + Session["CompanyCode"].ToString() + "/User/";
                    string timestamp = DateTime.Now.ToString("yyyyMMddHHmmssfff");
                    if (!System.IO.Directory.Exists(Server.MapPath("~") + "/Images/Company/" + Session["CompanyCode"].ToString() + "/User/"))
                    {
                        System.IO.Directory.CreateDirectory(Server.MapPath("~") + "/Images/Company/" + Session["CompanyCode"].ToString() + "/User/");
                    }
                    path = Server.MapPath(folderPath) + UserName.Text + "_user_" + timestamp + Path.GetExtension(inputUpload.FileName);
                    absolutePath = folderPath + UserName.Text + "_user_" + timestamp + Path.GetExtension(inputUpload.FileName); ;
                    imgPreview.ImageUrl = absolutePath;
                    inputUpload.SaveAs(path);
                }
                bool status = false;
                if (UserId.Value != "0" && UserId.Value != "")
                {
                    MembershipUser user = Membership.GetUser(UserName.Text);
                    if (Status.SelectedValue == "0" && user.IsApproved)
                    {
                        user.IsApproved = false;
                        Membership.UpdateUser(user);
                    }
                    if(!Roles.IsUserInRole(UserName.Text,UserType.SelectedValue))
                    {
                        Roles.AddUserToRole(UserName.Text, UserType.SelectedValue);
                    }
                    status = XBDataProvider.UserRegistration.UpdateUserRegDetails(UserId.Value,Location.SelectedValue,ddlEmployeeId.SelectedValue,DefLocation.SelectedValue,User.Identity.Name,absolutePath);
                    if (status)
                    {
                        SetUserDetails(user.ProviderUserKey.ToString());
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                }
                else
                {
                    MembershipUser user = Membership.CreateUser(UserName.Text, Password.Text, Email.Text);
                    
                    if(user.UserName==UserName.Text)
                    {
                        if(Status.SelectedValue=="0")
                        {
                            user.IsApproved = false;
                            Membership.UpdateUser(user);
                        }
                        Roles.AddUserToRole(UserName.Text, UserType.SelectedValue);
                        status = XBDataProvider.UserRegistration.SaveAddlUserRegDetails(UserName.Text, ddlCompany.SelectedValue, Location.SelectedValue, ddlEmployeeId.SelectedValue, DefLocation.SelectedValue, User.Identity.Name, absolutePath);
                        if (status)
                        {
                            SaveSuccess.Visible = true;
                            UpdateSuccess.Visible = false;
                            failure.Visible = false;
                            //alreadyexist.Visible = false;
                        }
                        else
                        {
                            SaveSuccess.Visible = false;
                            UpdateSuccess.Visible = false;
                            failure.Visible = true;
                        }
                        ClearInputs(Page.Controls);
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                    
                }


            }
            catch (Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = false;
                alreadyexist.Visible = true;
            }

            //Label lblMsg = this.Master.FindControl("Message") as Label;
            //lblMsg.Text = "Company added successfully";
            //lblMsg.Visible = true;
        }

        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
            imgPreview.ImageUrl = "/Images/user/preview.png";
        }
    }
}