﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;

namespace XBDataProvider
{
    public static class BaseUnit
    {
        public static int SaveBaseUnit(string companyCode, string BaseUnit, string name, string reference, string createdBy, DateTime createdDate, bool status)
        {
            try
            {
                int rtnvalue = -1;
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@CompanyCode", companyCode));
                cmd.Parameters.Add(new SqlParameter("@BaseUnitCode", BaseUnit));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@Reference", reference));
                cmd.Parameters.Add(new SqlParameter("@CreatedBY", createdBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", createdBy));
                cmd.Parameters.Add(new SqlParameter("@createdDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                //cmd.Parameters.Add(new SqlParameter("@returnvar", rtnvalue));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BaseUnit_xpins", cmd);
            }
            catch (Exception ex)
            {
                return 0;
            }

        }

        public static int UpdateBaseUnit(int id, string name, string updatedBy, bool status)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                //DataProvider dtProv = new DataProvider();
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                cmd.Parameters.Add(new SqlParameter("@Name", name));
                cmd.Parameters.Add(new SqlParameter("@UpdatedBy", updatedBy));
                cmd.Parameters.Add(new SqlParameter("@UpdatedDate", DateTime.Now.Date));
                cmd.Parameters.Add(new SqlParameter("@status", status));
                return DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BaseUnit_xpupd", cmd);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public static DataTable GetBaseUnits(string companyCode)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@companyCode", companyCode));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetBaseUnits", cmd);
               // dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_GetBaseUnits");
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }

        public static DataTable GetBaseUnitById(int Id)
        {
            DataTable dtTable = new DataTable();
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                dtTable = DataProvider.GetSQLDataTable(connString, "dbo.sp_BaseUnitGetById", cmd);
            }
            catch (Exception ex)
            {

            }

            return dtTable;
        }
        public static void ActivateBaseUnit(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BaseUnit_Activate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeActivateBaseUnit(int id)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@Id", id));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BaseUnit_DeActivate", cmd);

            }
            catch (Exception ex)
            {
            }

        }

        public static void DeleteBaseUnit(string ids)
        {
            try
            {
                string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlCommand cmd = new SqlCommand();
                cmd.Parameters.Add(new SqlParameter("@ids", ids));
                DataProvider.ExecuteSqlProcedure(connString, "dbo.sp_BaseUnitDelete", cmd);

            }
            catch (Exception ex)
            {
            }

        }
    }
}
