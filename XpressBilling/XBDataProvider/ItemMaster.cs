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
    public partial class ItemMaster
    {
        public static int SaveBankMst(string companyCode, string AccountNo, string name, string Branch, string BankCode, string AccountType, string ContactCode, string Reference, string IBAN, string IFSC, string SWIFT, string MICR, string createdBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@AccountNo", AccountNo));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Branch", Branch));
                cmd.Parameters.Add(new SqlParameter("@BankCode", BankCode));
                cmd.Parameters.Add(new SqlParameter("@AccountType", AccountType));
                cmd.Parameters.Add(new SqlParameter("@ContactCode", ContactCode));
                cmd.Parameters.Add(new SqlParameter("@Reference", Reference));
                cmd.Parameters.Add(new SqlParameter("@IBAN", IBAN));
                cmd.Parameters.Add(new SqlParameter("@IFSC", IFSC));
                cmd.Parameters.Add(new SqlParameter("@SWIFT", SWIFT));
                cmd.Parameters.Add(new SqlParameter("@MICR", MICR));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                //cmd.Parameters.Add(new SqlParameter("@UpdatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now.Date));
                //cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMaster_xpins", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public static int UpdateBankMst(int id, string AccountNo, string name, string Branch, string ContactCode, string Reference, string IBAN, string IFSC, string SWIFT, string MICR, string bankcode, string updatedBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@AccountNo", AccountNo));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Branch", Branch));
                cmd.Parameters.Add(new SqlParameter("@ContactCode", ContactCode));
                cmd.Parameters.Add(new SqlParameter("@Reference", Reference));
                cmd.Parameters.Add(new SqlParameter("@IBAN", IBAN));
                cmd.Parameters.Add(new SqlParameter("@IFSC", IFSC));
                cmd.Parameters.Add(new SqlParameter("@SWIFT", SWIFT));
                cmd.Parameters.Add(new SqlParameter("@MICR", MICR));
                cmd.Parameters.Add(new SqlParameter("@BankCode", bankcode));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", updatedBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BankMaster_xpupd", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataTable GetAllBankMst()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllBankMst");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetAllActiveBankCode()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetAllActiveBankCodes");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetBankMstById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BankMasterGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static bool SaveItemMaster(string companyCode, string itemCode, string name, int itemType, string barCode, string searchKey,
                                      string itemGroupCode, string manufacturerCode, string baseUnitCode, int mrp, int retailPrice, int purchasePrice,
                                      int cost,int inverntoryValuation, int safetyStock, int reorderQuantity, string createdBy)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@ItemCode", itemCode));
                cmd.Parameters.Add(new SqlParameter("@ItemType", itemType));
                cmd.Parameters.Add(new SqlParameter("@SupplierBarcode", barCode));
                cmd.Parameters.Add(new SqlParameter("@SearchKey", searchKey));
                cmd.Parameters.Add(new SqlParameter("@ItemGroupCode", itemGroupCode));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerCode", manufacturerCode));
                cmd.Parameters.Add(new SqlParameter("@BaseUnitCode", baseUnitCode));
                cmd.Parameters.Add(new SqlParameter("@MRP", mrp));
                cmd.Parameters.Add(new SqlParameter("@RetailPrice", retailPrice));
                cmd.Parameters.Add(new SqlParameter("@PurchasePrice", purchasePrice));
                cmd.Parameters.Add(new SqlParameter("@ItemCost", cost));
                cmd.Parameters.Add(new SqlParameter("@InventoryValuation", inverntoryValuation));
                cmd.Parameters.Add(new SqlParameter("@SafetyStock", safetyStock));
                cmd.Parameters.Add(new SqlParameter("@ReorderQty", reorderQuantity));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@CreatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                int returnValue = DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ItemMaster_xpins", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool UpdateItemMaster( string itemMasterId, string name, string barCode, string searchKey,
                                      string itemGroupCode, string manufacturerCode, int mrp, int retailPrice, int purchasePrice,
                                      int cost, int inverntoryValuation, int safetyStock, int reorderQuantity, string updatedBy)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@itemMasterId", itemMasterId));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@SupplierBarcode", barCode));
                cmd.Parameters.Add(new SqlParameter("@SearchKey", searchKey));
                cmd.Parameters.Add(new SqlParameter("@ItemGroupCode", itemGroupCode));
                cmd.Parameters.Add(new SqlParameter("@ManufacturerCode", manufacturerCode));
                cmd.Parameters.Add(new SqlParameter("@MRP", mrp));
                cmd.Parameters.Add(new SqlParameter("@RetailPrice", retailPrice));
                cmd.Parameters.Add(new SqlParameter("@PurchasePrice", purchasePrice));
                cmd.Parameters.Add(new SqlParameter("@ItemCost", cost));
                cmd.Parameters.Add(new SqlParameter("@InventoryValuation", inverntoryValuation));
                cmd.Parameters.Add(new SqlParameter("@SafetyStock", safetyStock));
                cmd.Parameters.Add(new SqlParameter("@ReorderQty", reorderQuantity));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", updatedBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ItemMaster_xpupd", cmd);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }
        public static DataTable GetAllItemMaster()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_ItemMasterGetAll");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static DataTable GetItemMasterById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_ItemMasterById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static void ActivateItemMaster(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ItemMaster_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateItemMaster(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ItemMaster_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteItemMasters(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_ItemMasterDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
        public static DataTable GetItemMasters()
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetItemMasters", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
    }
}
