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
                DataTable dtLocation = XBDataProvider.Location.GetAllLocations();

                Location.DataSource = dtLocation;
                Location.DataValueField = "LocationCode";
                Location.DataTextField = "Name";
                Location.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Location";
                item.Value = "0";
                Location.Items.Insert(0, item);

                DefLocation.DataSource = dtLocation;
                DefLocation.DataValueField = "LocationCode";
                DefLocation.DataTextField = "Name";
                DefLocation.DataBind();
                DefLocation.Items.Insert(0, item);
                string id = Request.QueryString["ID"];
                if (id != null)
                {
                    
                    DataTable userDetails = XBDataProvider.User.GetUserById(id);
                    if (userDetails.Rows.Count > 0)
                    {
                        SetUserDetails(userDetails);

                    }
                }
                else
                {
                    UserId.Value = "0";
                }
            }
        }

        public void SetUserDetails(DataTable userDetails)
        {
            DataRow row = userDetails.Rows[0];
            UserId.Value = row["Id"].ToString(); ;
            UserName.Text = row["UserName"].ToString();
            UserName.ReadOnly = true;
            Email.Text = row["Email"].ToString();
            Email.ReadOnly = true;
            EmployeeId.Text = row["EmployeeId"].ToString();
            Company.Text = row["CompanyCode"].ToString();
            Company.ReadOnly = true;
            Location.SelectedValue = row["LocationCode"].ToString();
            DefLocation.SelectedValue = row["DefaultLocation"].ToString();
            string status =Convert.ToBoolean(row["Status"].ToString())?"1":"0";
            Status.SelectedValue = status;
            UserType.SelectedValue = row["UserType"].ToString();
            Password.ReadOnly = true;
            ConfPassword.ReadOnly = true;
            Password.Attributes.Add("class", Password.Attributes["class"].ToString().Replace("required", ""));
            ConfPassword.Attributes.Add("class", ConfPassword.Attributes["class"].ToString().Replace("required", ""));
            imgPreview.ImageUrl = HttpUtility.HtmlDecode(row["path"].ToString());

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
                    string filename = Path.GetFileName(inputUpload.FileName);
                    path = Server.MapPath("~/Images/user/") + filename;
                    absolutePath = "/Images/user/" + filename;
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
                    status = XBDataProvider.UserRegistration.UpdateUserRegDetails(UserId.Value,Location.SelectedValue,EmployeeId.Text,DefLocation.SelectedValue,User.Identity.Name,absolutePath);
                    if (status)
                    {
                        Message.Text = "Successfully updated";
                    }
                    else
                    {
                        Message.Text = "Oops..Something went wrong.Please try again";
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
                        status = XBDataProvider.UserRegistration.SaveAddlUserRegDetails(UserName.Text, Company.Text, Location.SelectedValue, EmployeeId.Text, DefLocation.SelectedValue, User.Identity.Name, absolutePath);
                        if (status)
                        {
                            Message.Text = "Successfully added";
                        }
                        else
                        {
                            Message.Text = "Oops..Something went wrong.Please try again";
                        }
                        ClearInputs(Page.Controls);
                    }
                    else
                    {
                        Message.Text = "Oops..Something went wrong.Please try again";
                    }
                    
                }


            }
            catch (Exception ex)
            {

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
        }
    }
}