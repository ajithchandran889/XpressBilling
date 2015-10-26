using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class TaxCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadTaxList();
            }
        }

        protected void listTaxCodePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listTaxCode.PageIndex = e.NewPageIndex;
            LoadTaxList();
        }
        private void LoadTaxList()
        {
            listTaxCode.DataSource = XBDataProvider.TaxCode.GetTaxCodes();
            listTaxCode.DataBind();
        }

        protected void listTaxCodeDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listTaxCode.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("TaxCodeDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void TaxCodeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdTaxCode"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.TaxCode.ActivateTaxCode(companyId);
                }
                else
                {
                    XBDataProvider.TaxCode.DeActivateTaxCode(companyId);
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listTaxCode.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.TaxCode.DeleteTaxCode(ids);
            LoadTaxList();
        }
    }
}