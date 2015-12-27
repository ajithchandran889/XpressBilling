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
    public static class Location
    {
        public static int SaveLocation(string companyCode,string LocationCode, string name, string PermanantAccountNo, DateTime FormationDate, string TaxId, string RegistrationNumber,
                                        string ContactCode, string Logo, string Note, string ErrorMsg, string userName,
                                        string phone, string mobile, string email, string web, string contactPerson, string designation, string address1, string address2,
                                        string city, string area, int zipCode, string country, string state, string fax, bool dbstatus)
        {
            try
            {
                int rtnvalue = -1;
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@LocationCode", LocationCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@PermanantAccountNo", PermanantAccountNo));
                cmd.Parameters.Add(new SqlParameter("@FormationDate", FormationDate));
                cmd.Parameters.Add(new SqlParameter("@TaxId", TaxId));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@ContactCode", ContactCode));
                cmd.Parameters.Add(new SqlParameter("@Logo", Logo));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
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
                cmd.Parameters.Add(new SqlParameter("@returnvar", rtnvalue));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_LocationMst_xpins", cmd);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static bool UpdateLocation(string locationId, string name, string PermanantAccountNo, DateTime FormationDate, string TaxId, string RegistrationNumber,
                                       string Logo, string Note,  string userName,bool dbstatus)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@LocationCode", locationId));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@PermanentAccountNo", PermanantAccountNo));
                cmd.Parameters.Add(new SqlParameter("@FormationDate", FormationDate));
                cmd.Parameters.Add(new SqlParameter("@TaxId", TaxId));
                cmd.Parameters.Add(new SqlParameter("@RegistrationNumber", RegistrationNumber));
                cmd.Parameters.Add(new SqlParameter("@Logo", Logo));
                cmd.Parameters.Add(new SqlParameter("@Note", Note));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", userName));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Status", dbstatus));
                 DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_LocationMst_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static DataTable GetAllLocations(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_LocationGetAll", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetLocationById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_LocationGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static void ActivateLocation(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Location_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateLocation(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_Location_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteLocation(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_LocationDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
       
    }
}
