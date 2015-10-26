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
            if (!IsPostBack)
            {
                LoadEmployeeList();
            }
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
        protected void listEmployeeDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listEmployee.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("EmployeeDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void EmployeeDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdEmployee"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Employee.ActivateEmployee(companyId);
                }
                else
                {
                    XBDataProvider.Employee.DeActivateEmployee(companyId);
                }
                LoadEmployeeList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listEmployee.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Employee.DeleteEmployee(ids);
            LoadEmployeeList();
        }
    }
}