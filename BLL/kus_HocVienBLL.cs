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
    public class kus_HocVienBLL
    {
        DataServices DB = new DataServices();
        public DataTable getTBAllHocVien()
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_HocVien";
            DataTable tb = DB.DAtable(sql);
            this.DB.CloseConnection();
            return tb;
        }
        public List<kus_HocVien> getAllHocVien()
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_HocVien";
            DataTable tb = DB.DAtable(sql);
            List<kus_HocVien> lst = new List<kus_HocVien>();
            foreach (DataRow r in tb.Rows)
            {
                kus_HocVien hv = new kus_HocVien();
                hv.HocVienID = (int)r["HocVienID"];
                hv.HocVienCode = (string)r["HocVienCode"];
                hv.InfoID = (string.IsNullOrEmpty(r["InfoID"].ToString())) ? 0 : (int)r["InfoID"];
                hv.DCThuongTru = (string.IsNullOrEmpty(r["DCThuongTru"].ToString())) ? "" : (string)r["DCThuongTru"];
                hv.DCTamTru = (string.IsNullOrEmpty(r["DCTamTru"].ToString())) ? "" : (string)r["DCTamTru"];
                hv.Email = (string.IsNullOrEmpty(r["Email"].ToString())) ? "" : (string)r["Email"];
                hv.DienThoai = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
                hv.HoTenPH = (string.IsNullOrEmpty(r["HoTenPH"].ToString())) ? "" : (string)r["HoTenPH"];
                hv.NgheNghiep = (string.IsNullOrEmpty(r["NgheNghiep"].ToString())) ? "" : (string)r["NgheNghiep"];
                hv.PhonePhuHuynh = (string.IsNullOrEmpty(r["PhonePhuHuynh"].ToString())) ? "" : (string)r["PhonePhuHuynh"];
                hv.HocVienStatus = (string.IsNullOrEmpty(r["HocVienStatus"].ToString())) ? 0 : (int)r["HocVienStatus"];
                hv.DateOfCreate = (DateTime)r["DateOfCreate"];
                hv.RandomCode = (string)r["RandomCode"];
                hv.ImgID = (string.IsNullOrEmpty(r["ImgID"].ToString())) ? 0 : (int)r["ImgID"];
                hv.AvailableBalances = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? 0 : (int)r["AvailableBalances"];
                lst.Add(hv);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<kus_HocVien> getHocVienWithID(int HocVienID)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_HocVien where HocVienID=@HocVienID";
            SqlParameter pHocVienID = new SqlParameter("HocVienID", HocVienID);
            DataTable tb = DB.DAtable(sql, pHocVienID);
            List<kus_HocVien> lst = new List<kus_HocVien>();
            foreach (DataRow r in tb.Rows)
            {
                kus_HocVien hv = new kus_HocVien();
                hv.HocVienID = (int)r["HocVienID"];
                hv.HocVienCode = (string)r["HocVienCode"];
                hv.InfoID = (string.IsNullOrEmpty(r["InfoID"].ToString())) ? 0 : (int)r["InfoID"];
                hv.DCThuongTru = (string.IsNullOrEmpty(r["DCThuongTru"].ToString())) ? "" : (string)r["DCThuongTru"];
                hv.DCTamTru = (string.IsNullOrEmpty(r["DCTamTru"].ToString())) ? "" : (string)r["DCTamTru"];
                hv.Email = (string.IsNullOrEmpty(r["Email"].ToString())) ? "" : (string)r["Email"];
                hv.DienThoai = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
                hv.HoTenPH = (string.IsNullOrEmpty(r["HoTenPH"].ToString())) ? "" : (string)r["HoTenPH"];
                hv.NgheNghiep = (string.IsNullOrEmpty(r["NgheNghiep"].ToString())) ? "" : (string)r["NgheNghiep"];
                hv.PhonePhuHuynh = (string.IsNullOrEmpty(r["PhonePhuHuynh"].ToString())) ? "" : (string)r["PhonePhuHuynh"];
                hv.HocVienStatus = (string.IsNullOrEmpty(r["HocVienStatus"].ToString())) ? 0 : (int)r["HocVienStatus"];
                hv.DateOfCreate = (DateTime)r["DateOfCreate"];
                hv.RandomCode = (string)r["RandomCode"];
                hv.ImgID = (string.IsNullOrEmpty(r["ImgID"].ToString())) ? 0 : (int)r["ImgID"];
                hv.AvailableBalances = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? 0 : (int)r["AvailableBalances"];
                lst.Add(hv);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<kus_HocVien> getHocVienWithMaHV(string HocVienCode)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_HocVien where HocVienCode=@HocVienCode";
            SqlParameter pHocVienCode = new SqlParameter("HocVienCode", HocVienCode);
            DataTable tb = DB.DAtable(sql, pHocVienCode);
            List<kus_HocVien> lst = new List<kus_HocVien>();
            foreach (DataRow r in tb.Rows)
            {
                kus_HocVien hv = new kus_HocVien();
                hv.HocVienID = (int)r["HocVienID"];
                hv.HocVienCode = (string)r["HocVienCode"];
                hv.InfoID = (string.IsNullOrEmpty(r["InfoID"].ToString())) ? 0 : (int)r["InfoID"];
                hv.DCThuongTru = (string.IsNullOrEmpty(r["DCThuongTru"].ToString())) ? "" : (string)r["DCThuongTru"];
                hv.DCTamTru = (string.IsNullOrEmpty(r["DCTamTru"].ToString())) ? "" : (string)r["DCTamTru"];
                hv.Email = (string.IsNullOrEmpty(r["Email"].ToString())) ? "" : (string)r["Email"];
                hv.DienThoai = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
                hv.HoTenPH = (string.IsNullOrEmpty(r["HoTenPH"].ToString())) ? "" : (string)r["HoTenPH"];
                hv.NgheNghiep = (string.IsNullOrEmpty(r["NgheNghiep"].ToString())) ? "" : (string)r["NgheNghiep"];
                hv.PhonePhuHuynh = (string.IsNullOrEmpty(r["PhonePhuHuynh"].ToString())) ? "" : (string)r["PhonePhuHuynh"];
                hv.HocVienStatus = (string.IsNullOrEmpty(r["HocVienStatus"].ToString())) ? 0 : (int)r["HocVienStatus"];
                hv.DateOfCreate = (DateTime)r["DateOfCreate"];
                hv.RandomCode = (string)r["RandomCode"];
                hv.ImgID = (string.IsNullOrEmpty(r["ImgID"].ToString())) ? 0 : (int)r["ImgID"];
                hv.AvailableBalances = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? 0 : (int)r["AvailableBalances"];
                lst.Add(hv);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<kus_HocVien> getAllHocVienAutoComplete(string pretext)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_HocVien where HocVienCode like @pretext+'%'";
            SqlParameter ppretext = new SqlParameter("@pretext", pretext);
            DataTable tb = DB.DAtable(sql);
            List<kus_HocVien> lst = new List<kus_HocVien>();
            foreach (DataRow r in tb.Rows)
            {
                kus_HocVien hv = new kus_HocVien();
                hv.HocVienID = (int)r["HocVienID"];
                hv.HocVienCode = (string)r["HocVienCode"];
                hv.InfoID = (string.IsNullOrEmpty(r["InfoID"].ToString())) ? 0 : (int)r["InfoID"];
                hv.DCThuongTru = (string.IsNullOrEmpty(r["DCThuongTru"].ToString())) ? "" : (string)r["DCThuongTru"];
                hv.DCTamTru = (string.IsNullOrEmpty(r["DCTamTru"].ToString())) ? "" : (string)r["DCTamTru"];
                hv.Email = (string.IsNullOrEmpty(r["Email"].ToString())) ? "" : (string)r["Email"];
                hv.DienThoai = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
                hv.HoTenPH = (string.IsNullOrEmpty(r["HoTenPH"].ToString())) ? "" : (string)r["HoTenPH"];
                hv.NgheNghiep = (string.IsNullOrEmpty(r["NgheNghiep"].ToString())) ? "" : (string)r["NgheNghiep"];
                hv.PhonePhuHuynh = (string.IsNullOrEmpty(r["PhonePhuHuynh"].ToString())) ? "" : (string)r["PhonePhuHuynh"];
                hv.HocVienStatus = (string.IsNullOrEmpty(r["HocVienStatus"].ToString())) ? 0 : (int)r["HocVienStatus"];
                hv.DateOfCreate = (DateTime)r["DateOfCreate"];
                hv.RandomCode = (string)r["RandomCode"];
                hv.ImgID = (string.IsNullOrEmpty(r["ImgID"].ToString())) ? 0 : (int)r["ImgID"];
                hv.AvailableBalances = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? 0 : (int)r["AvailableBalances"];
                lst.Add(hv);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<kus_HocVien> getHVWithCode(string code)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from kus_HocVien where RandomCode=@code";
            SqlParameter pcode = new SqlParameter("@code", code);
            DataTable tb = DB.DAtable(sql, pcode);
            List<kus_HocVien> lst = new List<kus_HocVien>();
            foreach (DataRow r in tb.Rows)
            {
                kus_HocVien hv = new kus_HocVien();
                hv.HocVienID = (int)r["HocVienID"];
                hv.HocVienCode = (string)r["HocVienCode"];
                hv.InfoID = (string.IsNullOrEmpty(r["InfoID"].ToString())) ? 0 : (int)r["InfoID"];
                hv.DCThuongTru = (string.IsNullOrEmpty(r["DCThuongTru"].ToString())) ? "" : (string)r["DCThuongTru"];
                hv.DCTamTru = (string.IsNullOrEmpty(r["DCTamTru"].ToString())) ? "" : (string)r["DCTamTru"];
                hv.Email = (string.IsNullOrEmpty(r["Email"].ToString())) ? "" : (string)r["Email"];
                hv.DienThoai = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
                hv.HoTenPH = (string.IsNullOrEmpty(r["HoTenPH"].ToString())) ? "" : (string)r["HoTenPH"];
                hv.NgheNghiep = (string.IsNullOrEmpty(r["NgheNghiep"].ToString())) ? "" : (string)r["NgheNghiep"];
                hv.PhonePhuHuynh = (string.IsNullOrEmpty(r["PhonePhuHuynh"].ToString())) ? "" : (string)r["PhonePhuHuynh"];
                hv.HocVienStatus = (string.IsNullOrEmpty(r["HocVienStatus"].ToString())) ? 0 : (int)r["HocVienStatus"];
                hv.DateOfCreate = (DateTime)r["DateOfCreate"];
                hv.RandomCode = (string)r["RandomCode"];
                hv.ImgID = (string.IsNullOrEmpty(r["ImgID"].ToString())) ? 0 : (int)r["ImgID"];
                hv.AvailableBalances = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? 0 : (int)r["AvailableBalances"];
                lst.Add(hv);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public Boolean CreateCodeHocVien(string code)
        {
            string sql = "insert into kus_HocVien(RandomCode) values(@code)";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter pCode = new SqlParameter("code", code);
            this.DB.Updatedata(sql, pCode);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean kus_UpdateHocVen(int HocVienID, int InfoID, string DCThuongTru, string DCTamTru, string Email, string DienThoai, string HoTenPH, string NgheNghiep, string PhonePhuHuynh, int HocVienStatus, int AvailableBalances)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "Exec kus_UpdateHocVen @HocVienID, @InfoID,@DCThuongTru,@DCTamTru,@Email,@DienThoai,@HoTenPH,@NgheNghiep,@PhonePhuHuynh,@HocVienStatus,@AvailableBalances";
            SqlParameter pHocVienID = new SqlParameter("HocVienID", HocVienID);
            SqlParameter pInfoID = new SqlParameter("InfoID", InfoID);
            SqlParameter pDCThuongTru = new SqlParameter("DCThuongTru", DCThuongTru);
            SqlParameter pDCTamTru = new SqlParameter("DCTamTru", DCTamTru);
            SqlParameter pEmail = new SqlParameter("Email", Email);
            SqlParameter pDienThoai = new SqlParameter("DienThoai", DienThoai);
            SqlParameter pHoTenPH = new SqlParameter("HoTenPH", HoTenPH);
            SqlParameter pNgheNghiep = new SqlParameter("NgheNghiep", NgheNghiep);
            SqlParameter pPhonePhuHuynh = new SqlParameter("PhonePhuHuynh", PhonePhuHuynh);
            SqlParameter pHocVienStatus = new SqlParameter("HocVienStatus", HocVienStatus);
            SqlParameter pAvailableBalances = new SqlParameter("@AvailableBalances", AvailableBalances);
            this.DB.Updatedata(sql, pHocVienID, pInfoID, pDCThuongTru, pDCTamTru, pEmail, pDienThoai, pHoTenPH, pNgheNghiep, pPhonePhuHuynh, pHocVienStatus, pAvailableBalances);
            this.DB.CloseConnection();
            return true;
        }
        //========APDATE HOC VIEN IMAGES======================================================================================================
        public Boolean UpdateHocVienImage(int HocVienID, int ImgID)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "update kus_HocVien set ImgID=@ImgID where HocVienID=@HocVienID";
            SqlParameter pImgID = new SqlParameter("@ImgID", ImgID);
            SqlParameter pHocVienID = new SqlParameter("@HocVienID", HocVienID);
            this.DB.Updatedata(sql, pImgID, pHocVienID);
            this.DB.CloseConnection();
            return true;
        }
        //=============GET HOC VIEN TO EXPROT to Excel=======================================================================================================================
        public DataTable ExprotHVtoExcel(int KhoaHoc)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec ExprotHVtoExcel @KhoaHoc";
            SqlParameter pKhoaHoc = new SqlParameter("@KhoaHoc", KhoaHoc);
            DataTable tb = DB.DAtable(sql, pKhoaHoc);
            this.DB.CloseConnection();
            return tb;
        }
        //============kus_HocVienInfor========================================================================================================================================
        public DataTable kus_HocVienInfor(string HocVienCode)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec kus_HocVienInfor @HocVienCode";
            SqlParameter pHocVienCode = new SqlParameter("@HocVienCode", HocVienCode);
            DataTable tb = DB.DAtable(sql, pHocVienCode);
            this.DB.CloseConnection();
            return tb;
        }
        // Search
        public DataTable SearchHocVienPageWise(int PageIndex, int PageSize, int CoSoID, int LoaiChuongTrinh, int ChuongTrinh, int LopHoc, int khoahoc, string HocVienCode, string TenHocVien, string Email, string DienThoai, string IdentityCard, string HoTenPH)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec SearchHocVienPageWise @PageIndex,@PageSize,@CoSoID,@LoaiChuongTrinh,@ChuongTrinh,@LopHoc,@khoahoc,@HocVienCode,@TenHocVien,@Email,@DienThoai,@IdentityCard,@HoTenPH";
            SqlParameter p1 = new SqlParameter("@PageIndex", PageIndex);
            SqlParameter p2= new SqlParameter("@PageSize", PageSize);
            SqlParameter p3 = (CoSoID == 0) ? new SqlParameter("@CoSoID", DBNull.Value) : new SqlParameter("@CoSoID", CoSoID);
            SqlParameter p4 = (LoaiChuongTrinh == 0) ? new SqlParameter("@LoaiChuongTrinh", DBNull.Value) : new SqlParameter("@LoaiChuongTrinh", LoaiChuongTrinh);
            SqlParameter p5 = (ChuongTrinh == 0) ? new SqlParameter("@ChuongTrinh", DBNull.Value) : new SqlParameter("@ChuongTrinh", ChuongTrinh);
            SqlParameter p6 = (LopHoc == 0) ? new SqlParameter("@LopHoc", DBNull.Value) : new SqlParameter("@LopHoc", LopHoc);
            SqlParameter p7 = (khoahoc == 0) ? new SqlParameter("@khoahoc", DBNull.Value) : new SqlParameter("@khoahoc", khoahoc);
            SqlParameter p8 = (HocVienCode == "") ? new SqlParameter("@HocVienCode", DBNull.Value) : new SqlParameter("@HocVienCode", HocVienCode);
            SqlParameter p9 = (TenHocVien == "") ? new SqlParameter("@TenHocVien", DBNull.Value) : new SqlParameter("@TenHocVien", TenHocVien);
            SqlParameter p10 = (Email == "") ? new SqlParameter("@Email", DBNull.Value) : new SqlParameter("@Email", Email);
            SqlParameter p11 = (DienThoai == "") ? new SqlParameter("@DienThoai", DBNull.Value) : new SqlParameter("@DienThoai", DienThoai);
            SqlParameter p12 = (IdentityCard == "") ? new SqlParameter("@IdentityCard", DBNull.Value) : new SqlParameter("@IdentityCard", IdentityCard);
            SqlParameter p13 = (HoTenPH == "") ? new SqlParameter("@HoTenPH", DBNull.Value) : new SqlParameter("@HoTenPH", HoTenPH);
            DataTable tb = DB.DAtable(sql, p1, p2, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            this.DB.CloseConnection();
            return tb;
        }
        public int CounthHocVienPageWise(int CoSoID, int LoaiChuongTrinh, int ChuongTrinh, int LopHoc, int khoahoc, string HocVienCode, string TenHocVien, string Email, string DienThoai, string IdentityCard, string HoTenPH)
        {
            int countHV = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "Exec CounthHocVienPageWise @CoSoID,@LoaiChuongTrinh,@ChuongTrinh,@LopHoc,@khoahoc,@HocVienCode,@TenHocVien,@Email,@DienThoai,@IdentityCard,@HoTenPH";
            SqlParameter p3 = (CoSoID == 0) ? new SqlParameter("@CoSoID", DBNull.Value) : new SqlParameter("@CoSoID", CoSoID);
            SqlParameter p4 = (LoaiChuongTrinh == 0) ? new SqlParameter("@LoaiChuongTrinh", DBNull.Value) : new SqlParameter("@LoaiChuongTrinh", LoaiChuongTrinh);
            SqlParameter p5 = (ChuongTrinh == 0) ? new SqlParameter("@ChuongTrinh", DBNull.Value) : new SqlParameter("@ChuongTrinh", ChuongTrinh);
            SqlParameter p6 = (LopHoc == 0) ? new SqlParameter("@LopHoc", DBNull.Value) : new SqlParameter("@LopHoc", LopHoc);
            SqlParameter p7 = (khoahoc == 0) ? new SqlParameter("@khoahoc", DBNull.Value) : new SqlParameter("@khoahoc", khoahoc);
            SqlParameter p8 = (HocVienCode == "") ? new SqlParameter("@HocVienCode", DBNull.Value) : new SqlParameter("@HocVienCode", HocVienCode);
            SqlParameter p9 = (TenHocVien == "") ? new SqlParameter("@TenHocVien", DBNull.Value) : new SqlParameter("@TenHocVien", TenHocVien);
            SqlParameter p10 = (Email == "") ? new SqlParameter("@Email", DBNull.Value) : new SqlParameter("@Email", Email);
            SqlParameter p11 = (DienThoai == "") ? new SqlParameter("@DienThoai", DBNull.Value) : new SqlParameter("@DienThoai", DienThoai);
            SqlParameter p12 = (IdentityCard == "") ? new SqlParameter("@IdentityCard", DBNull.Value) : new SqlParameter("@IdentityCard", IdentityCard);
            SqlParameter p13 = (HoTenPH == "") ? new SqlParameter("@HoTenPH", DBNull.Value) : new SqlParameter("@HoTenPH", HoTenPH);
            countHV = DB.GetValues(sql, p3, p4, p5, p6, p7, p8, p9, p10, p11, p12, p13);
            this.DB.CloseConnection();
            return countHV;
        }
        //Update AvailableBalances
        public Boolean UpdateAvailableBalancesByHocVienID(int HocVienID, int AvailableBalances)
        {
            if (!this.DB.OpenConnection())
            {
                return true;
            }
            string sql = "update kus_HocVien set AvailableBalances=@AvailableBalances where HocVienID=@HocVienID";
            SqlParameter pHocVienID = new SqlParameter("@HocVienID", HocVienID);
            SqlParameter pAvailableBalances = new SqlParameter("@AvailableBalances", AvailableBalances);
            this.DB.Updatedata(sql, pHocVienID, pAvailableBalances);
            this.DB.CloseConnection();
            return true;
        }

    }
}
