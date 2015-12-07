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
    public static class FirstFreeNumber
    {
        public static DataTable GetAllFirstFreeNumber()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_FirstFreeGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivateFirstFreeNumber(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FirstFreeNumber_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateFirstFreeNumber(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FirstFreeNumber_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteFirstFreeNumber(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FirstFreeNumberDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static string GetDocumentNumber()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            return DataProvider.ExecuteScalarString(connString, "dbo.sp_GetLastFirstFreeNumber", cmd);
        }

        public static int SaveFirstFreeMaster(string companyCode, string docNumber, int item, int transaction, string reference, string user)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@DocumentNo", docNumber));
                cmd.Parameters.Add(new SqlParameter("@Transaction", transaction));
                cmd.Parameters.Add(new SqlParameter("@SeqType", item));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@DocumentDate", DateTime.Now.Date));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_FirstFreeNumberMst_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static DataTable GetFirstFreeById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetFirstFreeById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetFirstFreeDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetFirstFreeDtlByMasterId", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static int SaveFirstFreeMasterDetail(DataTable dtFirstFreeDetails)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@tblFirstFreeDtl", dtFirstFreeDetails));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FirstFreeNumberDtl_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static DataTable GetOrderTypeExceptAddedItems(string companyCode)
        {

            DataTable dtTable =new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetOrderTypeExceptAddedItems", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static DataTable GetSaleQuotationCashCreditSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSaleQuotationCashCreditSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static DataTable GetStockEntryCashCreditSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetStockEntryCashCreditSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }


        public static DataTable GetManualInvoiceCashCreditSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetManualInvoiceCashCreditSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static DataTable GetInvoiceCashCreditSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetInvoiceCashCreditSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static DataTable GetSaleOrderCashCreditSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSaleOrderCashCreditSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static DataTable GetGRNSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetGRNSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static DataTable GetPurchaseOrderSequenceDetails(string companyCode)
        {

            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPurchaseOrderSequenceDetails", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }

        public static string FormatSequence(string prefix, int seqNo, int length)
        {
            int prefixLength = prefix.Length;
            string intCount = "D" + (length - prefixLength);
            return prefix + "" + seqNo.ToString(intCount);
        }
    }
}
