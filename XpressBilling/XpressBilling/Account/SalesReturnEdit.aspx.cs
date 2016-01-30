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
    public partial class SalesReturnEdit : System.Web.UI.Page
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
                currencyCode.Value = XBDataProvider.Currency.GetCurrencyCodeByCompany(CompanyCode.Value);
                currencyDecimal.Value = XBDataProvider.Currency.GetCurrencyDecimalByCompany(CompanyCode.Value).ToString();
                currencyCode1.InnerText = currencyCode.Value;
                currencyCode2.InnerText = currencyCode.Value;
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
                {
                    DataTable salesReturnDetails = XBDataProvider.SalesRetrun.GetSalesReturnById(id);
                    if (salesReturnDetails.Rows.Count > 0)
                    {
                        SetSalesReturnDetails(salesReturnDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
                    Date.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy"); PageStatus.Value = "create";
                    SalesReturnMstId.Value = "0";
                    btnFinalize.Visible = false;
                    btnPrint.Visible = false;
                    SetInitialRows();
                }
            }
        }

        public void SetSalesReturnDetails(DataTable salesReturnDetails)
        {
            try
            {
                DataRow row = salesReturnDetails.Rows[0];
                SalesReturnMstId.Value = row["ID"].ToString();
                SalesReturnType.SelectedValue = row["SalesReturnOrderType"].ToString();
                SalesReturnType.Enabled = false;
                SalesReturn.Text = row["SalesReturnNo"].ToString();
                Status.SelectedValue = row["Status"].ToString();
                Location.Text = row["LocationName"].ToString();
                Name.Text = row["BPName"].ToString();
                Reference.Text = row["Reference"].ToString();
                Telephone.Text = row["Telephone"].ToString();
                Reference.ReadOnly = true;
                Date.Text = Convert.ToDateTime(row["SalesReturnDate"]).ToString("MM'/'dd'/'yyyy");
                Date.ReadOnly = true;
                SalesMan.Text = row["SalesManName"].ToString();
                SalesMan.ReadOnly = true;
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("0.00");
                Amount.ReadOnly = true;
                SRCustomerId.ReadOnly = true;
                SRCustomerId.Text = row["BusinessPartnerCode"].ToString();
                SalesOrderNo.ReadOnly = true;
                SalesOrderNo.Text = row["SalesOrderNo"].ToString();
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                Amount.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                SRTotalAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                SRTotalDiscountAmt.Text = Convert.ToDecimal(row["DiscountAmount"]).ToString("f" + decimalPoints);
                SRTotalTaxAmt.Text = Convert.ToDecimal(row["TaxAmount"]).ToString("f" + decimalPoints);
                SRTotalOrderAmt.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                Demurages.Text = Convert.ToDecimal(row["Demurages"]).ToString("f" + decimalPoints);
                SRCorrectTotalOrderAmtHidden.Value = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints) + Convert.ToDecimal(row["Demurages"]).ToString("f" + decimalPoints);
                DisplaySalesReturnDetails();
                if(row["Status"].ToString()=="2")
                {
                    btnSaveDtl.Visible = false;
                    btnFinalize.Visible = false;
                }
                else
                {
                    if(SalesReturnType.SelectedValue=="1")
                        {
                            SalesOrderNo.Attributes["class"]="form-control";
                        }
                }
            }
            catch (Exception e)
            {

            }


        }

        private void DisplaySalesReturnDetails()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = XBDataProvider.SalesRetrun.GetSalesReturnDetailsByID(Convert.ToInt32(SalesReturnMstId.Value));

                if (dt.Rows.Count > 0)
                {
                    rowCount.Value = dt.Rows.Count.ToString();
                    SalesReturnDetail.DataSource = dt;
                    SalesReturnDetail.DataBind();
                    if (SalesOrderNo.Text != "")
                    {
                        SalesOrderDate.Value = dt.Rows[0]["SalesOrderDate"].ToString();
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
                dt.Columns.Add(new DataColumn("Rate", typeof(int)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("DiscountPercentage", typeof(string)));
                dt.Columns.Add(new DataColumn("DiscountAmt", typeof(int)));
                dt.Columns.Add(new DataColumn("Tax", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxPercentage", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxAmount", typeof(int)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(string)));
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["ItemCode"] = string.Empty;
                    dr["ItemName"] = string.Empty;
                    dr["Rate"] = DBNull.Value;
                    dr["Qty"] = DBNull.Value;
                    dr["BaseUnitCode"] = string.Empty;
                    dr["DiscountPercentage"] = string.Empty;
                    dr["DiscountAmt"] = DBNull.Value;
                    dr["Tax"] = string.Empty;
                    dr["TaxPercentage"] = string.Empty;
                    dr["TaxAmount"] = DBNull.Value;
                    dr["NetAmount"] = string.Empty;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                SalesReturnDetail.DataSource = dt;
                SalesReturnDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                int returnValue = 0;
                DataTable dt = new DataTable();
                DataTable dtDeletedIds = new DataTable();
                DataRow drDeletedIds = null;
                dtDeletedIds.Columns.Add(new DataColumn("ID", typeof(int)));
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("LocationCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesReturnMstId", typeof(int)));
                dt.Columns.Add(new DataColumn("SalesReturnNo", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesReturnDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("BusinessPartnerCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesOrderNo", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesOrderDate", typeof(DateTime)));
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
                int[] deletedIds = DeletedRowIDs.Value.Split(',').Where(str => str != "").Select(str => int.Parse(str)).ToArray();
                for (int k = 0; k < deletedIds.Length; k++)
                {
                    drDeletedIds = dtDeletedIds.NewRow();
                    if (deletedIds[k]!=0)
                    {
                        drDeletedIds["ID"] = deletedIds[k];
                        dtDeletedIds.Rows.Add(drDeletedIds);
                    }
                    else
                    {
                        deletedIds[k]=-1;
                    }
                }
                foreach (GridViewRow row in SalesReturnDetail.Rows)
                {
                    TextBox box2 = (TextBox)SalesReturnDetail.Rows[i].Cells[1].FindControl("SRItem");
                    TextBox box3 = (TextBox)SalesReturnDetail.Rows[i].Cells[2].FindControl("SRItemName");
                    TextBox box4 = (TextBox)SalesReturnDetail.Rows[i].Cells[5].FindControl("SRItemRate");
                    TextBox box5 = (TextBox)SalesReturnDetail.Rows[i].Cells[3].FindControl("SRQuantity");
                    TextBox box6 = (TextBox)SalesReturnDetail.Rows[i].Cells[4].FindControl("SRUnit");
                    TextBox box7 = (TextBox)SalesReturnDetail.Rows[i].Cells[6].FindControl("SRDiscPer");
                    TextBox box8 = (TextBox)SalesReturnDetail.Rows[i].Cells[7].FindControl("SRDiscAmt");
                    TextBox box9 = (TextBox)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxPer");
                    TextBox box10 = (TextBox)SalesReturnDetail.Rows[i].Cells[9].FindControl("SRTaxAmt");
                    TextBox box11 = (TextBox)SalesReturnDetail.Rows[i].Cells[10].FindControl("SRNetAmt");
                    HiddenField hdnFld = (HiddenField)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxCode");
                    if (box2.Text != "" && box2.Text.Length != 0 && Array.IndexOf(deletedIds, SalesReturnDetail.DataKeys[i]["ID"]) == -1)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(SalesReturnDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] = DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(SalesReturnDetail.DataKeys[i]["ID"]);
                        }
                        DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = LocationHidden.Value;
                        dr["SalesReturnMstId"] = Convert.ToInt32(SalesReturnMstId.Value);
                        dr["SalesReturnNo"] = SalesReturn.Text;
                        dr["SalesReturnDate"] = date;
                        dr["BusinessPartnerCode"] = SRCustomerId.Text;
                        dr["SalesOrderNo"] = SalesOrderNo.Text;
                        if(SalesOrderNo.Text!="")
                        {
                            dr["SalesOrderDate"] = SalesOrderDate.Value;
                        }
                        else
                        {
                            dr["SalesOrderDate"] = DateTime.Now;
                        }
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = currencyCode.Value;
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
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
                if (Request.Form["SRItem"] != null)
                {
                    string[] IItems = Request.Form["SRItem"].Split(',');
                    string[] IItemNames = Request.Form["SRItemName"].Split(',');
                    string[] IItemRates = Request.Form["SRItemRate"].Split(',');
                    string[] IQuantities = Request.Form["SRQuantity"].Split(',');
                    string[] IUnits = Request.Form["SRUnit"].Split(',');
                    string[] IDiscPers = Request.Form["SRDiscPer"].Split(',');
                    string[] IDiscAmts = Request.Form["SRDiscAmt"].Split(',');
                    string[] ITaxPers = Request.Form["SRTaxPer"].Split(',');
                    string[] ITaxAmts = Request.Form["SRTaxAmt"].Split(',');
                    string[] INetAmts = Request.Form["SRNetAmt"].Split(',');
                    string[] ITaxCodes = Request.Form["SRTaxCode"].Split(',');

                    for (int k = 0; k < IItems.Length; k++)
                    {
                        if (IItems[k] != "" && IItemNames[k] != "" && IItemRates[k] != "" && IQuantities[k] != "" && IDiscPers[k] != "" && IDiscAmts[k] != "" && ITaxPers[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                           
                            DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = LocationHidden.Value;
                            dr["SalesReturnMstId"] = Convert.ToInt32(SalesReturnMstId.Value);
                            dr["SalesReturnNo"] = SalesReturn.Text;
                            dr["SalesReturnDate"] = date;
                            dr["BusinessPartnerCode"] = SRCustomerId.Text;
                            dr["SalesOrderNo"] = SalesOrderNo.Text;
                            if (SalesOrderNo.Text != "")
                            {
                                dr["SalesOrderDate"] = SalesOrderDate.Value;
                            }
                            else
                            {
                                dr["SalesOrderDate"] = DateTime.Now;
                            }
                            dr["ItemCode"] = IItems[k];
                            dr["ItemName"] = IItemNames[k];
                            dr["BaseUnitCode"] = IUnits[k];
                            dr["Qty"] = Convert.ToInt32(IQuantities[k]);
                            dr["Currency"] = currencyCode.Value;
                            dr["Rate"] = float.Parse(IItemRates[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Discount"] = float.Parse(IDiscPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountAmt"] = float.Parse(IDiscAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Tax"] = ITaxCodes[k];
                            dr["TaxPercentage"] = float.Parse(ITaxPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TaxAmount"] = float.Parse(ITaxAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["NetAmount"] = float.Parse(INetAmts[k], CultureInfo.InvariantCulture.NumberFormat);
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
                if (SalesReturnMstId.Value=="0")
                {
                    int selectedSequenceId = Convert.ToInt32(SRSequenceNoID.Value);
                    DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    float amount = 0;
                    if (Request.Form[Amount.UniqueID] != "0")
                    {
                        amount = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                    }
                    returnValue = XBDataProvider.SalesRetrun.SaveSalesReturn(Session["CompanyCode"].ToString(), Convert.ToInt32(SalesReturnType.SelectedValue), SRCustomerId.Text,Request.Form[SalesReturn.UniqueID], 1, date, LocationHidden.Value, SalesManHidden.Value,
                        User.Identity.Name, Reference.Text, amount,Request.Form[Name.UniqueID] ,Request.Form[Telephone.UniqueID], SalesOrderNo.Text, selectedSequenceId, SRPayTerms.Text,
                            float.Parse(SRTotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalDiscountAmt.Text,
                            CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat),
                            float.Parse(SRTotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(Demurages.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt);
                    if (returnValue > 0)
                    {
                        SalesReturnMstId.Value = returnValue.ToString();
                        SalesReturn.Text = Request.Form[SalesReturn.UniqueID];
                        Name.Text = Request.Form[Name.UniqueID];
                        Telephone.Text = Request.Form[Telephone.UniqueID];
                        int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                        Amount.Text = Convert.ToDecimal(Request.Form[Amount.UniqueID]).ToString("f" + decimalPoints);
                        PageStatus.Value = "edit";
                        Status.SelectedValue = "1";
                        SaveSuccess.Visible = true;
                        failure.Visible = false;
                        btnFinalize.Visible = true;
                        btnPrint.Visible = true;
                        SRCustomerId.ReadOnly = true;
                        Location.ReadOnly = true;
                        SalesOrderNo.ReadOnly = true;
                        SalesMan.ReadOnly = true;
                        SalesReturnType.Enabled = false;
                        if(SalesReturnType.SelectedValue=="1")
                        {
                            SalesOrderNo.Attributes["class"]="form-control";
                        }
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
                        XBDataProvider.SalesRetrun.SaveSRDetail(Convert.ToInt32(SalesReturnMstId.Value), SRPayTerms.Text,Reference.Text,
                            float.Parse(SRTotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalDiscountAmt.Text,
                            CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat),
                            float.Parse(SRTotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(Demurages.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt, dtDeletedIds);
                        PageStatus.Value = "edit";
                        Amount.Text = Request.Form[Amount.UniqueID];
                        Status.SelectedValue = "1";
                        btnFinalize.Visible = true;
                        btnPrint.Visible = true;
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                }
                
                DisplaySalesReturnDetails();

            }
            catch (Exception ex)
            {

            }
        }

        protected void btnFinalizeClick(object sender, EventArgs e)
        {
            if (XBDataProvider.SalesRetrun.FinlizeSalesReturn(Convert.ToInt32(SalesReturnMstId.Value)))
            {
                Status.SelectedValue = "2";
                SalesReturnMstId.Value = SalesReturnMstId.Value;
                DisplaySalesReturnDetails();
                PageStatus.Value = "edit";
                Amount.Text = Request.Form[Amount.UniqueID];
                btnFinalize.Visible = false;
                btnSaveDtl.Visible = false;
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

        protected void buttonUpdate_Click(object sender, EventArgs e)
        {
            if (SalesReturnType.SelectedValue == "0")
            {
                SalesReturn.Text = Request.Form[SalesReturn.UniqueID];
                DataTable dtTable = XBDataProvider.SalesRetrun.GetInvoiceDetailsUsingInvoiceNo(SalesOrderNo.Text);
                rowCount.Value = dtTable.Rows.Count.ToString();
                SalesReturnDetail.DataSource = dtTable;
                SalesReturnDetail.DataBind();

                Name.Text = Request.Form[Name.UniqueID];
                Telephone.Text = Request.Form[Telephone.UniqueID];
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                SRTotalAmount.Text = Convert.ToDecimal(SRTotalAmountHidden.Value).ToString("f" + decimalPoints);
                SRTotalTaxAmt.Text = Convert.ToDecimal(SRTotalTaxAmtHidden.Value).ToString("f" + decimalPoints);
                SRTotalDiscountAmt.Text = Convert.ToDecimal(SRTotalDiscountAmtHidden.Value).ToString("f" + decimalPoints);
                SRTotalOrderAmt.Text = Convert.ToDecimal(SRTotalOrderAmtHidden.Value).ToString("f" + decimalPoints);
                Amount.Text = Convert.ToDecimal(SRTotalOrderAmtHidden.Value).ToString("f" + decimalPoints);
                SRCorrectTotalOrderAmtHidden.Value = Convert.ToDecimal(SRTotalOrderAmtHidden.Value).ToString("f" + decimalPoints);
                Demurages.Text = 0.ToString();
            }
            else
            {
                SetInitialRows();
                SalesOrderNo.Text = "";
                SRTotalAmount.Text = "";
                SRTotalTaxAmt.Text = "";
                SRTotalDiscountAmt.Text = "";
                SRTotalOrderAmt.Text = "";
                Amount.Text = "0";
                SalesReturn.Text = Request.Form[SalesReturn.UniqueID];
            }
        }

        [WebMethod]
        public static List<FirstFreeDetails> GetFirstFreerDetails(string companyCode)
        {
            List<FirstFreeDetails> result = new List<FirstFreeDetails>();
            try
            {
                DataRow row = null;
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetSalesReturnSequenceDetails(companyCode);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    FirstFreeDetails firstFreeDetails = new FirstFreeDetails();
                    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                    firstFreeDetails.id = row["ID"].ToString();
                    firstFreeDetails.sequenceNumber = sequenceNo;
                    firstFreeDetails.seqType = row["SeqType"].ToString();
                    firstFreeDetails.orderType = row["OrderType"].ToString() == "Against Order" ? "0" : "1";
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
        public static List<SalesOrderDetails> GetFinalizedSalesOrderDetails(string bpCode)
        {
            List<SalesOrderDetails> result = new List<SalesOrderDetails>();
            try
            {
                DataRow row = null;
                DataTable dtTableSalesOrderDetails = XBDataProvider.SalesRetrun.GetFinalizedSalesOrderDetails(bpCode);
                for (int i = 0; i < dtTableSalesOrderDetails.Rows.Count; i++)
                {
                    row = dtTableSalesOrderDetails.Rows[i];
                    SalesOrderDetails salesOrderDetails = new SalesOrderDetails();
                    salesOrderDetails.salesOrderNo = row["SalesOrderNo"].ToString();
                    salesOrderDetails.salesMan = row["SalesMan"].ToString();
                    salesOrderDetails.salesManName = row["SalesManName"].ToString();
                    salesOrderDetails.locationCode = row["LocationCode"].ToString();
                    salesOrderDetails.locationName = row["LocationName"].ToString();
                    salesOrderDetails.amount = row["Amount"].ToString();
                    salesOrderDetails.taxAmount = row["DiscountAmount"].ToString();
                    salesOrderDetails.discountAmount = row["TaxAmount"].ToString();
                    salesOrderDetails.orderAmount = row["OrderAmount"].ToString();
                    salesOrderDetails.salesOrderDate = row["SalesOrderDate"].ToString();
                    result.Add(salesOrderDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }

        protected void SalesReturnDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value) == 1)
            {
                LinkButton lnkDtn = e.Row.Cells[11].FindControl("lnkDeleteSR") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (Status.SelectedValue == "2")
            {
                e.Row.Cells[11].Visible = false;
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox item = e.Row.Cells[5].FindControl("SRItem") as TextBox;
                if (item.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    TextBox Rate = e.Row.Cells[5].FindControl("SRItemRate") as TextBox;
                    TextBox Discount = e.Row.Cells[6].FindControl("SRDiscPer") as TextBox;
                    TextBox DiscountAmt = e.Row.Cells[7].FindControl("SRDiscAmt") as TextBox;
                    TextBox NetAmount = e.Row.Cells[9].FindControl("SRNetAmt") as TextBox;
                    TextBox TaxAmount = e.Row.Cells[8].FindControl("SRTaxPer") as TextBox;
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
    public class SalesOrderDetails
    {
        public string salesOrderNo { get; set; }
        public string salesMan { get; set; }
        public string salesManName { get; set; }
        public string locationCode { get; set; }
        public string locationName { get; set; }
        public string orderAmount { get; set; }
        public string taxAmount { get; set; }
        public string discountAmount { get; set; }
        public string amount { get; set; }
        public string salesOrderDate { get; set; }
    }
}