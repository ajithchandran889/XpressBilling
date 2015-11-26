﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
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
                DataTable dtTable = XBDataProvider.BussinessPartner.GetAllBussinessPartnerCodes(Session["CompanyCode"].ToString());
                Session["BPDetails"] = dtTable;
                DataRow row = null;
                CustomerId.Items.Clear();
                if (dtTable.Rows.Count > 0)
                {
                    Name.Text = dtTable.Rows[0]["Name"].ToString();
                    Name.ReadOnly = true;
                    Location.Text = dtTable.Rows[0]["CountryCode"].ToString();
                    Location.ReadOnly = true;
                    Telephone.Text = dtTable.Rows[0]["Phone"].ToString();
                    Telephone.ReadOnly = true;
                    InvoiceType.SelectedValue = dtTable.Rows[0]["OrderType"].ToString();
                    InvoiceType.Enabled = false;
                }
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    ListItem list = new ListItem();
                    list.Value = row["BusinessPartnerCode"].ToString();
                    list.Text = row["BusinessPartnerCode"].ToString();
                    CustomerId.Items.Add(list);
                }
                int id = Convert.ToInt32(Request.QueryString["Id"]);
                if (id != null && id != 0)
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
                    Date.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy");
                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetInvoiceCashCreditSequenceDetails(Session["CompanyCode"].ToString());
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));
                        if (row["OrderType"].ToString() == "Cash")
                        {
                            Invoice.Text = sequenceNo;
                            CashSequenceNo.Value = sequenceNo;
                            CashSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["OrderType"].ToString() == "Credit")
                        {
                            CreditSequenceNo.Value = sequenceNo;
                            CreditSequenceNoID.Value = row["ID"].ToString();
                        }
                        
                    }
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
                CustomerId.SelectedValue = row["BusinessPartnerCode"].ToString();
                CustomerId.Enabled = false;
                Invoice.Text = row["SalesOrderNo"].ToString();
                Invoice.ReadOnly = true;
                Status.SelectedValue = row["Status"].ToString();
                Status.Enabled = false;
                InvoiceType.SelectedValue = row["Status"].ToString();
                InvoiceType.Enabled = false;
                Date.Text = Convert.ToDateTime(row["SalesOrderDate"]).ToString("MM'/'dd'/'yyyy");
                Date.ReadOnly = true;
                Name.Text = row["Reference"].ToString();
                Name.ReadOnly = true;
                Location.Text = row["LocationCode"].ToString();
                Location.ReadOnly = true;
                SalesMan.Text = row["SalesMan"].ToString();
                SalesMan.ReadOnly = true;
                Telephone.Text = row["Reference"].ToString();
                Telephone.ReadOnly = true;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                Amount.Text = row["Amount"].ToString();
                Amount.ReadOnly = true;
                IPayTerms.Text = row["PaymentTerms"].ToString();
                IDeliveryTerms.Text = row["DeliveryTerms"].ToString();
                IShipToAddress.Text = row["ShiptoAddress"].ToString();
                ITotalAmount.Text = row["Amount"].ToString();
                ITotalDiscountAmt.Text = row["DiscountAmount"].ToString();
                ITotalTaxAmt.Text = row["TaxAmount"].ToString();
                ITotalOrderAmt.Text = row["OrderAmount"].ToString();
                //CreatedUser.Text = row["CreatedBy"].ToString();
                //CreatedUser.Enabled = false;
                //Amount.Text = row["Amount"].ToString();
                //Amount.ReadOnly = true;
                //Currency.Text = row["Currency"].ToString();
                //Currency.ReadOnly = true;
                AddNewRow.Visible = false;
                if (row["Status"].ToString()=="2")
                {
                    btnFinalize.Visible = false;
                    btnSaveDtl.Visible=false;
                }
                else
                {
                    btnFinalize.Visible = true;
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
                InvoiceDetail.DataSource = dt;
                InvoiceDetail.DataBind();
            }
            AddNewRow.Visible = false;
        }

        protected void CustomerIdSelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable dtTable = Session["BPDetails"] as DataTable;
                DataRow row = null;
                string selectedBP = CustomerId.SelectedValue;
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    if (selectedBP == row["BusinessPartnerCode"].ToString())
                    {
                        Name.Text = row["Name"].ToString();
                        Name.ReadOnly = true;
                        Location.Text = row["CountryCode"].ToString();
                        Location.ReadOnly = true;
                        InvoiceType.SelectedValue = row["OrderType"].ToString();
                        InvoiceType.Enabled = false;
                        Telephone.Text = row["Phone"].ToString();
                        Telephone.ReadOnly = true;
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

                InvoiceDetail.DataSource = dt;
                InvoiceDetail.DataBind();
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
                        TextBox box2 = (TextBox)InvoiceDetail.Rows[i].Cells[1].FindControl("IItem");
                        TextBox box3 = (TextBox)InvoiceDetail.Rows[i].Cells[2].FindControl("IItemName");
                        TextBox box4 = (TextBox)InvoiceDetail.Rows[i].Cells[3].FindControl("IItemRate");
                        TextBox box5 = (TextBox)InvoiceDetail.Rows[i].Cells[4].FindControl("IQuantity");
                        TextBox box6 = (TextBox)InvoiceDetail.Rows[i].Cells[5].FindControl("IUnit");
                        TextBox box7 = (TextBox)InvoiceDetail.Rows[i].Cells[6].FindControl("IDiscPer");
                        TextBox box8 = (TextBox)InvoiceDetail.Rows[i].Cells[7].FindControl("IDiscAmt");
                        TextBox box9 = (TextBox)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxPer");
                        TextBox box10 = (TextBox)InvoiceDetail.Rows[i].Cells[9].FindControl("ITaxAmt");
                        TextBox box11 = (TextBox)InvoiceDetail.Rows[i].Cells[10].FindControl("INetAmt");
                        HiddenField hdFld = (HiddenField)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxCode");
                        box2.Text = dt.Rows[i]["ItemCode"].ToString();
                        box3.Text = dt.Rows[i]["ItemName"].ToString();
                        box4.Text = dt.Rows[i]["Rate"].ToString();
                        box5.Text = dt.Rows[i]["Qty"].ToString();
                        box6.Text = dt.Rows[i]["BaseUnitCode"].ToString();
                        box7.Text = dt.Rows[i]["DiscountPercentage"].ToString();
                        box8.Text = dt.Rows[i]["DiscountAmt"].ToString();
                        box9.Text = dt.Rows[i]["TaxPercentage"].ToString();
                        hdFld.Value = dt.Rows[i]["Tax"].ToString();
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
                        TextBox box2 = (TextBox)InvoiceDetail.Rows[i].Cells[1].FindControl("IItem");
                        TextBox box3 = (TextBox)InvoiceDetail.Rows[i].Cells[2].FindControl("IItemName");
                        TextBox box4 = (TextBox)InvoiceDetail.Rows[i].Cells[3].FindControl("IItemRate");
                        TextBox box5 = (TextBox)InvoiceDetail.Rows[i].Cells[4].FindControl("IQuantity");
                        TextBox box6 = (TextBox)InvoiceDetail.Rows[i].Cells[5].FindControl("IUnit");
                        TextBox box7 = (TextBox)InvoiceDetail.Rows[i].Cells[6].FindControl("IDiscPer");
                        TextBox box8 = (TextBox)InvoiceDetail.Rows[i].Cells[7].FindControl("IDiscAmt");
                        TextBox box9 = (TextBox)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxPer");
                        TextBox box10 = (TextBox)InvoiceDetail.Rows[i].Cells[9].FindControl("ITaxAmt");
                        TextBox box11 = (TextBox)InvoiceDetail.Rows[i].Cells[10].FindControl("INetAmt");
                        HiddenField hdFld = (HiddenField)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxCode");

                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i]["ID"] = Convert.ToInt32(InvoiceDetail.DataKeys[i]["ID"]); ;
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
                        dtCurrentTable.Rows[i]["DiscountPercentage"] = box7.Text;
                        dtCurrentTable.Rows[i]["TaxPercentage"] = box9.Text;
                        dtCurrentTable.Rows[i]["Tax"] = hdFld.Value;
                        dtCurrentTable.Rows[i]["NetAmount"] = box11.Text;
                        rowIndex++;
                    }
                    for (int j = 0; j < 5; j++)
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }

                    ViewState["CurrentTable"] = dtCurrentTable;

                    InvoiceDetail.DataSource = dtCurrentTable;
                    InvoiceDetail.DataBind();
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

        protected void SaveBtnDetailClick(object sender, EventArgs e)
        {
            try
            {
                #region set item details
                DataTable dt = new DataTable();
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
                dt.Columns.Add(new DataColumn("Rate", typeof(int)));
                dt.Columns.Add(new DataColumn("TotalRate", typeof(int)));
                dt.Columns.Add(new DataColumn("DiscountPercentage", typeof(string)));
                dt.Columns.Add(new DataColumn("DiscountAmt", typeof(int)));
                dt.Columns.Add(new DataColumn("Tax", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxPercentage", typeof(string)));
                dt.Columns.Add(new DataColumn("TaxAmount", typeof(int)));
                dt.Columns.Add(new DataColumn("NetAmount", typeof(string)));
                dt.Columns.Add(new DataColumn("Reference", typeof(string)));
                dt.Columns.Add(new DataColumn("Status", typeof(int)));
                dt.Columns.Add(new DataColumn("ErrorMsg", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("UpdatedBy", typeof(string)));
                dt.Columns.Add(new DataColumn("CreatedDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("UpdatedDate", typeof(DateTime)));
                int i = 0;
                foreach (GridViewRow row in InvoiceDetail.Rows)
                {
                    TextBox box2 = (TextBox)InvoiceDetail.Rows[i].Cells[1].FindControl("IItem");
                    TextBox box3 = (TextBox)InvoiceDetail.Rows[i].Cells[2].FindControl("IItemName");
                    TextBox box4 = (TextBox)InvoiceDetail.Rows[i].Cells[3].FindControl("IItemRate");
                    TextBox box5 = (TextBox)InvoiceDetail.Rows[i].Cells[4].FindControl("IQuantity");
                    TextBox box6 = (TextBox)InvoiceDetail.Rows[i].Cells[5].FindControl("IUnit");
                    TextBox box7 = (TextBox)InvoiceDetail.Rows[i].Cells[6].FindControl("IDiscPer");
                    TextBox box8 = (TextBox)InvoiceDetail.Rows[i].Cells[7].FindControl("IDiscAmt");
                    TextBox box9 = (TextBox)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxPer");
                    TextBox box10 = (TextBox)InvoiceDetail.Rows[i].Cells[9].FindControl("ITaxAmt");
                    TextBox box11 = (TextBox)InvoiceDetail.Rows[i].Cells[10].FindControl("INetAmt");
                    HiddenField hdnFld = (HiddenField)InvoiceDetail.Rows[i].Cells[8].FindControl("ITaxCode");
                    if (box2.Text != "" && box2.Text.Length != 0)
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
                        dr["LocationCode"] = Location.Text;
                        dr["SalesOrderMstId"] = Convert.ToInt32(SalesInvoiceId.Value);
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = "";
                        dr["Rate"] = Convert.ToInt32(box4.Text);
                        dr["TotalRate"] = Convert.ToInt32(Request.Form[Amount.UniqueID]);
                        dr["DiscountPercentage"] = box7.Text;
                        dr["DiscountAmt"] = Convert.ToInt32(box8.Text);
                        dr["Tax"] = hdnFld.Value;
                        dr["TaxPercentage"] = box9.Text;
                        dr["TaxAmount"] = Convert.ToInt32(box10.Text);
                        dr["NetAmount"] = box11.Text;
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
                #endregion
                
                if (SalesInvoiceId.Value != "" && SalesInvoiceId.Value != "0")
                {
                    if (XBDataProvider.Invoice.UpdateInvoiceDetails(Convert.ToInt32(SalesInvoiceId.Value), IPayTerms.Text, IDeliveryTerms.Text, IShipToAddress.Text, Convert.ToInt32(ITotalAmount.Text), Convert.ToInt32(ITotalDiscountAmt.Text), Convert.ToInt32(ITotalTaxAmt.Text), Convert.ToInt32(ITotalOrderAmt.Text), User.Identity.Name, dt))
                    {
                        Amount.Text = Request.Form[Amount.UniqueID];
                    }
                }
                else
                {
                    int selectedSequenceId = 0;
                    if (InvoiceType.SelectedValue == "0")
                    {
                        selectedSequenceId = Convert.ToInt32(CashSequenceNoID.Value);
                    }
                    else if (InvoiceType.SelectedValue == "1")
                    {
                        selectedSequenceId = Convert.ToInt32(CreditSequenceNoID.Value);
                    }
                    int returnValue = 0;
                    DateTime date = DateTime.ParseExact(Date.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                    returnValue = XBDataProvider.Invoice.AddInvoiceWithDetails(Session["CompanyCode"].ToString(), CustomerId.SelectedValue, Invoice.Text, 1,
                                               Convert.ToInt32(InvoiceType.SelectedValue), date, Name.Text, Location.Text, SalesMan.Text, Telephone.Text, Reference.Text,
                                                IPayTerms.Text, IDeliveryTerms.Text, IShipToAddress.Text, Convert.ToInt32(ITotalAmount.Text), Convert.ToInt32(ITotalDiscountAmt.Text), Convert.ToInt32(ITotalTaxAmt.Text), Convert.ToInt32(ITotalOrderAmt.Text), User.Identity.Name, selectedSequenceId, dt);
                    if (returnValue>0)
                    {
                        SalesInvoiceId.Value = returnValue.ToString();
                        btnPrint.Visible = true;
                        SetInvoiceChildGrid();
                        PageStatus.Value = "edit";
                        Status.SelectedValue = "1";
                        Amount.Text = Request.Form[Amount.UniqueID];
                        btnFinalize.Visible = true;
                    }
                }
            }
            catch(Exception ex)
            {

            }
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
                btnFinalize.Visible = false;
                btnSaveDtl.Visible = false;
            }
        }
    }
}