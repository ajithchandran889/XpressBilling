using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
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
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
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
            listTaxMst.DataSource = XBDataProvider.TaxMst.GetAllTax(Session["CompanyCode"].ToString());
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

        [WebMethod]
        public static List<TaxItemDetail> GetAllTaxDetails(string companyCode)
        {
            List<TaxItemDetail> result = new List<TaxItemDetail>();
            try
            {
                DataTable dtTable = XBDataProvider.TaxMst.GetAllTaxDetails(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    TaxItemDetail itemTax = new TaxItemDetail();
                    itemTax.code = row["TaxCode"].ToString();
                    itemTax.Per = Convert.ToInt32(row["TaxPercentage"].ToString());
                    result.Add(itemTax);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }
    }
    public class TaxItemDetail
    {
        public string code { get; set; }
        public int Per { get; set; }
    }
}