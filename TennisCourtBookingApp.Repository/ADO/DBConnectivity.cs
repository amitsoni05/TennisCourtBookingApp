using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;

namespace TennisCourtBookingApp.Repository.ADO
{
    public class DBConnectivity
    {
        public string conStr = "Data Source=172.16.0.241;Initial Catalog=TestSPC5;User ID=traininguser; Password=admin123;";
        public SqlConnection con;
        public SqlDataAdapter Da;
        public SqlCommand Cmd;
        public SqlCommandBuilder Cb;
        public DBConnectivity()
        {
        }
        public void OpenConnection()
        {
            try
            {
                con = new SqlConnection();
                con.ConnectionString = conStr;
                if ((con.State == ConnectionState.Open)) con.Close();
                con.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void CloseConnection()
        {
            try
            {
                if (con.State == ConnectionState.Open) con.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SetdatdFromSp(List<StoredProcModel> parm, string CommandText)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (parm.Count > 0)
                {
                    foreach (var item in parm)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                OpenConnection();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = CommandText;
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public DataTable GetDataFromSP(List<StoredProcModel> parm, string CommandText, ref int totalRecords)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (parm.Count > 0)
                {
                    foreach (var item in parm)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                DataTable dt = new DataTable();
                OpenConnection();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = CommandText;
                Da = new SqlDataAdapter(cmd);
                Da.Fill(ds);
                if (ds.Tables.Count > 1)
                    totalRecords = Convert.ToInt32(ds.Tables[1].Rows[0][0].ToString());
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Da.Dispose();
                CloseConnection();
                cmd.Dispose();
            }
        }
        public DataTable GetDataFromSP(List<StoredProcModel> parm, string CommandText)
        {
            SqlCommand cmd = new SqlCommand();
            try
            {
                if (parm.Count > 0)
                {
                    foreach (var item in parm)
                    {
                        cmd.Parameters.AddWithValue(item.Key, item.Value);
                    }
                }
                DataSet ds = new DataSet();
                DataSet ds2 = new DataSet();
                DataTable dt = new DataTable();
                OpenConnection();
                cmd.Connection = con;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = CommandText;
                Da = new SqlDataAdapter(cmd);
                Da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                Da.Dispose();
                CloseConnection();
                cmd.Dispose();
            }
        }
    }
}
