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
    public partial class PurchaseOrderEdit : System.Web.UI.Page
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
                DataTable dtTable = XBDataProvider.BussinessPartner.GetAllSupplierCodes(Session["CompanyCode"].ToString());
                Session["BPDetails"] = dtTable;
                DataRow row = null;
                SupplierId.Items.Clear();
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    ListItem list = new ListItem();
                    list.Value = row["BusinessPartnerCode"].ToString();
                    list.Text = row["BusinessPartnerCode"].ToString();
                    SupplierId.Items.Add(list);
                }
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable purchaseOrderDetails = XBDataProvider.PurchaseOrder.GetPurchaseOrderById(id);
                    if (purchaseOrderDetails.Rows.Count > 0)
                    {
                        SetPurchaseOrderDetails(purchaseOrderDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
                    PageStatus.Value = "create";
                    if (dtTable.Rows.Count > 0)
                    {
                        Name.Text = dtTable.Rows[0]["Name"].ToString();
                        Name.ReadOnly = true;
                        Location.Text = dtTable.Rows[0]["CountryCode"].ToString();
                        Location.ReadOnly = true;
                        Telephone.Text = dtTable.Rows[0]["Phone"].ToString();
                        Telephone.ReadOnly = true;
                        OrderType.SelectedValue = dtTable.Rows[0]["OrderType"].ToString();
                        OrderType.Enabled = false;
                    }
                    CreatedDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    CreatedDate.ReadOnly = true;

                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetPurchaseOrderSequenceDetails(Session["CompanyCode"].ToString());
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));

                        if (row["OrderType"].ToString() == "Local")
                        {
                            OrderNo.Text = sequenceNo;
                            LocalSequenceNo.Value = sequenceNo;
                            LocalSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["OrderType"].ToString() == "Import")
                        {
                            ImportSequenceNo.Value = sequenceNo;
                            ImportSequenceNoID.Value = row["ID"].ToString();
                        }
                        OrderNo.ReadOnly = true;
                    }
                    gridDetails.Visible = false;
                }
            }
        }

        public void SetPurchaseOrderDetails(DataTable purchaseOrderDetails)
        {
            try
            {
                SaveBtn.Visible = false;
                CancelBtn.Visible = false;
                btnConverOrder.Visible = true;
                btnPrint.Visible = true;
                DataRow row = purchaseOrderDetails.Rows[0];
                PurchaseOrderId.Value = row["ID"].ToString();
                Name.Text = row["Name"].ToString();
                OrderType.SelectedValue = row["OrderType"].ToString();
                OrderType.Enabled = false;
                OrderNo.Text = row["PurchaseOrderNo"].ToString();
                OrderNo.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                SupplierId.Text = row["BussinessPartnerCode"].ToString();
                SupplierId.Enabled = false;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                SalesMan.Text = row["Buyer"].ToString();
                SalesMan.ReadOnly = true;
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("0.00");
                Amount.ReadOnly = true;
                PayTerms.Text = row["PaymentTerms"].ToString();
                DeliveryTerms.Text = row["DeliveryTerms"].ToString();
                POTotalAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("0.00");
                POTotalDiscountAmt.Text = Convert.ToDecimal(row["DiscountAmount"]).ToString("0.00");
                POTotalTaxAmt.Text =  Convert.ToDecimal(row["TaxAmount"]).ToString("0.00");
                POTotalOrderAmt.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("0.00");
                Telephone.Text = row["Telephone"].ToString();
                ShipToAddress.Text = row["ShipToAddress"].ToString();
                Telephone.ReadOnly = true;
                CreatedDate.Text = Convert.ToDateTime(row["PurchaseOrderDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedDate.ReadOnly = true;
                Location.Text = row["LocationCode"].ToString();
                Location.ReadOnly = true;
                AddNewRow.Visible = false;

                SetPurchaseOrderChildGrid();
            }
            catch (Exception e)
            {

            }


        }

        public void SetPurchaseOrderChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.PurchaseOrder.GetPurchaseOrderDtlById(Convert.ToInt32(PurchaseOrderId.Value));

            if (dt.Rows.Count > 0)
            {
                PurchaseOrderDetail.DataSource = dt;
                PurchaseOrderDetail.DataBind();
            }
            AddNewRow.Visible = false;
        }

        protected void SupplierIdSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTable = Session["BPDetails"] as DataTable;
                DataRow row = null;
                string selectedBP = SupplierId.SelectedValue;
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    if (selectedBP == row["BusinessPartnerCode"].ToString())
                    {
                        Name.Text = row["Name"].ToString();
                        Name.ReadOnly = true;
                        Location.Text = row["CountryCode"].ToString();
                        Location.ReadOnly = true;
                        Telephone.Text = row["Phone"].ToString();
                        Telephone.ReadOnly = true;
                        OrderType.SelectedValue = row["OrderType"].ToString();
                        OrderType.Enabled = false;
                        if (row["OrderType"].ToString() == "0")
                        {
                            OrderNo.Text = LocalSequenceNo.Value;
                        }
                        else
                        {
                            OrderNo.Text = ImportSequenceNo.Value;
                        }
                    }
                }

            }
            catch (Exception ex)
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
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Discount", typeof(float)));
                dt.Columns.Add(new DataColumn("DiscountAmt", typeof(float)));
                dt.Columns.Add(new DataColumn("Tax", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxPercentage", typeof(float)));
                dt.Columns.Add(new DataColumn("TaxAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(float)));
                for (int i = 0; i < 20; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["ItemCode"] = string.Empty;
                    dr["ItemName"] = string.Empty;
                    dr["Rate"] = DBNull.Value;
                    dr["Qty"] = DBNull.Value;
                    dr["BaseUnitCode"] = string.Empty;
                    dr["Discount"] = DBNull.Value;
                    dr["DiscountAmt"] = DBNull.Value;
                    dr["Tax"] = string.Empty;
                    dr["TaxPercentage"] = DBNull.Value;
                    dr["TaxAmount"] = DBNull.Value;
                    dr["NetAmount"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                PurchaseOrderDetail.DataSource = dt;
                PurchaseOrderDetail.DataBind();
            }
            catch (Exception e)
            {

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
                        TextBox box2 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[1].FindControl("POItem");
                        TextBox box3 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[2].FindControl("POName");
                        TextBox box4 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[3].FindControl("PORate");
                        TextBox box5 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[4].FindControl("POQuantity");
                        TextBox box6 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[5].FindControl("POUnit");
                        TextBox box7 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[6].FindControl("PODiscPer");
                        TextBox box8 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[7].FindControl("PODiscAmt");
                        TextBox box9 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxPer");
                        TextBox box10 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[9].FindControl("POTaxAmt");
                        TextBox box11 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[10].FindControl("PONetAmt");
                        HiddenField hdnFld = (HiddenField)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxCode");
                        box2.Text = dt.Rows[i]["ItemCode"].ToString();
                        box3.Text = dt.Rows[i]["ItemName"].ToString();
                        box4.Text = dt.Rows[i]["Rate"].ToString();
                        box5.Text = dt.Rows[i]["Qty"].ToString();
                        box6.Text = dt.Rows[i]["BaseUnitCode"].ToString();
                        box7.Text = dt.Rows[i]["Discount"].ToString();
                        box8.Text = dt.Rows[i]["DiscountAmt"].ToString();
                        box9.Text = dt.Rows[i]["TaxPercentage"].ToString();
                        hdnFld.Value = dt.Rows[i]["Tax"].ToString();
                        box10.Text = dt.Rows[i]["TaxAmount"].ToString();
                        box11.Text = dt.Rows[i]["NetAmount"].ToString();

                        rowIndex++;
                    }
                }
            }
        }

        private void AddNewRowToGrid()
        {
            int rowIndex = 0;
            Amount.Text = Request.Form[Amount.UniqueID];
            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[1].FindControl("POItem");
                        TextBox box3 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[2].FindControl("POName");
                        TextBox box4 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[3].FindControl("PORate");
                        TextBox box5 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[4].FindControl("POQuantity");
                        TextBox box6 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[5].FindControl("POUnit");
                        TextBox box7 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[6].FindControl("PODiscPer");
                        TextBox box8 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[7].FindControl("PODiscAmt");
                        TextBox box9 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxPer");
                        TextBox box10 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[9].FindControl("POTaxAmt");
                        TextBox box11 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[10].FindControl("PONetAmt");
                        HiddenField hdnFld = (HiddenField)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxCode");

                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i]["ID"] = Convert.ToInt32(PurchaseOrderDetail.DataKeys[i]["ID"]); ;
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
                        if (box8.Text != "")
                        {
                            dtCurrentTable.Rows[i]["DiscountAmt"] = box8.Text;
                        }
                        if (box10.Text != "")
                        {
                            dtCurrentTable.Rows[i]["TaxAmount"] = box10.Text;
                        }
                        dtCurrentTable.Rows[i]["BaseUnitCode"] = box6.Text;
                        if (box7.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Discount"] = box7.Text;
                        }
                        if (box9.Text != "")
                        {
                            dtCurrentTable.Rows[i]["TaxPercentage"] = box9.Text;
                        }
                        dtCurrentTable.Rows[i]["Tax"] = hdnFld.Value;
                        if (box11.Text != "")
                        {
                            dtCurrentTable.Rows[i]["NetAmount"] = box11.Text;
                        }
                        rowIndex++;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    PurchaseOrderDetail.DataSource = dtCurrentTable;
                    PurchaseOrderDetail.DataBind();
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
        
        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                int returnValue = 0;
                int selectedSequenceId = 0;
                if (OrderType.SelectedValue == "0")
                {
                    selectedSequenceId = Convert.ToInt32(LocalSequenceNoID.Value);
                }
                else
                {
                    selectedSequenceId = Convert.ToInt32(ImportSequenceNoID.Value);
                }
                returnValue = XBDataProvider.PurchaseOrder.SavePO(Session["CompanyCode"].ToString(), Location.Text, OrderNo.Text,
                    DateTime.Today.Date, Convert.ToInt32(OrderType.SelectedValue), Reference.Text, SupplierId.SelectedValue,
                    SalesMan.Text, 1, User.Identity.Name, selectedSequenceId, Telephone.Text,Name.Text,ShipToAddress.Text);
                if (returnValue > 0)
                {
                    PurchaseOrderId.Value = returnValue.ToString();
                    gridDetails.Visible = true;
                    SaveBtn.Visible = false;
                    CancelBtn.Visible = false;
                    SetInitialRows();
                    SaveSuccess.Visible = true;
                    failure.Visible = false;
                }
                else
                {
                    failure.Visible = true;
                }


            }
            catch (Exception ex)
            {

            }

        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {

                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("LocationCode", typeof(string)));
                dt.Columns.Add(new DataColumn("PurchaseOrderMstId", typeof(int)));
                dt.Columns.Add(new DataColumn("PurchaseOrderNo", typeof(string)));
                dt.Columns.Add(new DataColumn("PurchaseOrderDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("OrderQty", typeof(int)));
                dt.Columns.Add(new DataColumn("ReceivedQty", typeof(int)));
                dt.Columns.Add(new DataColumn("InOrderQty", typeof(int)));
                dt.Columns.Add(new DataColumn("Currency", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("TotalRate", typeof(float)));
                dt.Columns.Add(new DataColumn("DiscountPercentage", typeof(float)));
                dt.Columns.Add(new DataColumn("DiscountAmt", typeof(float)));
                dt.Columns.Add(new DataColumn("Tax", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxPercentage", typeof(float)));
                dt.Columns.Add(new DataColumn("TaxAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dt.Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                int i = 0;
                foreach (GridViewRow row in PurchaseOrderDetail.Rows)
                {
                    TextBox box2 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[1].FindControl("POItem");
                    TextBox box3 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[2].FindControl("POName");
                    TextBox box4 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[3].FindControl("PORate");
                    TextBox box5 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[4].FindControl("POQuantity");
                    TextBox box6 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[5].FindControl("POUnit");
                    TextBox box7 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[6].FindControl("PODiscPer");
                    TextBox box8 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[7].FindControl("PODiscAmt");
                    TextBox box9 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxPer");
                    TextBox box10 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[9].FindControl("POTaxAmt");
                    TextBox box11 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[10].FindControl("PONetAmt");
                    HiddenField hdnFld = (HiddenField)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxCode");
                    if (box2.Text != "" && box2.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(PurchaseOrderDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] = DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(PurchaseOrderDetail.DataKeys[i]["ID"]);
                        }
                        DateTime date = DateTime.ParseExact(CreatedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = Location.Text;
                        dr["PurchaseOrderMstId"] = Convert.ToInt32(PurchaseOrderId.Value);
                        dr["PurchaseOrderNo"] = OrderNo.Text;
                        dr["PurchaseOrderDate"] = date;
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["OrderQty"] = Convert.ToInt32(box5.Text);
                        dr["ReceivedQty"] = 0;
                        dr["InOrderQty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = "";
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                        dr["DiscountPercentage"] =  float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["DiscountAmt"] =  float.Parse(box8.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Tax"] =  hdnFld.Value;
                        dr["TaxPercentage"] =  float.Parse(box9.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TaxAmount"] = float.Parse(box10.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["NetAmount"] = float.Parse(box11.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Status"] = 1;
                        dr["ErrorMsg"] = null;
                        dr["Reference"] = Reference.Text;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dt.Rows.Add(dr);
                        i++;
                    }

                }
                if (dt.Rows.Count > 0)
                {
                    XBDataProvider.PurchaseOrder.SavePODetail(Convert.ToInt32(PurchaseOrderId.Value), PayTerms.Text, DeliveryTerms.Text, float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, ShipToAddress.Text, dt);
                    btnConverOrder.Visible = true;
                    btnPrint.Visible = true;
                    PageStatus.Value = "edit";
                    Status.SelectedValue = "1";
                    SaveSuccess.Visible = false;
                    UpdateSuccess.Visible = true;
                    failure.Visible = false;
                    Amount.Text = Request.Form[Amount.UniqueID];
                }
                else
                {
                    SaveSuccess.Visible = false;
                    UpdateSuccess.Visible = false;
                    failure.Visible = true;
                }
                SetPurchaseOrderChildGrid();

            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnConvertOrderClick(object sender, EventArgs e)
        {
            //DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetGRNSequenceDetails(Session["CompanyCode"].ToString());
            //DataRow row = null;
            //string orderNo = "";
            //for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
            //{
            //    row = dtTableSequenceDetails.Rows[i];

            //    if (row["OrderType"].ToString() == "Cash" && QuotationType.SelectedValue == "0")
            //    {
            //        orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
            //        salesOrderLastIncId.Value = row["ID"].ToString();
            //    }
            //    else if (row["OrderType"].ToString() == "Credit" && QuotationType.SelectedValue == "1")
            //    {
            //        orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
            //        salesOrderLastIncId.Value = row["ID"].ToString();
            //    }
            //}

            if (XBDataProvider.PurchaseOrder.ConvertToGRN(Convert.ToInt32(PurchaseOrderId.Value), OrderType.SelectedItem.Text))
            {
                btnConverOrder.Visible = false;
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                FinalizeSuccess.Visible = true;
                failure.Visible = false;
            }
            else
            {
                failure.Visible = true;
                FinalizeSuccess.Visible = false;
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
            }
        }

    }
}