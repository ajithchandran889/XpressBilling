using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BaseUnit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBaseUnitList();
        }

        protected void listBaseUnitPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBaseUnit.PageIndex = e.NewPageIndex;
            LoadBaseUnitList();
        }
        private void LoadBaseUnitList()
        {
            listBaseUnit.DataSource = XBDataProvider.BaseUnit.GetBaseUnits();
            listBaseUnit.DataBind();
        }
    }
}