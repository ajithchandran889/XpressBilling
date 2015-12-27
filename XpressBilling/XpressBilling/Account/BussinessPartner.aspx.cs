using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BussinessPartner : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadBPList();
            }

        }   

        protected void BussinessPartnerPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListBussnessPartner.PageIndex = e.NewPageIndex;
            LoadBPList();
        }
        private void LoadBPList()
        {
            ListBussnessPartner.DataSource = XBDataProvider.BussinessPartner.GetAllBussinessPartner(Session["CompanyCode"].ToString());
            ListBussnessPartner.DataBind();
        }
        protected void listBussnessPartnerDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in ListBussnessPartner.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("BussinessPartnerDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void BussinessPartnerDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdBussinessPartner"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.BussinessPartner.ActivateBP(companyId);
                }
                else
                {
                    XBDataProvider.BussinessPartner.DeActivateBP(companyId);
                }
                LoadBPList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListBussnessPartner.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.BussinessPartner.DeleteBP(ids);
            LoadBPList();
        }
    }
}