using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BankMst : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadBankMstList();
        }

        protected void listBankMstPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBankMst.PageIndex = e.NewPageIndex;
            LoadBankMstList();
        }
        private void LoadBankMstList()
        {
            listBankMst.DataSource = XBDataProvider.BankMst.GetAllBankMst();
            listBankMst.DataBind();
        }
    }
}