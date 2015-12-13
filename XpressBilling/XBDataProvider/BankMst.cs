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
    public partial class BankMst
    {
        public static int SaveBankMst(string companyCode, string AccountNo, string name, string Branch, string BankCode, string AccountType, string ContactCode, string Reference, string IBAN, string IFSC, string SWIFT, string MICR, string createdBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@AccountNo", AccountNo));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Branch", Branch));
                cmd.Parameters.Add(new SqlParameter("@BankCode", BankCode));
                cmd.Parameters.Add(new SqlParameter("@AccountType", AccountType));
                cmd.Parameters.Add(new SqlParameter("@ContactCode", ContactCode));
                cmd.Parameters.Add(new SqlParameter("@Reference", Reference));
                cmd.Parameters.Add(new SqlParameter("@IBAN", IBAN));
                cmd.Parameters.Add(new SqlParameter("@IFSC", IFSC));
                cmd.Parameters.Add(new SqlParameter("@SWIFT", SWIFT));
                cmd.Parameters.Add(new SqlParameter("@MICR", MICR));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMaster_xpins", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int UpdateBankMst(int id, string AccountNo, string name, string Branch, string ContactCode, string Reference, string IBAN, string IFSC, string SWIFT, string MICR,string bankcode, string updatedBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@AccountNo", AccountNo));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Branch", Branch));
                cmd.Parameters.Add(new SqlParameter("@ContactCode", ContactCode));
                cmd.Parameters.Add(new SqlParameter("@Reference", Reference));
                cmd.Parameters.Add(new SqlParameter("@IBAN", IBAN));
                cmd.Parameters.Add(new SqlParameter("@IFSC", IFSC));
                cmd.Parameters.Add(new SqlParameter("@SWIFT", SWIFT));
                cmd.Parameters.Add(new SqlParameter("@MICR", MICR));
                cmd.Parameters.Add(new SqlParameter("@BankCode", bankcode));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", updatedBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMaster_xpupd", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataTable GetAllBankMst(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllBankMst", cmd);
               // dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllBankMst");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetAllActiveBankCode(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllActiveBankCodes", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllActiveBankCodes");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetBankMstById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BankMasterGetById", cmd);
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
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMst_Activate", cmd);

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
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMst_DeActivate", cmd);

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
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMstDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
    }
}
