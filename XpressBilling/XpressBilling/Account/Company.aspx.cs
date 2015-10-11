using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Company : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadCompanyList();
        }

        protected void listCompanyPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listCompany.PageIndex = e.NewPageIndex;
            LoadCompanyList();
        }
        private void LoadCompanyList()
        {
            listCompany.DataSource = XBDataProvider.Company.GetAllCompanies();
            listCompany.DataBind();
        }
    }
}