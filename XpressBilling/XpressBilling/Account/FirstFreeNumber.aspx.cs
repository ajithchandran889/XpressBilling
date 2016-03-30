using System;
using System.Collections.Generic;
using System.Data;
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
            DataTable dtTable = XBDataProvider.FirstFreeNumber.GetAllFirstFreeNumber();
            DataColumn column = new DataColumn("TransactionType", typeof(String));
            dtTable.Columns.Add(column);
            DataRow row = null;
            for (int index = 0; index < dtTable.Rows.Count;index++ )
            {
                row = dtTable.Rows[index];
                if (row["Transactions"].ToString() == "0")
                {
                    row["TransactionType"] = "Sales Quotation";
                }
                else if (row["Transactions"].ToString() == "1")
                {
                    row["TransactionType"] = "Sales Order";
                }
                else if (row["Transactions"].ToString() == "2")
                {
                    row["TransactionType"] = "Manual Invoice";
                }
                else if (row["Transactions"].ToString() == "3")
                {
                    row["TransactionType"] = "Sales Return";
                }
                else if (row["Transactions"].ToString() == "4")
                {
                    row["TransactionType"] = "Purchase Order";
                }
                else if (row["Transactions"].ToString() == "5")
                {
                    row["TransactionType"] = "Stock Adjustment";
                }
                else if (row["Transactions"].ToString() == "6")
                {
                    row["TransactionType"] = "Material Issue";
                }
                else if (row["Transactions"].ToString() == "7")
                {
                    row["TransactionType"] = "Sales Invoice";
                }
                else if (row["Transactions"].ToString() == "8")
                {
                    row["TransactionType"] = "Goods Receipt";
                }
                else if (row["Transactions"].ToString() == "11")
                {
                    row["TransactionType"] = "Receipt";
                }
            }
            ListFirstFreeNumber.DataSource = dtTable;
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