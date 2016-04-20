using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace XBDataProvider
{
    public static class SalesInvoicePrint
    {
        public static DataSet GetSalesInvoicePrintData(string locationCode, string businessPartnerCode, string salesOrderNo, string companyCode)
        {
            DataSet ds = new DataSet();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@LocationCode", locationCode));
                cmd.Parameters.Add(new SqlParameter("@BusinessPartnerCode", businessPartnerCode));
                cmd.Parameters.Add(new SqlParameter("@SalesOrderNo", salesOrderNo));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                SqlParameter param = new SqlParameter("@tableNames", SqlDbType.VarChar, 200);
                param.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(param);
                ds = DataProvider.GetSQLDataSet(connString, "dbo.sp_SalesInvoicePrint", cmd);
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
