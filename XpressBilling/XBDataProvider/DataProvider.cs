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
    internal static class DataProvider
    {
        /// <summary>
        /// Gets a SQL Connection by getting the connection string using a 
        /// connection string obtained from the configuration manager.
        /// </summary>
        /// <returns>An Open SQLConnection Object.</returns>
        internal static SqlConnection GetSqlConnection()
        {
            string connString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection conn = new SqlConnection(connString);

            conn.Open();

            return conn;
        }

        /// <summary>
        /// Execute a query
        /// </summary>
        /// <param name="sqlCmd">SQLCommand Object containing all the required SQLParameters.</param>
        /// <param name="conString">String value containing the connection string information.</param>
        ///  <param name="ProcedureName">string value contaning the Stored procedure name</param>
        /// <returns>Disconnected DataTable Object</returns>
        internal static void ExecuteQuery(string connString, SqlCommand cmd, string procedureName)
        {
            try
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = procedureName;
                cmd.Connection = GetSqlConnection();
                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                throw ex;
            }
            finally
            {
                if (cmd.Connection.State == ConnectionState.Open)
                {
                    cmd.Connection.Close();
                }
            }
        } 

        /// <summary>
        /// Executes a Stored Procedure using a SQLCmd Object.
        /// </summary>
        /// <param name="connString">String value containing the connection string information.</param>
        /// <param name="procedureName">String containing the name of the stored procedure that will be executed.</param>
        /// <param name="sqlCmd">SQLCommand Object containing all the required SQLParameters.</param>
        /// <returns>An Int32 value indicating the result of the query execution.</returns>
        internal static Int32 ExecuteSqlProcedure(string connString, string procedureName, SqlCommand sqlCmd)
        {
            int returnValue = 0;

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                returnValue = sqlCmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Save the Exception Code so it can be returned.
                //returnValue = ex.ErrorCode;
                returnValue= 0;
                // Rethrow the exception.
                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {
                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();

                }
            }

            return returnValue;
        }

        /// <summary>
        /// Returns a boolean scalar value from a Stored Procedure using a SQLCommand Object.
        /// </summary>
        /// <param name="connString">String value containing the connection string information.</param>
        /// <param name="procedureName">String containing the name of the stored procedure that will be executed.</param>
        /// <param name="sqlCmd">SQLCommand Object containing all the required SQLParameters.</param>
        /// <returns>Boolean value</returns>
        internal static bool ExecuteScalarBoolean(string connString, string procedureName, SqlCommand sqlCmd)
        {
            bool returnValue = false;

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                returnValue = (bool)sqlCmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                // Rethrow the exception.
                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {

                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }

            return returnValue;
        }

        /// <summary>
        /// Returns a string scalar value from a Stored Procedure using a SQLCommand Object.
        /// </summary>
        /// <param name="connString">String value containing the connection string information.</param>
        /// <param name="procedureName">String containing the name of the stored procedure that will be executed.</param>
        /// <param name="sqlCmd">SQLCommand Object containing all the required SQLParameters.</param>
        /// <returns>String value</returns>
        internal static string ExecuteScalarString(string connString, string procedureName, SqlCommand sqlCmd)
        {
            string returnValue = null;

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                returnValue = (string)sqlCmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                // Rethrow the exception.
                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {

                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }            
            return returnValue;
        }

        internal static double ExecuteScalarDouble(string connString, string procedureName, SqlCommand sqlCmd)
        {
            double returnValue = 0;

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                returnValue = (double)sqlCmd.ExecuteScalar();
            }
            catch (SqlException ex)
            {
                // Rethrow the exception.
                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {

                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }
            return returnValue;
        }

        internal static int ExecuteScalarInt(string connString, string procedureName, SqlCommand sqlCmd)
        {
            int returnValue = 0;

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                returnValue = Convert.ToInt32(sqlCmd.ExecuteScalar());
            }
             catch (SqlException ex)
            {
                // Rethrow the exception.
                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {

                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }

            return returnValue;
        }
        /// <summary>
        /// Gets a disconnected DataTable Object from a Stored Procedure using a SQLCommand Object.
        /// </summary>
        /// <param name="connString">String value containing the connection string information.</param>
        /// <param name="procedureName">String containing the name of the stored procedure that will be executed.</param>
        /// <param name="sqlCmd">SQLCommand Object containing all the required SQLParameters.</param>
        /// <returns>Disconnected DataTable Object</returns>
        internal static DataTable GetSQLDataTable(string connString, string procedureName, SqlCommand sqlCmd)
        {
            DataTable dt = new DataTable();

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                // Rethrow the exception.

                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {
                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }

            return dt;
        }

        internal static DataTable GetSQLDataTable(string connString, string procedureName)
        {
            DataTable dt = new DataTable();
            SqlCommand sqlCmd = new SqlCommand();
            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(dt);
            }
            catch (SqlException ex)
            {
                // Rethrow the exception.

                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {
                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }

            return dt;
        }
        /// <summary>
        /// Gets a disconnected DataSet Object from a Stored Procedure using a SQLCommand Object.
        /// </summary>
        /// <param name="connString">String value containing the connection string information.</param>
        /// <param name="procedureName">String containing the name of the stored procedure that will be executed.</param>
        /// <param name="sqlCmd">SQLCommand Object containing all the required SQLParameters.</param>
        /// <returns>Disconnected DataSet Object</returns>
        internal static DataSet GetSQLDataSet(string connString, string procedureName, SqlCommand sqlCmd)
        {
            DataSet ds = new DataSet();

            try
            {
                sqlCmd.CommandType = CommandType.StoredProcedure;
                sqlCmd.CommandText = procedureName;
                sqlCmd.Connection = GetSqlConnection();

                SqlDataAdapter da = new SqlDataAdapter(sqlCmd);
                da.Fill(ds);
            }
            catch (SqlException ex)
            {
                // Rethrow the exception.

                throw ex;
            }
            finally
            {
                if (sqlCmd.Connection.State == ConnectionState.Open)
                {
                    sqlCmd.Connection.Dispose();
                    //sqlCmd.Connection.Close();
                }
            }

            return ds;
        }
    }
}
