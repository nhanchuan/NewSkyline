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
    public class kus_DiemDanhBLL
    {
        DataServices dt = new DataServices();
        public List<kus_DiemDanh> GetlistWithID(int DiemDanhID)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_DiemDanh where DiemDanhID=@DiemDanhID";
            SqlParameter pDiemDanhID = new SqlParameter("@DiemDanhID", DiemDanhID);
            DataTable tb = dt.DAtable(sql, pDiemDanhID);
            List<kus_DiemDanh> lst = new List<kus_DiemDanh>();
            foreach (DataRow r in tb.Rows)
            {
                kus_DiemDanh dd = new kus_DiemDanh();
                dd.DiemDanhID = (int)r["DiemDanhID"];
                dd.NgayDiemDanh = (int)r["NgayDiemDanh"];
                dd.HocVien = (int)r["HocVien"];
                dd.DiemDanh = (string.IsNullOrEmpty(r["DiemDanh"].ToString())) ? 0 : (int)r["DiemDanh"];
                dd.CoPhep = (string.IsNullOrEmpty(r["CoPhep"].ToString())) ? 0 : (int)r["CoPhep"];
                dd.GhiChu = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                dd.DateOfCreate = (DateTime)r["DateOfCreate"];
                lst.Add(dd);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public List<kus_DiemDanh> GetlistWithNgayDiemDanh(int NgayDiemDanh)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_DiemDanh where NgayDiemDanh=@NgayDiemDanh";
            SqlParameter pNgayDiemDanh = new SqlParameter("@NgayDiemDanh", NgayDiemDanh);
            DataTable tb = dt.DAtable(sql, pNgayDiemDanh);
            List<kus_DiemDanh> lst = new List<kus_DiemDanh>();
            foreach(DataRow r in tb.Rows)
            {
                kus_DiemDanh dd = new kus_DiemDanh();
                dd.DiemDanhID = (int)r["DiemDanhID"];
                dd.NgayDiemDanh = (int)r["NgayDiemDanh"];
                dd.HocVien = (int)r["HocVien"];
                dd.DiemDanh = (string.IsNullOrEmpty(r["DiemDanh"].ToString())) ? 0 : (int)r["DiemDanh"];
                dd.CoPhep = (string.IsNullOrEmpty(r["CoPhep"].ToString())) ? 0 : (int)r["CoPhep"];
                dd.GhiChu = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
                dd.DateOfCreate = (DateTime)r["DateOfCreate"];
                lst.Add(dd);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public DataTable TBDiemDanhWithNgayID(int NgayDiemDanh)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select diemdanh.DiemDanhID,diemdanh.NgayDiemDanh,diemdanh.HocVien,diemdanh.DiemDanh,diemdanh.CoPhep,diemdanh.GhiChu,diemdanh.DateOfCreate, hocvien.HocVienCode,hocvien.InfoID,hocvien.DienThoai,info.FirstName,info.LastName,info.Birthday,info.Sex";
            sql += " ";
            sql += "from kus_DiemDanh diemdanh full outer join kus_HocVien hocvien on diemdanh.HocVien=hocvien.HocVienID full outer join CustomerBasicInfo info on hocvien.InfoID=info.InfoID where diemdanh.DiemDanhID is not null and diemdanh.NgayDiemDanh=@NgayDiemDanh";
            SqlParameter pNgayDiemDanh = new SqlParameter("@NgayDiemDanh", NgayDiemDanh);
            DataTable tb = dt.DAtable(sql, pNgayDiemDanh);
            this.dt.CloseConnection();
            return tb;
        }
        //Insert - create
        public Boolean CreateDiemDanh(int NgayDiemDanh, int HocVien)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into kus_DiemDanh(NgayDiemDanh,HocVien) values (@NgayDiemDanh,@HocVien)";
            SqlParameter pNgayDiemDanh = new SqlParameter("@NgayDiemDanh", NgayDiemDanh);
            SqlParameter pHocVien = new SqlParameter("@HocVien", HocVien);
            this.dt.Updatedata(sql, pNgayDiemDanh, pHocVien);
            this.dt.CloseConnection();
            return true;
        }
        //Update diem danh
        public Boolean UpdateDiemDanh(int DiemDanhID, int DiemDanh, int CoPhep, string GhiChu)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "update kus_DiemDanh set DiemDanh=@DiemDanh, CoPhep=@CoPhep, GhiChu=@GhiChu where DiemDanhID=@DiemDanhID";
            SqlParameter pDiemDanhID = new SqlParameter("@DiemDanhID", DiemDanhID);
            SqlParameter pDiemDanh = (DiemDanh == 0) ? new SqlParameter("@DiemDanh", DBNull.Value) : new SqlParameter("@DiemDanh", DiemDanh);
            SqlParameter pCoPhep = (CoPhep == 0) ? new SqlParameter("@CoPhep", DBNull.Value) : new SqlParameter("@CoPhep", CoPhep);
            SqlParameter pGhiChu = (GhiChu == "") ? new SqlParameter("@GhiChu", DBNull.Value) : new SqlParameter("@GhiChu", GhiChu);
            this.dt.Updatedata(sql, pDiemDanhID, pDiemDanh, pCoPhep, pGhiChu);
            this.dt.CloseConnection();
            return true;
        }
        //Update ghi chu
        public Boolean UpdateGhiChuDiemDanh(string GhiChu, int DiemDanhID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "Update kus_DiemDanh set GhiChu=@GhiChu where DiemDanhID=@DiemDanhID";
            SqlParameter pDiemDanhID = new SqlParameter("@DiemDanhID", DiemDanhID);
            SqlParameter pGhiChu = (GhiChu == "") ? new SqlParameter("@GhiChu", DBNull.Value) : new SqlParameter("@GhiChu", GhiChu);
            this.dt.Updatedata(sql, pDiemDanhID, pGhiChu);
            this.dt.CloseConnection();
            return true;
        }
    }

}
