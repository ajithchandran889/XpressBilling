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
        public static int SaveCurrency(string companyCode,string currencyCode,string name,string decimalValue,string reference,string createdBy,string createdDate,string status)
        {
            try 
            {
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
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Currency_xpins", cmd);
            }
            catch(Exception ex)
            {
                return -1;
            }
           
        }
        public static DataTable GetAllCurrencies()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CurrencyGetAll");
            }
            catch(Exception ex)
            {

            }
            
            return dtTable;
        }
    }
}
