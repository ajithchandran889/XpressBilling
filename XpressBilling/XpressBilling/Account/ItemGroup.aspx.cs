using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class ItemGroup : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadItemGroupList();
            }
        }

        protected void listItemGroupPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listItemGroup.PageIndex = e.NewPageIndex;
            LoadItemGroupList();
        }
        private void LoadItemGroupList()
        {
            listItemGroup.DataSource = XBDataProvider.ItemGroup.GetAllItemGroup(Session["CompanyCode"].ToString());
            listItemGroup.DataBind();
        }

        protected void listItemGroupDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in listItemGroup.Rows)
            {
                DropDownList ddlCompanyUser = gvRow.FindControl("ItemGroupDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlCompanyUser != null && hfSelectedValue != null)
                {

                    ddlCompanyUser.SelectedValue = hfSelectedValue.Value;
                }
            }
        }
        protected void ItemGroupDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int companyId = Convert.ToInt32(ddl.Attributes["IdItemGroup"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.ItemGroup.ActivateItemGroup(companyId);
                }
                else
                {
                    XBDataProvider.ItemGroup.DeActivateItemGroup(companyId);
                }
                LoadItemGroupList();
            }
            catch (Exception ex)
            {

            }

        }
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listItemGroup.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.ItemGroup.DeleteItemGroup(ids);
            LoadItemGroupList();
        }
    }
}