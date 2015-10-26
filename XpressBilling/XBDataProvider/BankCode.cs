using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace XBDataProvider
{
    public partial class BankCode
    {

        public static int SaveBankCode(string companyCode, string BankCode, string name, string reference, string createdBy, DateTime createdDate, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@BankCode", BankCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankCode_xpins", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }

        }
        
        public static int UpdateBankCode(int id, string name, string updatedBy, bool status)
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
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankCode_xpupd", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataTable GetBankCode()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetBankCode");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static bool IsBankCodeexist()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllBankCodes");
                if (dtTable.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static DataTable GetBankCodeById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BankCodeGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivateBankCode(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankCode_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateBankCode(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankCode_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteBankCodes(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankCodeDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
       
    }
}
