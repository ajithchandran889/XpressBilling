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
            if (!IsPostBack)
            {
                LoadCountryList();
            }
        }

        protected void listCountryPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listCountry.PageIndex = e.NewPageIndex;
            LoadCountryList();
        }
        private void LoadCountryList()
        {
            listCountry.DataSource = XBDataProvider.Country.GetCountries();
            listCountry.DataBind();
        }
        protected void listCountryDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listCountry.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("CountryDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void CountryDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdCountry"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Country.ActivateCountry(companyId);
                }
                else
                {
                    XBDataProvider.Country.DeActivateCountry(companyId);
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listCountry.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Country.DeleteCountry(ids);
            LoadCountryList();
        }
    }
}