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
                DataTable dtTable = XBDataProvider.SalesRetrun.GetAllFinalizedBussinessPartnerCodesFromInvoice(Session["CompanyCode"].ToString());
                Session["BPDetails"] = dtTable;
                DataRow row = null;
                CustomerId.Items.Clear();
                if (dtTable.Rows.Count > 0)
                {
                    Name.Text = dtTable.Rows[0]["Name"].ToString();
                    Location.Text = dtTable.Rows[0]["CountryCode"].ToString();
                    Telephone.Text = dtTable.Rows[0]["Phone"].ToString();

                }
                string firstCustomer = dtTable.Rows[0]["BusinessPartnerCode"].ToString();
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    if (!CustomerId.Items.Contains(new ListItem(row["BusinessPartnerCode"].ToString())))
                    {
                        ListItem list = new ListItem();
                        list.Value = row["BusinessPartnerCode"].ToString();
                        list.Text = row["BusinessPartnerCode"].ToString();
                        CustomerId.Items.Add(list);
                    }
                    if (firstCustomer == row["BusinessPartnerCode"].ToString())
                    {
                        ListItem list = new ListItem();
                        list.Value = row["SalesOrderNo"].ToString();
                        list.Text = row["SalesOrderNo"].ToString();
                        InvoiceNoList.Items.Add(list);
                    }
                }
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
                    Date.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetSalesReturnSequenceDetails(Session["CompanyCode"].ToString());
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                        if (row["OrderType"].ToString() == "Against Order")
                        {
                            SalesReturn.Text = sequenceNo;
                            AgainstSequenceNo.Value = sequenceNo;
                            AgainstSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["OrderType"].ToString() == "Manual Return Order")
                        {
                            ManualSequenceNo.Value = sequenceNo;
                            ManualSequenceNoID.Value = row["ID"].ToString();
                        }

                    }
                    PageStatus.Value = "create";
                    SalesReturnMstId.Value = "0";
                    gridDetails.Visible = false;
                }
            }
        }

        public void SetSalesReturnDetails(DataTable salesReturnDetails)
        {
            try
            {
                SaveBtn.Visible = false;
                CancelBtn.Visible = false;
                DataRow row = salesReturnDetails.Rows[0];
                SalesReturnMstId.Value = row["ID"].ToString();
                SalesReturnType.SelectedValue = row["SalesReturnOrderType"].ToString();
                SalesReturnType.Enabled = false;
                SalesReturn.Text = row["SalesReturnNo"].ToString();
                Status.SelectedValue = row["Status"].ToString();
                Location.Text = row["LocationCode"].ToString();
                Name.Text = row["LocationCode"].ToString();
                Reference.Text = row["Reference"].ToString();
                Telephone.Text = row["Telephone"].ToString();
                Reference.ReadOnly = true;
                Date.Text = Convert.ToDateTime(row["SalesReturnDate"]).ToString("MM'/'dd'/'yyyy");
                Date.ReadOnly = true;
                SalesMan.Text = row["SalesMan"].ToString();
                SalesMan.Enabled = false;
                Amount.Text = Convert.ToDecimal(row["Amount"]).ToString("0.00");
                Amount.ReadOnly = true;
                CustomerIdText.Visible = true;
                CustomerId.Visible = false;
                CustomerIdText.Text = row["BusinessPartnerCode"].ToString();
                InvoiceNoText.Visible = true;
                InvoiceNoList.Visible = false;
                InvoiceNoText.Text = row["SalesOrderNo"].ToString();
                DisplaySalesReturnDetails();
                if(row["Status"].ToString()=="2")
                {
                    btnSaveDtl.Visible = false;
                    btnFinalize.Visible = false;
                }
                //int i = 0;
                //if (row["Status"].ToString() == "2")
                //{

                //    foreach (GridViewRow gvr in StockEntryDetail.Rows)
                //    {
                //        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                //        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                //        TextBox box4 = (TextBox)StockEntryDetail.Rows[i].Cells[3].FindControl("SERate");
                //        TextBox box5 = (TextBox)StockEntryDetail.Rows[i].Cells[4].FindControl("SEQuantity");
                //        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                //        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                //        box2.Enabled = false;
                //        box3.Enabled = false;
                //        box4.Enabled = false;
                //        box5.Enabled = false;
                //        box6.Enabled = false;
                //        box7.Enabled = false;
                //        i++;
                //    }
                //}
                //else if (row["Status"].ToString() == "1")
                //{
                //    foreach (GridViewRow gvr in StockEntryDetail.Rows)
                //    {
                //        TextBox box2 = (TextBox)StockEntryDetail.Rows[i].Cells[1].FindControl("Item");
                //        TextBox box3 = (TextBox)StockEntryDetail.Rows[i].Cells[2].FindControl("Name");
                //        TextBox box6 = (TextBox)StockEntryDetail.Rows[i].Cells[5].FindControl("Unit");
                //        TextBox box7 = (TextBox)StockEntryDetail.Rows[i].Cells[6].FindControl("SEAmount");
                //        box2.Enabled = false;
                //        box3.Enabled = false;
                //        box6.Enabled = false;
                //        box7.Enabled = false;
                //        i++;
                //    }
                //}
            }
            catch (Exception e)
            {

            }


        }

        protected void CustomerIdSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTable = Session["BPDetails"] as DataTable;
                DataRow row = null;
                string selectedBP = CustomerId.SelectedValue;
                InvoiceNoList.Items.Clear();
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    if (selectedBP == row["BusinessPartnerCode"].ToString())
                    {
                        Name.Text = row["Name"].ToString();
                        Location.Text = row["CountryCode"].ToString();
                        Telephone.Text = row["Phone"].ToString();
                        if (row["OrderType"].ToString() == "0")
                        {
                            SalesReturn.Text = AgainstSequenceNo.Value;
                        }
                        else if (row["OrderType"].ToString() == "1")
                        {
                            SalesReturn.Text = ManualSequenceNo.Value;
                        }
                        ListItem list = new ListItem();
                        list.Value = row["SalesOrderNo"].ToString();
                        list.Text = row["SalesOrderNo"].ToString();
                        InvoiceNoList.Items.Add(list);
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void DisplaySalesReturnDetails()
        {
            try
            {
                DataTable dtTable = XBDataProvider.SalesRetrun.GetSalesReturnDetailsByID(Convert.ToInt32(SalesReturnMstId.Value));
                if(dtTable.Rows.Count==0 && SalesReturnType.SelectedValue=="0")
                {
                    dtTable = XBDataProvider.SalesRetrun.GetInvoiceDetailsUsingInvoiceNo(InvoiceNoList.SelectedValue);
                    btnFinalize.Visible = false;
                }
                if(dtTable.Rows.Count>0)
                {
                    SalesReturnDetail.DataSource = dtTable;
                    SalesReturnDetail.DataBind();
                    SRTotalAmount.Text = Convert.ToDecimal(dtTable.Rows[0]["TotalAmount"]).ToString("0.00");
                    Amount.Text = Convert.ToDecimal(dtTable.Rows[0]["TotalAmount"]).ToString("0.00");
                    SRTotalDiscountAmt.Text = Convert.ToDecimal(dtTable.Rows[0]["TotalDiscountAmount"]).ToString("0.00");
                    SRTotalTaxAmt.Text = Convert.ToDecimal(dtTable.Rows[0]["TotalTaxAmount"]).ToString("0.00");
                    SRTotalOrderAmt.Text = Convert.ToDecimal(dtTable.Rows[0]["TotalOrderAmount"]).ToString("0.00");
                    SalesOrderDate.Value = dtTable.Rows[0]["SalesOrderDate"].ToString();
                }
                
            }
            catch (Exception ex)
            {

            }
        }

        protected void SalesReturnTypeSelectedIndexChanged(object sender, EventArgs e)
        {
            if(SalesReturnType.SelectedValue=="1")
            {
                InvoiceNoList.Items.Clear();
                InvoiceNoList.Enabled = false;
                Reference.ReadOnly = false;
                Reference.CssClass = "form-control required";
                SalesReturn.Text = ManualSequenceNo.Value;
            }
            else if (SalesReturnType.SelectedValue == "0")
            {
                InvoiceNoList.Enabled = true;
                Reference.ReadOnly = true;
                Reference.CssClass = "form-control" ;
                DataTable dtTable = Session["BPDetails"] as DataTable;
                DataRow row = null;
                string selectedBP = CustomerId.SelectedValue;
                InvoiceNoList.Items.Clear();
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    if (selectedBP == row["BusinessPartnerCode"].ToString())
                    {
                        ListItem list = new ListItem();
                        list.Value = row["SalesOrderNo"].ToString();
                        list.Text = row["SalesOrderNo"].ToString();
                        InvoiceNoList.Items.Add(list);
                    }
                }
                SalesReturn.Text = AgainstSequenceNo.Value;
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
                for (int i = 0; i < 20; i++)
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

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                #region Save
                int returnValue = 0;
                int selectedSequenceId = 0;
                string reference = "";
                string invoiceNo = "";
                if (SalesReturnType.SelectedValue == "0")
                {
                    selectedSequenceId = Convert.ToInt32(AgainstSequenceNoID.Value);
                    invoiceNo = InvoiceNoList.SelectedValue;
                }
                else if (SalesReturnType.SelectedValue == "1")
                {
                    reference = Reference.Text;
                    selectedSequenceId = Convert.ToInt32(ManualSequenceNoID.Value);
                }
                DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                float amount = 0;
                if (Amount.Text != "")
                {
                    amount = float.Parse(Amount.Text, CultureInfo.InvariantCulture.NumberFormat);
                }
                returnValue = XBDataProvider.SalesRetrun.SaveSalesReturn(Session["CompanyCode"].ToString(), Convert.ToInt32(SalesReturnType.SelectedValue), CustomerId.SelectedValue, SalesReturn.Text, 1, date, Location.Text, SalesMan.Text,
                    User.Identity.Name, reference, amount, Telephone.Text, invoiceNo, selectedSequenceId);
                if (returnValue > 0)
                {
                    SalesReturnMstId.Value = returnValue.ToString();
                    PageStatus.Value = "edit";
                    gridDetails.Visible = true;
                    SaveBtn.Visible = false;
                    CancelBtn.Visible = false;
                    SetInitialRows();
                    SaveSuccess.Visible = true;
                    failure.Visible = false;
                    if(SalesReturnType.SelectedValue=="0")
                    {
                        DisplaySalesReturnDetails();
                    }
                    else
                    {
                        AddNewRow.Visible = true;
                        SetInitialRows();
                        SalesOrderDate.Value = DateTime.Now.ToString();
                    }
                    btnFinalize.Visible = false;
                }
                else
                {
                    failure.Visible = true;
                }
                #endregion
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
                foreach (GridViewRow row in SalesReturnDetail.Rows)
                {
                    TextBox box2 = (TextBox)SalesReturnDetail.Rows[i].Cells[1].FindControl("SRItem");
                    TextBox box3 = (TextBox)SalesReturnDetail.Rows[i].Cells[2].FindControl("SRItemName");
                    TextBox box4 = (TextBox)SalesReturnDetail.Rows[i].Cells[3].FindControl("SRItemRate");
                    TextBox box5 = (TextBox)SalesReturnDetail.Rows[i].Cells[4].FindControl("SRQuantity");
                    TextBox box6 = (TextBox)SalesReturnDetail.Rows[i].Cells[5].FindControl("SRUnit");
                    TextBox box7 = (TextBox)SalesReturnDetail.Rows[i].Cells[6].FindControl("SRDiscPer");
                    TextBox box8 = (TextBox)SalesReturnDetail.Rows[i].Cells[7].FindControl("SRDiscAmt");
                    TextBox box9 = (TextBox)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxPer");
                    TextBox box10 = (TextBox)SalesReturnDetail.Rows[i].Cells[9].FindControl("SRTaxAmt");
                    TextBox box11 = (TextBox)SalesReturnDetail.Rows[i].Cells[10].FindControl("SRNetAmt");
                    HiddenField hdnFld = (HiddenField)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxCode");
                    if (box2.Text != "" && box2.Text.Length != 0)
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
                        dr["LocationCode"] = Location.Text;
                        dr["SalesReturnMstId"] = Convert.ToInt32(SalesReturnMstId.Value);
                        dr["SalesReturnNo"] = SalesReturn.Text;
                        dr["SalesReturnDate"] = date;
                        dr["BusinessPartnerCode"] = CustomerIdText.Text;
                        dr["SalesOrderNo"] = InvoiceNoText.Text;
                        dr["SalesOrderDate"] = SalesOrderDate.Value;
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = "";
                        dr["Rate"] = float.Parse(box4.Text, CultureInfo.InvariantCulture.NumberFormat);
                        dr["TotalRate"] = float.Parse(Amount.Text, CultureInfo.InvariantCulture.NumberFormat);
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
                if (dt.Rows.Count > 0)
                {
                    XBDataProvider.SalesRetrun.SaveSRDetail(Convert.ToInt32(SalesReturnMstId.Value), SRPayTerms.Text, float.Parse(SRTotalAmount.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalDiscountAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalTaxAmt.Text, CultureInfo.InvariantCulture.NumberFormat), float.Parse(SRTotalOrderAmt.Text, CultureInfo.InvariantCulture.NumberFormat), User.Identity.Name, dt);
                    PageStatus.Value = "edit";
                    Status.SelectedValue = "1";
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
                        TextBox box2 = (TextBox)SalesReturnDetail.Rows[i].Cells[1].FindControl("SRItem");
                        TextBox box3 = (TextBox)SalesReturnDetail.Rows[i].Cells[2].FindControl("SRItemName");
                        TextBox box4 = (TextBox)SalesReturnDetail.Rows[i].Cells[3].FindControl("SRItemRate");
                        TextBox box5 = (TextBox)SalesReturnDetail.Rows[i].Cells[4].FindControl("SRQuantity");
                        TextBox box6 = (TextBox)SalesReturnDetail.Rows[i].Cells[5].FindControl("SRUnit");
                        TextBox box7 = (TextBox)SalesReturnDetail.Rows[i].Cells[6].FindControl("SRDiscPer");
                        TextBox box8 = (TextBox)SalesReturnDetail.Rows[i].Cells[7].FindControl("SRDiscAmt");
                        TextBox box9 = (TextBox)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxPer");
                        TextBox box10 = (TextBox)SalesReturnDetail.Rows[i].Cells[9].FindControl("SRTaxAmt");
                        TextBox box11 = (TextBox)SalesReturnDetail.Rows[i].Cells[10].FindControl("SRNetAmt");
                        HiddenField hdnFld = (HiddenField)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxCode");
                        box2.Text = dt.Rows[i]["ItemCode"].ToString();
                        box3.Text = dt.Rows[i]["ItemName"].ToString();
                        box4.Text = dt.Rows[i]["Rate"].ToString();
                        box5.Text = dt.Rows[i]["Qty"].ToString();
                        box6.Text = dt.Rows[i]["BaseUnitCode"].ToString();
                        box7.Text = dt.Rows[i]["DiscountPercentage"].ToString();
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

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)SalesReturnDetail.Rows[i].Cells[1].FindControl("SRItem");
                        TextBox box3 = (TextBox)SalesReturnDetail.Rows[i].Cells[2].FindControl("SRItemName");
                        TextBox box4 = (TextBox)SalesReturnDetail.Rows[i].Cells[3].FindControl("SRItemRate");
                        TextBox box5 = (TextBox)SalesReturnDetail.Rows[i].Cells[4].FindControl("SRQuantity");
                        TextBox box6 = (TextBox)SalesReturnDetail.Rows[i].Cells[5].FindControl("SRUnit");
                        TextBox box7 = (TextBox)SalesReturnDetail.Rows[i].Cells[6].FindControl("SRDiscPer");
                        TextBox box8 = (TextBox)SalesReturnDetail.Rows[i].Cells[7].FindControl("SRDiscAmt");
                        TextBox box9 = (TextBox)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxPer");
                        TextBox box10 = (TextBox)SalesReturnDetail.Rows[i].Cells[9].FindControl("SRTaxAmt");
                        TextBox box11 = (TextBox)SalesReturnDetail.Rows[i].Cells[10].FindControl("SRNetAmt");
                        HiddenField hdnFld = (HiddenField)SalesReturnDetail.Rows[i].Cells[8].FindControl("SRTaxCode");

                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i]["ID"] = Convert.ToInt32(SalesReturnDetail.DataKeys[i]["ID"]); ;
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
                            dtCurrentTable.Rows[i]["DiscountPercentage"] = box7.Text;
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

                    SalesReturnDetail.DataSource = dtCurrentTable;
                    SalesReturnDetail.DataBind();
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
    }
}