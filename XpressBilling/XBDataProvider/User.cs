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
    public static class User
    {
        public static DataTable GetAllUsers()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_UsersGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetUserById(string Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetUserById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
