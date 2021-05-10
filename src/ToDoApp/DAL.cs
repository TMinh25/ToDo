﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Converters;
using ToDoApp.Models;

namespace ToDoApp.Controls
{
    class DAL // Data Access Layer
    {
        static private SqlConnection conn = null;
        private SqlCommand cmd = null;

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
        public SqlDataReader getDataReader(string tablename)
        {
            string strSQL = "SELECT * FROM [dbo].[" + tablename + "]";
            cmd = new SqlCommand(strSQL, conn);
            this.OpenConnector();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
        }
        public DataTable getDataTable(string tablename)
        {
            SqlDataReader dr = getDataReader(tablename);
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
        
        public DataTable getDataTableQuery(SqlCommand command)
        {
            SqlDataReader dr = getDataQuery(command);
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
        public DataTable getDataTableQuery(string strSql)
        {
            SqlDataReader dr = getDataQuery(strSql);
            DataTable dt = new DataTable();
            dt.Load(dr);
            return dt;
        }
        public SqlDataReader getDataQuery(string strSql)
        {
            cmd = new SqlCommand(strSql, conn);
            this.OpenConnector();
            SqlDataReader dr = cmd.ExecuteReader();
            return dr;
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