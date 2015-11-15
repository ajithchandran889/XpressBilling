using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class ItemMaster : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadItemMasterList();
              
            }

        }

        protected void ItemMasterPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListItemMaster.PageIndex = e.NewPageIndex;
            LoadItemMasterList();
        }
        private void LoadItemMasterList()
        {
            ListItemMaster.DataSource = XBDataProvider.ItemMaster.GetAllItemMaster();
            ListItemMaster.DataBind();
        }
        protected void ItemMasterModeDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in ListItemMaster.Rows)
            {
                DropDownList ddlItemMaster = gvRow.FindControl("ItemMasterDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlItemMaster != null && hfSelectedValue != null)
                {

                    ddlItemMaster.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void ItemMasterDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int id = Convert.ToInt32(ddl.Attributes["IdItemMaster"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.ItemMaster.ActivateItemMaster(id);
                }
                else
                {
                    XBDataProvider.ItemMaster.DeActivateItemMaster(id);
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListItemMaster.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.ItemMaster.DeleteItemMasters(ids);
            LoadItemMasterList();
        }
    }
}