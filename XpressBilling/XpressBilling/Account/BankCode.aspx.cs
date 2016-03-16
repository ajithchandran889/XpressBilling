using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class BankCode : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                LoadBankCodeList();
            }
        }

        protected void listBankCodePageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            listBankCode.PageIndex = e.NewPageIndex;
            LoadBankCodeList();
        }
        private void LoadBankCodeList()
        {
            listBankCode.DataSource = XBDataProvider.BankCode.GetBankCode(Session["CompanyCode"].ToString());
            listBankCode.DataBind();
        }
        protected void listBankCodeDataBound(object sender, EventArgs e)
        {
            //GridViewRow row = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Normal);
            //for (int i = 0; i < listBankCode.Columns.Count; i++)
            //{
            //    if (listBankCode.Columns[i].HeaderText == "BankCode" || listBankCode.Columns[i].HeaderText == "Name")
            //    {
            //        TableHeaderCell cell = new TableHeaderCell();
            //        TextBox txtSearch = new TextBox();
            //        txtSearch.Width = 70;
            //        txtSearch.Attributes["placeholder"] = listBankCode.Columns[i].HeaderText;
            //        //txtSearch.CssClass = "search_textbox";
            //        cell.Controls.Add(txtSearch);
            //        row.Controls.Add(cell);
            //    }
            //    else
            //    {
            //        TableHeaderCell cell = new TableHeaderCell();
            //        Label lbl = new Label();
            //        cell.Controls.Add(lbl);
            //        row.Controls.Add(cell);
            //    }
            //}
            //listBankCode.HeaderRow.Parent.Controls.AddAt(1, row);
        }
        //protected void BankCodeDdlSelectedIndexChanged(object sender, EventArgs e)
        //{
            //DropDownList ddl = sender as DropDownList;
            //try
            //{
            //   int companyId = Convert.ToInt32(ddl.Attributes["IdBankCode"]);
            //    if (ddl.SelectedValue == "1")
            //    {
            //        XBDataProvider.BankCode.ActivateBankCode(companyId);
            //    }
            //    else
            //    {
            //        XBDataProvider.BankCode.DeActivateBankCode(companyId);
            //    }
            //    LoadBankCodeList();
            //}
            //catch (Exception ex)
            //{

            //}

        //}
        protected void deleteRecordsClick(object sender, EventArgs e)
        {
            string ids = string.Empty;
            foreach (GridViewRow grow in listBankCode.Rows)
            {
                CheckBox chkdel = (CheckBox)grow.FindControl("chkDel");
                if (chkdel.Checked)
                {
                    HiddenField hfSelectedId = grow.FindControl("selectedId") as HiddenField;
                    ids += hfSelectedId.Value + ",";
                }
            }
            XBDataProvider.BankCode.DeleteBankCodes(ids);
            LoadBankCodeList();
        }
        //protected void searchbankcodeClick(object sender, EventArgs e)
        //{
        //    try
        //    {
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
        private void LoadBankCodeListBySearch()
        {
            listBankCode.DataSource = XBDataProvider.BankCode.GetBankCode(Session["CompanyCode"].ToString());
            listBankCode.DataBind();
        }

    }
}