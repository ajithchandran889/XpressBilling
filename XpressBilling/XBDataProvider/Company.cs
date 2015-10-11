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
        public static int SaveCompany(string companyCode, string name, string PermanantAccountNo, string FormationDate, string TaxId, string RegistrationNumber, 
                                      string ContactCode, string Logo, string Note, bool status, string ErrorMsg, string userName,
                                      string phone,string mobile,string email,string web,string contactPerson,string designation,string address1,string address2,
                                      string city,string area,string zipCode,string country)
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
                cmd.Parameters.Add(new SqlParameter("@ContactCode", ContactCode));
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
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_CompanyMst_xpins", cmd);
            }
            catch (Exception ex)
            {
                return -1;
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
    }
}
