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
    public static class City
    {
        
        public static DataTable GetCities(string countryCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@countryCode", countryCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CityGetByCountryCode",cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
