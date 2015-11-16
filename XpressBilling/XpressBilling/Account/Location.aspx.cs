using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class Location : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadLocationList();
            }
        }

        protected void listLocationPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listLocation.PageIndex = e.NewPageIndex;
            LoadLocationList();
        }
        private void LoadLocationList()
        {
            listLocation.DataSource = XBDataProvider.Location.GetAllLocations(Session["CompanyCode"].ToString());
            listLocation.DataBind();
        }

        protected void listLocationDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listLocation.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("LocationDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void LocationDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdLocation"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.Location.ActivateLocation(companyId);
                }
                else
                {
                    XBDataProvider.Location.DeActivateLocation(companyId);
                }
                LoadLocationList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listLocation.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.Location.DeleteLocation(ids);
            LoadLocationList();
        }
    }
}