using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadContactList();
            }

        }

        protected void listContactPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listContact.PageIndex = e.NewPageIndex;
            LoadContactList();
        }
        private void LoadContactList()
        {
            listContact.DataSource = XBDataProvider.Contact.GetAllContactCode();
            listContact.DataBind();
        }
        protected void listContactDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listContact.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("ContactStatusDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void ContactStatusDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdContact"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Contact.ActivateContact(companyId);
                }
                else
                {
                    XBDataProvider.Contact.DeActivateContact(companyId);
                }
                LoadContactList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listContact.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Contact.DeleteContact(ids);
            LoadContactList();
        }
    }
}