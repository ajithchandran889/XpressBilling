using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBDataProvider
{
    public static class Country
    {
        public static DataTable GetCountries()
        {
            DataTable dtCountries = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtCountries = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetCountries");
            }
            catch (Exception ex)
            {

            }
            return dtCountries;
        }
    }
}
