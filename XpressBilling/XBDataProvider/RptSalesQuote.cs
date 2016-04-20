using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace XBDataProvider
{
    public static class RptSalesQuote
    {
        #region SaveRptSalesQuote

        /// <summary>
        /// SaveRptSalesQuote
        /// </summary>
        /// <param name="companyCode"></param>
        /// <param name="header"></param>
        /// <param name="declaration"></param>
        /// <param name="footer"></param>
        /// <param name="reportID"></param>
        /// <param name="copies"></param>
        /// <param name="headerText"></param>
        /// <param name="declarationText"></param>
        /// <param name="footerText"></param>
        /// <param name="accountNo"></param>
        /// <param name="accountName"></param>
        /// <param name="bankCode"></param>
        /// <param name="bankName"></param>
        /// <param name="branch"></param>
        /// <param name="IFSC"></param>
        /// <param name="IBAN"></param>
        /// <returns></returns>
        public static int SaveRptSalesQuote(string companyCode, bool header, bool declaration,bool footer, int reportID, int copies,
            string headerText, string declarationText, string footerText, string accountNo, string accountName, string bankCode,
             string bankName, string branch, string IFSC, string IBAN, string RptName)
        {
            try
            {
                int rtnvalue = -1;
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();

                cmd.Parameters.Add(new SqlParameter("@CompanyCode",  companyCode));
			cmd.Parameters.Add(new SqlParameter("@Header",  header));
			cmd.Parameters.Add(new SqlParameter("@Declaration",  declaration));
			cmd.Parameters.Add(new SqlParameter("@Footer",  footer));
			cmd.Parameters.Add(new SqlParameter("@Report",  reportID));
			cmd.Parameters.Add(new SqlParameter("@Copies",  copies));
			cmd.Parameters.Add(new SqlParameter("@HeaderText",  headerText));
			cmd.Parameters.Add(new SqlParameter("@DeclarationText",  declarationText));
			cmd.Parameters.Add(new SqlParameter("@FooterText",  footerText));
			cmd.Parameters.Add(new SqlParameter("@AccountNo",  accountNo));
			cmd.Parameters.Add(new SqlParameter("@AccountName",  accountName));
			cmd.Parameters.Add(new SqlParameter("@BankCode",  bankCode));
			cmd.Parameters.Add(new SqlParameter("@BankName",  bankName));
			cmd.Parameters.Add(new SqlParameter("@Branch",  branch));
			cmd.Parameters.Add(new SqlParameter("@IFSC",  IFSC));
            cmd.Parameters.Add(new SqlParameter("@IBAN", IBAN));
                cmd.Parameters.Add(new SqlParameter("@RptName", RptName));

                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_save_SalesQuotationReport", cmd);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        #endregion SaveRptSalesQuote


        public static DataTable GetAllActiveAcoountNo(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllActiveBankMst", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetSalesQuoteParameters(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSalsQuoteParameters", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
