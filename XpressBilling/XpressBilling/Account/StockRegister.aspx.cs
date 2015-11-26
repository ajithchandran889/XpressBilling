using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class StockRegister : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                DataTable dtTable = XBDataProvider.ItemMaster.GetAllItemMaster(Session["CompanyCode"].ToString());
                Session["ItemMaster"] = dtTable;
                ItemCode.DataSource = dtTable;
                ItemCode.DataValueField = "ItemCode";
                ItemCode.DataTextField = "ItemCode";
                ItemCode.DataBind();
                ListItem item = new ListItem();
                item.Text = "Select Item";
                item.Value = "";
                ItemCode.Items.Insert(0, item);
            }
           
        }

        protected void SearchBtnClick(object sender, EventArgs e)
        {
            try
            {
                DateTime periodFrom = DateTime.ParseExact(PeriodFrom.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DateTime periodTo = DateTime.ParseExact(PeriodTo.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DataTable dtTable = XBDataProvider.StockRegister.GetItemDetails(ItemCode.SelectedValue, Location.SelectedValue, periodFrom,periodTo);
                if(dtTable.Rows.Count>0)
                {
                    int i = 0;
                    DataTable dt = new DataTable();
                    DataRow dr = null;
                    dt.Columns.Add(new DataColumn("Transaction", typeof(string)));
                    dt.Columns.Add(new DataColumn("DocNo", typeof(string)));
                    dt.Columns.Add(new DataColumn("DocDate", typeof(string)));
                    dt.Columns.Add(new DataColumn("BaseUnit", typeof(string)));
                    dt.Columns.Add(new DataColumn("InQty", typeof(string)));
                    dt.Columns.Add(new DataColumn("OutQnty", typeof(string)));
                    dt.Columns.Add(new DataColumn("AvilableQnty", typeof(string)));
                    dt.Columns.Add(new DataColumn("UnitCost", typeof(string)));
                    dt.Columns.Add(new DataColumn("Total", typeof(string)));
                    dr = dt.NewRow();
                    dr["Transaction"] = "Opening";
                    dr["DocNo"] = string.Empty;
                    dr["DocDate"] = string.Empty;
                    dr["BaseUnit"] = dtTable.Rows[0]["BaseUnit"].ToString();
                    dr["InQty"] = string.Empty;
                    dr["OutQnty"] = string.Empty;
                    int availableQty = 0;
                    if (dtTable.Rows[0]["InQty"].ToString() == "0")
                    {
                        availableQty = Convert.ToInt32(dtTable.Rows[0]["OutQty"]) + Convert.ToInt32(dtTable.Rows[0]["AvilableQnty"]);
                        dr["AvilableQnty"] = availableQty.ToString();
                    }
                    else
                    {
                        availableQty = Convert.ToInt32(dtTable.Rows[0]["AvilableQnty"]) - Convert.ToInt32(dtTable.Rows[0]["OutQty"]);
                        dr["AvilableQnty"] = availableQty.ToString();
                    }
                    dr["UnitCost"] = string.Empty;
                    dr["Total"] = string.Empty;
                    dt.Rows.Add(dr);
                    foreach(DataRow row in dtTable.Rows)
                    {
                        dr = dt.NewRow();
                        if(row["Transactions"].ToString()=="1")
                        {
                            dr["Transaction"] = "Opening";
                        }
                        else if (row["Transactions"].ToString() == "2")
                        {
                            dr["Transaction"] = "Addition";
                        }
                        else if (row["Transactions"].ToString() == "3")
                        {
                            dr["Transaction"] = "Deduction";
                        }
                        else if (row["Transactions"].ToString() == "4")
                        {
                            dr["Transaction"] = "Sales Invoice";
                        }
                        
                        dr["DocNo"] = row["DocumentNo"].ToString();
                        dr["DocDate"] = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy"); ;
                        dr["BaseUnit"] = row["BaseUnit"].ToString();
                        dr["InQty"] = row["InQty"].ToString() == "0" ? "" : row["InQty"].ToString();
                        dr["OutQnty"] = row["OutQty"].ToString() == "0" ? "" : row["OutQty"].ToString();
                        dr["AvilableQnty"] = row["AvilableQnty"].ToString();
                        dr["UnitCost"] = row["ItemCost"].ToString();
                        dr["Total"] = Convert.ToInt32(row["AvilableQnty"]) + Convert.ToInt32(row["ItemCost"]);
                        dt.Rows.Add(dr);
                    }
                    gridDetails.Visible = true;
                    message.Visible = false;
                    StockRegisterDetail.DataSource = dt;
                    StockRegisterDetail.DataBind();
                }
                else
                {
                    gridDetails.Visible = false;
                    message.Visible = true;
                    message.Text = "Sorry,No data found";
                }
            }
            catch(Exception ex)
            {

            }
        }


        protected void ItemCodeSelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dtTable = Session["ItemMaster"] as DataTable;
            foreach(DataRow row in dtTable.Rows)
            {
                if(row["ItemCode"].ToString() == ItemCode.SelectedValue)
                {
                    ItemName.Text = row["Name"].ToString();
                    DataTable dtLocation = XBDataProvider.StockRegister.GetLocationByItemCode(ItemCode.SelectedValue);
                    Location.DataSource = dtLocation;
                    Location.DataValueField = "LocationCode";
                    Location.DataTextField = "LocationCode";
                    Location.DataBind();
                }
            }
        }
    }
}