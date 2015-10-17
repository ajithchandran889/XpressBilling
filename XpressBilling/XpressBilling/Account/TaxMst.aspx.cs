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
            LoadTaxList();
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
    }
}