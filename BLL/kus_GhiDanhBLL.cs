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
    public class kus_GhiDanhBLL
    {
        DataServices DB = new DataServices();
        public List<kus_GhiDanh> LstCheckHV_GhiDanh(int HocVienID, int KhoaHoc)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_GhiDanh where HocVienID=@HocVienID and KhoaHoc=@KhoaHoc";
            SqlParameter pHocVienID = new SqlParameter("HocVienID", HocVienID);
            SqlParameter pKhoaHoc = new SqlParameter("KhoaHoc", KhoaHoc);
            DataTable tb = DB.DAtable(sql, pHocVienID, pKhoaHoc);
            List<kus_GhiDanh> lst = new List<kus_GhiDanh>();
            foreach (DataRow r in tb.Rows)
            {
                kus_GhiDanh gd = new kus_GhiDanh();
                gd.GhiDanhID = (int)r["GhiDanhID"];
                gd.HocVienID = (string.IsNullOrEmpty(r["HocVienID"].ToString())) ? 0 : (int)r["HocVienID"];
                gd.KhoaHoc = (string.IsNullOrEmpty(r["KhoaHoc"].ToString())) ? 0 : (int)r["KhoaHoc"];
                gd.NVGhiDanh = (string.IsNullOrEmpty(r["NVGhiDanh"].ToString())) ? 0 : (int)r["NVGhiDanh"];
                gd.GhiChu = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                gd.NgayDangKy = (DateTime)r["NgayDangKy"];
                gd.DatCoc = (string.IsNullOrEmpty(r["DatCoc"].ToString())) ? 0 : (int)r["DatCoc"];
                gd.GhiDanhCode = (string)r["GhiDanhCode"];
                gd.RemainFee = (string.IsNullOrEmpty(r["RemainFee"].ToString())) ? 0 : (int)r["RemainFee"];
                lst.Add(gd);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<kus_GhiDanh> getListGDWithHocVien(int HocVienID)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_GhiDanh where HocVienID=@HocVienID";
            SqlParameter pHocVienID = new SqlParameter("HocVienID", HocVienID);
            DataTable tb = DB.DAtable(sql, pHocVienID);
            List<kus_GhiDanh> lst = new List<kus_GhiDanh>();
            foreach (DataRow r in tb.Rows)
            {
                kus_GhiDanh gd = new kus_GhiDanh();
                gd.GhiDanhID = (int)r["GhiDanhID"];
                gd.HocVienID = (string.IsNullOrEmpty(r["HocVienID"].ToString())) ? 0 : (int)r["HocVienID"];
                gd.KhoaHoc = (string.IsNullOrEmpty(r["KhoaHoc"].ToString())) ? 0 : (int)r["KhoaHoc"];
                gd.NVGhiDanh = (string.IsNullOrEmpty(r["NVGhiDanh"].ToString())) ? 0 : (int)r["NVGhiDanh"];
                gd.GhiChu = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                gd.NgayDangKy = (DateTime)r["NgayDangKy"];
                gd.DatCoc = (string.IsNullOrEmpty(r["DatCoc"].ToString())) ? 0 : (int)r["DatCoc"];
                gd.GhiDanhCode = (string)r["GhiDanhCode"];
                gd.RemainFee = (string.IsNullOrEmpty(r["RemainFee"].ToString())) ? 0 : (int)r["RemainFee"];
                lst.Add(gd);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<kus_GhiDanh> getListGDCode(string GhiDanhCode)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_GhiDanh where GhiDanhCode=@GhiDanhCode";
            SqlParameter pGhiDanhCode = new SqlParameter("@GhiDanhCode", GhiDanhCode);
            DataTable tb = DB.DAtable(sql, pGhiDanhCode);
            List<kus_GhiDanh> lst = new List<kus_GhiDanh>();
            foreach (DataRow r in tb.Rows)
            {
                kus_GhiDanh gd = new kus_GhiDanh();
                gd.GhiDanhID = (int)r["GhiDanhID"];
                gd.HocVienID = (string.IsNullOrEmpty(r["HocVienID"].ToString())) ? 0 : (int)r["HocVienID"];
                gd.KhoaHoc = (string.IsNullOrEmpty(r["KhoaHoc"].ToString())) ? 0 : (int)r["KhoaHoc"];
                gd.NVGhiDanh = (string.IsNullOrEmpty(r["NVGhiDanh"].ToString())) ? 0 : (int)r["NVGhiDanh"];
                gd.GhiChu = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                gd.NgayDangKy = (DateTime)r["NgayDangKy"];
                gd.DatCoc = (string.IsNullOrEmpty(r["DatCoc"].ToString())) ? 0 : (int)r["DatCoc"];
                gd.GhiDanhCode = (string)r["GhiDanhCode"];
                gd.RemainFee = (string.IsNullOrEmpty(r["RemainFee"].ToString())) ? 0 : (int)r["RemainFee"];
                lst.Add(gd);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public Boolean GhiDanhMoi(int HocVienID, int KhoaHoc, int NVGhiDanh, string GhiChu, int DatCoc, int RemainFee)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "Exec kus_GhiDanhMoi @HocVienID,@KhoaHoc,@NVGhiDanh,@GhiChu,@DatCoc,@RemainFee";
            SqlParameter pHocVienID = (HocVienID == 0) ? new SqlParameter("@HocVienID", DBNull.Value) : new SqlParameter("@HocVienID", HocVienID);
            SqlParameter pKhoaHoc = (KhoaHoc == 0) ? new SqlParameter("@KhoaHoc", DBNull.Value) : new SqlParameter("@KhoaHoc", KhoaHoc);
            SqlParameter pNVGhiDanh = (NVGhiDanh == 0) ? new SqlParameter("@NVGhiDanh", DBNull.Value) : new SqlParameter("@NVGhiDanh", NVGhiDanh);
            SqlParameter pGhiChu = new SqlParameter("@GhiChu", GhiChu);
            SqlParameter pDatCoc = (DatCoc == 0) ? new SqlParameter("@DatCoc", DBNull.Value) : new SqlParameter("@DatCoc", DatCoc);
            SqlParameter pRemainFee = new SqlParameter("@RemainFee", RemainFee);
            this.DB.Updatedata(sql, pHocVienID, pKhoaHoc, pNVGhiDanh, pGhiChu, pDatCoc, pRemainFee);
            this.DB.CloseConnection();
            return true;
        }
        //Table Tao DiemDanh
        public DataTable TbGhiDanhWithKhoaHoc(int KhoaHoc)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select ghidanh.GhiDanhID,ghidanh.HocVienID from kus_GhiDanh ghidanh full outer join kus_HocVien hocvien on ghidanh.HocVienID=hocvien.HocVienID full outer join CustomerBasicInfo info on hocvien.InfoID=info.InfoID";
            sql += " ";
            sql += "where ghidanh.GhiDanhID is not null and ghidanh.KhoaHoc=@KhoaHoc order by info.FirstName asc";
            SqlParameter pKhoaHoc = new SqlParameter("@KhoaHoc", KhoaHoc);
            DataTable tb = DB.DAtable(sql, pKhoaHoc);
            this.DB.CloseConnection();
            return tb;
        }
        //===================================================================================================================
        public DataTable kus_getHVGhiDanh(int PageIndex, int PageSize, DateTime Startdate, DateTime Enddate)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec kus_getHVGhiDanh @PageIndex,@PageSize,@Startdate,@Enddate";
            SqlParameter pPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter pPageSize = new SqlParameter("PageSize", PageSize);
            SqlParameter pStartdate = new SqlParameter("Startdate", Startdate);
            SqlParameter pEnddate = new SqlParameter("Enddate", Enddate);
            DataTable tb = DB.DAtable(sql, pPageIndex, pPageSize, pStartdate, pEnddate);
            this.DB.CloseConnection();
            return tb;
        }
        public int Countkus_getHVGhiDanh(DateTime Startdate, DateTime Enddate)
        {
            int dem = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(hocvien.HocVienID) from kus_HocVien hocvien full outer join kus_GhiDanh ghidanh on hocvien.HocVienID=ghidanh.HocVienID where hocvien.HocVienID is not null and ghidanh.NgayDangKy between @Startdate and @Enddate";
            SqlParameter pStartdate = new SqlParameter("Startdate", Startdate);
            SqlParameter pEnddate = new SqlParameter("Enddate", Enddate);
            dem = DB.GetValues(sql, pStartdate, pEnddate);
            this.DB.CloseConnection();
            return dem;
        }
        //==================================================================================================================
        public DataTable kus_getHVGhiDanhInCoSo(int PageIndex, int PageSize, DateTime Startdate, DateTime Enddate, int CoSoID)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec kus_getHVGhiDanhInCoSo @PageIndex,@PageSize,@Startdate,@Enddate,@CoSoID";
            SqlParameter pPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter pPageSize = new SqlParameter("PageSize", PageSize);
            SqlParameter pStartdate = new SqlParameter("Startdate", Startdate);
            SqlParameter pEnddate = new SqlParameter("Enddate", Enddate);
            SqlParameter pCoSoID = new SqlParameter("CoSoID", CoSoID);
            DataTable tb = DB.DAtable(sql, pPageIndex, pPageSize, pStartdate, pEnddate, pCoSoID);
            this.DB.CloseConnection();
            return tb;
        }
        public int CountgetHVGhiDanhInCoSo(DateTime Startdate, DateTime Enddate, int CoSoID)
        {
            int dem = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(hocvien.HocVienID) from kus_HocVien hocvien full outer join kus_GhiDanh ghidanh on hocvien.HocVienID=ghidanh.HocVienID full outer join nc_KhoaHoc khoahoc on ghidanh.KhoaHoc=khoahoc.ID where hocvien.HocVienID is not null and ghidanh.NgayDangKy between @Startdate and @Enddate and khoahoc.CoSoID=@CoSoID";
            SqlParameter pStartdate = new SqlParameter("Startdate", Startdate);
            SqlParameter pEnddate = new SqlParameter("Enddate", Enddate);
            SqlParameter pCoSoID = new SqlParameter("CoSoID", CoSoID);
            dem = DB.GetValues(sql, pStartdate, pEnddate, pCoSoID);
            this.DB.CloseConnection();
            return dem;
        }
        //=====================================================================================================================
        //====================kus_getHVGhiDanhInLopHoc==============================================================================================
        public DataTable kus_getHVGhiDanhInKhoaHoc(int PageIndex, int PageSize, int KhoaHoc)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec kus_getHVGhiDanhInKhoaHoc @PageIndex,@PageSize,@KhoaHoc";
            SqlParameter pPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter pPageSize = new SqlParameter("PageSize", PageSize);
            SqlParameter pKhoaHoc = new SqlParameter("KhoaHoc", KhoaHoc);
            DataTable tb = DB.DAtable(sql, pPageIndex, pPageSize, pKhoaHoc);
            this.DB.CloseConnection();
            return tb;
        }
        public int CountgetHVGhiDanhInKhoaHoc(int KhoaHoc)
        {
            int dem = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(hocvien.HocVienID) from kus_HocVien hocvien full outer join kus_GhiDanh ghidanh on hocvien.HocVienID=ghidanh.HocVienID full outer join nc_KhoaHoc khoahoc on ghidanh.KhoaHoc=khoahoc.ID where hocvien.HocVienID is not null and khoahoc.ID=@KhoaHoc";
            SqlParameter pKhoaHoc = new SqlParameter("@KhoaHoc", KhoaHoc);
            dem = DB.GetValues(sql, pKhoaHoc);
            this.DB.CloseConnection();
            return dem;
        }
        //=====================================================================================================================
        public DataTable kus_getTTPhieuGhiDanh(string GDCode)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec kus_getTTPhieuGhiDanh @GDCode";
            SqlParameter pGDCode = new SqlParameter("GDCode", GDCode);
            DataTable tb = DB.DAtable(sql, pGDCode);
            this.DB.CloseConnection();
            return tb;
        }
        //=======Reset So Tien Dat Coc============================================================================================================
        public Boolean ResetDatcoc(int GhiDanhID)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "update kus_GhiDanh set DatCoc=@DatCoc where GhiDanhID=@GhiDanhID";
            SqlParameter pDatCoc = new SqlParameter("@DatCoc", DBNull.Value);
            SqlParameter pGhiDanhID = new SqlParameter("@GhiDanhID", GhiDanhID);
            this.DB.Updatedata(sql, pDatCoc, pGhiDanhID);
            this.DB.CloseConnection();
            return true;
        }
        //===========Update ghi chu=======================================
        public Boolean UpdateGhichu(int HocVienID, string GhiChu)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "update kus_GhiDanh set GhiChu=@GhiChu where HocVienID=@HocVienID";
            SqlParameter pHocVienID = new SqlParameter("@HocVienID", HocVienID);
            SqlParameter pGhiChu = (GhiChu == "") ? new SqlParameter("@GhiChu", DBNull.Value) : new SqlParameter("@GhiChu", GhiChu);
            this.DB.Updatedata(sql, pHocVienID, pGhiChu);
            this.DB.CloseConnection();
            return true;
        }

        /// <summary>
        /// Tính tổng số Học Viên Ghi Danh tại Trung Tâm
        /// </summary>
        /// <returns>Interger</returns>
        public int CountHocVien()
        {
            int count = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(*) from kus_GhiDanh";
            count = DB.GetValues(sql);
            this.DB.CloseConnection();
            return count;

        }
        // Get tablr ghi danh 
        public DataTable TBGhiDanhByHocVienID(int HocVienID)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select ghidanh.*,khoahoc.MaKhoaHoc,khoahoc.TenKhoaHoc,khoahoc.NgayKhaiGiang,khoahoc.NgayKetThuc,lophoc.LopHocCode,lophoc.TenLopHoc";
            sql += " ";
            sql += "from kus_GhiDanh ghidanh full outer join kus_HocVien hocvien on ghidanh.HocVienID=hocvien.HocVienID full outer join nc_KhoaHoc khoahoc on ghidanh.KhoaHoc=khoahoc.ID";
            sql += " ";
            sql += "full outer join nc_LopHoc lophoc on khoahoc.LopHoc=lophoc.ID full outer join Employees emp on ghidanh.NVGhiDanh=emp.EmployeesID where ghidanh.GhiDanhID is not null and ghidanh.HocVienID=@HocVienID order by ghidanh.NgayDangKy desc";
            SqlParameter pHocVienID = new SqlParameter("@HocVienID", HocVienID);
            DataTable tb = DB.DAtable(sql, pHocVienID);
            this.DB.CloseConnection();
            return tb;
        }
        //Update RemainFee
        public Boolean UpdateRemainFee(int GhiDanhID, int RemainFee)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "update kus_GhiDanh set RemainFee=@RemainFee where GhiDanhID=@GhiDanhID";
            SqlParameter pGhiDanhID = new SqlParameter("@GhiDanhID", GhiDanhID);
            SqlParameter pRemainFee = new SqlParameter("@RemainFee", RemainFee);
            this.DB.Updatedata(sql, pGhiDanhID, pRemainFee);
            this.DB.CloseConnection();
            return true;
        }
    }
}
