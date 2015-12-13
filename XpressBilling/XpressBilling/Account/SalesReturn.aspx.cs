using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class SalesReturn : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadSalesReturnList();
            }
        }

        private void LoadSalesReturnList()
        {
            ListSalesReturn.DataSource = XBDataProvider.SalesRetrun.GetAllSalesReturn(Session["CompanyCode"].ToString());
            ListSalesReturn.DataBind();
        }

        protected void SalesReturnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListSalesReturn.PageIndex = e.NewPageIndex;
            LoadSalesReturnList();
        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListSalesReturn.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.SalesRetrun.DeleteSalesReturn(ids);
            LoadSalesReturnList();
        }
    }
}