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
    public static class StockRegister
    {
        public static DataTable GetLocationByItemCode(string itemCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@itemCode", itemCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetCountriesByItemCode", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetItemDetails(string itemCode,string location,DateTime from,DateTime to)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@itemCode", itemCode));
                cmd.Parameters.Add(new SqlParameter("@location", location));
                cmd.Parameters.Add(new SqlParameter("@from", from));
                cmd.Parameters.Add(new SqlParameter("@to", to));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetItemDetails", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
