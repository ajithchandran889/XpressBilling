using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BankCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBankCodeList();
            }
        }

        protected void listBankCodePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBankCode.PageIndex = e.NewPageIndex;
            LoadBankCodeList();
        }
        private void LoadBankCodeList()
        {
            listBankCode.DataSource = XBDataProvider.BankCode.GetBankCode();
            listBankCode.DataBind();
        }
        protected void listBankCodeDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listBankCode.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("BankCodeDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void BankCodeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
               int companyId = Convert.ToInt32(ddl.Attributes["IdBankCode"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.BankCode.ActivateBankCode(companyId);
                }
                else
                {
                    XBDataProvider.BankCode.DeActivateBankCode(companyId);
                }
                LoadBankCodeList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listBankCode.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.BankCode.DeleteBankCodes(ids);
            LoadBankCodeList();
        }

    }
}