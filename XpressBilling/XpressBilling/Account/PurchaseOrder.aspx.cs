using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class PurchaseOrder : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadPurchaseOrderList();
            }
        }

        private void LoadPurchaseOrderList()
        {
            ListPurchaseOrder.DataSource = XBDataProvider.PurchaseOrder.GetAllPurchaseOrder();
            ListPurchaseOrder.DataBind();
        }

        protected void PurchaseOrderPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListPurchaseOrder.PageIndex = e.NewPageIndex;
            LoadPurchaseOrderList();
        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListPurchaseOrder.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.SalesQuotation.DeleteSalesQuotation(ids);
            LoadPurchaseOrderList();
        }
    }
}