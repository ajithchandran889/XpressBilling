using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class ItemGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadItemGroupList();
        }

        protected void listItemGroupPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listItemGroup.PageIndex = e.NewPageIndex;
            LoadItemGroupList();
        }
        private void LoadItemGroupList()
        {
            listItemGroup.DataSource = XBDataProvider.ItemGroup.GetAllItemGroup();
            listItemGroup.DataBind();
        }
    }
}