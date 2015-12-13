using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class PaymentModeEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id!=0)
                {
                    DataTable paymentModeDetails = XBDataProvider.PaymentMode.GetPaymentModeById(id);
                    if (paymentModeDetails.Rows.Count > 0)
                    {
                        SetPaymentModeDetails(paymentModeDetails);

                    }
                }
                else
                {
                    PaymentModeId.Value = "0";
                    PaymentMode.Visible = false;
                    lblPaymentMode.Visible = false;
                    CreatedUser.Visible = false;
                    lblCreatedUser.Visible = false;
                    CreatedDate.Visible = false;
                    lblCreatedDate.Visible = false;
                    lblstatus.Visible = false;
                    ddlStatus.Visible = false;
                }
            }
        }

        public void SetPaymentModeDetails(DataTable paymentModeDetails)
        {
            DataRow row = paymentModeDetails.Rows[0];
            PaymentModeId.Value = row["PaymentMode"].ToString();
            PaymentMode.Text = row["PaymentMode"].ToString();
            PaymentMode.ReadOnly = true;
            CreatedDate.Text = Convert.ToDateTime(row["CreatedDate"]).ToString("MM'/'dd'/'yyyy");
            CreatedDate.ReadOnly = true;
            Name.Text = row["Name"].ToString();
            CreatedUser.Text = row["CreatedBy"].ToString();
            CreatedUser.ReadOnly = true;
            Transaction.SelectedValue = row["Transactions"].ToString();
            if (row["Transactions"].ToString()=="1")
            {
                lblBankDetail.Text = "Bank Code";
                //ddlBankAccount.SelectedValue = row["BankCode"].ToString();
                BankAccount.Text = row["BankCode"].ToString();
            }
            else
            {
                BankAccount.Text = row["AccountNo"].ToString();
                lblBankDetail.Text = "Bank Account";
            }
            

        }

        protected void SaveClick(object sender, EventArgs e)
        {
            try
            {
                bool status = false;
                if (PaymentModeId.Value != "0" && PaymentModeId.Value != "")
                {
                    bool statusflag;
                    if (ddlStatus.SelectedValue == "0")
                        statusflag = false;
                    else
                        statusflag = true;
                    string hg = Transaction.SelectedValue;
                    status = XBDataProvider.PaymentMode.UpdatePaymentMode(PaymentModeId.Value, Name.Text, Convert.ToInt32(Transaction.SelectedValue), BankAccount.Text, User.Identity.Name, statusflag);
                    if (status)
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
                    status = XBDataProvider.PaymentMode.SavePaymentMode(Session["CompanyCode"].ToString(), Name.Text, Convert.ToInt32(Transaction.SelectedValue), BankAccount.Text, User.Identity.Name);
                    if (status)
                    {
                        ClearInputs(Page.Controls);
                        SaveSuccess.Visible =true;
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