using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBDataProvider
{
    public static class UserRegistration
    {
        public static bool SaveAddlUserRegDetails(string userName, string companyCode, string location=null,string employee=null,string defaultLocation=null,string createdBy=null,string path=null)
        {
            // create variables that need to be used locally.
            bool returnValue = true;


            // Setup the process for saving the additional registration values.
            SqlCommand cmdAddlDetails = new SqlCommand();
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            cmdAddlDetails.Parameters.Add(new SqlParameter("@UserName", userName));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@LocationCode", location));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@EmployeeCode", employee));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@DefaultLocation", defaultLocation));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@CreatedBy", createdBy));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@CreatedAt", DateTime.Now));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@Path", path));
            DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_InsertUserRegistrationDetails", cmdAddlDetails);

            return returnValue;

        }

        public static bool UpdateUserRegDetails(string userId, string location = null, string employee = null, string defaultLocation = null, string updatedBy = null, string path = null)
        {
            // create variables that need to be used locally.
            bool returnValue = true;


            // Setup the process for saving the additional registration values.
            SqlCommand cmdAddlDetails = new SqlCommand();
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            cmdAddlDetails.Parameters.Add(new SqlParameter("@Id", userId));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@LocationCode", location));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@EmployeeCode", employee));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@DefaultLocation", defaultLocation));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@updatedBy", updatedBy));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@updatedAt", DateTime.Now));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@Path", path));
            DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_UpdateUserRegistrationDetails", cmdAddlDetails);

            return returnValue;

        }

    }
}
