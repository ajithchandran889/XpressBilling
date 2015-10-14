using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Country : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCountryList();
        }

        protected void listCurrencyPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listCurrency.PageIndex = e.NewPageIndex;
            LoadCountryList();
        }
        private void LoadCountryList()
        {
            listCurrency.DataSource = XBDataProvider.Country.GetCountries();
            listCurrency.DataBind();
        }
    }
}