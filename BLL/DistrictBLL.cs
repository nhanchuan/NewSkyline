using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class DistrictBLL
    {
        DataServices DB = new DataServices();
        public List<District> getallDistrict()
        {
            string sql = "select * from District";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            DataTable tb = DB.DAtable(sql);
            List<District> lst = new List<District>();
            foreach (DataRow r in tb.Rows)
            {
                District d = new District();
                d.DistrictID = (int)r["DistrictID"];
                d.DistrictName = (string.IsNullOrEmpty(r["DistrictName"].ToString())) ? "" : (string)r["DistrictName"];
                d.ProvinceID = (string.IsNullOrEmpty(r["ProvinceID"].ToString())) ? 0 : (int)r["ProvinceID"];
                lst.Add(d);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<District> getDistrictwithProid(int proid)
        {
            string sql = "select * from District where ProvinceID=@proid";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter paramproid = new SqlParameter("proid", proid);
            DataTable tb = DB.DAtable(sql, paramproid);
            List<District> lst = new List<District>();
            foreach (DataRow r in tb.Rows)
            {
                District d = new District();
                d.DistrictID = (int)r["DistrictID"];
                d.DistrictName = (string.IsNullOrEmpty(r["DistrictName"].ToString())) ? "" : (string)r["DistrictName"];
                d.ProvinceID = (string.IsNullOrEmpty(r["ProvinceID"].ToString())) ? 0 : (int)r["ProvinceID"];
                lst.Add(d);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<District> getDistrictwithDisId(int disId)
        {
            string sql = "select * from District where DistrictID=@disId";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter pdisId = new SqlParameter("disId", disId);
            DataTable tb = DB.DAtable(sql, pdisId);
            List<District> lst = new List<District>();
            foreach (DataRow r in tb.Rows)
            {
                District d = new District();
                d.DistrictID = (int)r["DistrictID"];
                d.DistrictName = (string.IsNullOrEmpty(r["DistrictName"].ToString())) ? "" : (string)r["DistrictName"];
                d.ProvinceID = (string.IsNullOrEmpty(r["ProvinceID"].ToString())) ? 0 : (int)r["ProvinceID"];
                lst.Add(d);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<District> getLstwithNameProID(string DistrictName, int ProvinceID)
        {
            
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from District where DistrictName=@DistrictName and ProvinceID=@ProvinceID";
            SqlParameter pDistrictName = new SqlParameter("@DistrictName", DistrictName);
            SqlParameter pProvinceID = new SqlParameter("@ProvinceID", ProvinceID);
            DataTable tb = DB.DAtable(sql, pDistrictName, pProvinceID);
            List<District> lst = new List<District>();
            foreach (DataRow r in tb.Rows)
            {
                District d = new District();
                d.DistrictID = (int)r["DistrictID"];
                d.DistrictName = (string.IsNullOrEmpty(r["DistrictName"].ToString())) ? "" : (string)r["DistrictName"];
                d.ProvinceID = (string.IsNullOrEmpty(r["ProvinceID"].ToString())) ? 0 : (int)r["ProvinceID"];
                lst.Add(d);
            }
            this.DB.CloseConnection();
            return lst;
        }
        //New
        public Boolean NewDistrict(string DistrictName, int ProvinceID)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "insert into District(DistrictName,ProvinceID) values(@DistrictName,@ProvinceID)";
            SqlParameter pDistrictName = new SqlParameter("@DistrictName", DistrictName);
            SqlParameter pProvinceID = new SqlParameter("@ProvinceID", ProvinceID);
            this.DB.Updatedata(sql, pDistrictName, pProvinceID);
            this.DB.CloseConnection();
            return true;
        }
    }
}
