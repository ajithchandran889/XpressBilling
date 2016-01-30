using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBDataProvider
{
    public static  class Invoice
    {
        public static DataTable GetAllInvoice(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_InvoiceGetAll", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeleteInvoice(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_InvoiceDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static int AddInvoiceWithDetails(string CompanyCode,string CustomerId,string Invoice,int Status,
                                                int InvoiceType,DateTime Date,string Name,string location,string SalesMan,string Telephone,string Reference,
                                                string MIPayTerms, string MIDeliveryTerms, string MIShipToAddress, float MITotalAmount, float MITotalDiscountAmt,
                                                float MITotalTaxAmt, float MITotalOrderAmt, string userName, int selectedSequenceId, DataTable dtDetails,string currencyCode)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Currency", currencyCode));
                cmd.Parameters.Add(new SqlParameter("@companyCode", CompanyCode));
                cmd.Parameters.Add(new SqlParameter("@BuisinessPartnerCode", CustomerId));
                cmd.Parameters.Add(new SqlParameter("@SalesOrderNo", Invoice));
                cmd.Parameters.Add(new SqlParameter("@Status", Status));
                cmd.Parameters.Add(new SqlParameter("@OrderType", InvoiceType));
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@LocationCode", location));
                cmd.Parameters.Add(new SqlParameter("@Telephone", Telephone));
                cmd.Parameters.Add(new SqlParameter("@Reference", Reference));
                cmd.Parameters.Add(new SqlParameter("@Salesman", SalesMan));
                cmd.Parameters.Add(new SqlParameter("@PaymentTerms", MIPayTerms));
                cmd.Parameters.Add(new SqlParameter("@DeliveryTerms", MIDeliveryTerms));
                cmd.Parameters.Add(new SqlParameter("@ShiptoAddress", MIShipToAddress));
                cmd.Parameters.Add(new SqlParameter("@Amount", MITotalAmount));
                cmd.Parameters.Add(new SqlParameter("@DiscountAmount", MITotalDiscountAmt));
                cmd.Parameters.Add(new SqlParameter("@TaxAmount", MITotalTaxAmt));
                cmd.Parameters.Add(new SqlParameter("@OrderAmount", MITotalOrderAmt));
                //cmd.Parameters.Add(new SqlParameter("@Cash", Cash));
                //cmd.Parameters.Add(new SqlParameter("@Card", Card));
                //cmd.Parameters.Add(new SqlParameter("@RoundOff", RoundOff));
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceId", selectedSequenceId));
                cmd.Parameters.Add(new SqlParameter("@dtDetails", dtDetails));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@SalesOrderDate", Date));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_SalesInvoiceMst_xpins", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static bool UpdateInvoiceDetails(int invoiceId,string MIPayTerms, string MIDeliveryTerms, string MIShipToAddress, float MITotalAmount, float MITotalDiscountAmt, float MITotalTaxAmt, float MITotalOrderAmt, string userName, DataTable dtDetails,DataTable dtDeletedIds,string reference)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@invoiceId", invoiceId));
                cmd.Parameters.Add(new SqlParameter("@PaymentTerms", MIPayTerms));
                cmd.Parameters.Add(new SqlParameter("@DeliveryTerms", MIDeliveryTerms));
                cmd.Parameters.Add(new SqlParameter("@ShipToAddress", MIShipToAddress));
                cmd.Parameters.Add(new SqlParameter("@TotalAmount", MITotalAmount));
                cmd.Parameters.Add(new SqlParameter("@reference", reference));
                cmd.Parameters.Add(new SqlParameter("@DiscountAmount", MITotalDiscountAmt));
                cmd.Parameters.Add(new SqlParameter("@TaxAmount", MITotalTaxAmt));
                cmd.Parameters.Add(new SqlParameter("@OrderAmount", MITotalOrderAmt));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@dtDetails", dtDetails));
                cmd.Parameters.Add(new SqlParameter("@dtDeletedIds", dtDeletedIds));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_SalesInvoiceMst_xpupd", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable GetInvoiceById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetInvoiceById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetInvoiceDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetInvoiceDtlByMasterId", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool FinlizeInvoice(int Id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FinalizeInvoice", cmd);
                return true;
            }
            catch (Exception ex)
            {

            }

            return true;
        }
    }
}
