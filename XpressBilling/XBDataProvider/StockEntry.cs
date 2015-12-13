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
    public static class StockEntry
    {
        public static DataTable GetAllStockEntry(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_StockEntryGetAll", cmd);
               // dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_StockEntryGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeleteStockEntry(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_StockEntryDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static int SaveSE(string companyCode, int adjustmentType, string stockEntrynNo, int status, DateTime documentDate,
                           string locationCode, string createdUser, string currentUser, string reference, float amount, string currencyCode, int selectedSequenceId)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceId", selectedSequenceId));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Locationcode", locationCode));
                cmd.Parameters.Add(new SqlParameter("@DocumentNo", stockEntrynNo));
                cmd.Parameters.Add(new SqlParameter("@Transactiontype", adjustmentType));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@DocumentDate", documentDate));
                cmd.Parameters.Add(new SqlParameter("@Amount", amount));
                cmd.Parameters.Add(new SqlParameter("@Currency", currencyCode));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@Createdby", createdUser));
                cmd.Parameters.Add(new SqlParameter("@Updatedby", currentUser));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_StockAdjustmentMst_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static bool SaveSEDetail(int SEMasterId, float totalAmount, string user, DataTable SEDetail)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@SEMasterId", SEMasterId));
                cmd.Parameters.Add(new SqlParameter("@Amount", totalAmount));
                cmd.Parameters.Add(new SqlParameter("@SEDetail", SEDetail));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_StockAdjustmentDtl_xpins", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable GetStockEntryDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetStockEntryDtlByMasterId", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetStockEntryById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetStockEntryById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool ConvertToStockRegister(int Id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ConvertStockEntryToStockRegister", cmd);
                return true;
            }
            catch (Exception ex)
            {

            }

            return true;
        }
    }
}
