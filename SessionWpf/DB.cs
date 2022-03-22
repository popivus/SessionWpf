using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Windows;

namespace SessionWpf
{
    public static class DB
    {
        private static string ConnectionString = @"Data Source=MANAGER55\MANAGER55;Initial Catalog=SessionWpf;User ID=sa;Password=5Dfg893ass";
        private static SqlConnection Connection;
        private static SqlCommand Cmd;
        private static SqlDataAdapter adapter;
        private static DataSet dataSet;

        public static DataSet FillDataSet(string query)
        {
            Connection = new SqlConnection(ConnectionString);
            dataSet = new DataSet();
            try
            {
                Connection.Open();
                adapter = new SqlDataAdapter(query, Connection);
                adapter.Fill(dataSet);
                return dataSet;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally 
            {
                Connection.Close();
            }
        }

        public static string CmdScalar(string query)
        {
            Connection = new SqlConnection(ConnectionString);
            try
            {
                Connection.Open();
                Cmd = new SqlCommand(query, Connection);
                Cmd.ExecuteNonQuery();
                if (!query.StartsWith("INSERT") && !query.StartsWith("DELETE") && !query.StartsWith("UPDATE")) return Cmd.ExecuteScalar().ToString();
                else return null;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }
            finally
            {
                Connection.Close();
            }
        }
    }
}
