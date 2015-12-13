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
    public static class Contact
    {

        public static int SaveContact(string Contact, string Name, string CompanyCode, string Designation, string CompanyName, string Phone, string Mobile, string Fax, string Email, string Web, string Address1, string Address2, string Citycontact, string Area, string State, string Country, int Zip, bool status, string note, string errmsg, string username)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Contact", Contact));
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", CompanyCode));
                cmd.Parameters.Add(new SqlParameter("@Designation", Designation));
                cmd.Parameters.Add(new SqlParameter("@CompanyName", CompanyName));
                cmd.Parameters.Add(new SqlParameter("@Phone", Phone));
                cmd.Parameters.Add(new SqlParameter("@Mobile", Mobile));
                cmd.Parameters.Add(new SqlParameter("@Fax", Fax));
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Web", Web));
                cmd.Parameters.Add(new SqlParameter("@Address1", Address1));
                cmd.Parameters.Add(new SqlParameter("@Address2", Address2));
                cmd.Parameters.Add(new SqlParameter("@CityCode", Citycontact));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@State", State));
                cmd.Parameters.Add(new SqlParameter("@CountryCode", Country));
                cmd.Parameters.Add(new SqlParameter("@ZipPostalCode", Zip));
                cmd.Parameters.Add(new SqlParameter("@Note", note));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@ErrorMsg", errmsg));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", username));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", username));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                int returnValue = DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Contact_xpins", cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }


        public static bool UpdateContact(string Contact, string Name, string Designation, string Phone, string Mobile, string Fax, string Email, string Web, string Address1, string Address2, string Citycontact, string Area, string State, string Country, int Zip, bool status, string username)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Contact", Contact));
                cmd.Parameters.Add(new SqlParameter("@Name", Name));
                cmd.Parameters.Add(new SqlParameter("@Designation", Designation));
                cmd.Parameters.Add(new SqlParameter("@Phone", Phone));
                cmd.Parameters.Add(new SqlParameter("@Mobile", Mobile));
                cmd.Parameters.Add(new SqlParameter("@Fax", Fax));
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Web", Web));
                cmd.Parameters.Add(new SqlParameter("@Address1", Address1));
                cmd.Parameters.Add(new SqlParameter("@Address2", Address2));
                cmd.Parameters.Add(new SqlParameter("@CityCode", Citycontact));
                cmd.Parameters.Add(new SqlParameter("@Area", Area));
                cmd.Parameters.Add(new SqlParameter("@State", State));
                cmd.Parameters.Add(new SqlParameter("@CountryCode", Country));
                cmd.Parameters.Add(new SqlParameter("@ZipPostalCode", Zip));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", username));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ContactMst_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        

        //public static DataTable GetContactCodeById(int Id)
        //{
        //    DataTable dtTable = new DataTable();
        //    try
        //    {
        //        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        SqlCommand cmd = new SqlCommand();
        //        cmd.Parameters.Add(new SqlParameter("@Id", Id));
        //        dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_ContactGetById", cmd);
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return dtTable;
        //}

        public static int SaveContactCode(string companyCode, string name, string PermanantAccountNo, string FormationDate, string TaxId, string RegistrationNumber,
                                      string contactPerson, string Logo, string Note, bool status, string ErrorMsg, string userName,
                                      string phone, string mobile, string email, string web, string designation, string address1, string address2,
                                      string city, string area, string zipCode, string country, string state, String fax)
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
                cmd.Parameters.Add(new SqlParameter("@Logo", Logo));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
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
                int returnValue = DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CompanyMst_xpins", cmd);
                return returnValue;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static bool SaveContactCodeInitail(string companyCode, string name, string PermanantAccountNo, string FormationDate, string TaxId, string RegistrationNumber,
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

        public static bool UpdateContactCode(string companyId, string name, string PermanantAccountNo, string FormationDate, string TaxId, string RegistrationNumber,
                                      string Logo, string Note, string userName)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyId", companyId));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@PermanantAccountNo", PermanantAccountNo));
                cmd.Parameters.Add(new SqlParameter("@FormationDate", FormationDate));
                cmd.Parameters.Add(new SqlParameter("@TaxId", TaxId));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@Logo", Logo));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CompanyMst_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        //public static DataTable GetAllContactCode()
        //{
        //    DataTable dtTable = new DataTable();
        //    try
        //    {
        //        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_ContactGetAll");
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return dtTable;
        //}


        //public static DataTable GetAllContactCode()
        //{
        //    DataTable dtTable = new DataTable();
        //    try
        //    {
        //        string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //        dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllCompanyCode");
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //    return dtTable;
        //}

        public static bool IsContactCodeexist()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllCompanyCode");
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

        public static DataTable GetContactCodeById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_CompanyGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }


        public static DataTable GetAllContactCode(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllContactCode", cmd);
               // dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllContactCode");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivateContact(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Contact_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateContact(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Contact_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteContact(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ContactDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
       
    }
}
