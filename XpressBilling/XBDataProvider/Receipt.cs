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
    public static class Receipt
    {
        public static double GetDueAmounttByBP(string companyCode, string bpCode)
        {
            double result = 0;
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@bpCode", bpCode));
                result = DataProvider.ExecuteScalarDouble(connString, "dbo.sp_GetDueAmounttByBP", cmd);
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        public static DataTable GetReceiptById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetReceiptById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetReceiptDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetReceiptDtlById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        //public static DataTable GetReceiptLast3Transaction(int Id)
        //{
        //    DataTable dtTable = new DataTable();
        //    try
        //    {
        //        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Parameters.Add(new SqlParameter("@Id", Id));
        //        dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetReceiptDtlById", cmd);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return dtTable;
        //}

        public static bool UpdateReceiptDetails(int receiptId, float amount, string userName, DataTable dtDetails, DataTable dtDeletedIds, string reference,float lastDueAmount)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@receiptId", receiptId));
                cmd.Parameters.Add(new SqlParameter("@Amount", amount));
                cmd.Parameters.Add(new SqlParameter("@reference", reference));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@dtDetails", dtDetails));
                cmd.Parameters.Add(new SqlParameter("@dtDeletedIds", dtDeletedIds));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@lastDueAmount", lastDueAmount));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_Receipt_xpupd", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static int AddReceiptWithDetails(string CompanyCode, int recipetType, string recipetNo, int Status,string bussinessPartner,
                                         DateTime Date, string SalesMan, string location,  string Reference, float Amount, float UnAllocatedAmount,
                                         string userName, int selectedSequenceId, DataTable dtDetails, string currencyCode, float lastDueAmount)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Currency", currencyCode));
                cmd.Parameters.Add(new SqlParameter("@companyCode", CompanyCode));
                cmd.Parameters.Add(new SqlParameter("@recipetType", recipetType));
                cmd.Parameters.Add(new SqlParameter("@recipetNo", recipetNo));
                cmd.Parameters.Add(new SqlParameter("@Status", Status));
                cmd.Parameters.Add(new SqlParameter("@bussinessPartner", bussinessPartner));
                cmd.Parameters.Add(new SqlParameter("@LocationCode", location));
                cmd.Parameters.Add(new SqlParameter("@Reference", Reference));
                cmd.Parameters.Add(new SqlParameter("@Salesman", SalesMan));
                cmd.Parameters.Add(new SqlParameter("@Amount", Amount));
                cmd.Parameters.Add(new SqlParameter("@UnAllocatedAmount", UnAllocatedAmount));
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceId", selectedSequenceId));
                cmd.Parameters.Add(new SqlParameter("@dtDetails", dtDetails));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Date", Date));
                cmd.Parameters.Add(new SqlParameter("@lastDueAmount", lastDueAmount));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_Reciept_xpins", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }
        public static bool FinlizeReceipt(int Id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_FinalizeReceipt", cmd);
                return true;
            }
            catch (Exception ex)
            {

            }

            return true;
        }

        public static DataTable GetAllReceipt(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_ReceiptGetAll", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeleteReceipt(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ReceiptDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
    }
}
