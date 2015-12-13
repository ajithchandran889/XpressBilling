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
    public static class SalesQuotation
    {
        public static DataTable GetSalesQuotationById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSalesQuotationById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetAllSalesQuotation(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_SalesQuotationGetAll", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_SalesQuotationGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeleteSalesQuotation(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_SalesQuotationDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static int SaveSQ(string companyCode, string locationCode, string salesQuotationNo , DateTime salesQuotationDate,
                                   int orderType, string reference, string bussinesspartnersCode,string salesMan,DateTime validity,int status,string user,int selectedSequenceId,string telephone)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceId", selectedSequenceId));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Locationcode", locationCode));
                cmd.Parameters.Add(new SqlParameter("@Salesquotationno", salesQuotationNo));
                cmd.Parameters.Add(new SqlParameter("@Salesquotationdate", salesQuotationDate));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@Ordertype", orderType));
                cmd.Parameters.Add(new SqlParameter("@Businesspartnercode", bussinesspartnersCode));
                cmd.Parameters.Add(new SqlParameter("@Validity", validity));
                cmd.Parameters.Add(new SqlParameter("@Salesman", salesMan));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Telephone", telephone));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_SalesQuotationMst_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static bool SaveSQDetail(int SQMasterId, string paymentTerms, string deliveryTerms, float totalAmount, float totalDiscountAmt, float totalTaxAmt, float totalNetAmt, string user, DataTable SQDetail)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@SQMasterId", SQMasterId));
                cmd.Parameters.Add(new SqlParameter("@paymentTerms", paymentTerms));
                cmd.Parameters.Add(new SqlParameter("@deliveryTerms", deliveryTerms));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", totalAmount));
                cmd.Parameters.Add(new SqlParameter("@totalDiscountAmt", totalDiscountAmt));
                cmd.Parameters.Add(new SqlParameter("@totalTaxAmt", totalTaxAmt));
                cmd.Parameters.Add(new SqlParameter("@totalNetAmt", totalNetAmt));
                cmd.Parameters.Add(new SqlParameter("@SQDetail", SQDetail));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_SalesQuotationDtl_xpins", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static DataTable GetSalesQuotationDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSalesQuotationDtlByMasterId", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool ConvertToSaleOrder(int Id,string orderType,string orderNo,int salesOrderLastIncId)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                cmd.Parameters.Add(new SqlParameter("@OrderType", orderType));
                cmd.Parameters.Add(new SqlParameter("@SalesOrderNo", orderNo));
                cmd.Parameters.Add(new SqlParameter("@salesOrderLastIncId", salesOrderLastIncId));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ConvertQuotationToSalesOrder", cmd);
                return true;
            }
            catch (Exception ex)
            {

            }

            return true;
        }

    }
}
