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
    public partial class ManualInvoiceEdit : System.Web.UI.Page
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
                if (id != null && id != 0)
                {
                    DataTable ManualinvoiceDetails = XBDataProvider.ManualInvoice.GetManualInvoiceById(id);
                    if (ManualinvoiceDetails.Rows.Count > 0)
                    {
                        SetManualInvoiceDetails(ManualinvoiceDetails);
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
                    InvoiceId.Value = "0";
                }
            }
        }

        public void SetManualInvoiceDetails(DataTable ManualinvoiceDetails)
        {
            try
            {
                btnPrint.Visible = true;
                DataRow row = ManualinvoiceDetails.Rows[0];
                InvoiceId.Value = row["ID"].ToString();
                CustomerIdMI.Text = row["BusinessPartnerCode"].ToString();
                CustomerIdMI.ReadOnly = true;
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
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                Amount.ReadOnly = true;
                MIPayTerms.Text = row["PaymentTerms"].ToString();
                MIDeliveryTerms.Text = row["DeliveryTerms"].ToString();
                MIShipToAddress.Text = row["ShiptoAddress"].ToString();
                MITotalAmount.Text = Convert.ToDecimal(row["Amount"]).ToString("f" + decimalPoints);
                MITotalDiscountAmt.Text = Convert.ToDecimal(row["DiscountAmount"]).ToString("f" + decimalPoints);
                MITotalTaxAmt.Text = Convert.ToDecimal(row["TaxAmount"]).ToString("f" + decimalPoints);
                MITotalOrderAmt.Text = Convert.ToDecimal(row["OrderAmount"]).ToString("f" + decimalPoints);

                SetManualInvoiceChildGrid();
            }
            catch (Exception e)
            {

            }
        }

        public void SetManualInvoiceChildGrid()
        {
            DataTable dt = new DataTable();
            dt = XBDataProvider.ManualInvoice.GetManualInvoiceDtlById(Convert.ToInt32(InvoiceId.Value));

            if (dt.Rows.Count > 0)
            {
                rowCount.Value = dt.Rows.Count.ToString();
                ManualInvoiceDetail.DataSource = dt;
                ManualInvoiceDetail.DataBind();
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
                    dr["TaxPercentage"] = DBNull.Value;
                    dr["TaxAmount"] = DBNull.Value;
                    dr["NetAmount"] = DBNull.Value;
                    dt.Rows.Add(dr);
                }

                //dr = dt.NewRow();

                //Store the DataTable in ViewState
                ViewState["CurrentTable"] = dt;

                ManualInvoiceDetail.DataSource = dt;
                ManualInvoiceDetail.DataBind();
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
                foreach (GridViewRow row in ManualInvoiceDetail.Rows)
                {
                    TextBox box2 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[1].FindControl("MIItem");
                    TextBox box3 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[2].FindControl("MIItemName");
                    TextBox box4 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[5].FindControl("MIItemRate");
                    TextBox box5 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[3].FindControl("MIQuantity");
                    TextBox box6 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[4].FindControl("MIUnit");
                    TextBox box7 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[6].FindControl("MIDiscPer");
                    TextBox box8 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[7].FindControl("MIDiscAmt");
                    TextBox box9 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[8].FindControl("MITaxPer");
                    TextBox box10 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[9].FindControl("MITaxAmt");
                    TextBox box11 = (TextBox)ManualInvoiceDetail.Rows[i].Cells[10].FindControl("MINetAmt");
                    if (box2.Text != "" && box2.Text.Length != 0 && Array.IndexOf(deletedIds, ManualInvoiceDetail.DataKeys[i]["ID"]) == -1)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(ManualInvoiceDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] = DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(ManualInvoiceDetail.DataKeys[i]["ID"]);
                        }
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = Location.Text;
                        dr["SalesOrderMstId"] = Convert.ToInt32(InvoiceId.Value);
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = currencyCode.Value;
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                        dr["DiscountPercentage"] = float.Parse(box7.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["DiscountAmt"] = float.Parse(box8.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["Tax"] = box9.Text;
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
                if (Request.Form["MIItem"] != null)
                {
                    string[] MIItems = Request.Form["MIItem"].Split(',');
                    string[] MIItemNames = Request.Form["MIItemName"].Split(',');
                    string[] MIItemRates = Request.Form["MIItemRate"].Split(',');
                    string[] MIQuantitys = Request.Form["MIQuantity"].Split(',');
                    string[] MIUnits = Request.Form["MIUnit"].Split(',');
                    string[] MIDiscPers = Request.Form["MIDiscPer"].Split(',');
                    string[] MIDiscAmts = Request.Form["MIDiscAmt"].Split(',');
                    string[] MITaxPers = Request.Form["MITaxPer"].Split(',');
                    string[] MITaxAmts = Request.Form["MITaxAmt"].Split(',');
                    string[] MINetAmts = Request.Form["MINetAmt"].Split(',');
                    for (int k = 0; k < MIItems.Length; k++)
                    {
                        if (MIItems[k] != "" && MIItemNames[k] != "" && MIItemRates[k] != "" && MIQuantitys[k] != "" && MIDiscPers[k] != "" && MIDiscAmts[k] != "" && MITaxPers[k] != "")
                        {
                            dr = dt.NewRow();
                            dr["ID"] = DBNull.Value;
                            dr["CompanyCode"] = Session["CompanyCode"].ToString();
                            dr["LocationCode"] = Location.Text;
                            dr["SalesOrderMstId"] = Convert.ToInt32(InvoiceId.Value);
                            dr["ItemCode"] = MIItems[k];
                            dr["ItemName"] = MIItemNames[k];
                            dr["BaseUnitCode"] = MIUnits[k];
                            dr["Qty"] = Convert.ToInt32(MIQuantitys[k]);
                            dr["Currency"] = currencyCode.Value;
                            dr["Rate"] = float.Parse(MIItemRates[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TotalRate"] = float.Parse(Request.Form[Amount.UniqueID], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountPercentage"] = float.Parse(MIDiscPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["DiscountAmt"] = float.Parse(MIDiscAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["Tax"] = MITaxPers[k];
                            dr["TaxPercentage"] = float.Parse(MITaxPers[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["TaxAmount"] = float.Parse(MITaxAmts[k], CultureInfo.InvariantCulture.NumberFormat);
                            dr["NetAmount"] = float.Parse(MINetAmts[k], CultureInfo.InvariantCulture.NumberFormat);
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

                if (InvoiceId.Value != "" && InvoiceId.Value != "0")
                {
                    if (XBDataProvider.ManualInvoice.UpdateManualInvoiceDetails(Convert.ToInt32(InvoiceId.Value), MIPayTerms.Text, MIDeliveryTerms.Text, MIShipToAddress.Text, float.Parse(MITotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(MITotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(MITotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(MITotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt, dtDeletedIds,Reference.Text))
                    {
                        Amount.Text = Request.Form[Amount.UniqueID];
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = true;
                        failure.Visible = false;
                        SetManualInvoiceChildGrid();
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
                    int selectedSequenceId = Convert.ToInt32(MISequenceNoID.Value);
                    int returnValue = 0;
                    DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    returnValue = XBDataProvider.ManualInvoice.AddManualInvoiceWithDetails(Session["CompanyCode"].ToString(), Request.Form[CustomerIdMI.UniqueID], Request.Form[Invoice.UniqueID], 1,
                                               Convert.ToInt32(InvoiceType.SelectedValue), date, Request.Form[Name.UniqueID], LocationHidden.Value, SalesManHidden.Value, Request.Form[Telephone.UniqueID], Reference.Text,
                                                MIPayTerms.Text, MIDeliveryTerms.Text, MIShipToAddress.Text, float.Parse(MITotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(MITotalDiscountAmt.Text,
                                                CultureInfo.InvariantCulture.NumberFormat), float.Parse(MITotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(MITotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, selectedSequenceId, dt, currencyCode.Value);
                    if (returnValue > 0)
                    {
                        Invoice.Text = Request.Form[Invoice.UniqueID];
                        Name.Text = Request.Form[Name.UniqueID];
                        Telephone.Text = Request.Form[Telephone.UniqueID];
                        InvoiceId.Value = returnValue.ToString();
                        btnPrint.Visible = true;
                        PageStatus.Value = "edit";
                        Status.SelectedValue = "1";
                        Amount.Text = Request.Form[Amount.UniqueID];
                        SaveSuccess.Visible = true;
                        UpdateSuccess.Visible = false;
                        failure.Visible = false;
                        CustomerIdMI.ReadOnly = true;
                        Location.ReadOnly = true;
                        SalesMan.ReadOnly = true;
                        SetManualInvoiceChildGrid();
                    }
                    else
                    {
                        failure.Visible = true;
                        SaveSuccess.Visible = false;
                        UpdateSuccess.Visible = false;
                    }
                }
            }
            catch (Exception ex)
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
                DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetManualInvoiceCashCreditSequenceDetails(companyCode);
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

        protected void ManualInvoiceDetailRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0 && Convert.ToInt32(rowCount.Value) == 1)
            {
                LinkButton lnkDtn = e.Row.Cells[11].FindControl("lnkDeleteMI") as LinkButton;
                lnkDtn.Style.Add("display", "None");
            }
            if (e.Row.RowIndex != -1)
            {
                TextBox item = e.Row.Cells[5].FindControl("MIItem") as TextBox;
                if (item.Text != "")
                {
                    int decimalPoints = Convert.ToInt32(currencyDecimal.Value);
                    TextBox Rate = e.Row.Cells[5].FindControl("MIItemRate") as TextBox;
                    TextBox Discount = e.Row.Cells[6].FindControl("MIDiscPer") as TextBox;
                    TextBox DiscountAmt = e.Row.Cells[7].FindControl("MIDiscAmt") as TextBox;
                    TextBox NetAmount = e.Row.Cells[9].FindControl("MITaxAmt") as TextBox;
                    TextBox TaxAmount = e.Row.Cells[8].FindControl("MINetAmt") as TextBox;
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