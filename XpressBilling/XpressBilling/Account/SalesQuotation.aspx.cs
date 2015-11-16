using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class SalesQuotation : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSalesQuotationList();
            }
        }

        private void LoadSalesQuotationList()
        {
            ListSalesQuotation.DataSource = XBDataProvider.SalesQuotation.GetAllSalesQuotation();
            ListSalesQuotation.DataBind();
        }

        protected void SalesQuotationPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListSalesQuotation.PageIndex = e.NewPageIndex;
            LoadSalesQuotationList();
        }

       

        

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListSalesQuotation.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.SalesQuotation.DeleteSalesQuotation(ids);
            LoadSalesQuotationList();
        }
    }
}