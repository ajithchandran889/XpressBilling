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
    public static class SalesRetrun
    {
        public static DataTable GetAllSalesReturn(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_SalesReturnGetAll", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeleteSalesReturn(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_SalesReturnDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static DataTable GetAllFinalizedBussinessPartnerCodesFromInvoice(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllFinalizedBussinessPartnerCodesFromInvoice", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetInvoiceDetailsUsingInvoiceNo(string invoiceNo)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@invoiceNo", invoiceNo));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetInvoiceDetailsUsingInvoiceNo", cmd);
            }
            catch (Exception ex)
            {

            }
            return dtTable;
        }

        public static DataTable GetSalesReturnDetailsByID(int id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSalesReturnDetailsByID", cmd);
            }
            catch (Exception ex)
            {

            }
            return dtTable;
        }
        public static int SaveSalesReturn(string companyCode, int salesReturnType, string customerId, string salesReturnNo, int status, DateTime documentDate,
                                   string locationCode, string salesMan, string currentUser, string reference, float amount,string name, string telephone,string invoiceNo, int selectedSequenceId
                                 , string paymentTerms, float totalAmount, float totalDiscountAmt, float totalTaxAmt, float totalNetAmt, float Demurages, string user, DataTable SQDetail)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceId", selectedSequenceId));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@customerId", customerId));
                cmd.Parameters.Add(new SqlParameter("@Locationcode", locationCode));
                cmd.Parameters.Add(new SqlParameter("@DocumentNo", salesReturnNo));
                cmd.Parameters.Add(new SqlParameter("@invoiceNo", invoiceNo));
                cmd.Parameters.Add(new SqlParameter("@Transactiontype", salesReturnType));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@DocumentDate", documentDate));
                cmd.Parameters.Add(new SqlParameter("@name", name));
                cmd.Parameters.Add(new SqlParameter("@telephone", telephone));
                cmd.Parameters.Add(new SqlParameter("@salesMan", salesMan));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@Createdby", currentUser));
                cmd.Parameters.Add(new SqlParameter("@Updatedby", currentUser));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@paymentTerms", paymentTerms));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", totalAmount));
                cmd.Parameters.Add(new SqlParameter("@totalDiscountAmt", totalDiscountAmt));
                cmd.Parameters.Add(new SqlParameter("@totalTaxAmt", totalTaxAmt));
                cmd.Parameters.Add(new SqlParameter("@totalNetAmt", totalNetAmt));
                cmd.Parameters.Add(new SqlParameter("@Demurages", Demurages));
                cmd.Parameters.Add(new SqlParameter("@SRDetail", SQDetail));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_SalesreturnMst_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static DataTable GetSalesReturnById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSalesReturnyId", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool SaveSRDetail(int SQMasterId, string paymentTerms,string reference, float totalAmount, float totalDiscountAmt, float totalTaxAmt, float totalNetAmt, float demurages, string user, DataTable SQDetail, DataTable dtDeletedIds)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@SRMasterId", SQMasterId));
                cmd.Parameters.Add(new SqlParameter("@paymentTerms", paymentTerms));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", totalAmount));
                cmd.Parameters.Add(new SqlParameter("@totalDiscountAmt", totalDiscountAmt));
                cmd.Parameters.Add(new SqlParameter("@totalTaxAmt", totalTaxAmt));
                cmd.Parameters.Add(new SqlParameter("@totalNetAmt", totalNetAmt));
                cmd.Parameters.Add(new SqlParameter("@demurages", demurages));
                cmd.Parameters.Add(new SqlParameter("@reference", reference));
                cmd.Parameters.Add(new SqlParameter("@SRDetail", SQDetail));
                cmd.Parameters.Add(new SqlParameter("@dtDeletedIds", dtDeletedIds));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_SalesReturnDtl_xpins", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool FinlizeSalesReturn(int Id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FinlizeSalesReturn", cmd);
                return true;
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public static DataTable GetFinalizedSalesOrderDetails(string bpCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@codBP", bpCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetFinalizedSalesOrderDetails", cmd);
            }
            catch (Exception ex)
            {

            }
            return dtTable;
        }

        public static DataTable GetItemMasters(string companyCode,string location)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@location", location));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetItemMasters_SR", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
