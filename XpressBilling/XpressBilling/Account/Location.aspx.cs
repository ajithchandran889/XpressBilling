using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Location : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadLocationList();
        }

        protected void listLocationPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listLocation.PageIndex = e.NewPageIndex;
            LoadLocationList();
        }
        private void LoadLocationList()
        {
            listLocation.DataSource = XBDataProvider.Location.GetAllLocations();
            listLocation.DataBind();
        }
    }
}