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
    public class kus_GhiDanhTiemNamgBLL
    {
        DataServices dt = new DataServices();

        public List<kus_GhiDanhTiemNamg> ListByHocVienIDandLopHoc(int HocVienID, int LopHoc)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_GhiDanhTiemNamg where HocVienID=@HocVienID and LopHoc=@LopHoc";
            SqlParameter pHocVienID = new SqlParameter("@HocVienID", HocVienID);
            SqlParameter pLopHoc = new SqlParameter("@LopHoc", LopHoc);

            DataTable tb = dt.DAtable(sql, pHocVienID, pLopHoc);
            List<kus_GhiDanhTiemNamg> lst = new List<kus_GhiDanhTiemNamg>();
            foreach (DataRow r in tb.Rows)
            {
                kus_GhiDanhTiemNamg gh = new kus_GhiDanhTiemNamg();
                gh.ID = (int)r["ID"];
                gh.HocVienID = (string.IsNullOrEmpty(r["HocVienID"].ToString())) ? 0 : (int)r["HocVienID"];
                gh.LopHoc = (string.IsNullOrEmpty(r["LopHoc"].ToString())) ? 0 : (int)r["LopHoc"];
                gh.NVGhiDanh = (string.IsNullOrEmpty(r["NVGhiDanh"].ToString())) ? 0 : (int)r["NVGhiDanh"];
                gh.NgayGD = (DateTime)r["NgayGD"];
                gh.GhiChu = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                gh.GDStatus = (string.IsNullOrEmpty(r["GDStatus"].ToString())) ? false : (Boolean)r["GDStatus"];
                lst.Add(gh);
            }
            this.dt.CloseConnection();
            return lst;
        }


        public Boolean Newkus_GhiDanhTiemNamg(int HocVienID, int LopHoc, int NVGhiDanh, string GhiChu, Boolean GDStatus)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into kus_GhiDanhTiemNamg(HocVienID,LopHoc,NVGhiDanh,GhiChu,GDStatus) values (@HocVienID,@LopHoc,@NVGhiDanh,@GhiChu,@GDStatus)";
            SqlParameter pHocVienID = (HocVienID == 0) ? new SqlParameter("@HocVienID", DBNull.Value) : new SqlParameter("@HocVienID", HocVienID);
            SqlParameter pLopHoc = (LopHoc == 0) ? new SqlParameter("@LopHoc", DBNull.Value) : new SqlParameter("@LopHoc", LopHoc);
            SqlParameter pNVGhiDanh = (NVGhiDanh == 0) ? new SqlParameter("@NVGhiDanh", DBNull.Value) : new SqlParameter("@NVGhiDanh", NVGhiDanh);
            SqlParameter pGhiChu = (GhiChu == "") ? new SqlParameter("@GhiChu", DBNull.Value) : new SqlParameter("@GhiChu", GhiChu);
            SqlParameter pGDStatus = new SqlParameter("@GDStatus", GDStatus);
            this.dt.Updatedata(sql, pHocVienID, pLopHoc, pNVGhiDanh, pGhiChu, pGDStatus);
            this.dt.CloseConnection();
            return true;
        }

        //===================================================================================================================
        public DataTable kus_getHVGhiDanhTiemNang(int PageIndex, int PageSize, int LopHoc)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "Exec kus_getHVGhiDanhTiemNang @PageIndex,@PageSize,@LopHoc";
            SqlParameter pPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter pPageSize = new SqlParameter("PageSize", PageSize);
            SqlParameter pLopHoc = new SqlParameter("@LopHoc", LopHoc);
            DataTable tb = dt.DAtable(sql, pPageIndex, pPageSize, pLopHoc);
            this.dt.CloseConnection();
            return tb;
        }
        public int Countkus_GhiDanhTiemNamg(int LopHoc)
        {
            int dem = 0;
            if (!this.dt.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(*) from kus_GhiDanhTiemNamg where LopHoc=@LopHoc";
            SqlParameter pLopHoc = new SqlParameter("@LopHoc", LopHoc);
            dem = dt.GetValues(sql);
            this.dt.CloseConnection();
            return dem;
        }
        //==================================================================================================================
        //ListGhi Danh Tiềm Năng By Lop Hoc ID
        public DataTable ListGhiDanhTiemNangByLopID()
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select *,[dbo].[CountSLGhiDanhTiemNangByID](ID) as SLGhiDanh from nc_LopHoc";
            DataTable tb = dt.DAtable(sql);
            this.dt.CloseConnection();
            return tb;
        }

        //DELETE 
        public Boolean DeleteByID(int ID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from kus_GhiDanhTiemNamg where ID=@ID";
            SqlParameter pID = new SqlParameter("@ID", ID);
            this.dt.Updatedata(sql, pID);
            this.dt.CloseConnection();
            return true;
        }
    }
}
