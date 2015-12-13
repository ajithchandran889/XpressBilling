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
    public static class PaymentMode
    {
        public static bool SavePaymentMode(string companyCode,string name, int transaction, string bankAccount, string user)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Transaction", transaction));
                if(transaction==1)
                {
                    cmd.Parameters.Add(new SqlParameter("@BankCode", bankAccount));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@AccountNo", bankAccount));
                }
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PaymentMode_xpins", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool UpdatePaymentMode(string id, string name, int transaction, string bankAccount, string user,bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Transactions", transaction));
                if (transaction == 1)
                {
                    cmd.Parameters.Add(new SqlParameter("@BankCode", bankAccount));
                }
                else
                {
                    cmd.Parameters.Add(new SqlParameter("@AccountNo", bankAccount));
                }
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PaymentMode_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        

        public static DataTable GetPaymentModeById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPaymentModeById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetAllPaymentMode(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_PaymentModeGetAll", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_PaymentModeGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivatePaymentMode(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PaymentMode_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivatePaymentMode(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PaymentMode_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeletePaymentModes(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PaymentModeDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
    }
}
