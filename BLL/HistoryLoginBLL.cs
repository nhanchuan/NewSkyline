using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;

namespace BLL
{
    public class HistoryLoginBLL
    {
        DataServices dt = new DataServices();
        public List<HistoryLogin> getAllHistoryLogin()
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from HistoryLogin";
            DataTable tb = dt.DAtable(sql);
            List<HistoryLogin> lst = new List<HistoryLogin>();
            foreach (DataRow r in tb.Rows)
            {
                HistoryLogin hl = new HistoryLogin();
                hl.HistoryID = (long)r["HistoryID"];
                hl.UserID = (int)r["UserID"];
                hl.DateOfLogin = (DateTime)r["DateOfLogin"];
                hl.ClientIP = (string.IsNullOrEmpty(r["ClientIP"].ToString())) ? "" : (string)r["ClientIP"];
                lst.Add(hl);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public List<HistoryLogin> getTop100SortHistoryLoginDesc()
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select top 100 * from HistoryLogin order by DateOfLogin desc";
            DataTable tb = dt.DAtable(sql);
            List<HistoryLogin> lst = new List<HistoryLogin>();
            foreach (DataRow r in tb.Rows)
            {
                HistoryLogin hl = new HistoryLogin();
                hl.HistoryID = (long)r["HistoryID"];
                hl.UserID = (int)r["UserID"];
                hl.DateOfLogin = (DateTime)r["DateOfLogin"];
                hl.ClientIP = (string.IsNullOrEmpty(r["ClientIP"].ToString())) ? "" : (string)r["ClientIP"];
                lst.Add(hl);
            }
            this.dt.CloseConnection();
            return lst;
        }
        //getTbHistoryLogin
        public DataTable getTbHistoryLogin()
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "Exec getTbHistoryLogin";
            DataTable tb = dt.DAtable(sql);
            this.dt.CloseConnection();
            return tb;
        }
        //Create
        public Boolean NewHistoryLogin(int UserID, string ClientIP)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into HistoryLogin(UserID,ClientIP) values(@UserID,@ClientIP)";
            SqlParameter pUserID = new SqlParameter("@UserID", UserID);
            SqlParameter pClientIP = (ClientIP == "") ? new SqlParameter("@ClientIP", DBNull.Value) : new SqlParameter("@ClientIP", ClientIP);
            this.dt.Updatedata(sql, pUserID, pClientIP);
            this.dt.CloseConnection();
            return true;
        }
        //Clear History Login
        public Boolean ClearHistoryLogin()
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from HistoryLogin";
            this.dt.Updatedata(sql);
            this.dt.CloseConnection();
            return true;
        }
    }
}
