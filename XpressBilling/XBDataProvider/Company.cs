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
    public static class Company
    {
        public static int SaveCompany(string companyCode, string name, string PermanantAccountNo, DateTime FormationDate, string TaxId, string RegistrationNumber,
                                      string contactPerson, string Logo, string Note, bool status, string ErrorMsg, string userName,
                                      string phone,string mobile,string email,string web,string designation,string address1,string address2,
                                      string city, string area, int zipCode, string country, string state, String fax, bool dbstatus, string Currency)
        {
            try
            {
                int rtnvalue = -1;
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@PermanantAccountNo", PermanantAccountNo));
                cmd.Parameters.Add(new SqlParameter("@FormationDate", FormationDate));
                cmd.Parameters.Add(new SqlParameter("@TaxId", TaxId));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@Logo", Logo));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
               // cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@ErrorMsg", ErrorMsg));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Phone", phone));
                cmd.Parameters.Add(new SqlParameter("@Mobile", mobile));
                cmd.Parameters.Add(new SqlParameter("@Email", email));
                cmd.Parameters.Add(new SqlParameter("@Web", web));
                cmd.Parameters.Add(new SqlParameter("@ContactPerson", contactPerson));
                cmd.Parameters.Add(new SqlParameter("@Designation", designation));
                cmd.Parameters.Add(new SqlParameter("@Address1", address1));
                cmd.Parameters.Add(new SqlParameter("@Address2", address2));
                cmd.Parameters.Add(new SqlParameter("@City", city));
                cmd.Parameters.Add(new SqlParameter("@Area", area));
                cmd.Parameters.Add(new SqlParameter("@ZipCode", zipCode));
                cmd.Parameters.Add(new SqlParameter("@Country", country));
                cmd.Parameters.Add(new SqlParameter("@State", state));
                cmd.Parameters.Add(new SqlParameter("@Fax", fax));
                cmd.Parameters.Add(new SqlParameter("@Status", dbstatus));
                cmd.Parameters.Add(new SqlParameter("@CurrencyCode", Currency));
                cmd.Parameters.Add(new SqlParameter("@returnvar", rtnvalue));
                int returnValue = DataProvider.ExecuteScalarInt(connString, "dbo.sp_CompanyMst_xpins", cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static bool SaveCompanyInitail(string companyCode, string name, string PermanantAccountNo, string FormationDate, string TaxId, string RegistrationNumber,
                                      string Note, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@PermanantAccountNo", PermanantAccountNo));
                cmd.Parameters.Add(new SqlParameter("@FormationDate", FormationDate));
                cmd.Parameters.Add(new SqlParameter("@TaxId", TaxId));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CompanyMst_xpins_initial", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool UpdateCompany(string companyId, string name, string PermanantAccountNo, DateTime FormationDate, string TaxId, string RegistrationNumber,
                                      string Logo, string Note, string userName, bool dbstatus, string Currency)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@PermanantAccountNo", PermanantAccountNo));
                //cmd.Parameters.Add(new SqlParameter("@FormationDate", FormationDate));
                cmd.Parameters.Add(new SqlParameter("@TaxId", TaxId));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@Logo", Logo));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Status", dbstatus));
                cmd.Parameters.Add(new SqlParameter("@CurrencyCode", Currency));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CompanyMst_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static DataTable GetAllCompanies()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CompanyGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }


        public static DataTable GetAllCompanyCode()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllCompanyCode");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetAllActiveCompany()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllActiveCompany");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static bool IsCompanyexist()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable=DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllCompanyCode");
                if(dtTable.Rows.Count>0)
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

        public static DataTable GetCompanyById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CompanyGetById",cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivateCompany(int id)
        {
            try
            {
              string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
              SqlCommand cmd = new SqlCommand();
              cmd.Parameters.Add(new SqlParameter("@CompanyId", id));
              DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Company_Activate",cmd);
                
            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateCompany(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyId", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Company_DeActivate",cmd);
                
            }
            catch (Exception ex)
            {
            }

        }
        public static void DeleteCompany(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CompanyDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static DataTable GetCompanyDetailsByCode(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable= DataProvider.GetSQLDataTable(connString, "dbo.sp_GetCompanyDetailsByCompanyCode", cmd);

            }
            catch (Exception ex)
            {
            }
            return dtTable;
        }
    }
}
