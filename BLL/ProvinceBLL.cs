﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class ProvinceBLL
    {
        DataServices DB = new DataServices();
        public List<Province> getAllProvince()
        {
            string sql = "select * from Province";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            DataTable tb = DB.DAtable(sql);
            List<Province> lst = new List<Province>();
            foreach (DataRow r in tb.Rows)
            {
                Province p = new Province();
                p.ProvinceID = (int)r["ProvinceID"];
                p.ProvinceName = (string.IsNullOrEmpty(r["ProvinceName"].ToString())) ? "" : (string)r["ProvinceName"];
                p.CountryID = (string.IsNullOrEmpty(r["CountryID"].ToString())) ? 0 : (int)r["CountryID"];
                lst.Add(p);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<Province> getProvinceWithCid(int cid)
        {
            string sql = "select * from Province where CountryID=@cid";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter paramcid = new SqlParameter("cid", cid);
            DataTable tb = DB.DAtable(sql, paramcid);
            List<Province> lst = new List<Province>();
            foreach (DataRow r in tb.Rows)
            {
                Province p = new Province();
                p.ProvinceID = (int)r["ProvinceID"];
                p.ProvinceName = (string.IsNullOrEmpty(r["ProvinceName"].ToString())) ? "" : (string)r["ProvinceName"];
                p.CountryID = (string.IsNullOrEmpty(r["CountryID"].ToString())) ? 0 : (int)r["CountryID"];
                lst.Add(p);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<Province> getProvinceWithProvinceName(string ProvinceName,int CountryID)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from Province where ProvinceName=@ProvinceName and CountryID=@CountryID";
            SqlParameter pProvinceName = new SqlParameter("@ProvinceName", ProvinceName);
            SqlParameter pCountryID = new SqlParameter("@CountryID", CountryID);
            DataTable tb = DB.DAtable(sql, pProvinceName, pCountryID);
            List<Province> lst = new List<Province>();
            foreach (DataRow r in tb.Rows)
            {
                Province p = new Province();
                p.ProvinceID = (int)r["ProvinceID"];
                p.ProvinceName = (string.IsNullOrEmpty(r["ProvinceName"].ToString())) ? "" : (string)r["ProvinceName"];
                p.CountryID = (string.IsNullOrEmpty(r["CountryID"].ToString())) ? 0 : (int)r["CountryID"];
                lst.Add(p);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<Province> getProvinceWithProId(int proid)
        {
            string sql = "select * from Province where ProvinceID=@proid";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter pproid = new SqlParameter("proid", proid);
            DataTable tb = DB.DAtable(sql, pproid);
            List<Province> lst = new List<Province>();
            foreach (DataRow r in tb.Rows)
            {
                Province p = new Province();
                p.ProvinceID = (int)r["ProvinceID"];
                p.ProvinceName = (string.IsNullOrEmpty(r["ProvinceName"].ToString())) ? "" : (string)r["ProvinceName"];
                p.CountryID = (string.IsNullOrEmpty(r["CountryID"].ToString())) ? 0 : (int)r["CountryID"];
                lst.Add(p);
            }
            this.DB.CloseConnection();
            return lst;
        }
        //New
        public Boolean NewProvince(string ProvinceName, int CountryID)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "insert into Province(ProvinceName,CountryID) values (@ProvinceName,@CountryID)";
            SqlParameter pProvinceName = new SqlParameter("@ProvinceName", ProvinceName);
            SqlParameter pCountryID = new SqlParameter("@CountryID", CountryID);
            this.DB.Updatedata(sql, pProvinceName, pCountryID);
            this.DB.CloseConnection();
            return true;
        }
    }
}
