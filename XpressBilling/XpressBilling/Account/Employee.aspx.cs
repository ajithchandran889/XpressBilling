using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Employee : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            LoadEmployeeList();
        }

        protected void listEmployeePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listEmployee.PageIndex = e.NewPageIndex;
            LoadEmployeeList();
        }
        private void LoadEmployeeList()
        {
            listEmployee.DataSource = XBDataProvider.Employee.GetAllEmployee();
            listEmployee.DataBind();
        }
    }
}