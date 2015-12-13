using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class PriceBook : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadPriceBookList();
            }

        }

        protected void PriceBookPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ListPriceBook.PageIndex = e.NewPageIndex;
            LoadPriceBookList();
        }
        private void LoadPriceBookList()
        {
            ListPriceBook.DataSource = XBDataProvider.PriceBook.GetAllPriceBook(Session["CompanyCode"].ToString());
            ListPriceBook.DataBind();
        }
        protected void listPriceBookDataBound(object sender, EventArgs e)
        {
            foreach (GridViewRow gvRow in ListPriceBook.Rows)
            {
                DropDownList ddlPriceBook = gvRow.FindControl("PriceBookDdl") as DropDownList;
                HiddenField hfSelectedValue = gvRow.FindControl("selectedvalue") as HiddenField;

                if (ddlPriceBook != null && hfSelectedValue != null)
                {

                    ddlPriceBook.SelectedValue = hfSelectedValue.Value;
                }
            }
        }

        protected void PriceBookDdlSelectedIndexChanged(object sender, EventArgs e)
        {
            DropDownList ddl = sender as DropDownList;
            try
            {
                int priceBookId = Convert.ToInt32(ddl.Attributes["IdPriceBook"]);
                if (ddl.SelectedValue == "1")
                {
                    XBDataProvider.PriceBook.ActivatePriceBook(priceBookId);
                }
                else
                {
                    XBDataProvider.PriceBook.DeActivatePriceBook(priceBookId);
                }
            }
            catch (Exception ex)
            {

            }

        }

        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in ListPriceBook.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.PriceBook.DeletePriceBooks(ids);
            LoadPriceBookList();
        }
    }
}