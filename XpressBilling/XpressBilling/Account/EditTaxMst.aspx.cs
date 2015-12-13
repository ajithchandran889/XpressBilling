using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class EditTaxMst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtTaxCodes = XBDataProvider.TaxMst.GetAllTaxCodes(Session["CompanyCode"].ToString());

                ddlTaxCode.DataSource = dtTaxCodes;
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
                    DataTable TaxMstDetails = XBDataProvider.TaxMst.GetTaxById(id);
                    if (TaxMstDetails.Rows.Count > 0)
                    {
                        SetTaxDetails(TaxMstDetails);
                    }
                }
                else
                {
                    lbldate.Visible = false;
                    lblstatus.Visible = false;
                    lblusername.Visible = false;
                    UserName.Visible = false;
                    CreatedDate.Visible = false;
                    ddlStatus.Visible = false;
                    TaxId.Value = "0";
                }
            }
        }

        public void SetTaxDetails(DataTable TaxMstDetails)
        {
            DataRow row = TaxMstDetails.Rows[0];
            Tax.Text = row["Tax"].ToString();
            Tax.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            TaxPercentage.Text = row["TaxPercentage"].ToString();
            ddlTaxCode.SelectedValue = row["TaxCode"].ToString();
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            CreatedDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CreatedDate.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            TaxId.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (TaxId.Value != "0" && TaxId.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                    msgstatus = XBDataProvider.TaxMst.UpdateTaxMst(Convert.ToInt32(TaxId.Value), Name.Text,TaxPercentage.Text, User.Identity.Name, status);
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
                    msgstatus = XBDataProvider.TaxMst.SaveTaxMst(hdncompanycode.Value, Tax.Text, Name.Text, ddlTaxCode.SelectedValue, TaxPercentage.Text, reference, User.Identity.Name, true);

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