using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace XpressBilling.Account
{
    public partial class PriceBookEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                CompanyCode.Value = Session["CompanyCode"].ToString();
                string lastDocNumber = XBDataProvider.PriceBook.GetDocumentNumber();
                if (lastDocNumber == null || lastDocNumber == "")
                {
                    lastDocNumber = "0";
                }
                else
                {
                    lastDocNumber = lastDocNumber.Replace('D', ' ');
                }
                LastDocumentNumber.Value = lastDocNumber;
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    PageStatus.Value = "edit";
                    DataTable priceBookDetails = XBDataProvider.PriceBook.GetPriceBookById(id);
                    if (priceBookDetails.Rows.Count > 0)
                    {
                        SetPriceBookDetails(priceBookDetails);

                    }
                }
                else
                {

                    PageStatus.Value = "create";
                    PriceBookId.Value = "0";
                    Currency.Text = XBDataProvider.Currency.GetCurrencyCodeByCompany(Session["CompanyCode"].ToString()); 
                    CreatedUser.Visible = false;
                    CreatedDate.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                    //txtSearch.Visible = false;
                    //btnSearch.Visible = false;
                    gridDetails.Visible = false;
                    savePriceBook.Visible = false;
                    filterArea.Visible = false;
                    SetInitialRow();
                    DataTable dtAddedOrderTypes = XBDataProvider.PriceBook.GetAddedOrderTypes(Session["CompanyCode"].ToString());
                    DataRow row = null;
                    for (int i = 0; i < dtAddedOrderTypes.Rows.Count;i++)
                    {
                        row = dtAddedOrderTypes.Rows[i];
                        if (row["PriceType"].ToString()=="0")
                        {
                            OrderType_0.Items.Remove(OrderType_0.Items.FindByValue(row["OrderType"].ToString()));
                        }
                        else if (row["PriceType"].ToString() == "1")
                        {
                            OrderType_1.Items.Remove(OrderType_0.Items.FindByValue(row["OrderType"].ToString()));
                        }
                    }
                        
                }


            }
        }

        public void SetPriceBookDetails(DataTable priceBookDetails)
        {
            try
            {
                DataRow row = priceBookDetails.Rows[0];
                PriceBookId.Value = row["ID"].ToString();
                Type.SelectedValue = row["PriceType"].ToString();
                if (row["PriceType"].ToString() == "0")
                {
                    OrderType_1.Visible = false;
                    OrderType_0.Visible = true;
                    OrderType_0.SelectedValue = row["OrderType"].ToString();

                    OrderType_0.Enabled = false;
                }
                else if (row["PriceType"].ToString() == "1")
                {
                    OrderType_0.Visible = false;
                    OrderType_1.Visible = true;
                    OrderType_1.SelectedValue = row["OrderType"].ToString();
                    OrderType_1.Style.Add("display", "block");
                    OrderType_1.Enabled = false;
                }
                Currency.Text = row["CurrencyCode"].ToString();
                PriceBookDocNo.Text = row["DocumentNo"].ToString();
                CreatedDate.Text = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedUser.Text = row["CreatedBy"].ToString();
                Type.Enabled = false;
                SetPriceBookDetailsChildGrid();
            }
            catch (Exception e)
            {

            }


        }

        public void SetPriceBookDetailsChildGrid()
        {
            DataTable dt = new DataTable();
            //if (txtSearch.Text == "")
            //{
                dt = XBDataProvider.PriceBook.GetPriceBookDtlById(Convert.ToInt32(PriceBookId.Value));
                rowCount.Value = dt.Rows.Count.ToString();
            //}
            //else
            //{
            //    dt = XBDataProvider.PriceBook.GetPriceBookDtlByIdAndSearchKey(Convert.ToInt32(PriceBookId.Value), txtSearch.Text);

            //}
            if (dt.Rows.Count > 0)
            {
                PriceBookDetail.DataSource = dt;
                PriceBookDetail.DataBind();
                return;
            }
            //SetInitialRow();

        }

        [WebMethod]
        public static List<ItemMasteDetails> GetItemMasters(string companyCode)
        {
            List<ItemMasteDetails> result = new List<ItemMasteDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.ItemMaster.GetItemMasters(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ItemMasteDetails itemMasteDetails = new ItemMasteDetails();
                    itemMasteDetails.code = row["ItemCode"].ToString();
                    itemMasteDetails.name = row["Name"].ToString();
                    itemMasteDetails.BaseUnitCode = row["BaseUnitCode"].ToString();
                    itemMasteDetails.supplierBarcode = row["SupplierBarcode"].ToString();
                    itemMasteDetails.mrp = Convert.ToInt32(row["MRP"].ToString());
                    itemMasteDetails.retailPrice = Convert.ToInt32(row["RetailPrice"].ToString());
                    itemMasteDetails.TaxCode = row["TaxCode"].ToString();
                    itemMasteDetails.TaxPer = row["TaxPercentage"].ToString() != "" ? Convert.ToInt32(row["TaxPercentage"].ToString()) : 0;
                    itemMasteDetails.Qnty = row["Qnty"].ToString() != "" ? Convert.ToInt32(row["Qnty"].ToString()) : 0;
                    result.Add(itemMasteDetails);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        private void SetPreviousData()
        {
            try
            {
                int rowIndex = 0;
                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dt = (DataTable)ViewState["CurrentTable"];
                    if (dt.Rows.Count > 0)
                    {
                        PriceBookDetail.DataSource = dt;
                        PriceBookDetail.DataBind();
                    }
                }
            }
            catch (Exception e)
            {

            }

        }

        private void SetInitialRow()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                dt.Columns.Add(new DataColumn("SupplierBarcode", typeof(string)));

                dt.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("MRP", typeof(float)));
                dt.Columns.Add(new DataColumn("Price", typeof(float)));
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["ItemCode"] = string.Empty;
                    dr["Name"] = string.Empty;
                    dr["SupplierBarcode"] = string.Empty;

                    dr["CurrencyCode"] = string.Empty;
                    dr["MRP"] = DBNull.Value;
                    dr["Price"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                PriceBookDetail.DataSource = dt;
                PriceBookDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void savePriceBookClick(object sender, EventArgs e)
        {
            try
            {
                string newDocumentNumber = "D" + (Convert.ToInt32(LastDocumentNumber.Value) + 1);
                int orderType = 0;
                if (Type.SelectedValue == "0")
                {
                    orderType = Convert.ToInt32(OrderType_0.SelectedValue);
                }
                else
                {
                    orderType = Convert.ToInt32(OrderType_1.SelectedValue);
                }
                DataTable dtDeletedIds = new DataTable();
                DataRow drDeletedIds = null;
                dtDeletedIds.Columns.Add(new DataColumn("ID", typeof(int)));
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("PriceBookMstID", typeof(int)));
                dt.Columns.Add(new DataColumn("DocumentNo", typeof(string)));
                dt.Columns.Add(new DataColumn("DocumentDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("PriceType", typeof(int)));
                dt.Columns.Add(new DataColumn("OrderType", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SupplierBarcode", typeof(string)));
                dt.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("MRP", typeof(float)));
                dt.Columns.Add(new DataColumn("Price", typeof(float)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("ApprovalStatus", typeof(int)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dt.Columns.Add(new DataColumn("Name", typeof(string)));
                int i = 0;
                int[] deletedIds = DeletedRowIDs.Value.Split(',').Where(str=>str!="").Select(str => int.Parse(str)).ToArray();
                for (int k = 0; k < deletedIds.Length;k++ )
                {
                    drDeletedIds = dtDeletedIds.NewRow();
                    drDeletedIds["ID"] = deletedIds[k];
                    dtDeletedIds.Rows.Add(drDeletedIds);
                }
                foreach (GridViewRow row in PriceBookDetail.Rows)
                {
                    TextBox box1 = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("ItemCode");
                    TextBox box2 = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("SupplierBarcode");
                    TextBox box3 = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("Description");
                    TextBox box4 = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("CurrencyCode");
                    TextBox box5 = (TextBox)PriceBookDetail.Rows[i].Cells[5].FindControl("MRP");
                    TextBox box6 = (TextBox)PriceBookDetail.Rows[i].Cells[6].FindControl("Price");
                    if (Array.IndexOf(deletedIds, PriceBookDetail.DataKeys[i]["ID"]) == -1 && box1.Text!="")
                    {
                        dr = dt.NewRow();
                        dr["ID"] = PriceBookDetail.DataKeys[i]["ID"];
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
                        dr["DocumentNo"] = newDocumentNumber;
                        dr["DocumentDate"] = DateTime.Now.Date;
                        dr["PriceType"] = Type.SelectedValue;
                        if (Type.SelectedValue == "0")
                        {
                            dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
                        }
                        else
                        {
                            dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
                        }
                        dr["ItemCode"] = box1.Text;
                        dr["SupplierBarcode"] = box2.Text;
                        dr["CurrencyCode"] = box4.Text;
                        dr["MRP"] = float.Parse(Convert.ToDecimal(box5.Text).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
                        dr["Price"] = float.Parse(Convert.ToDecimal(box6.Text).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
                        dr["Reference"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dr["ApprovalStatus"] = 0;
                        dr["Status"] = 1;
                        dr["Name"] = box3.Text;
                        dt.Rows.Add(dr);
                        i++;
                    }
                    
                }
                string[] Ids = Request.Form["ID"] != null ? Request.Form["ID"].Split(',') : new string[] { };
                string[] ItemCodes = Request.Form["ItemCode"] != null ? Request.Form["ItemCode"].Split(',') : new string[] { };
                string[] SupplierBarcodes = Request.Form["SupplierBarcode"] != null ? Request.Form["SupplierBarcode"].Split(',') : new string[] { };
                string[] CurrencyCodes = Request.Form["CurrencyCode"] != null ? Request.Form["CurrencyCode"].Split(',') : new string[] { };
                string[] MRPs = Request.Form["MRP"] != null ? Request.Form["MRP"].Split(',') : new string[] { };
                string[] Prices = Request.Form["Price"] != null ? Request.Form["Price"].Split(',') : new string[] { };
                string[] Names = Request.Form["Description"] != null ? Request.Form["Description"].Split(',') : new string[] { };
                for (int k = 0; k < ItemCodes.Length; k++)
                {
                    if (ItemCodes[k] != "")
                    {
                        dr = dt.NewRow();
                        dr["ID"] = Convert.ToInt32(Ids[k]);
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
                        dr["DocumentNo"] = newDocumentNumber;
                        dr["DocumentDate"] = DateTime.Now.Date;
                        dr["PriceType"] = Type.SelectedValue;
                        if (Type.SelectedValue == "0")
                        {
                            dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
                        }
                        else
                        {
                            dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
                        }
                        dr["ItemCode"] = ItemCodes[k];
                        dr["SupplierBarcode"] = SupplierBarcodes[k];
                        dr["CurrencyCode"] = CurrencyCodes[k];
                        dr["MRP"] = float.Parse(Convert.ToDecimal(MRPs[k]).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
                        dr["Price"] = float.Parse(Convert.ToDecimal(Prices[k]).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
                        dr["Reference"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dr["ApprovalStatus"] = 0;
                        dr["Status"] = 1;
                        dr["Name"] = Names[k];
                        dt.Rows.Add(dr);
                    }
                }
                if (Convert.ToInt32(PriceBookId.Value) > 0)
                {
                    XBDataProvider.PriceBook.SavePriceBookMasterDetail(dt, dtDeletedIds);
                    SaveSuccess.Visible = true;
                }
                else
                {
                    int id = XBDataProvider.PriceBook.SavePriceBookMaster(Session["CompanyCode"].ToString(), newDocumentNumber, Convert.ToInt32(Type.SelectedValue), orderType, Currency.Text, User.Identity.Name, dt);
                    if (id > 0)
                    {
                        filterArea.Visible = true;
                        Type.Enabled = false;
                        OrderType_0.Enabled = false;
                        OrderType_1.Enabled = false;
                        PriceBookDocNo.Text = newDocumentNumber;
                        PriceBookId.Value = id.ToString();
                        SaveSuccess.Visible = true;
                        PageStatus.Value = "edit";
                    }
                }
                //SetPriceBookDetailsChildGrid();
            }
            catch (Exception ex)
            {

            }


        }

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            //SetPriceBookDetailsChildGrid();
        }

        protected void PriceBookDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbl = (Label)e.Row.Cells[0].FindControl("indexIcrement");
            string txt = e.Row.Cells[0].Text == "No:" ? "header" : (lbl == null ? String.Empty : lbl.Text);
            if (txt == string.Empty)
                e.Row.Visible = false;
            if (e.Row.RowIndex==0)
            {
                e.Row.Cells[7].Visible = false;
            }
        }

        protected void ddlCurrencySelectedIndexChanged(object sender, EventArgs e)
        {
            if (OrderType_0.SelectedValue != "" || OrderType_1.SelectedValue != "")
            {
                gridDetails.Visible = true;
                savePriceBook.Visible = true;
            }
            if(Type.SelectedValue=="0")
            {
                OrderType_0.Style.Add("display", "block");
                OrderType_1.Style.Add("display", "none");
                
            }
            else if (Type.SelectedValue == "1")
            {
                OrderType_0.Style.Add("display", "none");
                OrderType_1.Style.Add("display", "block");

            }
        }

        [WebMethod]
        public static string GetPriceBookDetails(string searchTerm, int pageIndex, int priceBookId, int filterItem)
        {
            return XBDataProvider.PriceBook.GetPriceBookData(searchTerm, pageIndex, priceBookId, filterItem);
            
        }

        
    }

    public class ItemMasteDetails
    {
        public string code { get; set; }
        public string name { get; set; }
        public string supplierBarcode { get; set; }
        public int mrp { get; set; }
        public int retailPrice { get; set; }
        public string BaseUnitCode { get; set; }
        public string TaxCode { get; set; }
        public int TaxPer { get; set; }
        public int Qnty { get; set; }
    }
}