using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Globalization;

namespace XpressBilling.Account
{
    public partial class EditBankCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != 0)
                {
                    DataTable BankCodeDetails = XBDataProvider.BankCode.GetBankCodeById(id);
                    if (BankCodeDetails.Rows.Count > 0)
                    {
                        SetBankCodeDetails(BankCodeDetails);
                    }
                }
                else
                {
                    lbldate.Visible = false;
                    lblstatus.Visible = false;
                    lblusername.Visible = false;
                    UserName.Visible = false;
                    CretdDate.Visible = false;
                    ddlStatus.Visible = false;
                    hdnBankCode.Value = "0";
                }
            }
        }

        public void SetBankCodeDetails(DataTable BankCodeDetails)
        {
            DataRow row = BankCodeDetails.Rows[0];
            BankCode.Text = row["BankCode"].ToString();
            BankCode.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            UserName.Text = row["Reference"].ToString();
            UserName.ReadOnly = true;
            CretdDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CretdDate.ReadOnly = true;
            ddlStatus.SelectedValue = row["Status"].ToString();
            hdnBankCode.Value = row["ID"].ToString();

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                int msgstatus = 0;
                hdncompanycode.Value = Session["CompanyCode"].ToString();
                if (hdnBankCode.Value != "0" && hdnBankCode.Value != null)
                {
                    bool status;
                    if (ddlStatus.SelectedValue == "0")
                        status = false;
                    else
                        status = true;
                   msgstatus= XBDataProvider.BankCode.UpdateBankCode(Convert.ToInt32(hdnBankCode.Value), Name.Text, User.Identity.Name, status);
                   if (msgstatus == 1)
                   {
                       SaveSuccess.Visible =false;
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
                    msgstatus = XBDataProvider.BankCode.SaveBankCode(hdncompanycode.Value, BankCode.Text, Name.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, true);

                    
                    if (msgstatus == 1)
                    {
                        ClearInputs(Page.Controls); 
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