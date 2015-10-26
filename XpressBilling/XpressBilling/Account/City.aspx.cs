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
            if (!IsPostBack)
            {
                LoadCityList();
            }
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
        protected void listCityDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listCity.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("CityDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void CityDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdCity"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.City.ActivateCity(companyId);
                }
                else
                {
                    XBDataProvider.City.DeActivateCity(companyId);
                }
                LoadCityList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listCity.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.City.DeleteCity(ids);
            LoadCityList();
        }
    }
}