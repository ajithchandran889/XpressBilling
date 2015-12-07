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
    public static class GRN
    {
        public static DataTable GetAllGRN(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GRNGetAll",cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void DeleteGRN(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_GRNDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static DataTable GetSupplierCodesFromPurchaseOrder(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSupplierCodesFromPurchaseOrder", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetSupplierDetails(string codBP)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@codBP", codBP));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetSupplierDetailsGoodsReciept", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetGRNById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetGRNById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetGRNDtl(string selectPO)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@selectPO", selectPO));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetGRNDtlByPO", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool SaveGRNDetail(int GRNMasterID, string goodsReceiptNo, DateTime goodsReceiptDate, string packingSlip, int totalQty, string reference, int grnType, DataTable GRNDetail, int selectedSequenceID)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@GRNMasterID", GRNMasterID));
                cmd.Parameters.Add(new SqlParameter("@GoodsReceiptNo", goodsReceiptNo));
                cmd.Parameters.Add(new SqlParameter("@GoodsReceiptDate", goodsReceiptDate));
                cmd.Parameters.Add(new SqlParameter("@PackingSlip", packingSlip));
                cmd.Parameters.Add(new SqlParameter("@TotalQty", totalQty));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@GrnType", grnType));
                cmd.Parameters.Add(new SqlParameter("@GRNDetail", GRNDetail));
                cmd.Parameters.Add(new SqlParameter("@selectedSequenceID", selectedSequenceID));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_GRN_xpins", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool UpdateGRNDetail(int GRNMasterID, int totalQty, string reference, DataTable GRNDetail)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@GRNMasterID", GRNMasterID));
                cmd.Parameters.Add(new SqlParameter("@TotalQty", totalQty));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@GRNDetail", GRNDetail));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_GRN_xpupdt", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool FinalizeGrn(int GRNMasterID)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@GRNMasterID", GRNMasterID));
                DataProvider.ExecuteScalarInt(connString, "dbo.sp_FinalizeGrn", cmd);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
 
    }
}
