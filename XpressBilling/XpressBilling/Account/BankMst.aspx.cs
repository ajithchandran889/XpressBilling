using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BankMst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBankMstList();
            }
        }

        protected void listBankMstPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBankMst.PageIndex = e.NewPageIndex;
            LoadBankMstList();
        }
        private void LoadBankMstList()
        {
            listBankMst.DataSource = XBDataProvider.BankMst.GetAllBankMst();
            listBankMst.DataBind();
        }
        protected void listBankCodeDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listBankMst.Rows)
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
                    XBDataProvider.BankMst.ActivateBankCode(companyId);
                }
                else
                {
                    XBDataProvider.BankMst.DeActivateBankCode(companyId);
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listBankMst.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.BankMst.DeleteBankCodes(ids);
            LoadBankMstList();
        }
    }
}