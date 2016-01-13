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
    public static class BussinessPartner
    {

        public static int SaveBP(string companyCode,string bussinessPartner, string name,int bussinessPartnerType, int OrderType, int Discount, int CreditLimit,
                                      string contactPerson,string tin,string cst, string Note,   string userName,
                                      string phone, string mobile, string email, string web, string designation, string address1, string address2,
                                      string city, string area, string zipCode, string country, string state, String fax)
        {
            try
            {
                int rtnvalue = -1;
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerCode", bussinessPartner));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerType", bussinessPartnerType));
                cmd.Parameters.Add(new SqlParameter("@OrderType", OrderType));
                cmd.Parameters.Add(new SqlParameter("@Discount", Discount));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@CreditLimit", CreditLimit));
                cmd.Parameters.Add(new SqlParameter("@TaxId1", tin));
                cmd.Parameters.Add(new SqlParameter("@TaxId2", cst));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Phone", phone));
                cmd.Parameters.Add(new SqlParameter("@Mobile", mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", email));
                cmd.Parameters.Add(new SqlParameter("@Web", web));
                cmd.Parameters.Add(new SqlParameter("@ContactPerson", contactPerson));
                cmd.Parameters.Add(new SqlParameter("@ContactCode", bussinessPartner));
                cmd.Parameters.Add(new SqlParameter("@Designation", designation));
                cmd.Parameters.Add(new SqlParameter("@Address1", address1));
                cmd.Parameters.Add(new SqlParameter("@Address2", address2));
                cmd.Parameters.Add(new SqlParameter("@City", city));
                cmd.Parameters.Add(new SqlParameter("@Area", area));
                cmd.Parameters.Add(new SqlParameter("@ZipCode", zipCode));
                cmd.Parameters.Add(new SqlParameter("@Country", country));
                cmd.Parameters.Add(new SqlParameter("@State", state));
                cmd.Parameters.Add(new SqlParameter("@Fax", fax));
                cmd.Parameters.Add(new SqlParameter("@returnvar", rtnvalue));
                int returnValue = DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BusinessPartnerMst_xpins", cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static bool UpdateBP(string bpId, string name, int discount, int creditLimit, string tin, string cst,
                                      string Note, string userName,int status,int orderType)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", bpId));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Discount", discount));
                cmd.Parameters.Add(new SqlParameter("@CreditLimit", creditLimit));
                cmd.Parameters.Add(new SqlParameter("@Tin", tin));
                cmd.Parameters.Add(new SqlParameter("@Cst", cst));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@orderType", orderType));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BusinessPartnerMst_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static DataTable GetBPById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetBussinessPartnerById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivateBP(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@BPId", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BussinessPartner_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateBP(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@BPId", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BussinessPartner_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteBP(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BussinessPartnerDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static DataTable GetAllBussinessPartner(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BussinessPartnerGetAll", cmd);
                //dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BussinessPartnerGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetAllBussinessPartnerCodes(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BussinessPartnerGetAllCodes",cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetAllBussinessPartnerSuplierCodes(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BussinessPartnerGetAllActiveSupplierCodes", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetAllBussinessPartnerCustomerCodes(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BussinessPartnerGetAllCustomerCodes", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetAllSupplierCodes(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BussinessPartnerGetAllSupplierCodes", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
