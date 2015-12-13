using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class EditItemGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtItemGroups = XBDataProvider.ItemGroup.GetAllTaxCodes(Session["CompanyCode"].ToString());
                ddlTaxCode.DataSource = dtItemGroups;
                ddlTaxCode.DataValueField = "TaxCode";
                ddlTaxCode.DataTextField = "name";
                ddlTaxCode.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select TaxCode";
                item.Value = "0";
                ddlTaxCode.Items.Insert(0, item);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable ItemGroupDetails = XBDataProvider.ItemGroup.GetItemGroupById(id);
                    if (ItemGroupDetails.Rows.Count > 0)
                    {
                        SetItemGroupDetails(ItemGroupDetails);
                    }
                }
                else
                {
                    lbldate.Visible = false;
                    lblstatus.Visible = false;
                    lblusername.Visible = false;
                    UserName.Visible = false;
                    Date.Visible = false;
                    ddlStatus.Visible = false;
                    ItemId.Value = "0";
                }
            }
        }

        public void SetItemGroupDetails(DataTable ItemGroupDetails)
        {
            DataRow row = ItemGroupDetails.Rows[0];
            ItemGroup.Text = row["ItemGroupCode"].ToString();
            ItemGroup.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            ddlTaxCode.SelectedValue = row["TaxCode"].ToString();
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            Date.Text = row["CreatedDate"].ToString();
            Date.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            ItemId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (ItemId.Value != "0" && ItemId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.ItemGroup.UpdateItemGroup(Convert.ToInt32(ItemId.Value), Name.Text, User.Identity.Name, status);
                    if (msgstatus != -1)
                    {
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
                    string reference = "";
                    string cesscode = "";
                    msgstatus = XBDataProvider.ItemGroup.SaveItemGroup(hdncompanycode.Value, ItemGroup.Text, Name.Text, ddlTaxCode.SelectedValue,cesscode, reference, User.Identity.Name, true);
                    ClearInputs(Page.Controls);
                    if (msgstatus == 1)
                    {
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = false;
                    }
                    else if (msgstatus == -1)
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        alreadyexist.Visible = true;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                        alreadyexist.Visible = false;
                    }
                }

            }
            catch (Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = true;
                alreadyexist.Visible = false;
            }


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