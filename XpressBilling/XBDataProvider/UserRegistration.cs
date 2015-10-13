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
        public static int SaveAddlUserRegDetails( string userName, string companyCode)
        {
            // create variables that need to be used locally.
            int returnValue = 0;


            // Setup the process for saving the additional registration values.
            SqlCommand cmdAddlDetails = new SqlCommand();
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            cmdAddlDetails.Parameters.Add(new SqlParameter("@UserName", userName));
            cmdAddlDetails.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
            returnValue = DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_InsertUserRegistrationDetails", cmdAddlDetails);

            return returnValue;

        }
    }
}
