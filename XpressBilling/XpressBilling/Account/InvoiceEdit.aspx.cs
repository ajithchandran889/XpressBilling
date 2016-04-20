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
    public partial class InvoiceEdit : System.Web.UI.Page
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
                currencyCode1.InnerText = currencyCode.Value;
                currencyCode2.InnerText = currencyCode.Value;
                currencyDecimal.Value = XBDataProvider.Currency.GetCurrencyDecimalByCompany(CompanyCode.Value).ToString();
                DataRow row = null;
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != 0)
                {
                    DataTable InvoiceDetails = XBDataProvider.Invoice.GetInvoiceById(id);
                    if (InvoiceDetails.Rows.Count > 0)
                    {
                        SetInvoiceDetails(InvoiceDetails);
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
                    Date.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    Invoice.ReadOnly = true;
                    SetInitialRows();
                    PageStatus.Value = "create";
                    SalesInvoiceId.Value = "0";
                }
            }
        }

        public void SetInvoiceDetails(DataTable InvoiceDetails)
        {
            try
            {
                btnPrint.Visible = true;

                DataRow row = InvoiceDetails.Rows[0];
                SalesInvoiceId.Value = row["ID"].ToString();
                CustomerIdSI.Text = row["BusinessPartnerCode"].ToString();
                CustomerIdSI.ReadOnly = true;
                Invoice.Text = row["SalesOrderNo"].ToString();
                Invoice.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                InvoiceType.SelectedValue = row["OrderType"].ToString();
                InvoiceType.Enabled = false;
                Date.Text = Convert.ToDateTime(row["SalesOrderDate"]).ToString("MM'/'dd'/'yyyy");
                Date.ReadOnly = true;
                Name.Text = row["BPName"].ToString();
                Name.ReadOnly = true;
                Location.Text = row["Location"].ToString();
                Location.ReadOnly = true;
                LocationHidden.Value = row["LocationCode"].ToString();
                SalesMan.Text = row["SalesManName"].ToString();
                SalesMan.ReadOnly = true;
                SalesManHidden.Value = row["SalesMan"].ToString();
                Telephone.Text = row["BPTelephone"].ToString();
                Telephone.ReadOnly = true;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                Amount.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                Amount.ReadOnly = true;
                IPayTerms.Text = row["PaymentTerms"].ToString();
                IDeliveryTerms.Text = row["DeliveryTerms"].ToString();
                IShipToAddress.Text = row["ShiptoAddress"].ToString();
                ITotalAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                ITotalDiscountAmt.Text = Convert.ToDecimal(row["DiscountAmount"]).ToString("f" + decimalPoints);
                ITotalTaxAmt.Text = Convert.ToDecimal(row["TaxAmount"]).ToString("f" + decimalPoints);
                ITotalOrderAmt.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);
                if (row["Status"].ToString() == "2")
                {
                    btnFinalizeI.Visible = false;
                    btnSaveDtl.Visible = false;
                }
                else
                {
                    btnFinalizeI.Visible = true;
                }
                SetInvoiceChildGrid();
            }
            catch (Exception e)
            {

            }
        }

        public void SetInvoiceChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.Invoice.GetInvoiceDtlById(Convert.ToInt32(SalesInvoiceId.Value));

            if (dt.Rows.Count > 0)
            {
                rowCount.Value = dt.Rows.Count.ToString();
                InvoiceDetail.DataSource = dt;
                InvoiceDetail.DataBind();
                
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
                dt.Columns.Add(new DataColumn("DiscountPercentage", typeof(float)));
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
                    dr["DiscountPercentage"] = DBNull.Value;
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

                InvoiceDetail.DataSource = dt;
                InvoiceDetail.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                #region set item details
                DataTable dt = new DataTable();
                DataTable dtDeletedIds = new DataTable();
                DataRow drDeletedIds = null;
                dtDeletedIds.Columns.Add(new DataColumn("ID", typeof(int)));
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("LocationCode", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesOrderMstId", typeof(int)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
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
                foreach (GridViewRow row in InvoiceDetail.Rows)
                {
                    TextBox box2 = (TextBox)InvoiceDetail.Rows[i].Cells[1].FindControl("IItem");
                    TextBox box3 = (TextBox)InvoiceDetail.Rows[i].Cells[2].FindControl("IItemName");
                    TextBox box4 = (TextBox)InvoiceDetail.Rows[i].Cells[5].FindControl("IItemRate");
                    TextBox box5 = (TextBox)InvoiceDetail.Rows[i].Cells[3].FindControl("IQuantity");
                    TextBox box6 = (TextBox)InvoiceDetail.Rows[i].Cells[4].FindControl("IUnit");
                    TextBox box7 = (TextBox)InvoiceDetail.Rows[i].Cells[6].FindControl("IDiscPer");
                    TextBox box8 = (TextBox)InvoiceDetail.Rows[i].Cells[7].FindControl("IDiscAmt");
                    TextBox box9 = (TextBox)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxPer");
                    TextBox box10 = (TextBox)InvoiceDetail.Rows[i].Cells[9].FindControl("ITaxAmt");
                    TextBox box11 = (TextBox)InvoiceDetail.Rows[i].Cells[10].FindControl("INetAmt");
                    HiddenField hdnFld = (HiddenField)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxCode");
                    if (box2.Text != "" && box2.Text.Length != 0 && Array.IndexOf(deletedIds, InvoiceDetail.DataKeys[i]["ID"]) == -1)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(InvoiceDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] = DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(InvoiceDetail.DataKeys[i]["ID"]);
                        }

                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = LocationHidden.Value;
                        dr["SalesOrderMstId"] = Convert.ToInt32(SalesInvoiceId.Value);
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
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
                    }
                    i++;
                }
                if (Request.Form["IItem"] != null)
                {
                    string[] IItems = Request.Form["IItem"].Split(',');
                    string[] IItemNames = Request.Form["IItemName"].Split(',');
                    string[] IItemRates = Request.Form["IItemRate"].Split(',');
                    string[] IQuantities = Request.Form["IQuantity"].Split(',');
                    string[] IUnits = Request.Form["IUnit"].Split(',');
                    string[] IDiscPers = Request.Form["IDiscPer"].Split(',');
                    string[] IDiscAmts = Request.Form["IDiscAmt"].Split(',');
                    string[] ITaxPers = Request.Form["ITaxPer"].Split(',');
                    string[] ITaxAmts = Request.Form["ITaxAmt"].Split(',');
                    string[] INetAmts = Request.Form["INetAmt"].Split(',');
                    string[] ITaxCodes = Request.Form["ITaxCode"].Split(',');

                    for (int k = 0; k < IItems.Length; k++)
                    {
                        if (IItems[k] != "" && IItemNames[k] != "" && IItemRates[k] != "" && IQuantities[k] != "" && IDiscPers[k] != "" && IDiscAmts[k] != "" && ITaxPers[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = LocationHidden.Value;
                            dr["SalesOrderMstId"] = Convert.ToInt32(SalesInvoiceId.Value);
                            dr["ItemCode"] = IItems[k];
                            dr["ItemName"] = IItemNames[k];
                            dr["BaseUnitCode"] = IUnits[k];
                            dr["Qty"] = Convert.ToInt32(IQuantities[k]);
                            dr["Currency"] = currencyCode.Value;
                            dr["Rate"] = float.Parse(IItemRates[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountPercentage"] = float.Parse(IDiscPers[k], CultureInfo.InvariantCulture.NumberFormat);
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

                #endregion

                if (SalesInvoiceId.Value != "" && SalesInvoiceId.Value != "0")
                {
                    if (XBDataProvider.Invoice.UpdateInvoiceDetails(Convert.ToInt32(SalesInvoiceId.Value), IPayTerms.Text, IDeliveryTerms.Text, IShipToAddress.Text, float.Parse(ITotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ITotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ITotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ITotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt, dtDeletedIds,Reference.Text))
                    {
                        Amount.Text = Request.Form[Amount.UniqueID];
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                        SetInvoiceChildGrid();
                    }
                    else
                    {
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                        failure.Visible = true;
                    }
                }
                else
                {
                    int selectedSequenceId = Convert.ToInt32(SISequenceNoID.Value);
                    int returnValue = 0;
                    DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    returnValue = XBDataProvider.Invoice.AddInvoiceWithDetails(Session["CompanyCode"].ToString(), CustomerIdSI.Text, Request.Form[Invoice.UniqueID], 1,
                                               Convert.ToInt32(selectedInvoiceType.Value), date, Request.Form[Name.UniqueID], LocationHidden.Value, SalesManHidden.Value, Request.Form[Telephone.UniqueID], Reference.Text,
                                                IPayTerms.Text, IDeliveryTerms.Text, IShipToAddress.Text, float.Parse(ITotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ITotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ITotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(ITotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, selectedSequenceId, dt, currencyCode.Value);
                    if (returnValue > 0)
                    {
                        InvoiceType.SelectedValue = selectedInvoiceType.Value;
                        Name.Text = Request.Form[Name.UniqueID];
                        Name.ReadOnly = true;
                        Telephone.Text = Request.Form[Telephone.UniqueID];
                        Telephone.ReadOnly = true;
                        Invoice.Text = Request.Form[Invoice.UniqueID];
                        Invoice.ReadOnly = true;
                        SalesInvoiceId.Value = returnValue.ToString();
                        btnPrint.Visible = true;
                        SetInvoiceChildGrid();
                        PageStatus.Value = "edit";
                        Status.SelectedValue = "1";
                        Amount.Text = Request.Form[Amount.UniqueID];
                        btnFinalizeI.Visible = true;
                        SaveSuccess.Visible = true;
                        CustomerIdSI.ReadOnly = true;
                        Location.ReadOnly = true;
                        SalesMan.ReadOnly = true;
                        failure.Visible = false;
                    }
                    else
                    {
                        failure.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }

        protected void btnPrintClick(object sender, EventArgs e)
        {
            string locationCode = "Bng";
            string businessPartnerCode = "L101";
            string salesOrderNo = "SOnum";
            string companyCode = "Techen";
            string url = "PrintSalesInvoice.aspx?lc=" + locationCode + "&bpc=" + businessPartnerCode + "&son=" + salesOrderNo + "&cc=" + companyCode;
            string s = "window.open('" + url + "', 'popup_window', 'width=875,height=600,left=100,top=50,resizable=yes');";
            ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
        }

        protected void btnFinalizeClick(object sender, EventArgs e)
        {
            if (XBDataProvider.Invoice.FinlizeInvoice(Convert.ToInt32(SalesInvoiceId.Value)))
            {
                Status.SelectedValue = "2";
                SalesInvoiceId.Value = SalesInvoiceId.Value;
                btnPrint.Visible = true;
                SetInvoiceChildGrid();
                PageStatus.Value = "edit";
                Amount.Text = Request.Form[Amount.UniqueID];
                btnFinalizeI.Visible = false;
                btnSaveDtl.Visible = false;
                SaveSuccess.Visible = false;
                UpdateSuccess.Visible = false;
                FinalizeSuccess.Visible = true;
                failure.Visible = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "isActive", "window.print();", true);
                string locationCode = "Bng";
                string businessPartnerCode = "L101";
                string salesOrderNo = "SOnum";
                string companyCode = "Techen";
                string url = "PrintSalesInvoice.aspx?lc=" + locationCode + "&bpc=" + businessPartnerCode + "&son=" + salesOrderNo + "&cc=" + companyCode;
                string s = "window.open('" + url + "', 'popup_window', 'width=875,height=600,left=100,top=50,resizable=yes');";
                ClientScript.RegisterStartupScript(this.GetType(), "script", s, true);
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
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetInvoiceCashCreditSequenceDetails(companyCode);
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

        protected void InvoiceDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value) == 1)
            {
                LinkButton lnkDtn = e.Row.Cells[11].FindControl("lnkDeleteSI") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (Status.SelectedValue == "2")
            {
                e.Row.Cells[11].Visible = false;
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox item = e.Row.Cells[5].FindControl("IItem") as TextBox;
                if (item.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    TextBox Rate = e.Row.Cells[5].FindControl("IItemRate") as TextBox;
                    TextBox Discount = e.Row.Cells[6].FindControl("IDiscPer") as TextBox;
                    TextBox DiscountAmt = e.Row.Cells[7].FindControl("IDiscAmt") as TextBox;
                    TextBox NetAmount = e.Row.Cells[9].FindControl("INetAmt") as TextBox;
                    TextBox TaxAmount = e.Row.Cells[8].FindControl("ITaxAmt") as TextBox;
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

        protected void ReceiptDetailRowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header) // If header created
            {
                GridView Projectgrid = (GridView)sender;

                // Creating a Row
                GridViewRow HeaderRow = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);
                GridViewRow HeaderRow2 = new GridViewRow(0, 0, DataControlRowType.Header, DataControlRowState.Insert);

                TableCell HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "No:";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Payment Mode";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Reference";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.ColumnSpan = 2; // Give Colspan 6 to header
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Number";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                // HeaderCell.RowSpan = 1;
                HeaderCell.ColumnSpan = 1;
                HeaderRow2.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Date";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                //HeaderCell.RowSpan = 1;
                HeaderCell.ColumnSpan = 1;
                HeaderRow2.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Due Amount";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Amount";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "TDS Amount";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "Net Amount";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                HeaderCell = new TableHeaderCell();
                HeaderCell.Text = "";
                HeaderCell.HorizontalAlign = HorizontalAlign.Center;
                HeaderCell.CssClass = "tableHeaderAlignCenter";
                HeaderCell.RowSpan = 2;
                HeaderCell.ColumnSpan = 1;
                HeaderRow.Cells.Add(HeaderCell);

                Projectgrid.Controls[0].Controls.AddAt(0, HeaderRow);
                Projectgrid.Controls[0].Controls.AddAt(1, HeaderRow2);

            }
        }
        protected void ReceiptDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value) == 1)
            {
                LinkButton lnkDtn = e.Row.Cells[8].FindControl("lnkDeleteR") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox item = e.Row.Cells[1].FindControl("RPaymentMode") as TextBox;
                if (item.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    TextBox RDueAmount = e.Row.Cells[4].FindControl("RDueAmount") as TextBox;
                    TextBox Discount = e.Row.Cells[5].FindControl("RAmount") as TextBox;
                    TextBox TDSAmount = e.Row.Cells[6].FindControl("TDSAmount") as TextBox;
                    TextBox RNetAmount = e.Row.Cells[7].FindControl("RNetAmount") as TextBox;
                    double dueAmount = Convert.ToDouble(RDueAmount.Text);
                    double discountAmt = Convert.ToDouble(Discount.Text);
                    double tdsAmt = Convert.ToDouble(TDSAmount.Text);
                    double netAmount = Convert.ToDouble(RNetAmount.Text);
                    RDueAmount.Text = dueAmount.ToString("f" + decimalPoints);
                    Discount.Text = discountAmt.ToString("f" + decimalPoints);
                    TDSAmount.Text = tdsAmt.ToString("f" + decimalPoints);
                    RNetAmount.Text = netAmount.ToString("f" + decimalPoints);
                }
            }
        }

      
    }
}