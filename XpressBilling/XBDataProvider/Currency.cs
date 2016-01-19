using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace XBDataProvider
{
    public static class Currency
    {
        public static int SaveCurrency(string companyCode,string currencyCode,string name,string decimalValue,string reference,string createdBy,DateTime createdDate,bool status)
        {
            try 
            {
                int rtnvalue=-1;
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@CurrencyCode", currencyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Decimal", decimalValue));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@createdDate", createdDate));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                cmd.Parameters.Add(new SqlParameter("@returnvar", rtnvalue));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Currency_xpins", cmd);
            }
            catch(Exception ex)
            {
                return 0;
            }
           
        }

        public static int UpdateCurrency(string  id, string name, string decimalValue, string reference, string createdBy, DateTime createdDate, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                //cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                //cmd.Parameters.Add(new SqlParameter("@CurrencyCode", currencyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Decimal", decimalValue));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                //cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", createdBy));
                //cmd.Parameters.Add(new SqlParameter("@createdDate", createdDate));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Currency_xpupd", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static DataTable GetAllCurrencies(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CurrencyGetAll", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CurrencyGetAll");
            }
            catch(Exception ex)
            {

            }
            
            return dtTable;
        }
        public static DataTable GetAllActiveCurrencies(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_ActiveCurrencyGetAll", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetCurrencyById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CurrencyGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static void ActivateCurrency(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CurrencyId", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Currency_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateCurrency(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CurrencyId", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Currency_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }
        public static void DeleteCurrency(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CurrencyDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
        public static string GetCurrencyCodeByCompany(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetCurrencyCodeByCompany", cmd);
                if (dtTable.Rows.Count > 0)
                {
                    return dtTable.Rows[0]["CurrencyCode"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return "";
        }
        public static int GetCurrencyDecimalByCompany(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetCurrencyDecimalByCompany", cmd);
                if (dtTable.Rows.Count > 0)
                {
                    return Convert.ToInt32(dtTable.Rows[0]["Decimal"].ToString());
                }
            }
            catch (Exception ex)
            {

            }
            return 0;
        }
    }
}
