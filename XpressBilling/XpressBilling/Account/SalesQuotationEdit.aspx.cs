﻿using System;
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
                    QuotationType.SelectedValue = dtTable.Rows[0]["OrderType"].ToString();
                    QuotationType.Enabled = false;
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
                    DataTable salesQuotationDetails = XBDataProvider.SalesQuotation.GetSalesQuotationById(id);
                    if (salesQuotationDetails.Rows.Count > 0)
                    {
                        SetSalesQuotationDetails(salesQuotationDetails);
                        PageStatus.Value = "edit";
                    }
                }
                else
                {
                    PageStatus.Value = "create";
                    CreatedDate.Text = DateTime.Now.Date.ToString("MM'/'dd'/'yyyy"); 
                    CreatedDate.ReadOnly = true;
                    Validity.Text = DateTime.Now.Date.AddDays(10).ToString("MM'/'dd'/'yyyy");
                    SalesOrder.ReadOnly = true;

                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetSaleQuotationCashCreditSequenceDetails(Session["CompanyCode"].ToString());
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        string sequenceNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(), Convert.ToInt32(row["lastSequenceNo"]), Convert.ToInt32(row["Digits"]));

                        if (row["OrderType"].ToString() == "Cash")
                        {
                            Quotation.Text = sequenceNo;
                            CashSequenceNo.Value = sequenceNo;
                            CashSequenceNoID.Value = row["ID"].ToString();
                        }
                        else if (row["OrderType"].ToString() == "Credit")
                        {
                            CreditSequenceNo.Value = sequenceNo;
                            CreditSequenceNoID.Value = row["ID"].ToString();
                        }
                        Quotation.ReadOnly = true;
                    }
                    gridDetails.Visible = false;
                }
            }
        }

        public void SetSalesQuotationDetails(DataTable salesQuotationDetails)
        {
            try
            {
                SaveBtn.Visible = false;
                CancelBtn.Visible = false;
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
                CustomerId.Enabled = false;
                Reference.Text = row["Reference"].ToString();
                Reference.ReadOnly = true;
                SalesMan.Text = row["SalesMan"].ToString();
                SalesMan.ReadOnly = true;
                Validity.Text =Convert.ToDateTime(row["Validity"]).ToString("MM'/'dd'/'yyyy"); ;
                Validity.ReadOnly = true;
                PayTerms.Text = row["PaymentTerms"].ToString();
                DeliveryTerms.Text = row["DeliveryTerms"].ToString();
                TotalAmount.Text = row["Amount"].ToString();
                TotalDiscountAmt.Text = row["DiscountAmount"].ToString();
                TotalTaxAmt.Text = row["TaxAmount"].ToString();
                TotalOrderAmt.Text = row["OrderAmount"].ToString();
                Telephone.Text = row["OrderAmount"].ToString();
                Telephone.ReadOnly = true;
                CreatedDate.Text = Convert.ToDateTime(row["SalesQuotationDate"]).ToString("MM'/'dd'/'yyyy");
                CreatedDate.ReadOnly = true;
                Location.Text = row["LocationCode"].ToString();
                Location.ReadOnly = true;
                SalesOrder.Text = row["SalesOrderNo"].ToString();
                AddNewRow.Visible = false;
                if(SalesOrder.Text!="")
                {
                    btnConverOrder.Visible = false;
                    btnSaveDtl.Visible = false;
                }

                SetSalesQuotationChildGrid();
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
                for (int i = 0; i < dtTable.Rows.Count; i++)
                {
                    row = dtTable.Rows[i];
                    if (selectedBP == row["BusinessPartnerCode"].ToString())
                    {
                        Name.Text = row["Name"].ToString();
                        Name.ReadOnly = true;
                        Location.Text = row["CountryCode"].ToString();
                        Location.ReadOnly = true;
                        Telephone.Text = dtTable.Rows[0]["Phone"].ToString();
                        Telephone.ReadOnly = true;
                        QuotationType.SelectedValue = dtTable.Rows[0]["OrderType"].ToString();
                        QuotationType.Enabled = false;
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
                dt.Columns.Add(new DataColumn("Discount", typeof(string)));
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
                    dr["Discount"] = string.Empty;
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
                SalesQuotationDetail.DataSource = dt;
                SalesQuotationDetail.DataBind();
                return;
            }
            AddNewRow.Visible = false;
            //SetInitialRow();

        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {
            try
            {
                int returnValue = 0;
                int selectedSequenceId = 0;
                if (QuotationType.SelectedValue == "0")
                {
                    selectedSequenceId = Convert.ToInt32(CashSequenceNoID.Value);
                }
                else
                {
                    selectedSequenceId = Convert.ToInt32(CreditSequenceNoID.Value);
                }
                DateTime validity = DateTime.ParseExact(Validity.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                returnValue = XBDataProvider.SalesQuotation.SaveSQ(Session["CompanyCode"].ToString(), Location.Text, Quotation.Text,
                    DateTime.Today.Date, Convert.ToInt32(QuotationType.SelectedValue), Reference.Text, CustomerId.SelectedValue,
                    SalesMan.Text, validity, 1, User.Identity.Name, selectedSequenceId, Telephone.Text);
                if (returnValue > 0)
                {
                    SalesQuotationId.Value = returnValue.ToString();
                    gridDetails.Visible = true;
                    SaveBtn.Visible = false;
                    CancelBtn.Visible = false;
                    SetInitialRows();
                    //ClearInputs(Page.Controls);
                    //Message.Text = "Successfully added";
                }
                else
                {
                    //Message.Text = "Oops..Something went wrong.Please try again";
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
                dt.Columns.Add(new DataColumn("SalesQuotationMstID", typeof(int)));
                dt.Columns.Add(new DataColumn("SalesQuotationNo", typeof(string)));
                dt.Columns.Add(new DataColumn("SalesQuotationDate", typeof(DateTime)));
                dt.Columns.Add(new DataColumn("ItemCode", typeof(string)));
                dt.Columns.Add(new DataColumn("ItemName", typeof(string)));
                dt.Columns.Add(new DataColumn("BaseUnitCode", typeof(string)));
                dt.Columns.Add(new DataColumn("Qty", typeof(int)));
                dt.Columns.Add(new DataColumn("Currency", typeof(string)));
                dt.Columns.Add(new DataColumn("Rate", typeof(int)));
                dt.Columns.Add(new DataColumn("TotalRate", typeof(int)));
                dt.Columns.Add(new DataColumn("Discount", typeof(string)));
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
                foreach (GridViewRow row in SalesQuotationDetail.Rows)
                {
                    TextBox box2 = (TextBox)SalesQuotationDetail.Rows[i].Cells[1].FindControl("SQItem");
                    TextBox box3 = (TextBox)SalesQuotationDetail.Rows[i].Cells[2].FindControl("SQName");
                    TextBox box4 = (TextBox)SalesQuotationDetail.Rows[i].Cells[3].FindControl("SQRate");
                    TextBox box5 = (TextBox)SalesQuotationDetail.Rows[i].Cells[4].FindControl("SQQuantity");
                    TextBox box6 = (TextBox)SalesQuotationDetail.Rows[i].Cells[5].FindControl("SQUnit");
                    TextBox box7 = (TextBox)SalesQuotationDetail.Rows[i].Cells[6].FindControl("SQDiscPer");
                    TextBox box8 = (TextBox)SalesQuotationDetail.Rows[i].Cells[7].FindControl("SQDiscAmt");
                    TextBox box9 = (TextBox)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxPer");
                    TextBox box10 = (TextBox)SalesQuotationDetail.Rows[i].Cells[9].FindControl("SQTaxAmt");
                    TextBox box11 = (TextBox)SalesQuotationDetail.Rows[i].Cells[10].FindControl("SQNetAmt");
                    HiddenField hdnFld = (HiddenField)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxCode");
                    if (box2.Text != "" && box2.Text.Length != 0)
                    {
                        dr = dt.NewRow();
                        if (string.IsNullOrEmpty(SalesQuotationDetail.DataKeys[i]["ID"].ToString()))
                        {
                            dr["ID"] =DBNull.Value;
                        }
                        else
                        {
                            dr["ID"] = Convert.ToInt32(SalesQuotationDetail.DataKeys[i]["ID"]);
                        }
                        DateTime date = DateTime.ParseExact(CreatedDate.Text, "MM/dd/yyyy", CultureInfo.InvariantCulture);
                        dr["CompanyCode"] = Session["CompanyCode"].ToString();
                        dr["LocationCode"] = Location.Text;
                        dr["SalesQuotationMstID"] = Convert.ToInt32(SalesQuotationId.Value);
                        dr["SalesQuotationNo"] = Quotation.Text;
                        dr["SalesQuotationDate"] = date;
                        dr["ItemCode"] = box2.Text;
                        dr["ItemName"] = box3.Text;
                        dr["BaseUnitCode"] = box6.Text;
                        dr["Qty"] = Convert.ToInt32(box5.Text);
                        dr["Currency"] = "";
                        dr["Rate"] = Convert.ToInt32(box4.Text);
                        dr["TotalRate"] = Convert.ToInt32(TotalAmount.Text);
                        dr["Discount"] = box7.Text;
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
                if (dt.Rows.Count > 0)
                {
                    XBDataProvider.SalesQuotation.SaveSQDetail(Convert.ToInt32(SalesQuotationId.Value), PayTerms.Text, DeliveryTerms.Text, Convert.ToInt32(TotalAmount.Text), Convert.ToInt32(TotalDiscountAmt.Text), Convert.ToInt32(TotalTaxAmt.Text), Convert.ToInt32(TotalOrderAmt.Text), User.Identity.Name, dt);
                    btnConverOrder.Visible = true;
                    btnPrint.Visible = true;
                    PageStatus.Value = "edit";
                    Status.SelectedValue = "1";
                }
                SetSalesQuotationChildGrid();

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
        public static List<ContactDetails> GetContactCodes()
        {
            List<ContactDetails> result = new List<ContactDetails>();
            try
            {
                DataTable dtTable = XBDataProvider.Contact.GetAllContactCode();
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

        protected void BtnConvertOrderClick(object sender, EventArgs e)
        {
            DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetSaleOrderCashCreditSequenceDetails(Session["CompanyCode"].ToString());
            DataRow row = null;
            string orderNo = "";
            for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
            {
                row = dtTableSequenceDetails.Rows[i];
                
                if (row["OrderType"].ToString() == "Cash" && QuotationType.SelectedValue == "0")
                {
                    orderNo  =XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(),Convert.ToInt32(row["lastSequenceNo"]),Convert.ToInt32(row["Digits"]));
                    salesOrderLastIncId.Value = row["ID"].ToString();
                }
                else if (row["OrderType"].ToString() == "Credit" && QuotationType.SelectedValue == "1")
                {
                    orderNo = XBDataProvider.FirstFreeNumber.FormatSequence(row["Prefix"].ToString(),Convert.ToInt32(row["lastSequenceNo"]),Convert.ToInt32(row["Digits"]));
                    salesOrderLastIncId.Value = row["ID"].ToString();
                }
            }
            
            if (XBDataProvider.SalesQuotation.ConvertToSaleOrder(Convert.ToInt32(SalesQuotationId.Value), QuotationType.SelectedItem.Text, orderNo,Convert.ToInt32(salesOrderLastIncId.Value)))
            {
                SalesOrder.Text = orderNo;
                btnConverOrder.Visible = false;
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
                        TextBox box2 = (TextBox)SalesQuotationDetail.Rows[i].Cells[1].FindControl("SQItem");
                        TextBox box3 = (TextBox)SalesQuotationDetail.Rows[i].Cells[2].FindControl("SQName");
                        TextBox box4 = (TextBox)SalesQuotationDetail.Rows[i].Cells[3].FindControl("SQRate");
                        TextBox box5 = (TextBox)SalesQuotationDetail.Rows[i].Cells[4].FindControl("SQQuantity");
                        TextBox box6 = (TextBox)SalesQuotationDetail.Rows[i].Cells[5].FindControl("SQUnit");
                        TextBox box7 = (TextBox)SalesQuotationDetail.Rows[i].Cells[6].FindControl("SQDiscPer");
                        TextBox box8 = (TextBox)SalesQuotationDetail.Rows[i].Cells[7].FindControl("SQDiscAmt");
                        TextBox box9 = (TextBox)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxPer");
                        TextBox box10 = (TextBox)SalesQuotationDetail.Rows[i].Cells[9].FindControl("SQTaxAmt");
                        TextBox box11 = (TextBox)SalesQuotationDetail.Rows[i].Cells[10].FindControl("SQNetAmt");
                        HiddenField hdnFld = (HiddenField)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxCode");
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

            if (ViewState["CurrentTable"] != null)
            {
                DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
                DataRow drCurrentRow = null;
                if (dtCurrentTable.Rows.Count > 0)
                {
                    for (int i = 0; i < dtCurrentTable.Rows.Count; i++)
                    {
                        TextBox box2 = (TextBox)SalesQuotationDetail.Rows[i].Cells[1].FindControl("SQItem");
                        TextBox box3 = (TextBox)SalesQuotationDetail.Rows[i].Cells[2].FindControl("SQName");
                        TextBox box4 = (TextBox)SalesQuotationDetail.Rows[i].Cells[3].FindControl("SQRate");
                        TextBox box5 = (TextBox)SalesQuotationDetail.Rows[i].Cells[4].FindControl("SQQuantity");
                        TextBox box6 = (TextBox)SalesQuotationDetail.Rows[i].Cells[5].FindControl("SQUnit");
                        TextBox box7 = (TextBox)SalesQuotationDetail.Rows[i].Cells[6].FindControl("SQDiscPer");
                        TextBox box8 = (TextBox)SalesQuotationDetail.Rows[i].Cells[7].FindControl("SQDiscAmt");
                        TextBox box9 = (TextBox)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxPer");
                        TextBox box10 = (TextBox)SalesQuotationDetail.Rows[i].Cells[9].FindControl("SQTaxAmt");
                        TextBox box11 = (TextBox)SalesQuotationDetail.Rows[i].Cells[10].FindControl("SQNetAmt");
                        HiddenField hdnFld = (HiddenField)SalesQuotationDetail.Rows[i].Cells[8].FindControl("SQTaxCode");
                        
                        //drCurrentRow["RowNumber"] = i + 1;
                        dtCurrentTable.Rows[i]["ID"] = Convert.ToInt32(SalesQuotationDetail.DataKeys[i]["ID"]); ;
                        dtCurrentTable.Rows[i]["ItemCode"] = box2.Text;
                        dtCurrentTable.Rows[i]["ItemName"] = box3.Text;
                        if(box4.Text!="")
                        {
                            dtCurrentTable.Rows[i]["Rate"] = box4.Text;
                        }
                        if (box5.Text != "")
                        {
                            dtCurrentTable.Rows[i]["Qty"] =box5.Text;
                        }
                        if(box8.Text!="")
                        {
                            dtCurrentTable.Rows[i]["DiscountAmt"] = box8.Text;
                        }
                        if (box10.Text != "")
                        {
                            dtCurrentTable.Rows[i]["TaxAmount"] = box10.Text;
                        }
                        dtCurrentTable.Rows[i]["BaseUnitCode"] = box6.Text;
                        dtCurrentTable.Rows[i]["Discount"] = box7.Text;
                        dtCurrentTable.Rows[i]["TaxPercentage"] = box9.Text;
                        dtCurrentTable.Rows[i]["Tax"] = hdnFld.Value;
                        dtCurrentTable.Rows[i]["NetAmount"] = box11.Text;
                        rowIndex++;
                    }
                    for (int j = 0; j < 5;j++ )
                    {
                        drCurrentRow = dtCurrentTable.NewRow();
                        dtCurrentTable.Rows.Add(drCurrentRow);
                    }
                       
                    ViewState["CurrentTable"] = dtCurrentTable;

                    SalesQuotationDetail.DataSource = dtCurrentTable;
                    SalesQuotationDetail.DataBind();
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
    public class ContactDetails
    {
        public string code { get; set; }
    }
}