using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace XBDataProvider
{
    public static class SalesQuotationPrint
    {
        public static DataSet GetSalesQuotationPrintData(string locationCode, string businessPartnerCode, string SalesQuotationNo, string companyCode)
        {
            DataSet ds = new DataSet();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@LocationCode", locationCode));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerCode", businessPartnerCode));
                cmd.Parameters.Add(new SqlParameter("@SalesQuotationNo", SalesQuotationNo));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                SqlParameter param = new SqlParameter("@tableNames", SqlDbType.VarChar, 200);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                ds = DataProvider.GetSQLDataSet(connString, "dbo.sp_SalesQuotationPrint", cmd);
                string strTableNames = cmd.Parameters["@tableNames"].Value.ToString();
                string[] tableNames = strTableNames.Split(',');

                for (int i = 0; i < tableNames.Length; i++)
                {
                    ds.Tables[i].TableName = tableNames[i];
                }
            }
            catch (Exception ex)
            {

            }

            return ds;
        }
    }
}
