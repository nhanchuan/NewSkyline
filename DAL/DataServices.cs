﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace DAL
{
    public class DataServices
    {
        string strconn = ConfigurationManager.ConnectionStrings["connectionStrCon"].ToString();
        private SqlConnection m_conn;


        /// <summary>
        /// Thuộc tính kết nối
        /// </summary>
        public SqlConnection Conn
        {
            get { return m_conn; }
            set { m_conn = value; }
        }

        /// <summary>
        /// 
        /// </summary>
        public DataServices()
        {
            this.Conn = new SqlConnection(strconn);
        }

        
        /// <summary>
        /// Hàm mở kết nối với cơ sở dữ liệu
        /// </summary>
        /// <returns></returns>
        public Boolean OpenConnection()
        {
            try
            {
                if (this.Conn.State == ConnectionState.Closed)
                {
                    this.Conn.Open();
                }
                return true;
            }
            catch (Exception)
            { }
            return false;
        }

        
        /// <summary>
        /// Hàm đóng kết nối với cơ sở dữ liệu
        /// </summary>
        public void CloseConnection()
        {
            this.Conn.Close();
        }

        /// <summary>
        /// Hàm lấy dữ liệu trả về Bảng
        /// </summary>
        /// <param name="strsql">Câu lệnh sql</param>
        /// <param name="thamao">Tham số parameter</param>
        /// <returns></returns>
        public DataTable DAtable(string strsql, params SqlParameter[] thamao)
        {
            try
            {
                DataTable table = new DataTable();
                SqlCommand cmd = new SqlCommand(strsql, this.Conn);
                cmd.Parameters.AddRange(thamao);
                table.Load(cmd.ExecuteReader());
                return table;
            }
            catch (Exception)
            { }
            return null;
        }
        public void Updatedata(string strsql, params SqlParameter[] thamaso)
        {
            try
            {
                SqlCommand cmd = new SqlCommand(strsql, this.Conn);
                cmd.Parameters.AddRange(thamaso);
                cmd.ExecuteNonQuery();
            }
            catch (Exception)
            { }
        }

        public int GetValues(string strsql, params SqlParameter[] param)
        {
            int val = 0;
            try
            {
                SqlCommand cmd = new SqlCommand(strsql, this.Conn);
                cmd.Parameters.AddRange(param);
                val = (int)cmd.ExecuteScalar();

            }
            catch (Exception)
            { }
            return val;
        }
        public DateTime GetDateValues(string strsql, params SqlParameter[] param)
        {
            DateTime val = new DateTime(1900, 01, 01);
            try
            {
                SqlCommand cmd = new SqlCommand(strsql, this.Conn);
                cmd.Parameters.AddRange(param);
                val = (DateTime)cmd.ExecuteScalar();

            }
            catch (Exception)
            { }
            return val;
        }
        public string GetStringValues(string strsql, params SqlParameter[] param)
        {
            string val = "";
            try
            {
                SqlCommand cmd = new SqlCommand(strsql, this.Conn);
                cmd.Parameters.AddRange(param);
                val = (string)cmd.ExecuteScalar();

            }
            catch (Exception)
            { }
            return val;
        }
    }
}
