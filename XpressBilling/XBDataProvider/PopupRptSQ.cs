using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace XBDataProvider
{
   public static class PopupRptSQ
   {

       #region SaveSQReport
       /// <summary>
       /// SaveSQReport
       /// </summary>
       /// <param name="companyCode"></param>
       /// <param name="location"></param>
       /// <param name="quatation"></param>
       /// <param name="SqId"></param>
       /// <param name="TextH1"></param>
       /// <param name="TextL1"></param>
       /// <param name="TextL2"></param>
       /// <param name="TextL3"></param>
       /// <param name="TextL4"></param>
       /// <returns></returns>
       public static int SaveSQReport(string companyCode, string location, string quatation, int SqId, string TextH1, string TextL1,string TextL2, string TextL3, string TextL4)
       {
           try
           {
               int rtnvalue = -1;
               string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
               //DataProvider dtProv = new DataProvider();
               SqlCommand cmd = new SqlCommand();
               cmd.Parameters.Add(new SqlParameter("@location", location));
               cmd.Parameters.Add(new SqlParameter("@quatation", quatation));
               cmd.Parameters.Add(new SqlParameter("@SqId", SqId));
               cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));

               cmd.Parameters.Add(new SqlParameter("@TextH1", TextH1));
               cmd.Parameters.Add(new SqlParameter("@TextL1", TextL1));
               cmd.Parameters.Add(new SqlParameter("@TextL2", TextL2));
               cmd.Parameters.Add(new SqlParameter("@TextL3", TextL3));
               cmd.Parameters.Add(new SqlParameter("@TextL4", TextL4));
              
               return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_save_SQReport_TC", cmd);
           }
           catch (Exception ex)
           {
               return 0;
           }

       }
       #endregion SaveSQReport

       #region GetSQReportBySQNO
       /// <summary>
       /// GetSQReportBySQNO
       /// </summary>
       /// <param name="sqNo"></param>
       /// <returns></returns>
       public static DataTable GetSQReportBySQNO(string sqNo)
       {
           DataTable dtTable = new DataTable();
           try
           {
               string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
               SqlCommand cmd = new SqlCommand();
               cmd.Parameters.Add(new SqlParameter("@sqNo", sqNo));
               dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_SQReportGetById", cmd);
           }
           catch (Exception ex)
           {

           }

           return dtTable;
       }

       #endregion GetAllActiveCompany
   }
}
