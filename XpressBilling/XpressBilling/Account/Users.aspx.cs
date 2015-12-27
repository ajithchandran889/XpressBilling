using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadUserList();
            }
            
        }

        protected void listUserPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listUser.PageIndex = e.NewPageIndex;
            LoadUserList();
        }

        private void LoadUserList()
        {
            listUser.DataSource = XBDataProvider.User.GetAllUsers(Session["CompanyCode"].ToString());
            listUser.DataBind();
        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listUser.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.User.DeleteUsers(ids);
            LoadUserList();
        }
        protected void listUserDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listUser.Rows)
            {
                DropDownList ddlListUser = gvRow.FindControl("UserStatusDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlListUser != null && hfSelectedValue != null)
                {
                    if (Convert.ToBoolean(hfSelectedValue.Value) == true)
                        ddlListUser.SelectedValue = "1";
                    else
                        ddlListUser.SelectedValue = "0";
                }
            }
        }

        protected void UserStatusDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            var userId = ddl.Attributes["userName"];
            MembershipUser user = Membership.GetUser(userId);
            if (ddl.SelectedValue == "1")
            {
                if (!user.IsApproved)
                {
                    user.IsApproved = true;
                    Membership.UpdateUser(user);
                }
            }
            else
            {
                if (user.IsApproved)
                {
                    user.IsApproved = false;
                    Membership.UpdateUser(user);
                }
            }
        }
    }
}