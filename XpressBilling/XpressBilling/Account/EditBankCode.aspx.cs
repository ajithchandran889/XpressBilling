using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace XpressBilling.Account
{
    public partial class EditBankCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
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
                    Date.Visible = false;
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
            Date.Text = row["CreatedDate"].ToString();
            Date.ReadOnly = true;
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
                       lblMsg.InnerText = "Successfully updated";
                   }
                   else
                   {
                       lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                   }
                }
                else
                {
                    msgstatus = XBDataProvider.BankCode.SaveBankCode(hdncompanycode.Value, BankCode.Text, Name.Text, User.Identity.Name, User.Identity.Name, DateTime.Today, true);
                    if (msgstatus == 1)
                    {
                        lblMsg.InnerText = "Successfully added";
                    }
                    else
                    {
                        lblMsg.InnerText = "Oops..Something went wrong.Please try again";
                    }
                    ClearInputs(Page.Controls);
                }

                
            }
            catch (Exception ex)
            {

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