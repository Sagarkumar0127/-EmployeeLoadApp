using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace FinpulseDataLoadModule.Data
{
    public abstract class BaseData
    {
         
        static string ConnectionString = ConfigurationManager.ConnectionStrings["FPLoadConnectionString"].ConnectionString;
        const int CommandTimeout = 3600;
        SqlConnection con;



        protected void ExecuteSQLStatement(string statement, List<SqlParameter> param)
        {
            try
            {
                con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = statement;
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.CommandTimeout = CommandTimeout;
                cmd.Connection = con;
                cmd.Parameters.AddRange(param.ToArray());
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected void ExecuteSqlStatement(string statement)
        {
            try
            {
                con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = statement;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandTimeout = CommandTimeout;
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        protected void ExecuteSQLSP(string spName, List<SqlParameter> param)
        {
            try
            {
                con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = spName;
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.CommandTimeout = CommandTimeout;
                cmd.Parameters.AddRange(param.ToArray());
                cmd.Connection = con;
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        protected DataTable GetDataTableFromQuery(string spName, List<SqlParameter> param)
        {
           return _getDataTable(CommandType.Text, spName, param);
        }


        protected DataTable GetDataTableFromSP(string spName, List<SqlParameter> param)
        {
           return _getDataTable(CommandType.StoredProcedure, spName, param);
        }

        private DataTable _getDataTable(CommandType cmdType, string cmdText, List<SqlParameter> param)
        {
 
            try
            {
                con = new SqlConnection(ConnectionString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType =  cmdType;
                cmd.CommandTimeout = CommandTimeout;
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(param.ToArray());
                cmd.Connection = con;
                DataTable dt = new DataTable ();
                SqlDataAdapter da = new SqlDataAdapter (cmd);
                da.Fill(dt);
                return dt;
               
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       

       
            

    }
}
