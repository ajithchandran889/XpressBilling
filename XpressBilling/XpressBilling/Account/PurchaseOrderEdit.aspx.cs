using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Services;
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
                currencyCode.Value = XBDataProvider.Currency.GetCurrencyCodeByCompany(CompanyCode.Value);
                CompanyCode.Value = Session["CompanyCode"].ToString();
                currencyCode1.InnerText = currencyCode.Value;
                currencyCode2.InnerText = currencyCode.Value;
                DataRow row = null;
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                currencyDecimal.Value = XBDataProvider.Currency.GetCurrencyDecimalByCompany(CompanyCode.Value).ToString();
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
                    CreatedDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    CreatedDate.ReadOnly = true;
                    
                    PurchaseOrderId.Value = "0";
                    DataTable dtUser = XBDataProvider.User.GetUserById(Membership.GetUser().ProviderUserKey.ToString());
                    row = dtUser.Rows[0];
                    if (User.IsInRole("User"))
                    {
                        Location.Text = row["LocationName"].ToString();
                        LocationHidden.Value = row["LocationCode"].ToString();
                    }
                    else
                    {
                        Location.Text = row["DefaultLocationName"].ToString();
                        LocationHidden.Value = row["DefaultLocation"].ToString();
                    }
                    SalesMan.Text = row["EmployeeName"].ToString();
                    SalesManHidden.Value = row["EmployeeId"].ToString();
                    SetInitialRows();
                }
            }
        }

        public void SetPurchaseOrderDetails(DataTable purchaseOrderDetails)
        {
            try
            {
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
                SupplierId.ReadOnly = true;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                SalesMan.Text = row["Buyer"].ToString();
                SalesMan.ReadOnly = true;
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                Amount.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                Amount.ReadOnly = true;
                PayTerms.Text = row["PaymentTerms"].ToString();
                DeliveryTerms.Text = row["DeliveryTerms"].ToString();
                POTotalAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                POTotalDiscountAmt.Text = Convert.ToDecimal(row["DiscountAmount"]).ToString("f" + decimalPoints);
                POTotalTaxAmt.Text = Convert.ToDecimal(row["TaxAmount"]).ToString("f" + decimalPoints);
                POTotalOrderAmt.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                Telephone.Text = row["Telephone"].ToString();
                ShipToAddress.Text = row["ShipToAddress"].ToString();
                Telephone.ReadOnly = true;
                CreatedDate.Text = Convert.ToDateTime(row["PurchaseOrderDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedDate.ReadOnly = true;
                Location.Text = row["LocationCode"].ToString();
                Location.ReadOnly = true;
                if (Convert.ToInt32(row["IsFialized"].ToString()) > 0)
                {
                    IsFinalized.Value = "1";
                    btnConverOrder.Visible = false;
                    btnSaveDtl.Visible = false;
                }
                else
                {
                    IsFinalized.Value = "0";
                    btnConverOrder.Visible = true;
                }
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
                rowCount.Value = dt.Rows.Count.ToString();
                PurchaseOrderDetail.DataSource = dt;
                PurchaseOrderDetail.DataBind();
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
                for (int i = 0; i < 1; i++)
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
                rowCount.Value = dt.Rows.Count.ToString();
                PurchaseOrderDetail.DataSource = dt;
                PurchaseOrderDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                int selectedSequenceId = 0;
                int returnValue = 0;
                if (POSequenceNoID.Value!="")
                {
                    selectedSequenceId = Convert.ToInt32(POSequenceNoID.Value);
                }
                
                DataTable dtDeletedIds = new DataTable();
                DataRow drDeletedIds = null;
                dtDeletedIds.Columns.Add(new DataColumn("ID", typeof(int)));
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
                int[] deletedIds = DeletedRowIDs.Value.Split(',').Where(str => str != "").Select(str => int.Parse(str)).ToArray();
                for (int k = 0; k < deletedIds.Length; k++)
                {
                    drDeletedIds = dtDeletedIds.NewRow();
                    drDeletedIds["ID"] = deletedIds[k];
                    dtDeletedIds.Rows.Add(drDeletedIds);
                }
                foreach (GridViewRow row in PurchaseOrderDetail.Rows)
                {
                    TextBox box2 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[1].FindControl("POItem");
                    TextBox box3 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[2].FindControl("POName");
                    TextBox box4 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[5].FindControl("PORate");
                    TextBox box5 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[3].FindControl("POQuantity");
                    TextBox box6 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[4].FindControl("POUnit");
                    TextBox box7 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[6].FindControl("PODiscPer");
                    TextBox box8 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[7].FindControl("PODiscAmt");
                    TextBox box9 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxPer");
                    TextBox box10 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[9].FindControl("POTaxAmt");
                    TextBox box11 = (TextBox)PurchaseOrderDetail.Rows[i].Cells[10].FindControl("PONetAmt");
                    HiddenField hdnFld = (HiddenField)PurchaseOrderDetail.Rows[i].Cells[8].FindControl("POTaxCode");
                    if (Array.IndexOf(deletedIds, PurchaseOrderDetail.DataKeys[i]["ID"]) == -1 && box2.Text != "" && box2.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        dr["ID"] = PurchaseOrderDetail.DataKeys[i]["ID"]; 
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
                        dr["Currency"] = currencyCode.Value;
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                        dr["DiscountPercentage"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["DiscountAmt"] = float.Parse(box8.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Tax"] = hdnFld.Value;
                        dr["TaxPercentage"] = float.Parse(box9.Text, CultureInfo.InvariantCulture.NumberFormat);
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
                if (Request.Form["POItem"] != null)
                {
                    string[] POItems = Request.Form["POItem"].Split(',');
                    string[] PONames = Request.Form["POName"].Split(',');
                    string[] PORates = Request.Form["PORate"].Split(',');
                    string[] POQuantitys = Request.Form["POQuantity"].Split(',');
                    string[] POUnits = Request.Form["POUnit"].Split(',');
                    string[] PODiscPers = Request.Form["PODiscPer"].Split(',');
                    string[] PODiscAmts = Request.Form["PODiscAmt"].Split(',');
                    string[] POTaxPers = Request.Form["POTaxPer"].Split(',');
                    string[] POTaxAmts = Request.Form["POTaxAmt"].Split(',');
                    string[] PONetAmts = Request.Form["PONetAmt"].Split(',');
                    string[] POTaxCodes = Request.Form["POTaxCode"].Split(',');

                    for (int k = 0; k < POItems.Length; k++)
                    {
                        if (POItems[k] != "" && PONames[k] != "" && PORates[k] != "" && POQuantitys[k] != "" && PODiscPers[k] != "" && PODiscAmts[k] != "" && POTaxPers[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                            DateTime date = DateTime.ParseExact(CreatedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = Location.Text;
                            dr["PurchaseOrderMstId"] = Convert.ToInt32(PurchaseOrderId.Value);
                            dr["PurchaseOrderNo"] = OrderNo.Text;
                            dr["PurchaseOrderDate"] = date;
                            dr["ItemCode"] = POItems[k];
                            dr["ItemName"] = PONames[k];
                            dr["BaseUnitCode"] = POUnits[k];
                            dr["OrderQty"] = Convert.ToInt32(POQuantitys[k]);
                            dr["ReceivedQty"] = 0;
                            dr["InOrderQty"] = Convert.ToInt32(POQuantitys[k]);
                            dr["Currency"] = "";
                            dr["Rate"] = float.Parse(PORates[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountPercentage"] = float.Parse(PODiscPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountAmt"] = float.Parse(PODiscAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Tax"] = POTaxCodes[k];
                            dr["TaxPercentage"] = float.Parse(POTaxPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TaxAmount"] = float.Parse(POTaxAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["NetAmount"] = float.Parse(PONetAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Status"] = 1;
                            dr["ErrorMsg"] = null;
                            dr["Reference"] = Reference.Text;
                            dr["CreatedBy"] = User.Identity.Name;
                            dr["UpdatedBy"] = User.Identity.Name;
                            dr["CreatedDate"] = DateTime.Now.Date;
                            dr["UpdatedDate"] = DateTime.Now.Date;
                            dt.Rows.Add(dr);
                        }
                    }
                }
                if(PurchaseOrderId.Value=="0")
                {
                    returnValue = XBDataProvider.PurchaseOrder.SavePO(Session["CompanyCode"].ToString(), LocationHidden.Value,Request.Form[OrderNo.UniqueID],
                    DateTime.Today.Date, Convert.ToInt32(OrderType.SelectedValue), Reference.Text,Request.Form[SupplierId.UniqueID],
                    SalesManHidden.Value, 1, User.Identity.Name, selectedSequenceId, Request.Form[Telephone.UniqueID],Request.Form[Name.UniqueID],
                     PayTerms.Text, DeliveryTerms.Text, float.Parse(Request.Form[POTotalAmount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(POTotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), ShipToAddress.Text, dt, currencyCode.Value);
                    if (returnValue > 0)
                    {
                        OrderNo.Text = Request.Form[OrderNo.UniqueID];
                        SupplierId.ReadOnly = true;
                        Amount.Text = Request.Form[Amount.UniqueID];
                        Telephone.Text = Request.Form[Telephone.UniqueID];
                        Name.Text = Request.Form[Name.UniqueID];
                        SupplierId.Text = Request.Form[SupplierId.UniqueID];
                        PurchaseOrderId.Value = returnValue.ToString();
                        PageStatus.Value = "edit";
                        SaveSuccess.Visible = true;
                        failure.Visible = false;
                        Status.SelectedValue = "1";
                        btnConverOrder.Visible = true;
                        btnPrint.Visible = true;
                        SetPurchaseOrderChildGrid();
                    }
                    else
                    {
                        failure.Visible = true;
                    }
                }
                else
                {
                    if (dt.Rows.Count > 0)
                    {
                        XBDataProvider.PurchaseOrder.SavePODetail(Convert.ToInt32(PurchaseOrderId.Value), PayTerms.Text, DeliveryTerms.Text, float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(POTotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, ShipToAddress.Text, dt, dtDeletedIds);
                        btnConverOrder.Visible = true;
                        btnPrint.Visible = true;
                        PageStatus.Value = "edit";
                        Status.SelectedValue = "1";
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                        Amount.Text = Request.Form[Amount.UniqueID];
                        SetPurchaseOrderChildGrid();
                        
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                }
                
                

            }
            catch (Exception ex)
            {

            }
        }

        protected void BtnConvertOrderClick(object sender, EventArgs e)
        {
            if (XBDataProvider.PurchaseOrder.ConvertToGRN(Convert.ToInt32(PurchaseOrderId.Value), OrderType.SelectedItem.Text))
            {
                IsFinalized.Value = "2";
                btnConverOrder.Visible = false;
                btnSaveDtl.Visible = false;
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                FinalizeSuccess.Visible = true;
                failure.Visible = false;
                SetPurchaseOrderChildGrid();
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
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetPurchaseOrderSequenceDetails(companyCode);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    FirstFreeDetails firstFreeDetails = new FirstFreeDetails();
                    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                    firstFreeDetails.id = row["ID"].ToString();
                    firstFreeDetails.sequenceNumber = sequenceNo;
                    firstFreeDetails.seqType = row["SeqType"].ToString();
                    firstFreeDetails.orderType = row["OrderType"].ToString() == "Local" ? "0" : "1";
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
        public static List<CustomerDetails> GetPurchaseCustomerDetails(string companyCode)
        {
            List<CustomerDetails> result = new List<CustomerDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.BussinessPartner.GetAllBussinessPartnerSuplierCodes(companyCode);
                //Session["BPDetails"] = dtTable;

                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    CustomerDetails custDetails = new CustomerDetails();
                    custDetails.name = dtTable.Rows[i]["Name"].ToString();
                    custDetails.code = dtTable.Rows[i]["BusinessPartnerCode"].ToString();
                    custDetails.telephone = dtTable.Rows[i]["Phone"].ToString();
                    custDetails.orderType = dtTable.Rows[i]["OrderType"].ToString();
                    result.Add(custDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }

        protected void PurchaseOrderDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value) == 1)
            {
                LinkButton lnkDtn = e.Row.Cells[11].FindControl("lnkDeletePO") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (IsFinalized.Value == "1")
            {
                e.Row.Cells[11].Visible = false;
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox item = e.Row.Cells[5].FindControl("POItem") as TextBox;
                if (item.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    TextBox Rate = e.Row.Cells[5].FindControl("PORate") as TextBox;
                    TextBox Discount = e.Row.Cells[6].FindControl("PODiscPer") as TextBox;
                    TextBox DiscountAmt = e.Row.Cells[7].FindControl("PODiscAmt") as TextBox;
                    TextBox NetAmount = e.Row.Cells[9].FindControl("PONetAmt") as TextBox;
                    TextBox TaxAmount = e.Row.Cells[8].FindControl("POTaxAmt") as TextBox;
                    double rate = Convert.ToDouble(Rate.Text);
                    double discount = Convert.ToDouble(Discount.Text);
                    double discountAmt = Convert.ToDouble(DiscountAmt.Text);
                    double netAmount = Convert.ToDouble(NetAmount.Text);
                    double taxAmount = Convert.ToDouble(TaxAmount.Text);
                    Rate.Text = rate.ToString("f" + decimalPoints);
                    Discount.Text = discount.ToString("f" + decimalPoints);
                    DiscountAmt.Text = discountAmt.ToString("f" + decimalPoints);
                    NetAmount.Text = netAmount.ToString("f" + decimalPoints);
                    TaxAmount.Text = taxAmount.ToString("f" + decimalPoints);
                }
            }
        }
    }
}
