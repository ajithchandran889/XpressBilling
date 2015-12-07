using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XBDataProvider
{
    public static class PurchaseOrder
    {
        public static DataTable GetAllPurchaseOrder()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_PurchaseOrderGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeletePurchaseOrder(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_PurchaseOrderDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static DataTable GetPurchaseOrderById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPurchaseOrderById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static int SavePO(string companyCode, string locationCode, string purchaseOrderNo, DateTime purchaseOrderDate,
                           int orderType, string reference, string bussinesspartnersCode, string salesMan, int status,
                           string user, int selectedSequenceId, string telephone,string name,string shipToAddress)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceId", selectedSequenceId));
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Locationcode", locationCode));
                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderNo", purchaseOrderNo));
                cmd.Parameters.Add(new SqlParameter("@PurchaseOrderDate", purchaseOrderDate));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@Ordertype", orderType));
                cmd.Parameters.Add(new SqlParameter("@Businesspartnercode", bussinesspartnersCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@ShipToAddress", shipToAddress));
                cmd.Parameters.Add(new SqlParameter("@Salesman", salesMan));
                cmd.Parameters.Add(new SqlParameter("@Status", status));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@Telephone", telephone));
                return DataProvider.ExecuteScalarInt(connString, "dbo.sp_PurchaseOrderMst_xpins", cmd);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static bool SavePODetail(int POMasterId, string paymentTerms, string deliveryTerms, int totalAmount, int totalDiscountAmt, int totalTaxAmt, int totalNetAmt, string user, string shipToAddress,DataTable PODetail)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@POMasterId", POMasterId));
                cmd.Parameters.Add(new SqlParameter("@paymentTerms", paymentTerms));
                cmd.Parameters.Add(new SqlParameter("@deliveryTerms", deliveryTerms));
                cmd.Parameters.Add(new SqlParameter("@totalAmount", totalAmount));
                cmd.Parameters.Add(new SqlParameter("@totalDiscountAmt", totalDiscountAmt));
                cmd.Parameters.Add(new SqlParameter("@totalTaxAmt", totalTaxAmt));
                cmd.Parameters.Add(new SqlParameter("@totalNetAmt", totalNetAmt));
                cmd.Parameters.Add(new SqlParameter("@PODetail", PODetail));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", user));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@ShipToAddress", shipToAddress));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_PurchaseOrderDtl_xpins", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static DataTable GetPurchaseOrderDtlById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetPurchaseOrderDtlById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool ConvertToGRN(int Id, string orderType)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                cmd.Parameters.Add(new SqlParameter("@OrderType", orderType));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ConvertPOToGRN", cmd);
                return true;
            }
            catch (Exception ex)
            {

            }

            return true;
        }

       
    }
}
