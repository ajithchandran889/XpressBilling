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
    public partial class ReceiptEdit : System.Web.UI.Page
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
                DataTable dtCurrency = XBDataProvider.Currency.GetCurrencyDetailsByCompany(CompanyCode.Value);
                currencyDecimal.Value = XBDataProvider.Currency.GetCurrencyDecimalByCompany(CompanyCode.Value).ToString();
                currencyCode.Value = dtCurrency.Rows[0]["CurrencyCode"].ToString();
                DataRow row = null;
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != 0)
                {
                    DataTable receiptDetails = XBDataProvider.Receipt.GetReceiptById(id);
                    if (receiptDetails.Rows.Count > 0)
                    {
                        SetReceiptDetails(receiptDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
                    PageStatus.Value = "create";
                    Currency.Text = dtCurrency.Rows[0]["Name"].ToString();
                    CreatedDateR.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
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
                   // ReceiptId.Value = "0";
                }
                SetInitialRowsReceipt();
            }
        }

        public void SetReceiptDetails(DataTable receiptDetails)
        {
            try
            {
                btnPrint.Visible = true;
                DataRow row = receiptDetails.Rows[0];
                ReceiptId.Value = row["ID"].ToString();
                CurrentDueAmount.Value = row["DueAmount"].ToString();
                TransactionTypeR.SelectedValue = row["TransactionType"].ToString();
                TransactionTypeR.Enabled = false;
                ReceiptNo.Text = row["DocumentNo"].ToString();
                ReceiptNo.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                BusinessPartnerR.Text = row["BusinessPartnerName"].ToString();
                BusinessPartnerR.ReadOnly = true;
                BussinessPartnerCode.Value = row["BusinessPartner"].ToString();
                CreatedDateR.Text = Convert.ToDateTime(row["DocumentDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedDateR.ReadOnly = true;
                Currency.Text = row["Currency"].ToString();
                Currency.ReadOnly = true;
                SalesMan.Text = row["SalesManName"].ToString();
                SalesMan.ReadOnly = true;
                Location.Text = row["Location"].ToString();
                Location.ReadOnly = true;
                LocationHidden.Value = row["EnterPriseUnit"].ToString();
                Reference.Text = row["Reference"].ToString();
                int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                Amount.ReadOnly = true;
                UnAllocatedAmount.Text = Convert.ToDecimal(row["UnallocatedAmount"]).ToString("f" + decimalPoints);

                if (Status.SelectedValue =="2")
                {
                    IsFinalized.Value = "1";
                    btnConverOrder.Visible = false;
                    btnSaveDtl.Visible = false;
                    Reference.ReadOnly = true;
                }
                else
                {
                    IsFinalized.Value = "0";
                    btnConverOrder.Visible = true;
                }
                SetReceiptChildGrid();
            }
            catch (Exception e)
            {

            }


        }

        public void SetReceiptChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.Receipt.GetReceiptDtlById(Convert.ToInt32(ReceiptId.Value));

            if (dt.Rows.Count > 0)
            {
                if (TransactionTypeR.SelectedValue=="1")
                {
                    rowCountAR.Value = dt.Rows.Count.ToString();
                    ReceiptDetailAdvanceReceipt.DataSource = dt;
                    ReceiptDetailAdvanceReceipt.DataBind();
                    //ReceiptDetailAdvanceReceipt.CssClass.Replace("table table-fix hideElement", "table table-fix");
                    //ReceiptDetail.CssClass.Replace("table table-fix", "table table-fix hideElement");
                }
                else
                {
                    rowCount.Value = dt.Rows.Count.ToString();
                    ReceiptDetail.DataSource = dt;
                    ReceiptDetail.DataBind();
                    //ReceiptDetailAdvanceReceipt.CssClass.Replace("table table-fix", "table table-fix hideElement");
                    //ReceiptDetail.CssClass.Replace("table table-fix hideElement", "table table-fix");
                }
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
                float lastDueAmount = 0;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("ReceiptMstID", typeof(int)));
                dt.Columns.Add(new DataColumn("CompanyCode", typeof(string)));
                dt.Columns.Add(new DataColumn("EnterPriseUnit", typeof(string)));
                dt.Columns.Add(new DataColumn("BusinessPartner", typeof(string)));
                dt.Columns.Add(new DataColumn("DocumentNo", typeof(string)));
                dt.Columns.Add(new DataColumn("DocumentDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Pos", typeof(int)));
                dt.Columns.Add(new DataColumn("PaymentMode", typeof(int)));
                dt.Columns.Add(new DataColumn("ReferenceNo", typeof(float)));
                dt.Columns.Add(new DataColumn("ReferenceDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("Currency", typeof(string)));
                dt.Columns.Add(new DataColumn("DueAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("Amount", typeof(float)));
                dt.Columns.Add(new DataColumn("TDSAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                int i = 0,j=1;
                int[] deletedIds;
                if(TransactionTypeR.SelectedValue=="1")
                {
                    deletedIds = DeletedRowIDsAR.Value.Split(',').Where(str => str != "" && str !="0").Select(str => int.Parse(str)).ToArray();
                }
                else
                {
                    deletedIds = DeletedRowIDs.Value.Split(',').Where(str => str != "" && str != "0").Select(str => int.Parse(str)).ToArray();
                }
                
                for (int k = 0; k < deletedIds.Length; k++)
                {
                    if (deletedIds[k]!=0)
                    {
                        drDeletedIds = dtDeletedIds.NewRow();
                        drDeletedIds["ID"] = deletedIds[k];
                        dtDeletedIds.Rows.Add(drDeletedIds);
                    }
                    
                }
                foreach (GridViewRow row in ReceiptDetail.Rows)
                {
                    TextBox box2 = new TextBox();
                    TextBox box3 = new TextBox();
                    TextBox box4 = new TextBox();
                    TextBox box5 = new TextBox();
                    TextBox box6 = new TextBox();
                    TextBox box7 = new TextBox();
                    HiddenField hdnFld=new HiddenField();
                    bool isDeleted = false;
                    if (TransactionTypeR.SelectedValue == "1")
                    {
                        box2 = (TextBox)ReceiptDetailAdvanceReceipt.Rows[i].Cells[2].FindControl("ARNumber");
                        box3 = (TextBox)ReceiptDetailAdvanceReceipt.Rows[i].Cells[3].FindControl("ARDate");
                        //box4 = (TextBox)ReceiptDetail.Rows[i].Cells[5].FindControl("RDueAmount");
                        box4.Text = "0";
                        box5 = (TextBox)ReceiptDetailAdvanceReceipt.Rows[i].Cells[4].FindControl("ARAmount");
                        box6 = (TextBox)ReceiptDetailAdvanceReceipt.Rows[i].Cells[5].FindControl("ATDSAmount");
                        box7 = (TextBox)ReceiptDetailAdvanceReceipt.Rows[i].Cells[6].FindControl("ARNetAmount");
                        hdnFld = (HiddenField)ReceiptDetailAdvanceReceipt.Rows[i].Cells[1].FindControl("ARPaymentModeID");
                        isDeleted=Array.IndexOf(deletedIds, ReceiptDetailAdvanceReceipt.DataKeys[i]["ID"]) == -1;
                    }
                    else
                    {
                        box2 = (TextBox)ReceiptDetail.Rows[i].Cells[2].FindControl("RNumber");
                        box3 = (TextBox)ReceiptDetail.Rows[i].Cells[3].FindControl("RDate");
                        box4 = (TextBox)ReceiptDetail.Rows[i].Cells[4].FindControl("RDueAmount");
                        box5 = (TextBox)ReceiptDetail.Rows[i].Cells[5].FindControl("RAmount");
                        box6 = (TextBox)ReceiptDetail.Rows[i].Cells[6].FindControl("TDSAmount");
                        box7 = (TextBox)ReceiptDetail.Rows[i].Cells[7].FindControl("RNetAmount");
                        hdnFld = (HiddenField)ReceiptDetail.Rows[i].Cells[1].FindControl("RPaymentModeID");
                        isDeleted=Array.IndexOf(deletedIds, ReceiptDetail.DataKeys[i]["ID"]) == -1;
                    }
                    if ( box7.Text != "" && box7.Text.Length != 0 && isDeleted)
                    {
                        dr = dt.NewRow();
                        if (TransactionTypeR.SelectedValue == "1")
                        {
                            if (string.IsNullOrEmpty(ReceiptDetailAdvanceReceipt.DataKeys[i]["ID"].ToString()))
                            {
                                dr["ID"] = DBNull.Value;
                            }
                            else
                            {
                                dr["ID"] = Convert.ToInt32(ReceiptDetailAdvanceReceipt.DataKeys[i]["ID"]);
                            }
                        }
                        else
                        {
                            if (string.IsNullOrEmpty(ReceiptDetail.DataKeys[i]["ID"].ToString()))
                            {
                                dr["ID"] = DBNull.Value;
                            }
                            else
                            {
                                dr["ID"] = Convert.ToInt32(ReceiptDetail.DataKeys[i]["ID"]);
                            }
                        }
                        dr["ReceiptMstID"] =ReceiptId.Value==""?0: Convert.ToInt32(ReceiptId.Value);
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["EnterPriseUnit"] = LocationHidden.Value;
                        dr["BusinessPartner"] = BusinessPartnerR.Text;
                        dr["DocumentNo"] = Request.Form[ReceiptNo.UniqueID];
                        dr["DocumentDate"] =  DateTime.ParseExact(CreatedDateR.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        dr["Pos"] = j;
                        dr["PaymentMode"] =Convert.ToInt32(hdnFld.Value);
                        dr["ReferenceNo"] = box2.Text.Length > 0 ? Convert.ToInt32(box2.Text) : 0;
                        string dateString = string.Empty;
                        if(box3.Text.Length>0)
                        {
                            dateString=box3.Text.Replace('-','/');
                        }
                        dr["ReferenceDate"] = box3.Text.Length > 0 ? DateTime.ParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture) : new DateTime(1753, 1, 1, 12, 0, 0);
                        dr["Currency"] = currencyCode.Value;
                        dr["DueAmount"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Amount"] = float.Parse(box5.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TDSAmount"] = float.Parse(box6.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["NetAmount"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Status"] = 1;
                        dr["ErrorMsg"] = null;
                        dr["Reference"] = Reference.Text;
                        dr["CreatedBy"] = User.Identity.Name;
                        dr["UpdatedBy"] = User.Identity.Name;
                        dr["CreatedDate"] = DateTime.Now.Date;
                        dr["UpdatedDate"] = DateTime.Now.Date;
                        dt.Rows.Add(dr);
                    }
                    i++; j++;
                }
                if (Request.Form["RPaymentModeID"] != null || Request.Form["ARPaymentModeID"] != null)
                {
                    string[] RPaymentModeIDs = null;
                    string[] RNumbers = null;
                    string[] RDates = null;
                    string[] RDueAmounts = null;
                    string[] RAmounts = null;
                    string[] TDSAmounts = null;
                    string[] RNetAmounts = null;
                    if (TransactionTypeR.SelectedValue == "1")
                    {
                        RPaymentModeIDs = Request.Form["ARPaymentModeID"].Split(',');
                        RNumbers = Request.Form["ARNumber"].Split(',');
                        RDates = Request.Form["ARDate"].Split(',');
                        RAmounts = Request.Form["ARAmount"].Split(',');
                        TDSAmounts = Request.Form["ATDSAmount"].Split(',');
                        RNetAmounts = Request.Form["ARNetAmount"].Split(',');
                    }
                    else
                    {
                        RPaymentModeIDs = Request.Form["RPaymentModeID"].Split(',');
                        RNumbers = Request.Form["RNumber"].Split(',');
                        RDates = Request.Form["RDate"].Split(',');
                        RDueAmounts = Request.Form["RDueAmount"].Split(',');
                        RAmounts = Request.Form["RAmount"].Split(',');
                        TDSAmounts = Request.Form["TDSAmount"].Split(',');
                        RNetAmounts = Request.Form["RNetAmount"].Split(',');
                    }

                    for (int k = 0; k < RPaymentModeIDs.Length; k++)
                    {
                        if (RPaymentModeIDs[k] != ""  && RAmounts[k] != "" && TDSAmounts[k] != "" && RNetAmounts[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                            dr["ReceiptMstID"] = ReceiptId.Value == "" ? 0 : Convert.ToInt32(ReceiptId.Value);
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["EnterPriseUnit"] = LocationHidden.Value;
                            dr["BusinessPartner"] = BusinessPartnerR.Text;
                            dr["DocumentNo"] = Request.Form[ReceiptNo.UniqueID];
                            dr["DocumentDate"] = DateTime.ParseExact(CreatedDateR.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                            dr["Pos"] = j;
                            dr["PaymentMode"] = Convert.ToInt32(RPaymentModeIDs[k]);
                            dr["ReferenceNo"] = RNumbers[k].Length > 0 ? Convert.ToInt32(RNumbers[k]) : 0;
                            string dateString = string.Empty;
                            if (RDates[k].Length > 0)
                            {
                                dateString = RDates[k].Replace('-', '/');
                            }
                            dr["ReferenceDate"] = RDates[k].Length > 0 ? DateTime.ParseExact(dateString, "MM/dd/yyyy", CultureInfo.InvariantCulture) : new DateTime(1753, 1, 1, 12, 0, 0);
                            dr["Currency"] = currencyCode.Value;
                            dr["DueAmount"] = RDueAmounts==null?0: float.Parse(RDueAmounts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Amount"] = float.Parse(RAmounts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TDSAmount"] = float.Parse(TDSAmounts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["NetAmount"] = float.Parse(RNetAmounts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Status"] = 1;
                            dr["ErrorMsg"] = null;
                            dr["Reference"] = Reference.Text;
                            dr["CreatedBy"] = User.Identity.Name;
                            dr["UpdatedBy"] = User.Identity.Name;
                            dr["CreatedDate"] = DateTime.Now.Date;
                            dr["UpdatedDate"] = DateTime.Now.Date;
                            dt.Rows.Add(dr);
                            
                            j++;
                        }
                    }



                }

                #endregion
                lastDueAmount = TransactionTypeR.SelectedValue == "1" ? 0 : float.Parse(CurrentDueAmount.Value, CultureInfo.InvariantCulture.NumberFormat);
                if (ReceiptId.Value != "" && ReceiptId.Value != "0")
                {
                    if (XBDataProvider.Receipt.UpdateReceiptDetails(Convert.ToInt32(ReceiptId.Value), float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt, dtDeletedIds, Reference.Text, lastDueAmount))
                    {
                        Amount.Text = Request.Form[Amount.UniqueID];
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                        SetReceiptChildGrid();
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
                    int selectedSequenceId = Convert.ToInt32(RSequenceNoID.Value);
                    int returnValue = 0;
                    float unAllocatedAmount=UnAllocatedAmount.Text!=""?float.Parse(UnAllocatedAmount.Text, CultureInfo.InvariantCulture.NumberFormat):0;
                    DateTime date = DateTime.ParseExact(CreatedDateR.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    returnValue = XBDataProvider.Receipt.AddReceiptWithDetails(Session["CompanyCode"].ToString(),Convert.ToInt32(TransactionTypeR.SelectedValue), Request.Form[ReceiptNo.UniqueID], 1,
                                               BusinessPartnerR.Text, date, SalesManHidden.Value, LocationHidden.Value, Reference.Text, float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat), unAllocatedAmount, User.Identity.Name, selectedSequenceId, dt, currencyCode.Value, lastDueAmount);
                    if (returnValue > 0)
                    {
                        ReceiptNo.Text = Request.Form[ReceiptNo.UniqueID];
                        ReceiptNo.ReadOnly = true;
                        ReceiptId.Value = returnValue.ToString();
                        btnPrint.Visible = true;
                        SetReceiptChildGrid();
                        PageStatus.Value = "edit";
                        Status.SelectedValue = "1";
                        Amount.Text = Request.Form[Amount.UniqueID];
                        btnConverOrder.Visible = true;
                        SaveSuccess.Visible = true;
                        TransactionTypeR.Enabled = false;
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

        protected void BtnFinalizeReceiptClick(object sender, EventArgs e)
        {
            try
            {
                if (XBDataProvider.Receipt.FinlizeReceipt(Convert.ToInt32(ReceiptId.Value)))
                {
                    Status.SelectedValue = "2";
                    ReceiptId.Value = ReceiptId.Value;
                    btnPrint.Visible = true;
                    SetReceiptChildGrid();
                    PageStatus.Value = "edit";
                    Amount.Text = Request.Form[Amount.UniqueID];
                    btnConverOrder.Visible = false;
                    btnSaveDtl.Visible = false;
                    SaveSuccess.Visible = false;
                    UpdateSuccess.Visible = false;
                    FinalizeSuccess.Visible = true;
                    failure.Visible = false;
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
            catch(Exception ex)
            {

            }
            
        }

        [WebMethod]
        public static List<FirstFreeDetails> GetFirstFreerDetails(string companyCode)
        {
            List<FirstFreeDetails> result = new List<FirstFreeDetails>();
            try
            {
                DataRow row = null;
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetReceiptFFSequenceDetails(companyCode);
                for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                {
                    row = dtTableSequenceDetails.Rows[i];
                    FirstFreeDetails firstFreeDetails = new FirstFreeDetails();
                    string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                    firstFreeDetails.id = row["ID"].ToString();
                    firstFreeDetails.sequenceNumber = sequenceNo;
                    firstFreeDetails.seqType = row["SeqType"].ToString();
                    firstFreeDetails.orderType = row["OrderType"].ToString() == "Receipt Against Invoice" ? "0" : (row["OrderType"].ToString() == "Advance Receipt" ? "1" : (row["OrderType"].ToString() == "Advance Allocation" ? "2" : (row["OrderType"].ToString() == "Credit Note"?"3":"-1")));
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
        public static List<PaymentModes> GetAllPaymentModeWithOutCreditNote(string companyCode)
        {
            List<PaymentModes> result = new List<PaymentModes>();
            try
            {
                DataRow row = null;
                DataTable dtTablePaymentMode = XBDataProvider.PaymentMode.GetAllPaymentModeDetailsWithOutCreditNote(companyCode);
                for (int i = 0; i < dtTablePaymentMode.Rows.Count; i++)
                {
                    row = dtTablePaymentMode.Rows[i];
                    PaymentModes paymentModeDetails = new PaymentModes();
                    paymentModeDetails.id =Convert.ToInt32(row["ID"]);
                    paymentModeDetails.name = row["Name"].ToString();
                    result.Add(paymentModeDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }

        [WebMethod]
        public static List<PaymentModes> GetAllPaymentModeDetailsOnlyCreditNote(string companyCode)
        {
            List<PaymentModes> result = new List<PaymentModes>();
            try
            {
                DataRow row = null;
                DataTable dtTablePaymentMode = XBDataProvider.PaymentMode.GetAllPaymentModeDetailsOnlyCreditNote(companyCode);
                for (int i = 0; i < dtTablePaymentMode.Rows.Count; i++)
                {
                    row = dtTablePaymentMode.Rows[i];
                    PaymentModes paymentModeDetails = new PaymentModes();
                    paymentModeDetails.id = Convert.ToInt32(row["ID"]);
                    paymentModeDetails.name = row["Name"].ToString();
                    result.Add(paymentModeDetails);
                }

            }
            catch (Exception e)
            {

            }


            return result;
        }

        [WebMethod]
        public static double GetDueAmounttByBP(string companyCode,string bpCode)
        {
            return XBDataProvider.Receipt.GetDueAmounttByBP(companyCode, bpCode);
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

        private void SetInitialRows()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("ID", typeof(int)));
                dt.Columns.Add(new DataColumn("PaymentMode", typeof(string)));
                dt.Columns.Add(new DataColumn("PaymentModeID", typeof(int)));
                dt.Columns.Add(new DataColumn("ReferenceNo", typeof(string)));
                dt.Columns.Add(new DataColumn("ReferenceDate", typeof(string)));
                dt.Columns.Add(new DataColumn("DueAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("Amount", typeof(float)));
                dt.Columns.Add(new DataColumn("TDSAmount", typeof(float)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(float)));
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["ID"] = "0";
                    dr["PaymentMode"] = string.Empty;
                    dr["ReferenceNo"] = string.Empty;
                    dr["ReferenceDate"] = string.Empty;
                    dr["DueAmount"] = DBNull.Value;
                    dr["Amount"] = DBNull.Value;
                    dr["TDSAmount"] = DBNull.Value;
                    dr["NetAmount"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                //ViewState["CurrentTable"] = dt;

                ReceiptDetail.DataSource = dt;
                ReceiptDetail.DataBind();
                ReceiptDetailAdvanceReceipt.DataSource = dt;
                ReceiptDetailAdvanceReceipt.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        private void SetInitialRowsReceipt()
        {
            try
            {
                DataTable dt = new DataTable();
                DataRow dr = null;
                dt.Columns.Add(new DataColumn("Receipt", typeof(string)));
                dt.Columns.Add(new DataColumn("Date", typeof(string)));
                dt.Columns.Add(new DataColumn("Transaction", typeof(string)));
                dt.Columns.Add(new DataColumn("PaymentMode", typeof(string)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("DueAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("Amount", typeof(string)));
                dt.Columns.Add(new DataColumn("TDSAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("Cashier", typeof(string)));
                dt.Columns.Add(new DataColumn("EU", typeof(string)));
                dt.Columns.Add(new DataColumn("Status", typeof(string)));
                for (int i = 0; i < 1; i++)
                {
                    dr = dt.NewRow();
                    dr["Receipt"] = string.Empty;
                    dr["Date"] = string.Empty;
                    dr["Transaction"] = string.Empty;
                    dr["PaymentMode"] = string.Empty;
                    dr["Reference"] = string.Empty;
                    dr["DueAmount"] = string.Empty;
                    dr["Amount"] = string.Empty;
                    dr["TDSAmount"] = string.Empty;
                    dr["Cashier"] = string.Empty;
                    dr["EU"] = string.Empty;
                    dr["Status"] = string.Empty;
                    dt.Rows.Add(dr);
                }


                ReceiptRecentTransaction.DataSource = dt;
                ReceiptRecentTransaction.DataBind();
            }
            catch (Exception e)
            {

            }

        }

        protected void ReceiptDetailAdvanceReceiptRowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCountAR.Value) == 1)
                {
                    LinkButton lnkDtn = e.Row.Cells[7].FindControl("lnkDeleteAR") as LinkButton;
                    lnkDtn.Style.Add("display", "None");
                }
                if (e.Row.RowIndex != -1)
                {
                    TextBox item = e.Row.Cells[1].FindControl("ARPaymentMode") as TextBox;
                    if (item.Text != "")
                    {
                        int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                        TextBox Discount = e.Row.Cells[4].FindControl("ARAmount") as TextBox;
                        TextBox TDSAmount = e.Row.Cells[5].FindControl("ATDSAmount") as TextBox;
                        TextBox RNetAmount = e.Row.Cells[6].FindControl("ARNetAmount") as TextBox;
                        double discountAmt = Convert.ToDouble(Discount.Text);
                        double tdsAmt = Convert.ToDouble(TDSAmount.Text);
                        double netAmount = Convert.ToDouble(RNetAmount.Text);
                        Discount.Text = discountAmt.ToString("f" + decimalPoints);
                        TDSAmount.Text = tdsAmt.ToString("f" + decimalPoints);
                        RNetAmount.Text = netAmount.ToString("f" + decimalPoints);
                    }
                }
            }
            catch(Exception ex)
            {

            }
            
        }

        protected void ReceiptDetailAdvanceReceiptRowCreated(object sender, GridViewRowEventArgs e)
        {
            try
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
            catch(Exception ex)
            {

            }
            
        }

        [WebMethod]
        public static string GetLastThreeReceiptByBpCode(string BpCode, string decPoint)
        {
            return XBDataProvider.Receipt.GetLastThreeReceiptByBpCode(BpCode, Convert.ToInt32(decPoint));

        }
    }
    public class PaymentModes
    {
        public int id { get; set; }
        public string name { get; set; }
    }
}