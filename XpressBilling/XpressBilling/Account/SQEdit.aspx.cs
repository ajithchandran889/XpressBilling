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
    public partial class sales_quotation : System.Web.UI.Page
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
                DataRow row = null;
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                currencyDecimal.Value = XBDataProvider.Currency.GetCurrencyDecimalByCompany(CompanyCode.Value).ToString();
                if (id != null && id != 0)
                {
                    DataTable salesQuotationDetails = XBDataProvider.SalesQuotation.GetSalesQuotationById(id);
                    if (salesQuotationDetails.Rows.Count > 0)
                    {
                        SetSalesQuotationDetails(salesQuotationDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
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
                    SalesQuotationId.Value = "0";
                    PageStatus.Value = "create";
                    CreatedDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    CreatedDate.ReadOnly = true;
                    Validity.Text = DateTime.Now.Date.AddDays(10).ToString("MM'/'dd'/'yyyy");
                    SalesOrder.ReadOnly = true;
                    SetInitialRows();
                }
            }
        }

        public void SetSalesQuotationDetails(DataTable salesQuotationDetails)
        {

            try
            {
                btnConverOrder.Visible = true;
                btnPrint.Visible = true;
                DataRow row = salesQuotationDetails.Rows[0];
                SalesQuotationId.Value = row["ID"].ToString();
                QuotationType.SelectedValue = row["OrderType"].ToString();
                QuotationType.Enabled = false;
                Quotation.Text = row["SalesQuotationNo"].ToString();
                Quotation.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                CustomerId.Text = row["BusinessPartnerCode"].ToString();
                CustomerId.ReadOnly = true;
                Name.Text = row["Name"].ToString();
                Name.ReadOnly = true;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                SalesManHidden.Value = row["SalesMan"].ToString();
                SalesMan.Text = row["SalesManName"].ToString();
                SalesMan.ReadOnly = true;
                Validity.Text = Convert.ToDateTime(row["Validity"]).ToString("MM'/'dd'/'yyyy"); ;
                PayTerms.Text = row["PaymentTerms"].ToString();
                DeliveryTerms.Text = row["DeliveryTerms"].ToString();
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                TotalAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                TotalDiscountAmt.Text = Convert.ToDecimal(row["DiscountAmount"]).ToString("f" + decimalPoints);
                TotalTaxAmt.Text = Convert.ToDecimal(row["TaxAmount"]).ToString("f" + decimalPoints);
                TotalOrderAmt.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                Telephone.Text = row["Telephone"].ToString();
                Telephone.ReadOnly = true;
                CreatedDate.Text = Convert.ToDateTime(row["SalesQuotationDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedDate.ReadOnly = true;
                LocationHidden.Value = row["LocationCode"].ToString();
                Location.Text = row["LocationName"].ToString();
                Location.ReadOnly = true;
                SalesOrder.Text = row["SalesOrderNo"].ToString();
                if (SalesOrder.Text != "")
                {
                    Validity.ReadOnly = true;
                    btnConverOrder.Visible = false;
                    SaveBtn.Visible = false;
                }

                SetSalesQuotationChildGrid();
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
                dt.Columns.Add(new DataColumn("Qty", typeof(float)));
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

                ViewState["CurrentTable"] = dt;

                SalesQuotationDetail.DataSource = dt;
                SalesQuotationDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        public void SetSalesQuotationChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.SalesQuotation.GetSalesQuotationDtlById(Convert.ToInt32(SalesQuotationId.Value));

            if (dt.Rows.Count > 0)
            {
                rowCount.Value = dt.Rows.Count.ToString();
                SalesQuotationDetail.DataSource = dt;
                SalesQuotationDetail.DataBind();
                
                return;
            }

        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                int returnValue = 0;
                DataTable dtDeletedIds = new DataTable();
                DataRow drDeletedIds = null;
                dtDeletedIds.Columns.Add(new DataColumn("ID", typeof(int)));
                DateTime validity = DateTime.ParseExact(Validity.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("LocationCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesQuotationMstID", typeof(int)));
                dt.Columns.Add(new DataColumn("SalesQuotationNo", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesQuotationDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("Currency", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(float)));
                dt.Columns.Add(new DataColumn("TotalRate", typeof(float)));
                dt.Columns.Add(new DataColumn("Discount", typeof(float)));
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
                DateTime date = DateTime.ParseExact(CreatedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                int[] deletedIds = DeletedRowIDs.Value.Split(',').Where(str => str != "").Select(str => int.Parse(str)).ToArray();
                for (int k = 0; k < deletedIds.Length; k++)
                {
                    drDeletedIds = dtDeletedIds.NewRow();
                    drDeletedIds["ID"] = deletedIds[k];
                    dtDeletedIds.Rows.Add(drDeletedIds);
                }
                foreach (GridViewRow row in SalesQuotationDetail.Rows)
                {
                    TextBox box2 = (TextBox)SalesQuotationDetail.Rows[i].Cells[1].FindControl("SQItem");
                    TextBox box3 = (TextBox)SalesQuotationDetail.Rows[i].Cells[2].FindControl("SQName");
                    TextBox box4 = (TextBox)SalesQuotationDetail.Rows[i].Cells[5].FindControl("SQRate");
                    TextBox box5 = (TextBox)SalesQuotationDetail.Rows[i].Cells[3].FindControl("SQQuantity");
                    TextBox box6 = (TextBox)SalesQuotationDetail.Rows[i].Cells[4].FindControl("SQUnit");
                    TextBox box7 = (TextBox)SalesQuotationDetail.Rows[i].Cells[6].FindControl("SQDiscPer");
                    TextBox box8 = (TextBox)SalesQuotationDetail.Rows[i].Cells[7].FindControl("SQDiscAmt");
                    TextBox box9 = (TextBox)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxPer");
                    TextBox box10 = (TextBox)SalesQuotationDetail.Rows[i].Cells[9].FindControl("SQTaxAmt");
                    TextBox box11 = (TextBox)SalesQuotationDetail.Rows[i].Cells[10].FindControl("SQNetAmt");
                    HiddenField hdnFld = (HiddenField)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxCode");
                    if (Array.IndexOf(deletedIds, SalesQuotationDetail.DataKeys[i]["ID"]) == -1 && box2.Text != "")
                    {
                        dr = dt.NewRow();
                        dr["ID"] = SalesQuotationDetail.DataKeys[i]["ID"];
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = LocationHidden.Value;
                        dr["SalesQuotationMstID"] = Convert.ToInt32(SalesQuotationId.Value);
                        dr["SalesQuotationNo"] = Quotation.Text;
                        dr["SalesQuotationDate"] = date;
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = currencyCode.Value;
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TotalRate"] = float.Parse(TotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Discount"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
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
                if (Request.Form["SQItem"] != null)
                {
                    string[] SQItems = Request.Form["SQItem"].Split(',');
                    string[] SQNames = Request.Form["SQName"].Split(',');
                    string[] SQRates = Request.Form["SQRate"].Split(',');
                    string[] SQQuantities = Request.Form["SQQuantity"].Split(',');
                    string[] SQUnits = Request.Form["SQUnit"].Split(',');
                    string[] SQDiscPers = Request.Form["SQDiscPer"].Split(',');
                    string[] SQDiscAmts = Request.Form["SQDiscAmt"].Split(',');
                    string[] SQTaxPers = Request.Form["SQTaxPer"].Split(',');
                    string[] SQTaxAmts = Request.Form["SQTaxAmt"].Split(',');
                    string[] SQNetAmts = Request.Form["SQNetAmt"].Split(',');
                    string[] SQTaxCodes = Request.Form["SQTaxCode"].Split(',');

                    for (int k = 0; k < SQItems.Length; k++)
                    {
                        if (SQItems[k] != "" && SQNames[k] != "" && SQRates[k] != "" && SQQuantities[k] != "" && SQDiscPers[k] != "" && SQDiscAmts[k] != "" && SQTaxPers[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = LocationHidden.Value;
                            dr["SalesQuotationMstID"] = Convert.ToInt32(SalesQuotationId.Value);
                            dr["SalesQuotationNo"] = Quotation.Text;
                            dr["SalesQuotationDate"] = date;
                            dr["ItemCode"] = SQItems[k];
                            dr["ItemName"] = SQNames[k];
                            dr["BaseUnitCode"] = SQUnits[k];
                            dr["Qty"] = Convert.ToInt32(SQQuantities[k].ToString());
                            dr["Currency"] = currencyCode.Value;
                            dr["Rate"] = float.Parse(SQRates[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TotalRate"] = float.Parse(TotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat);
                            dr["Discount"] = float.Parse(SQDiscPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountAmt"] = float.Parse(SQDiscAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Tax"] = SQTaxCodes[k];
                            dr["TaxPercentage"] = float.Parse(SQTaxPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TaxAmount"] = float.Parse(SQTaxAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["NetAmount"] = float.Parse(SQNetAmts[k], CultureInfo.InvariantCulture.NumberFormat);
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
                

                if (SalesQuotationId.Value == "0")
                {
                    int selectedSequenceId = Convert.ToInt32(SQSequenceNoID.Value);
                    returnValue = XBDataProvider.SalesQuotation.SaveSQ(Session["CompanyCode"].ToString(), LocationHidden.Value, Request.Form[Quotation.UniqueID],
                    DateTime.Today.Date, Convert.ToInt32(selectedQuotationType.Value), Reference.Text, CustomerId.Text,
                    SalesManHidden.Value, validity, 1, User.Identity.Name, selectedSequenceId, Telephone.Text,
                    PayTerms.Text, DeliveryTerms.Text, float.Parse(TotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat),
                    float.Parse(TotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(TotalTaxAmt.Text,
                    CultureInfo.InvariantCulture.NumberFormat), float.Parse(TotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), dt, Name.Text, currencyCode.Value);
                    if (returnValue > 0)
                    {
                        QuotationType.SelectedValue = selectedQuotationType.Value;
                        SalesQuotationId.Value = returnValue.ToString();
                        Quotation.Text = Request.Form[Quotation.UniqueID];
                        CustomerId.ReadOnly = true;
                        Name.ReadOnly = true;
                        Telephone.ReadOnly = true;
                        Location.ReadOnly = true;
                        SalesMan.ReadOnly = true;
                        PageStatus.Value = "edit";
                        SetSalesQuotationChildGrid();
                        SaveSuccess.Visible = true;
                        failure.Visible = false;
                        btnConverOrder.Visible = true;
                        btnPrint.Visible = true;
                        Status.SelectedValue = "1";
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
                        XBDataProvider.SalesQuotation.SaveSQDetail(Convert.ToInt32(SalesQuotationId.Value), Reference.Text, validity, PayTerms.Text, DeliveryTerms.Text, float.Parse(TotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(TotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(TotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(TotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt, dtDeletedIds);
                        btnConverOrder.Visible = true;
                        btnPrint.Visible = true;
                        Status.SelectedValue = "1";
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                        PageStatus.Value = "edit";
                        SetSalesQuotationChildGrid();
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

        private void ClearInputs(ControlCollection ctrls)
        {
            foreach (Control ctrl in ctrls)
            {
                if (ctrl is TextBox)
                    ((TextBox)ctrl).Text = string.Empty;
                else if (ctrl is DropDownList)
                    ((DropDownList)ctrl).ClearSelection();

                ClearInputs(ctrl.Controls);
            }
        }

        [WebMethod]
        public static List<ContactDetails> GetContactCodes(string companyCode)
        {
            List<ContactDetails> result = new List<ContactDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.Contact.GetAllContactCode(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ContactDetails itemContact = new ContactDetails();
                    itemContact.code = row["Contact"].ToString();

                    result.Add(itemContact);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        [WebMethod]
        public static List<ContactDetails> GetEmployeeCodes(string companyCode)
        {
            List<ContactDetails> result = new List<ContactDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.Employee.GetAllEmployeCode(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ContactDetails itemContact = new ContactDetails();
                    itemContact.code = row["EmployeeCode"].ToString();
                    itemContact.name = row["Name"].ToString();
                    result.Add(itemContact);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        [WebMethod]
        public static List<ContactDetails> GetLocationCodes(string companyCode)
        {
            List<ContactDetails> result = new List<ContactDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.Location.GetAllLocations(companyCode);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ContactDetails itemContact = new ContactDetails();
                    itemContact.code = row["LocationCode"].ToString();
                    itemContact.name = row["Name"].ToString();
                    result.Add(itemContact);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        [WebMethod]
        public static List<CustomerDetails> GetCustomerDetails(string companyCode)
        {
            List<CustomerDetails> result = new List<CustomerDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.BussinessPartner.GetAllBussinessPartnerCustomerCodes(companyCode);
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

        [WebMethod]
        public static List<FirstFreeDetails> GetFirstFreerDetails(string companyCode)
        {
            List<FirstFreeDetails> result = new List<FirstFreeDetails>();
            try
            {
                DataRow row = null;
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetSaleQuotationCashCreditSequenceDetails(companyCode);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    FirstFreeDetails firstFreeDetails = new FirstFreeDetails();
                    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                    firstFreeDetails.id = row["ID"].ToString();
                    firstFreeDetails.sequenceNumber = sequenceNo;
                    firstFreeDetails.seqType = row["SeqType"].ToString();
                    firstFreeDetails.orderType = row["OrderType"].ToString() == "Cash" ? "0" : "1";
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
        public static List<ItemMasteDetailsSQ> GetItemMasters(string companyCode, string orderType, string priceType, string location)
        {
            List<ItemMasteDetailsSQ> result = new List<ItemMasteDetailsSQ>();
            try
            {
                DataTable dtTable = XBDataProvider.SalesQuotation.GetItemMasters(companyCode, Convert.ToInt32(orderType), Convert.ToInt32(priceType),location);
                DataRow row = null;
                for (int index = 0; index < dtTable.Rows.Count; index++)
                {
                    row = dtTable.Rows[index];
                    ItemMasteDetailsSQ itemMasteDetails = new ItemMasteDetailsSQ();
                    itemMasteDetails.code = row["ItemCode"].ToString();
                    itemMasteDetails.name = row["Name"].ToString();
                    itemMasteDetails.BaseUnitCode = row["BaseUnitCode"].ToString();
                    itemMasteDetails.supplierBarcode = row["SupplierBarcode"].ToString();
                    itemMasteDetails.mrp = Convert.ToInt32(row["MRP"].ToString());
                    itemMasteDetails.retailPrice = Convert.ToInt32(row["RetailPrice"].ToString());
                    itemMasteDetails.TaxCode = row["TaxCode"].ToString();
                    itemMasteDetails.TaxPer = row["TaxPercentage"].ToString() != "" ? Convert.ToInt32(row["TaxPercentage"].ToString()) : 0;
                    itemMasteDetails.Qnty = row["Qnty"].ToString() != "" ? Convert.ToInt32(row["Qnty"].ToString()) : 0;
                    itemMasteDetails.currencyCode = row["CurrencyCode"].ToString();
                    itemMasteDetails.decimalPoint = row["Decimal"].ToString();
                    itemMasteDetails.itemType = Convert.ToInt32(row["ItemType"].ToString());
                    result.Add(itemMasteDetails);
                }
            }
            catch (Exception e)
            {

            }


            return result;
        }

        protected void BtnConvertOrderClick(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetSaleOrderCashCreditSequenceDetails(Session["CompanyCode"].ToString());
                DataRow row = null;
                string orderNo = "";
                if (dtTableSequenceDetails.Rows.Count > 0)
                {
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];

                        if (row["SeqType"].ToString() == "1" && row["OrderType"].ToString() == "Cash" && QuotationType.SelectedValue == "0" && row["EnterpriseUnitCode"].ToString() == LocationHidden.Value)
                        {
                            orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                            salesOrderLastIncId.Value = row["ID"].ToString();
                        }
                        else if (row["SeqType"].ToString() == "1" && row["OrderType"].ToString() == "Credit" && QuotationType.SelectedValue == "1" && row["EnterpriseUnitCode"].ToString() == LocationHidden.Value)
                        {
                            orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                            salesOrderLastIncId.Value = row["ID"].ToString();
                        }
                        else if (row["SeqType"].ToString() == "0" && row["OrderType"].ToString() == "Cash" && QuotationType.SelectedValue == "0")
                        {
                            orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                            salesOrderLastIncId.Value = row["ID"].ToString();
                        }
                        else if (row["SeqType"].ToString() == "0" && row["OrderType"].ToString() == "Credit" && QuotationType.SelectedValue == "1")
                        {
                            orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                            salesOrderLastIncId.Value = row["ID"].ToString();
                        }
                    }

                    if (XBDataProvider.SalesQuotation.ConvertToSaleOrder(Convert.ToInt32(SalesQuotationId.Value), QuotationType.SelectedItem.Text, orderNo, Convert.ToInt32(salesOrderLastIncId.Value)))
                    {
                        Validity.ReadOnly = true;
                        SalesOrder.Text = orderNo;
                        btnConverOrder.Visible = false;
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        FinalizeSuccess.Visible = true;
                        failure.Visible = false;
                        SaveBtn.Visible = false;
                        Validity.ReadOnly = true;
                        Reference.ReadOnly = true;
                        SalesQuotationDetail.Columns[11].Visible = false;
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
                else
                {
                    failureSO.Visible = true;
                }

            }
            catch (Exception ex)
            {

            }

        }

        protected void SalesQuotationDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value) == 1)
            {
                LinkButton lnkDtn = e.Row.Cells[11].FindControl("lnkDeleteSQ") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (SalesOrder.Text != "")
            {
                e.Row.Cells[11].Visible = false;
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox item = e.Row.Cells[5].FindControl("SQItem") as TextBox;
                if (item.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    TextBox Rate = e.Row.Cells[5].FindControl("SQRate") as TextBox;
                    TextBox Discount = e.Row.Cells[6].FindControl("SQDiscPer") as TextBox;
                    TextBox DiscountAmt = e.Row.Cells[7].FindControl("SQDiscAmt") as TextBox;
                    TextBox NetAmount = e.Row.Cells[9].FindControl("SQNetAmt") as TextBox;
                    TextBox TaxAmount = e.Row.Cells[8].FindControl("SQTaxAmt") as TextBox;
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
    public class ContactDetails
    {
        public string code { get; set; }
        public string name { get; set; }
    }
    public class CustomerDetails
    {
        public string code { get; set; }
        public string name { get; set; }
        public string telephone { get; set; }
        public string orderType { get; set; }
    }
    public class FirstFreeDetails
    {
        public string id { get; set; }
        public string sequenceNumber { get; set; }
        public string seqType { get; set; }
        public string orderType { get; set; }
        public string enterpriseUnitCode { get; set; }
    }
    public class ItemMasteDetailsSQ
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