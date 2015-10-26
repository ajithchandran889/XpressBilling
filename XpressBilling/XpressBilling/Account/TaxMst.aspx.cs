using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class TaxMst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTaxList();
            }
        }

        protected void listTaxMstPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listTaxMst.PageIndex = e.NewPageIndex;
            LoadTaxList();
        }
        private void LoadTaxList()
        {
            listTaxMst.DataSource = XBDataProvider.TaxMst.GetAllTax();
            listTaxMst.DataBind();
        }

        protected void listTaxMstDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listTaxMst.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("TaxMstDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void TaxMstDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdTaxMst"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.TaxMst.ActivateTaxMst(companyId);
                }
                else
                {
                    XBDataProvider.TaxMst.DeActivateTaxMst(companyId);
                }
                LoadTaxList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listTaxMst.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.TaxMst.DeleteTaxMst(ids);
            LoadTaxList();
        }
    }
}