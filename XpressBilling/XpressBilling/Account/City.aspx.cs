using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class City : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCityList();
        }

        protected void listCityPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listCity.PageIndex = e.NewPageIndex;
            LoadCityList();
        }
        private void LoadCityList()
        {
            listCity.DataSource = XBDataProvider.City.GetAllCities();
            listCity.DataBind();
        }
    }
}