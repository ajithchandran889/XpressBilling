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
            LoadTaxList();
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
    }
}