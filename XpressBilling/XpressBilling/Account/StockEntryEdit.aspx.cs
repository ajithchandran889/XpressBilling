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
    public partial class StokeEntryEdit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                if (Session["CompanyCode"] == null)
                {
                    Session["CompanyCode"] = XBDataProvider.User.GetCompanyCodeByUserId(User.Identity.Name);
                }
                currencyCode.Value = XBDataProvider.Currency.GetCurrencyCodeByCompany(CompanyCode.Value);
                CompanyCode.Value = Session["CompanyCode"].ToString();
                DataTable dtUsers = XBDataProvider.User.GetAllUsersByCompany(Session["CompanyCode"].ToString());
                CreatedUser.DataSource = dtUsers;
                CreatedUser.DataValueField = "UserName";
                CreatedUser.DataTextField = "UserName";
                CreatedUser.DataBind();
                currencyDecimal.Value = XBDataProvider.Currency.GetCurrencyDecimalByCompany(CompanyCode.Value).ToString();
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable stockEntryDetails = XBDataProvider.StockEntry.GetStockEntryById(id);
                    if (stockEntryDetails.Rows.Count > 0)
                    {
                        SetStockEntryDetails(stockEntryDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {

                    #region default values
                    PageStatus.Value = "create";
                    Date.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    DataTable dtTable = XBDataProvider.Company.GetCompanyDetailsByCode(Session["CompanyCode"].ToString());
                    DataRow rowCompany = dtTable.Rows[0];
                    Currency.Text = rowCompany["CurrencyCode"].ToString();
                    currencyCode1.InnerText = rowCompany["CurrencyCode"].ToString();
                    CreatedUser.SelectedValue = User.Identity.Name;
                    StokeEntryMstId.Value = "0";
                    SetInitialRows();
                    #endregion
                }
            }
        }

        public void SetStockEntryDetails(DataTable stockEntryDetails)
        {
            try
            {
                btnSaveDtl.Visible = true;
                btnConvertStockRegister.Visible = true;
                btnPrint.Visible = true;
                DataRow row = stockEntryDetails.Rows[0];
                StokeEntryMstId.Value = row["ID"].ToString();
                AdjustmentType.SelectedValue = row["TransactionType"].ToString();
                AdjustmentType.Enabled = false;
                Document.Text = row["DocumentNo"].ToString();
                Document.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                Location.Text = row["LocationCode"].ToString();
                LocationHidden.Value = row["LocationCode"].ToString();
                Location.ReadOnly = true;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                Date.Text = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                Date.ReadOnly = true;
                CreatedUser.Text = row["CreatedBy"].ToString();
                CreatedUser.Enabled = false;
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                Amount.ReadOnly = true;
                Currency.Text = row["Currency"].ToString();
                Currency.ReadOnly = true;

                SetStockEntryChildGrid();
                int i = 0;
                if (row["Status"].ToString() == "2")
                {
                    btnConvertStockRegister.Visible = false;
                    btnSaveDtl.Visible = false;
                }
                
            }
            catch (Exception e)
            {

            }


        }

        private void SetInitialRows()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("Amount", typeof(float)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["ItemCode"] = string.Empty;
                    dr["ItemName"] = string.Empty;
                    dr["Rate"] = DBNull.Value;
                    dr["Qty"] = DBNull.Value;
                    dr["Amount"] = DBNull.Value;
                    dr["BaseUnitCode"] = string.Empty;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                StockEntryDetail.DataSource = dt;
                StockEntryDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }
        
        public void SetStockEntryChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.StockEntry.GetStockEntryDtlById(Convert.ToInt32(StokeEntryMstId.Value));

            if (dt.Rows.Count > 0)
            {
                rowCount.Value = dt.Rows.Count.ToString();
                StockEntryDetail.DataSource = dt;
                StockEntryDetail.DataBind();
                
            }
        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtDeletedIds = new DataTable();
                DataRow drDeletedIds = null;
                dtDeletedIds.Columns.Add(new DataColumn("ID", typeof(int)));
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("LocationCode", typeof(string)));
                dt.Columns.Add(new DataColumn("StockAdjustmentMstId", typeof(int)));
                dt.Columns.Add(new DataColumn("Pos", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("Currency", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("Amount", typeof(float)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dt.Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                int i = 0;
                int[] deletedIds = DeletedRowIDs.Value.Split(',').Where(str => str != "").Select(str => int.Parse(str)).ToArray();
                for (int k = 0; k < deletedIds.Length; k++)
                {
                    drDeletedIds = dtDeletedIds.NewRow();
                    drDeletedIds["ID"] = deletedIds[k];
                    dtDeletedIds.Rows.Add(drDeletedIds);
                }
                foreach (GridViewRow row in StockEntryDetail.Rows)
                {
                    TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                    TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                    TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("SERate");
                    TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SEQuantity");
                    TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("Unit");
                    TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                    if (Array.IndexOf(deletedIds, StockEntryDetail.DataKeys[i]["ID"]) == -1 && box2.Text != "" && box2.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(StockEntryDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] = DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(StockEntryDetail.DataKeys[i]["ID"]);
                        }

                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = LocationHidden.Value;
                        dr["StockAdjustmentMstId"] = Convert.ToInt32(StokeEntryMstId.Value);
                        dr["Pos"] = 0;
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = currencyCode.Value;
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Amount"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Status"] = 1;
                        dr["Reference"] = "";
                        dr["ErrorMsg"] = null;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dt.Rows.Add(dr);
                        i++;
                    }
                }
                if (Request.Form["Item"] != null)
                {
                    string[] Items = Request.Form["Item"].Split(',');
                    string[] Names = Request.Form["Name"].Split(',');
                    string[] SERates = Request.Form["SERate"].Split(',');
                    string[] SEQuantitys = Request.Form["SEQuantity"].Split(',');
                    string[] Units = Request.Form["Unit"].Split(',');
                    string[] SEAmounts = Request.Form["SEAmount"].Split(',');

                    for (int k = 0; k < Items.Length; k++)
                    {
                        if (Items[k] != "" && Names[k] != "" && SERates[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = LocationHidden.Value;
                            dr["StockAdjustmentMstId"] = Convert.ToInt32(StokeEntryMstId.Value);
                            dr["Pos"] = 0;
                            dr["ItemCode"] = Items[k];
                            dr["ItemName"] = Names[k];
                            dr["BaseUnitCode"] = Units[k];
                            dr["Qty"] = Convert.ToInt32(SEQuantitys[k]);
                            dr["Currency"] = "";
                            dr["Rate"] = float.Parse(SERates[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Amount"] = float.Parse(SEAmounts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Status"] = 1;
                            dr["Reference"] = "";
                            dr["ErrorMsg"] = null;
                            dr["CreatedBy"] = User.Identity.Name;
                            dr["UpdatedBy"] = User.Identity.Name;
                            dr["CreatedDate"] = DateTime.Now.Date;
                            dr["UpdatedDate"] = DateTime.Now.Date;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                if (StokeEntryMstId.Value == "0")
                {
                    int returnValue = 0;
                    int selectedSequenceId = Convert.ToInt32(SESequenceNoID.Value);

                    
                    float amount = 0;
                    if (Request.Form[Amount.UniqueID] != "")
                    {
                        amount = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                    }
                    returnValue = XBDataProvider.StockEntry.SaveSE(Session["CompanyCode"].ToString(), Convert.ToInt32(AdjustmentType.SelectedValue), Request.Form[Document.UniqueID], 1, date, LocationHidden.Value, CreatedUser.SelectedValue,
                        User.Identity.Name, Reference.Text, amount, Currency.Text, selectedSequenceId, dt);
                    if (returnValue > 0)
                    {
                        Document.Text = Request.Form[Document.UniqueID];
                        PageStatus.Value = "edit";
                        StokeEntryMstId.Value = returnValue.ToString();
                        SaveSuccess.Visible = true;
                        failure.Visible = false;
                        Amount.Text = Request.Form[Amount.UniqueID].ToString();
                        Status.SelectedValue = "1";
                        SetStockEntryChildGrid();
                        btnConvertStockRegister.Visible = true;
                        btnPrint.Visible = true;
                        AdjustmentType.Enabled = false;
                        Location.ReadOnly = true;
                        CreatedUser.Enabled = false;
                    }
                    else
                    {
                        failure.Visible = true;
                    }
                    

                }
                else
                {
                    XBDataProvider.StockEntry.SaveSEDetail(Convert.ToInt32(StokeEntryMstId.Value), float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt, date, dtDeletedIds);
                    btnConvertStockRegister.Visible = true;
                    btnPrint.Visible = true;
                    Amount.Text = Request.Form[Amount.UniqueID].ToString();
                    Status.SelectedValue = "1";
                    SetStockEntryChildGrid();
                    PageStatus.Value = "edit";
                    SaveSuccess.Visible = false;
                    UpdateSuccess.Visible = true;
                    failure.Visible = false;

                }

            }
            catch (Exception ex)
            {
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                failure.Visible = true;
            }
        }

        private void SetPreviousData()
        {
            int rowIndex = 0;
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dt = (DataTable)ViewState["CurrentTable"];
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("SERate");
                        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SEQuantity");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                        box2.Text = dt.Rows[i]["ItemCode"].ToString();
                        box3.Text = dt.Rows[i]["ItemName"].ToString();
                        box4.Text = dt.Rows[i]["Rate"].ToString();
                        box5.Text = dt.Rows[i]["Qty"].ToString();
                        box6.Text = dt.Rows[i]["BaseUnitCode"].ToString();
                        box7.Text = dt.Rows[i]["Amount"].ToString();

                        rowIndex++;
                    }
                }
            }

        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            Amount.Text = Request.Form[Amount.UniqueID].ToString();
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("SERate");
                        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SEQuantity");
                        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("Unit");
                        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");


                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i]["ID"] = Convert.ToInt32(StockEntryDetail.DataKeys[i]["ID"]); ;
                        dtCurrentTable.Rows[i]["ItemCode"] = box2.Text;
                        dtCurrentTable.Rows[i]["ItemName"] = box3.Text;
                        if (box4.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Rate"] = box4.Text;
                        }
                        if (box5.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Qty"] = box5.Text;
                        }
                        dtCurrentTable.Rows[i]["BaseUnitCode"] = box6.Text;
                        if (box7.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Amount"] = box7.Text;
                        }
                        rowIndex++;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    StockEntryDetail.DataSource = dtCurrentTable;
                    StockEntryDetail.DataBind();
                }
            }
            else
            {
                Response.Write("ViewState is null");
            }

            //Set Previous Data on Postbacks
            SetPreviousData();
        }

        protected void AddNewRowClick(object sender, EventArgs e)
        {
            AddNewRowToGrid();
        }

        protected void btnConvertStockRegisterClick(object sender, EventArgs e)
        {
            if (XBDataProvider.StockEntry.ConvertToStockRegister(Convert.ToInt32(StokeEntryMstId.Value)))
            {
                Status.SelectedValue = "2";
                SetStockEntryChildGrid();
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                FinalizeSuccess.Visible = true;
                failure.Visible = false;
                btnSaveDtl.Visible = false;
                btnConvertStockRegister.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "window.print();", true);
            }
            else
            {
                failure.Visible = true;
                FinalizeSuccess.Visible = false;
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
            }
        }

        [WebMethod]
        public static List<FirstFreeDetails> GetFirstFreerDetails(string companyCode)
        {
            List<FirstFreeDetails> result = new List<FirstFreeDetails>();
            try
            {
                DataRow row = null;
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetStockEntryCashCreditSequenceDetails(companyCode);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    FirstFreeDetails firstFreeDetails = new FirstFreeDetails();
                    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                    firstFreeDetails.id = row["ID"].ToString();
                    firstFreeDetails.sequenceNumber = sequenceNo;
                    firstFreeDetails.seqType = row["SeqType"].ToString();
                    firstFreeDetails.orderType = row["OrderType"].ToString() == "Addition" ? "2" : (row["OrderType"].ToString() == "Deduction" ? "3" : "1");
                    firstFreeDetails.enterpriseUnitCode = row["EnterpriseUnitCode"].ToString();
                    result.Add(firstFreeDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }
        [WebMethod]
        public static List<ItemMasteDetailsSE> GetItemMasters(string companyCode)
        {
            List<ItemMasteDetailsSE> result = new List<ItemMasteDetailsSE>();
            try
            {
                DataTable dtTable = XBDataProvider.ItemMaster.GetItemMastersSE(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ItemMasteDetailsSE itemMasteDetails = new ItemMasteDetailsSE();
                    itemMasteDetails.code = row["ItemCode"].ToString();
                    itemMasteDetails.name = row["Name"].ToString();
                    itemMasteDetails.BaseUnitCode = row["BaseUnitCode"].ToString();
                    itemMasteDetails.supplierBarcode = row["SupplierBarcode"].ToString();
                    itemMasteDetails.mrp = Convert.ToInt32(row["MRP"].ToString());
                    itemMasteDetails.retailPrice = Convert.ToInt32(row["RetailPrice"].ToString());
                    itemMasteDetails.TaxCode = row["TaxCode"].ToString();
                    itemMasteDetails.TaxPer = row["TaxPercentage"].ToString() != "" ? Convert.ToInt32(row["TaxPercentage"].ToString()) : 0;
                    itemMasteDetails.Qnty = row["Qnty"].ToString() != "" ? Convert.ToInt32(row["Qnty"].ToString()) : 0;
                    itemMasteDetails.decimalPoint = row["Decimal"].ToString();
                    itemMasteDetails.currencyCode = row["CurrencyCode"].ToString();
                    itemMasteDetails.itemType = Convert.ToInt32(row["ItemType"].ToString());
                    result.Add(itemMasteDetails);
                }
            }
            catch (Exception e)
            {

            }
            return result;
        }

        protected void StockEntryDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value)==1)
            {
                LinkButton lnkDtn = e.Row.Cells[7].FindControl("lnkDeleteSE") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (Status.SelectedValue == "2")
            {
                e.Row.Cells[7].Visible = false;
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox SEAmount = e.Row.Cells[6].FindControl("SEAmount") as TextBox;
                if (SEAmount.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    double seAmount = Convert.ToDouble(SEAmount.Text);
                    SEAmount.Text = seAmount.ToString("f" + decimalPoints);
                }
            }
        }
    }
    public class ItemMasteDetailsSE
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
        public string currencyCode { get; set; }
        public string decimalPoint { get; set; }
        public int itemType { get; set; }
    }
}