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

        public static string GetCompanyCodeByUserId(string userName)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@userName", userName));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CompanyCodeByUserId", cmd);
                if(dtTable.Rows.Count>0)
                {
                    return dtTable.Rows[0]["CompanyCode"].ToString();
                }
            }
            catch (Exception ex)
            {

            }

            return "";
        }
    }
}
