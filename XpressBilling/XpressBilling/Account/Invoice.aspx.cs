using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Invoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadInvoiceList();
            }
        }

        private void LoadInvoiceList()
        {
            ListInvoice.DataSource = XBDataProvider.Invoice.GetAllInvoice(Session["CompanyCode"].ToString());
            ListInvoice.DataBind();
        }

        protected void InvoicePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListInvoice.PageIndex = e.NewPageIndex;
            LoadInvoiceList();
        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListInvoice.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Invoice.DeleteInvoice(ids);
            LoadInvoiceList();
        }
    }
}