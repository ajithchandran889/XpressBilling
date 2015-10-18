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
            if(!IsPostBack)
            {
                LoadCompanyList();
            }
            
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
        protected void listCompanyDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listCompany.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("CompanyStatusDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {
                    
                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void CompanyStatusDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdCompany"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Company.ActivateCompany(companyId);
                }
                else
                {
                    XBDataProvider.Company.DeActivateCompany(companyId);
                }
            }
            catch(Exception ex)
            {

            }
            
        }
    }
}