using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace XBDataProvider
{
    public static class Employee
    {
        public static int SaveEmployee(string companyCode, string Employee, string name, string doj, string reference, string createdBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@EmployeeCode", Employee));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@DateofJoining", doj));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_EmployeeMaster_xpins", cmd);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int UpdateEmployee(int id, string name, string updatedBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", updatedBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_EmployeeMaster_xpupd", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataTable GetAllEmployee(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllEmployees", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllEmployees");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetEmployeeById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_EmployeeMasterGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static void ActivateEmployee(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Employee_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateEmployee(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Employee_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteEmployee(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_EmployeeDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
    }
}
