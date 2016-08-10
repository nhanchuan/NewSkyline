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
                gh.LopHoc= (string.IsNullOrEmpty(r["LopHoc"].ToString())) ? 0 : (int)r["LopHoc"];
                gh.NVGhiDanh= (string.IsNullOrEmpty(r["NVGhiDanh"].ToString())) ? 0 : (int)r["NVGhiDanh"];
                gh.NgayGD = (DateTime)r["NgayGD"];
                gh.GhiChu= (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                gh.GDStatus= (string.IsNullOrEmpty(r["GDStatus"].ToString())) ? false : (Boolean)r["GDStatus"];
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
    }
}
