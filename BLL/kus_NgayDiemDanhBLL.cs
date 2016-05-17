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
    public class kus_NgayDiemDanhBLL
    {
        DataServices dt = new DataServices();
        DateTime DefaultDate = Convert.ToDateTime("01/01/1900");
        public List<kus_NgayDiemDanh> getListWithKhoaHoc(int KhoaHoc)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_NgayDiemDanh where KhoaHoc=@KhoaHoc";
            SqlParameter pKhoaHoc = new SqlParameter("@KhoaHoc", KhoaHoc);
            DataTable tb = dt.DAtable(sql, pKhoaHoc);
            List<kus_NgayDiemDanh> lst = new List<kus_NgayDiemDanh>();
            foreach (DataRow r in tb.Rows)
            {
                kus_NgayDiemDanh nd = new kus_NgayDiemDanh();
                nd.NgayID = (int)r["NgayID"];
                nd.KhoaHoc = (string.IsNullOrEmpty(r["KhoaHoc"].ToString())) ? 0 : (int)r["KhoaHoc"];
                nd.NgayDiemDanh = (string.IsNullOrEmpty(r["NgayDiemDanh"].ToString())) ? DefaultDate : (DateTime)r["NgayDiemDanh"];
                lst.Add(nd);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public DataTable getTableNgayDiemDanh(int KhoaHoc)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "Exec getTableNgayDiemDanh @KhoaHoc";
            SqlParameter pKhoaHoc = new SqlParameter("@KhoaHoc", KhoaHoc);
            DataTable tb = dt.DAtable(sql, pKhoaHoc);
            this.dt.CloseConnection();
            return tb;
        }

        //Insert
        public Boolean InsertNgayDiemDanh(int KhoaHoc, DateTime NgayDiemDanh)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into kus_NgayDiemDanh(KhoaHoc,NgayDiemDanh) values (@KhoaHoc,@NgayDiemDanh)";
            SqlParameter pKhoaHoc = new SqlParameter("@KhoaHoc", KhoaHoc);
            SqlParameter pNgayDiemDanh = new SqlParameter("@NgayDiemDanh", NgayDiemDanh);
            this.dt.Updatedata(sql, pKhoaHoc, pNgayDiemDanh);
            this.dt.CloseConnection();
            return true;
        }
    }
}
