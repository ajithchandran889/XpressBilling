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
                DataTable dtCurrency = XBDataProvider.Currency.GetAllActiveCurrencies(Session["CompanyCode"].ToString());

                ddlCurrency.DataSource = dtCurrency;
                ddlCurrency.DataValueField = "CurrencyCode";
                ddlCurrency.DataTextField = "Name";
                ddlCurrency.DataBind();
                ListItem itemcurrency = new ListItem();
                itemcurrency.Text = "--Select one--";
                itemcurrency.Value = "";
                ddlCurrency.Items.Insert(0, itemcurrency);
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
                    // Currency.Text = XBDataProvider.Currency.GetCurrencyCodeByCompany(Session["CompanyCode"].ToString()); ;
                    CreatedUser.Visible = false;
                    CreatedDate.Visible = false;
                    lblDate.Visible = false;
                    lblUser.Visible = false;
                    txtSearch.Visible = false;
                    btnSearch.Visible = false;
                    gridDetails.Visible = false;
                    savePriceBook.Visible = false;
                    SetInitialRow();
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
                //Currency.Text 
                ddlCurrency.SelectedValue = row["CurrencyCode"].ToString();
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
            if (txtSearch.Text == "")
            {
                dt = XBDataProvider.PriceBook.GetPriceBookDtlById(Convert.ToInt32(PriceBookId.Value));
            }
            else
            {
                dt = XBDataProvider.PriceBook.GetPriceBookDtlByIdAndSearchKey(Convert.ToInt32(PriceBookId.Value), txtSearch.Text);

            }
            if (dt.Rows.Count > 0)
            {
                PriceBookDetail.DataSource = dt;
                PriceBookDetail.DataBind();
                return;
            }
            //SetInitialRow();

        }

        private void AddNewRowToGrid()
        {
            try
            {
                int rowIndex = 0;

                if (ViewState["CurrentTable"] != null)
                {
                    DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                    DataRow drCurrentRow = null;
                    if (dtCurrentTable.Rows.Count > 0)
                    {
                        for (int i = 1; i <= dtCurrentTable.Rows.Count; i++)
                        {
                            //extract the TextBox values
                            TextBox box1 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[1].FindControl("ItemCode");
                            TextBox box2 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("SupplierBarcode");
                            //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
                            TextBox box4 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[3].FindControl("CurrencyCode");
                            TextBox box5 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[4].FindControl("MRP");
                            TextBox box6 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[5].FindControl("Price");

                            drCurrentRow = dtCurrentTable.NewRow();
                            dtCurrentTable.Rows[i - 1]["ID"] = PriceBookDetail.DataKeys[rowIndex]["ID"]; ;
                            dtCurrentTable.Rows[i - 1]["ItemCode"] = box1.Text;
                            dtCurrentTable.Rows[i - 1]["SupplierBarcode"] = box2.Text;
                            //dtCurrentTable.Rows[i - 1]["Description"] = box3.Text;
                            dtCurrentTable.Rows[i - 1]["CurrencyCode"] = box4.Text;
                            dtCurrentTable.Rows[i - 1]["MRP"] = float.Parse(box5.Text, CultureInfo.InvariantCulture.NumberFormat);
                            dtCurrentTable.Rows[i - 1]["Price"] = float.Parse(box6.Text, CultureInfo.InvariantCulture.NumberFormat);

                            rowIndex++;
                        }
                        dtCurrentTable.Rows.Add(drCurrentRow);
                        ViewState["CurrentTable"] = dtCurrentTable;

                        PriceBookDetail.DataSource = dtCurrentTable;
                        PriceBookDetail.DataBind();
                    }
                }
                else
                {
                    Response.Write("ViewState is null");
                }

                //Set Previous Data on Postbacks
                SetPreviousData();
            }
            catch (Exception e)
            {

            }

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

        protected void ButtonAddClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
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
                if (PageStatus.Value == "create")
                {
                    TextBox box1 = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("ItemCode");
                    TextBox box2 = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("SupplierBarcode");
                    TextBox box3 = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("Description");
                    TextBox box4 = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("CurrencyCode");
                    TextBox box5 = (TextBox)PriceBookDetail.Rows[i].Cells[5].FindControl("MRP");
                    TextBox box6 = (TextBox)PriceBookDetail.Rows[i].Cells[6].FindControl("Price");
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
                    string[] ItemCodes = Request.Form["ItemCode"] != null ? Request.Form["ItemCode"].Split(',') : new string[] { };
                    string[] SupplierBarcodes = Request.Form["SupplierBarcode"] != null ? Request.Form["SupplierBarcode"].Split(',') : new string[] { };
                    string[] CurrencyCodes = Request.Form["CurrencyCode"] != null ? Request.Form["CurrencyCode"].Split(',') : new string[] { };
                    string[] MRPs = Request.Form["MRP"] != null ? Request.Form["MRP"].Split(',') : new string[] { };
                    string[] Prices = Request.Form["Price"] != null ? Request.Form["Price"].Split(',') : new string[] { };
                    string[] Names = Request.Form["Description"] != null ? Request.Form["Description"].Split(',') : new string[] { };
                    for (int k = 0; k < ItemCodes.Length; k++)
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
                else
                {
                    foreach (GridViewRow row in PriceBookDetail.Rows)
                    {
                        TextBox box1 = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("ItemCode");
                        TextBox box2 = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("SupplierBarcode");
                        TextBox box3 = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("Description");
                        TextBox box4 = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("CurrencyCode");
                        TextBox box5 = (TextBox)PriceBookDetail.Rows[i].Cells[5].FindControl("MRP");
                        TextBox box6 = (TextBox)PriceBookDetail.Rows[i].Cells[6].FindControl("Price");
                        if (box1.Text != "" && box1.Text.Length != 0)
                        {
                            dr = dt.NewRow();
                            dr["ID"] = PriceBookDetail.DataKeys[i]["ID"];
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
                            dr["DocumentNo"] = PriceBookDocNo.Text;
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
                }
                if(Convert.ToInt32(PriceBookId.Value)>0)
                {
                    XBDataProvider.PriceBook.SavePriceBookMasterDetail(dt);
                    SaveSuccess.Visible = true;
                }
                else
                {
                    int id = XBDataProvider.PriceBook.SavePriceBookMaster(Session["CompanyCode"].ToString(), newDocumentNumber, Convert.ToInt32(Type.SelectedValue), orderType, ddlCurrency.SelectedValue, User.Identity.Name, dt);
                    if (id > 0)
                    {
                        Type.Enabled = false;
                        OrderType_0.Enabled = false;
                        OrderType_1.Enabled = false;
                        PriceBookDocNo.Text = newDocumentNumber;
                        PriceBookId.Value = id.ToString();
                        SaveSuccess.Visible = true;
                        PageStatus.Value = "edit";
                    }
                }
                SetPriceBookDetailsChildGrid();
            }
            catch (Exception ex)
            {

            }


        }

        //protected void savePriceBookClickDetails(object sender, EventArgs e)
        //{
        //    try
        //    {

        //        DataTable dt = new DataTable();
        //        DataRow dr = null;
        //        dt.Columns.Add(new DataColumn("ID", typeof(int)));
        //        dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
        //        dt.Columns.Add(new DataColumn("PriceBookMstID", typeof(int)));
        //        dt.Columns.Add(new DataColumn("DocumentNo", typeof(string)));
        //        dt.Columns.Add(new DataColumn("DocumentDate", typeof(DateTime)));
        //        dt.Columns.Add(new DataColumn("PriceType", typeof(int)));
        //        dt.Columns.Add(new DataColumn("OrderType", typeof(int)));
        //        dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
        //        dt.Columns.Add(new DataColumn("SupplierBarcode", typeof(string)));
        //        dt.Columns.Add(new DataColumn("CurrencyCode", typeof(string)));
        //        dt.Columns.Add(new DataColumn("MRP", typeof(float)));
        //        dt.Columns.Add(new DataColumn("Price", typeof(float)));
        //        dt.Columns.Add(new DataColumn("Reference", typeof(string)));
        //        dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
        //        dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
        //        dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
        //        dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
        //        dt.Columns.Add(new DataColumn("ApprovalStatus", typeof(int)));
        //        dt.Columns.Add(new DataColumn("Status", typeof(int)));
        //        int i = 0;
        //        if (PageStatus.Value == "create")
        //        {
        //            TextBox box1 = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("ItemCode");
        //            TextBox box2 = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("SupplierBarcode");
        //            //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
        //            TextBox box4 = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("CurrencyCode");
        //            TextBox box5 = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("MRP");
        //            TextBox box6 = (TextBox)PriceBookDetail.Rows[i].Cells[5].FindControl("Price");
        //            dr = dt.NewRow();
        //            dr["ID"] = PriceBookDetail.DataKeys[i]["ID"];
        //            dr["CompanyCode"] = Session["CompanyCode"].ToString();
        //            dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
        //            dr["DocumentNo"] = PriceBookDocNo.Text;
        //            dr["DocumentDate"] = DateTime.Now.Date;
        //            dr["PriceType"] = Type.SelectedValue;
        //            if (Type.SelectedValue == "0")
        //            {
        //                dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
        //            }
        //            else
        //            {
        //                dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
        //            }
        //            dr["ItemCode"] = box1.Text;
        //            dr["SupplierBarcode"] = box2.Text;
        //            dr["CurrencyCode"] = box4.Text;
        //            dr["MRP"] = float.Parse(Convert.ToDecimal(box5.Text).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
        //            dr["Price"] = float.Parse(Convert.ToDecimal(box6.Text).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
        //            dr["Reference"] = null;
        //            dr["CreatedBy"] = User.Identity.Name;
        //            dr["UpdatedBy"] = User.Identity.Name;
        //            dr["CreatedDate"] = DateTime.Now.Date;
        //            dr["UpdatedDate"] = DateTime.Now.Date;
        //            dr["ApprovalStatus"] = 0;
        //            dr["Status"] = 1;
        //            dt.Rows.Add(dr);
        //            string[] ItemCodes = Request.Form["ItemCode"]!=null?Request.Form["ItemCode"].Split(','):new string[] {};
        //            string[] SupplierBarcodes =Request.Form["SupplierBarcode"]!=null? Request.Form["SupplierBarcode"].Split(','):new string[] {};
        //            string[] CurrencyCodes =Request.Form["CurrencyCode"]!=null? Request.Form["CurrencyCode"].Split(','):new string[] {};
        //            string[] MRPs = Request.Form["MRP"]!=null?Request.Form["MRP"].Split(','):new string[] {};
        //            string[] Prices = Request.Form["Price"] != null ? Request.Form["Price"].Split(',') : new string[] { };
        //            for (int k = 0; k < ItemCodes.Length; k++)
        //            {
        //                dr = dt.NewRow();
        //                dr["ID"] = PriceBookDetail.DataKeys[i]["ID"];
        //                dr["CompanyCode"] = Session["CompanyCode"].ToString();
        //                dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
        //                dr["DocumentNo"] = PriceBookDocNo.Text;
        //                dr["DocumentDate"] = DateTime.Now.Date;
        //                dr["PriceType"] = Type.SelectedValue;
        //                if (Type.SelectedValue == "0")
        //                {
        //                    dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
        //                }
        //                else
        //                {
        //                    dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
        //                }
        //                dr["ItemCode"] = ItemCodes[k];
        //                dr["SupplierBarcode"] = SupplierBarcodes[k];
        //                dr["CurrencyCode"] = CurrencyCodes[k];
        //                dr["MRP"] = float.Parse(Convert.ToDecimal(MRPs[k]).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
        //                dr["Price"] = float.Parse(Convert.ToDecimal(Prices[k]).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
        //                dr["Reference"] = null;
        //                dr["CreatedBy"] = User.Identity.Name;
        //                dr["UpdatedBy"] = User.Identity.Name;
        //                dr["CreatedDate"] = DateTime.Now.Date;
        //                dr["UpdatedDate"] = DateTime.Now.Date;
        //                dr["ApprovalStatus"] = 0;
        //                dr["Status"] = 1;
        //                dt.Rows.Add(dr);
        //            }
        //        }
        //        else
        //        {
        //            foreach (GridViewRow row in PriceBookDetail.Rows)
        //            {
        //                TextBox box1 = (TextBox)PriceBookDetail.Rows[i].Cells[1].FindControl("ItemCode");
        //                TextBox box2 = (TextBox)PriceBookDetail.Rows[i].Cells[2].FindControl("SupplierBarcode");
        //                //TextBox box3 = (TextBox)PriceBookDetail.Rows[rowIndex].Cells[2].FindControl("Description");
        //                TextBox box4 = (TextBox)PriceBookDetail.Rows[i].Cells[3].FindControl("CurrencyCode");
        //                TextBox box5 = (TextBox)PriceBookDetail.Rows[i].Cells[4].FindControl("MRP");
        //                TextBox box6 = (TextBox)PriceBookDetail.Rows[i].Cells[5].FindControl("Price");
        //                if (box1.Text != "" && box1.Text.Length != 0)
        //                {
        //                    dr = dt.NewRow();
        //                    dr["ID"] = PriceBookDetail.DataKeys[i]["ID"];
        //                    dr["CompanyCode"] = Session["CompanyCode"].ToString();
        //                    dr["PriceBookMstID"] = Convert.ToInt32(PriceBookId.Value);
        //                    dr["DocumentNo"] = PriceBookDocNo.Text;
        //                    dr["DocumentDate"] = DateTime.Now.Date;
        //                    dr["PriceType"] = Type.SelectedValue;
        //                    if (Type.SelectedValue == "0")
        //                    {
        //                        dr["OrderType"] = Convert.ToInt32(OrderType_0.SelectedValue);
        //                    }
        //                    else
        //                    {
        //                        dr["OrderType"] = Convert.ToInt32(OrderType_1.SelectedValue);
        //                    }
        //                    dr["ItemCode"] = box1.Text;
        //                    dr["SupplierBarcode"] = box2.Text;
        //                    dr["CurrencyCode"] = box4.Text;
        //                    dr["MRP"] = float.Parse(Convert.ToDecimal(box5.Text).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
        //                    dr["Price"] = float.Parse(Convert.ToDecimal(box6.Text).ToString("0.00"), CultureInfo.InvariantCulture.NumberFormat);
        //                    dr["Reference"] = null;
        //                    dr["CreatedBy"] = User.Identity.Name;
        //                    dr["UpdatedBy"] = User.Identity.Name;
        //                    dr["CreatedDate"] = DateTime.Now.Date;
        //                    dr["UpdatedDate"] = DateTime.Now.Date;
        //                    dr["ApprovalStatus"] = 0;
        //                    dr["Status"] = 1;
        //                    dt.Rows.Add(dr);
        //                    i++;
        //                }

        //            }
        //        }
        //        if (dt.Rows.Count > 0)
        //        {
        //            XBDataProvider.PriceBook.SavePriceBookMasterDetail(dt);
        //            SaveSuccess.Visible = true;
        //        }
        //        SetPriceBookDetailsChildGrid();

        //    }
        //    catch (Exception ex)
        //    {
        //        failureMessage.Visible = true;
        //        SaveSuccess.Visible = true;
        //    }

        //}

        protected void BtnSearchClick(object sender, EventArgs e)
        {
            SetPriceBookDetailsChildGrid();
        }

        protected void PriceBookDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            Label lbl = (Label)e.Row.Cells[0].FindControl("indexIcrement");
            string txt = e.Row.Cells[0].Text == "No:" ? "header" : (lbl == null ? String.Empty : lbl.Text);
            if (txt == string.Empty)
                e.Row.Visible = false;
        }

        protected void ddlCurrencySelectedIndexChanged(object sender, EventArgs e)
        {
            if(ddlCurrency.SelectedValue!="")
            {
                gridDetails.Visible = true;
                savePriceBook.Visible = true;
            }
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