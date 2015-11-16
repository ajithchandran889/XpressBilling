using System;
using System.Collections.Generic;
using System.Data;
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
                DataTable dtTable = XBDataProvider.BussinessPartner.GetAllBussinessPartnerCodes();
                Session["BPDetails"] = dtTable;
                DataRow row = null;
                CustomerId.Items.Clear();
                if (dtTable.Rows.Count > 0)
                {
                    Name.Text = dtTable.Rows[0]["Name"].ToString();
                    Name.ReadOnly = true;
                    Location.Text = dtTable.Rows[0]["CountryCode"].ToString();
                    Location.ReadOnly = true;
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
                    DataTable invoiceDetails = XBDataProvider.SalesQuotation.GetSalesQuotationById(id);
                    if (invoiceDetails.Rows.Count > 0)
                    {
                        SetInvoiceDetails(invoiceDetails);
                    }
                }
                else
                {
                    InvoiceDate.Text = DateTime.Now.Date.ToString();
                    InvoiceDate.ReadOnly = true;
                    DataTable dtTableSequenceDetails = XBDataProvider.FirstFreeNumber.GetInvoiceCashCreditSequenceDetails(Session["CompanyCode"].ToString());
                    for (int i = 0; i < dtTableSequenceDetails.Rows.Count; i++)
                    {
                        row = dtTableSequenceDetails.Rows[i];
                        int length = Convert.ToInt32(row["Digits"]);
                        string prefix = row["Prefix"].ToString();
                        int sequence = Convert.ToInt32(row["lastSequenceNo"]);
                        string format = prefix.PadRight(length, '0');
                        string sequenceNo = sequence.ToString(format);

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
                        Invoice.ReadOnly = true;
                    }
                }
            }
        }
        public void SetInvoiceDetails(DataTable invoiceDetails)
        {
            try
            {
                
                //SetSalesQuotationChildGrid();
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
                    }
                }

            }
            catch (Exception ex)
            {

            }
        }

        protected void SaveBtnClick(object sender, EventArgs e)
        {

        }
        protected void AddNewRowClick(object sender, EventArgs e)
        {
            //AddNewRowToGrid();
        }
    }
}