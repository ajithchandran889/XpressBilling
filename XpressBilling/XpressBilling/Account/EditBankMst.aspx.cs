using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class EditBankMst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtbankcode = XBDataProvider.BankMst.GetAllActiveBankCode(Session["CompanyCode"].ToString());

                ddlbankcode.DataSource = dtbankcode;
                ddlbankcode.DataValueField = "BankCode";
                ddlbankcode.DataTextField = "Name";
                ddlbankcode.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Bank";
                item.Value = "";
                ddlbankcode.Items.Insert(0, item);
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != 0)
                {
                    DataTable BankMstDetails = XBDataProvider.BankMst.GetBankMstById(id);
                    if (BankMstDetails.Rows.Count > 0)
                    {
                        SetBankMstDetails(BankMstDetails);
                    }
                }
                else
                {
                    txtaccounttype.Visible = false;
                    lbldate.Visible = false;
                    lblstatus.Visible = false;
                    lblusername.Visible = false;
                    UserName.Visible = false;
                    CreatedDate.Visible = false;
                    ddlStatus.Visible = false;
                    hdnBankCode.Value = "0";
                }
            }
        }

        public void SetBankMstDetails(DataTable BankMstDetails)
        {
            DataRow row = BankMstDetails.Rows[0];
            AccountNo.Text = row["AccountNo"].ToString();            
            Branch.Text = row["Branch"].ToString();
            ddlbankcode.SelectedValue = row["BankCode"].ToString();
            //ddlAccountType.SelectedValue = row["AccountType"].ToString();   
           if(row["AccountType"].ToString()=="0")
            txtaccounttype.Text = "Savings";
           else
               txtaccounttype.Text = "Current";
            ddlAccountType.Visible = false;
            txtaccounttype.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            IBAN.Text = row["IBAN"].ToString();
            SWIFT.Text = row["SWIFT"].ToString();
            IFSC.Text = row["IFSC"].ToString();
            MICR.Text = row["MICR"].ToString();
            //ContactCode.Text = row["ContactCode"].ToString();
            Reference.Text = row["Reference"].ToString();
            UserName.Text = row["CreatedBy"].ToString();
            UserName.ReadOnly = true;
            CreatedDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CreatedDate.ReadOnly = true;
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
                    string ContactCode = "1";
                    msgstatus = XBDataProvider.BankMst.UpdateBankMst(Convert.ToInt32(hdnBankCode.Value), AccountNo.Text, Name.Text, Branch.Text, ContactCode, Reference.Text, IBAN.Text, IFSC.Text, SWIFT.Text, MICR.Text, ddlbankcode.SelectedValue, User.Identity.Name, status);
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
                    string ContactCode="1";
                    msgstatus = XBDataProvider.BankMst.SaveBankMst(hdncompanycode.Value, AccountNo.Text, Name.Text, Branch.Text, ddlbankcode.SelectedValue, ddlAccountType.SelectedValue, ContactCode,Reference.Text, IBAN.Text, IFSC.Text, SWIFT.Text, MICR.Text, User.Identity.Name, true);
                    ClearInputs(Page.Controls);
                    if (msgstatus == 1)
                    {
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
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
                failure.Visible = true;
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