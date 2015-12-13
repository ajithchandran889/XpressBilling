using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Manufacturer : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadManufacturerList();
            }
        }

        protected void listManufacturerPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listManufacturer.PageIndex = e.NewPageIndex;
            LoadManufacturerList();
        }
        private void LoadManufacturerList()
        {
            listManufacturer.DataSource = XBDataProvider.Manufacturer.GetAllManufacturer(Session["CompanyCode"].ToString());
            listManufacturer.DataBind();
        }

        protected void listManufacturerDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listManufacturer.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("ManufacturerDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void ManufacturerDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdManufacturer"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Manufacturer.ActivateManufacturer(companyId);
                }
                else
                {
                    XBDataProvider.Manufacturer.DeActivateManufacturer(companyId);
                }
                LoadManufacturerList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listManufacturer.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Manufacturer.DeleteManufacturer(ids);
            LoadManufacturerList();
        }
    }
}