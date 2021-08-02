using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Converters;
using ToDoApp.DTO;

namespace ToDoApp.Model
{ 
    // Data Access Layer
    class DAL
    {
        static private SqlConnection conn = null;
        //private SqlCommand cmd = null;

        public DAL()
        {
            conn = new SqlConnection(Properties.Settings.Default.connStr);
        }
        public void OpenConnector()
        {
            try
            {
                if (conn.State == ConnectionState.Closed) conn.Open();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void CloseConnector()
        {
            try
            {
                if (conn.State == ConnectionState.Open) conn.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static SqlCommand newSqlCommand(string strSql)
        {
            return new SqlCommand(strSql, conn);
        }
        public DataTable getDataTableQuery(SqlCommand command)
        {
            SqlDataReader dr = getDataQuery(command);
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
        public SqlDataReader getDataQuery(SqlCommand command)
        {
            SqlDataReader dr = null;
            try
            {
                this.OpenConnector();
                dr = command.ExecuteReader();
                return dr;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return dr;
        }
    }
}
