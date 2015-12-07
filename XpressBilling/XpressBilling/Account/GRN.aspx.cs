using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class GRN : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadGRNList();
            }
        }

        private void LoadGRNList()
        {
            ListGRN.DataSource = XBDataProvider.GRN.GetAllGRN(Session["CompanyCode"].ToString());
            ListGRN.DataBind();
        }

        protected void GRNPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListGRN.PageIndex = e.NewPageIndex;
            LoadGRNList();
        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListGRN.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.GRN.DeleteGRN(ids);
            LoadGRNList();
        }
    }
}