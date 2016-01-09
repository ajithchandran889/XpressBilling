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
    public static class PriceBook
    {
        public static DataTable GetAllPriceBook(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_PriceBookGetAll", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_PriceBookGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivatePriceBook(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PriceBook_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static DataTable GetPriceBookById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPriceBookById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetPriceBookDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPriceBookDtlByMasterId", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetPriceBookDtlByIdAndSearchKey(int Id,string searchKey)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                cmd.Parameters.Add(new SqlParameter("@SearchKey", searchKey));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPriceBookDtlByMasterIdSerachKey", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeActivatePriceBook(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PriceBook_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeletePriceBooks(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PriceBookDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
        public static string GetDocumentNumber()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            return DataProvider.ExecuteScalarString(connString, "dbo.sp_PriceBookGetLastDocumentNumber", cmd);
        }
        public static int SavePriceBookMaster(string companyCode, string docNumber, int item, int orderType, string currency, string user, DataTable dtPriceBookDetails)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@DocumentNo", docNumber));
                cmd.Parameters.Add(new SqlParameter("@PriceType", item));
                cmd.Parameters.Add(new SqlParameter("@OrderType", orderType));
                cmd.Parameters.Add(new SqlParameter("@CurrencyCode", currency));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@DocumentDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@tblPriceBookDtl", dtPriceBookDetails));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_PriceBookMaster_xpins", cmd);
                
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static DataTable GetAddedOrderTypes(string companyCode)
        {
            DataTable dt = new DataTable();
            try
            {
                
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dt= DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAddedOrderTypes", cmd);

            }
            catch (Exception ex)
            {
            }
            return dt;
        }
        public static int SavePriceBookMasterDetail(DataTable dtPriceBookDetails,DataTable deletedIDs)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@tblPriceBookDtl", dtPriceBookDetails));
                cmd.Parameters.Add(new SqlParameter("@deletedIDs", deletedIDs));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PriceBookDtl_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static string GetPriceBookData(string searchTerm, int pageIndex, int priceBookId, int filterItem)
        {
            DataTable dtPriceBook = new DataTable("PriceBookDetails");
            DataSet ds = new DataSet();
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@SearchTerm", searchTerm);
            cmd.Parameters.AddWithValue("@PageIndex", pageIndex);
            cmd.Parameters.AddWithValue("@PriceBookId", priceBookId);
            cmd.Parameters.AddWithValue("@PageSize", 20);
            cmd.Parameters.AddWithValue("@filterItem", filterItem);
            cmd.Parameters.Add("@RecordCount", SqlDbType.Int, 4).Direction = ParameterDirection.Output;
            dtPriceBook = DataProvider.GetSQLDataTable(connString, "sp_GetPriceBookDetailsWithPager", cmd);
            ds.Tables.Add(dtPriceBook);
            DataTable dt = new DataTable("Pager");
            dt.Columns.Add("PageIndex");
            dt.Columns.Add("PageSize");
            dt.Columns.Add("RecordCount");
            dt.Rows.Add();
            dt.Rows[0]["PageIndex"] = pageIndex;
            dt.Rows[0]["PageSize"] = 20;
            dt.Rows[0]["RecordCount"] = cmd.Parameters["@RecordCount"].Value;
            ds.Tables.Add(dt);
            return ds.GetXml();
        }
    }
}
