using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class ManualInvoice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadManualInvoiceList();
            }
        }

        private void LoadManualInvoiceList()
        {
            ListManualInvoice.DataSource = XBDataProvider.ManualInvoice.GetAllManualInvoice(Session["CompanyCode"].ToString());
            ListManualInvoice.DataBind();
        }

        protected void ManualInvoicePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListManualInvoice.PageIndex = e.NewPageIndex;
            LoadManualInvoiceList();
        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListManualInvoice.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.ManualInvoice.DeleteManualInvoice(ids);
            LoadManualInvoiceList();
        }
    }
}