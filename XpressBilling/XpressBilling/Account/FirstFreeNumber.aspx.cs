using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class FirstFreeNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadFirstFreeNumberList();
            }
        }

        private void LoadFirstFreeNumberList()
        {
            ListFirstFreeNumber.DataSource = XBDataProvider.FirstFreeNumber.GetAllFirstFreeNumber();
            ListFirstFreeNumber.DataBind();
        }

        protected void FirstFreeNumberPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListFirstFreeNumber.PageIndex = e.NewPageIndex;
            LoadFirstFreeNumberList();
        }

        protected void listFirstFreeNumberDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in ListFirstFreeNumber.Rows)
            {
                DropDownList ddlFirstFree = gvRow.FindControl("FirstFreeDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlFirstFree != null && hfSelectedValue != null)
                {

                    ddlFirstFree.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void FirstFreeNumberDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int firstFreeNumberId = Convert.ToInt32(ddl.Attributes["IdFirstFreeNumber"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.FirstFreeNumber.ActivateFirstFreeNumber(firstFreeNumberId);
                }
                else
                {
                    XBDataProvider.FirstFreeNumber.DeActivateFirstFreeNumber(firstFreeNumberId);
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListFirstFreeNumber.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.FirstFreeNumber.DeleteFirstFreeNumber(ids);
            LoadFirstFreeNumberList();
        }
    }
}