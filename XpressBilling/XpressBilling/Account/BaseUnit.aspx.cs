using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BaseUnit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadBaseUnitList();
            }
        }

        protected void listBaseUnitPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBaseUnit.PageIndex = e.NewPageIndex;
            LoadBaseUnitList();
        }
        private void LoadBaseUnitList()
        {
            listBaseUnit.DataSource = XBDataProvider.BaseUnit.GetBaseUnits();
            listBaseUnit.DataBind();
        }

        protected void listBaseUnitDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listBaseUnit.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("BaseUnitDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void BaseUnitDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdBaseUnit"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.BaseUnit.ActivateBaseUnit(companyId);
                }
                else
                {
                    XBDataProvider.BaseUnit.DeActivateBaseUnit(companyId);
                }
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listBaseUnit.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.BaseUnit.DeleteBaseUnit(ids);
            LoadBaseUnitList();
        }
    }
}