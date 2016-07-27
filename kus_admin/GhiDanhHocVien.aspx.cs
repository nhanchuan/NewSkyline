﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Configuration;

public partial class kus_admin_GhiDanhHocVien : BasePage
{
    CustomerBasicInfoBLL customerbasicinfo;
    kus_HocVienBLL kus_hocvien;
    kus_HocVienMoreInFoBLL hocvienmoreinfo;
    UserProfileBLL userprofile;
    EmployeesBLL employees;
    //kus_LopHocBLL kus_lophoc;
    nc_KhoaHocBLL nc_khoahoc;
    kus_GhiDanhBLL kus_ghidanh;
    nc_ChuongTrinhDaoTaoBLL nc_chuongtrinhdaotao;
    nc_LopHocBLL nc_lophoc;
    kus_CoSoBLL kus_coso;
    nc_LoaiCTDaoTaoBLL nc_loaictdaotao;
    kus_HTChiNhanhBLL kus_htchinhanh;
    REGISTRATION_FORM_ADVISORY_BLL registration_form;
    ImagesBLL images;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setcurenturl();
        if (!IsPostBack)
        {
            UserAccounts ac = Session.GetCurrentUser();
            if (ac == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {

                if (!check_permiss(ac.UserID, FunctionName.GhiDanhHocVien))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    string MaKhoaHoc = Request.QueryString["makhoahoc"];
                    if (!CheckQuerystring(MaKhoaHoc))
                    {
                        Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/GhiDanhKhoaHoc.aspx");
                    }
                    else
                    {
                        this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                        lblKhoaHocChose.Text = KhoaHocDangChon(MaKhoaHoc);
                        this.load_dlELoaiChuongTrinh();
                        this.load_dlEHTChiNhanh();
                        this.load_dlECoSo();
                        this.load_KhoaHocInfor(MaKhoaHoc);
                        panelGhiDanhMoi.Visible = true;
                        panelDaGhiDanh.Visible = false;
                    }
                }
            }
        }
    }

    private Boolean CheckQuerystring(string code)
    {
        nc_khoahoc = new nc_KhoaHocBLL();

        if (string.IsNullOrWhiteSpace(code) || string.IsNullOrEmpty(code))
        {
            return false;
        }
        else
        {
            List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(code);
            nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
            if (khoahoc == null)
            {
                return false;
            }
        }
        return true;
    }
    private string KhoaHocDangChon(string code)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(code);
        nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
        return khoahoc.MaKhoaHoc + " - " + khoahoc.TenKhoaHoc;
    }
    private void load_dlELoaiChuongTrinh()
    {
        nc_loaictdaotao = new nc_LoaiCTDaoTaoBLL();
        dlELoaiChuongTrinh.DataSource = nc_loaictdaotao.getListLoaiCTDaoTao();
        dlELoaiChuongTrinh.DataTextField = "TenChuongTrinh";
        dlELoaiChuongTrinh.DataValueField = "ID";
        dlELoaiChuongTrinh.DataBind();
        dlELoaiChuongTrinh.Items.Insert(0, new ListItem("-- Chọn loại chương trình --", "0"));
    }
    private void load_dlEChuongTrinh(int loaichuongtrinh)
    {
        nc_chuongtrinhdaotao = new nc_ChuongTrinhDaoTaoBLL();
        dlEChuongTrinh.DataSource = nc_chuongtrinhdaotao.GetChuongTrinhDaoTaoWithLoai(loaichuongtrinh);
        dlEChuongTrinh.DataTextField = "TenChuongTrinh";
        dlEChuongTrinh.DataValueField = "ID";
        dlEChuongTrinh.DataBind();
        dlEChuongTrinh.Items.Insert(0, new ListItem("-- Chọn chương trình --", "0"));
    }
    private void load_dlELopHoc(int chuongtrinh)
    {
        nc_lophoc = new nc_LopHocBLL();
        dlELopHoc.DataSource = nc_lophoc.getListLopHocWithChuongTrinh(chuongtrinh);
        dlELopHoc.DataTextField = "TenLopHoc";
        dlELopHoc.DataValueField = "ID";
        dlELopHoc.DataBind();
        dlELopHoc.Items.Insert(0, new ListItem("-- Chọn lớp học --", "0"));
    }
    private void load_dlEHTChiNhanh()
    {
        kus_htchinhanh = new kus_HTChiNhanhBLL();
        dlEHTChiNhanh.DataSource = kus_htchinhanh.getAllTBChiNhanh();
        dlEHTChiNhanh.DataTextField = "tenHTChiNhanh";
        dlEHTChiNhanh.DataValueField = "hTChiNhanhID";
        dlEHTChiNhanh.DataBind();
        dlEHTChiNhanh.Items.Insert(0, new ListItem("------- Chọn Hệ Thống Chi Nhánh -------", "0"));
    }
    private void load_dlECoSo()
    {
        kus_coso = new kus_CoSoBLL();
        dlECoSo.DataSource = kus_coso.getAllHTCoSo();
        dlECoSo.DataTextField = "TenCoSo";
        dlECoSo.DataValueField = "CoSoID";
        dlECoSo.DataBind();
        dlECoSo.Items.Insert(0, new ListItem("------ Chọn Cơ Sở -------", "0"));
    }
    private void load_KhoaHocInfor(string code)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        kus_coso = new kus_CoSoBLL();
        List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(code);
        nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
        if (khoahoc != null)
        {
            this.load_dlEChuongTrinh(khoahoc.LoaiChuongTrinh);
            this.load_dlELopHoc(khoahoc.ChuongTrinh);
            txtETenKhoaHoc.Text = khoahoc.TenKhoaHoc;
            txtESoLuong.Text = khoahoc.SoLuong.ToString();
            txtENgayKhaiGiang.Text = (khoahoc.NgayKhaiGiang.Year <= 1900) ? "" : khoahoc.NgayKhaiGiang.ToString("dd-MM-yyyy");
            txtENgayKetThuc.Text = (khoahoc.NgayKetThuc.Year <= 1900) ? "" : khoahoc.NgayKetThuc.ToString("dd-MM-yyyy");
            txtEThoiLuong.Text = khoahoc.ThoiLuong.ToString();
            dlELoaiChuongTrinh.Items.FindByValue(khoahoc.LoaiChuongTrinh.ToString()).Selected = true;
            dlEChuongTrinh.Items.FindByValue(khoahoc.ChuongTrinh.ToString()).Selected = true;
            dlELopHoc.Items.FindByValue(khoahoc.LopHoc.ToString()).Selected = true;
            dlECoSo.Items.FindByValue(khoahoc.CoSoID.ToString()).Selected = true;

            List<kus_CoSo> lstCoSo = kus_coso.getLSTCoSoWithID(khoahoc.CoSoID);
            kus_CoSo coso = lstCoSo.FirstOrDefault();
            dlEHTChiNhanh.Items.FindByValue((coso == null) ? 0.ToString() : coso.HTChiNhanhID.ToString()).Selected = true;
        }
        else
        {
            return;
        }
    }
    protected void btnAddNewHV_Click(object sender, EventArgs e)
    {
        try
        {
            customerbasicinfo = new CustomerBasicInfoBLL();
            kus_hocvien = new kus_HocVienBLL();
            userprofile = new UserProfileBLL();
            employees = new EmployeesBLL();
            kus_ghidanh = new kus_GhiDanhBLL();
            nc_khoahoc = new nc_KhoaHocBLL();
            nc_lophoc = new nc_LopHocBLL();
            //kus_lophoc = new kus_LopHocBLL();
            string FileCode = RandomName + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

            string firstname = txtFirstNameHV.Text;
            string lastname = txtLastNameHV.Text;
            string ngaysinh = txtNgaySinh.Text;
            DateTime birthday;
            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            if (string.IsNullOrWhiteSpace(ngaysinh) || DateTime.TryParseExact(ngaysinh, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out birthday) || getday(ngaysinh) == "" || getmonth(ngaysinh) == "" || getyear(ngaysinh) == "")
            {
                birthday = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                birthday = DateTime.ParseExact(getday(ngaysinh) + "/" + getmonth(ngaysinh) + "/" + getyear(ngaysinh), "dd/MM/yyyy", null);
            }
            string noisinh = txtNoiSinhHV.Text;
            string socmnd = txtSoCMNDHV.Text;
            string ngaycapcmnd = txtNgayCapCMND.Text;
            DateTime NgayCapCMND;
            if (string.IsNullOrWhiteSpace(ngaycapcmnd) || DateTime.TryParseExact(ngaycapcmnd, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayCapCMND) || getday(ngaycapcmnd) == "" || getmonth(ngaycapcmnd) == "" || getyear(ngaycapcmnd) == "")
            {
                NgayCapCMND = Convert.ToDateTime("01/01/1900");
            }
            else
            {
                NgayCapCMND = DateTime.ParseExact(getday(ngaycapcmnd) + "/" + getmonth(ngaycapcmnd) + "/" + getyear(ngaycapcmnd), "dd/MM/yyyy", null);
            }
            string noicap = txtNoiCapCMND.Text;
            int sex;
            if (!rdformnam.Checked && !rdformnu.Checked)
            {
                sex = 0;
            }
            else
            {
                sex = (rdformnam.Checked) ? 1 : (rdformnu.Checked) ? 2 : 0;
            }

            this.customerbasicinfo.CreateBasicCodeInfo(FileCode);

            List<CustomerBasicInfo> lstBS = customerbasicinfo.GetCusBasicInfoWithCode(FileCode);
            CustomerBasicInfo basicinfo = lstBS.FirstOrDefault();
            this.customerbasicinfo.UpdateCustomerBasicInfo(basicinfo.InfoID, firstname, lastname, "", birthday, noisinh, sex, socmnd, NgayCapCMND, noicap);
            string HocvienCode = RandomName + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            this.kus_hocvien.CreateCodeHocVien(HocvienCode);

            string diachithuongtru = txtDCThuongTru.Text;
            string diachitamtru = txtDCTamTru.Text;
            string email = txtEmail.Text;
            string dienthoai = txtPhoneHV.Text;
            string hotenPH = txtHoTenPH.Text;
            string nghenghiep = txtNgheNghiepPH.Text;
            string phonePH = txtDienThoaiPH.Text;
            //int NVGioiThieu=
            List<UserProfile> lstprofile = userprofile.getUserProfileWithID(Session.GetCurrentUser().UserID);
            UserProfile userpro = lstprofile.FirstOrDefault();
            List<Employees> lstemployee = employees.getEmpWithProfileId(userpro.ProfileID);
            Employees emp = lstemployee.FirstOrDefault();
            int NVGhiDanh = emp.EmployeesID;
            List<kus_HocVien> lstHV = kus_hocvien.getHVWithCode(HocvienCode);
            kus_HocVien hocvien = lstHV.FirstOrDefault();
            int datcoc = (txtDatCoc.Text == "") ? 0 : Convert.ToInt32(txtDatCoc.Text);
            string MaKhoaHoc = Request.QueryString["makhoahoc"];
            List<nc_LopHoc> lstLop = nc_lophoc.getListLopHocWithMaKhoaHoc(MaKhoaHoc);
            nc_LopHoc lop = lstLop.FirstOrDefault();
            if (datcoc > lop.MucHocPhi)
            {
                lblValidDatCoc.Text = "Số tiền đặt cọc vượt quá Mức học phí khóa học .";
            }
            else
            {
                if (this.kus_hocvien.kus_UpdateHocVen(hocvien.HocVienID, basicinfo.InfoID, diachithuongtru, diachitamtru, email, dienthoai, hotenPH, nghenghiep, phonePH, 1))
                {
                    if (NewHocVienMore(hocvien.HocVienID))
                    {

                        List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(MaKhoaHoc);
                        nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
                        //Kiem tra ghi danh
                        List<kus_GhiDanh> lstCheckGD = kus_ghidanh.LstCheckHV_GhiDanh(hocvien.HocVienID, khoahoc.ID);
                        kus_GhiDanh check_ghidanh = lstCheckGD.FirstOrDefault();
                        if (check_ghidanh != null)
                        {
                            Response.Write("<script>alert('Học Viên " + hocvien.HocVienCode + " Đã Ghi Danh khóa " + MaKhoaHoc + " - " + khoahoc.TenKhoaHoc + " !')</script>");
                        }
                        else
                        {
                            string ghichu = txtGhiChu.Text;

                            if (this.kus_ghidanh.GhiDanhMoi(hocvien.HocVienID, khoahoc.ID, NVGhiDanh, ghichu, datcoc))
                            {
                                Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/QLGhiDanh.aspx");
                            }
                            else
                            {
                                Response.Write("<script>alert('Ghi danh thất bại ! Lỗi kết nối CSDL !')</script>");
                            }
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            lblPageIsValid.Text = ex.ToString();
        }
    }
    private Boolean NewHocVienMore(int HocVienID)
    {
        hocvienmoreinfo = new kus_HocVienMoreInFoBLL();
        string HVGioiThieu = txtHVGioiThieu.Text;
        string TDHocVAn = "";
        if (chkMauGiao.Checked == true)
            TDHocVAn = "Mẫu giáo";
        else if (chkTieuHoc.Checked == true)
            TDHocVAn = "Tiểu học";
        else if (chkTHCS.Checked == true)
            TDHocVAn = "Trung học cơ sở";
        else if (chkTHPT.Checked == true)
            TDHocVAn = "Trung học phổ thông";
        else
            TDHocVAn = "Đại Học - Cao Đẳng";

        string tentruong = txtTenTruong.Text;
        string CCTA = txtCCKHac.Text;
        if (chkCC1.Checked == true)
            CCTA += ";" + "Starters";
        if (chkCC2.Checked == true)
            CCTA += ";" + "Movers";
        if (chkCC3.Checked == true)
            CCTA += ";" + "Flyers";
        if (chkCC4.Checked == true)
            CCTA += ";" + "KET";
        if (chkCC5.Checked == true)
            CCTA += ";" + "PET";
        if (chkCC6.Checked == true)
            CCTA += ";" + "FCE";

        string BTT = txtBTTKHac.Text;
        if (chkBTT1.Checked == true)
            BTT += ";" + "Báo chí";
        if (chkBTT2.Checked == true)
            BTT += ";" + "Internet";
        if (chkBTT3.Checked == true)
            BTT += ";" + "Bạn bè";
        if (chkBTT4.Checked == true)
            BTT += ";" + "Website";
        if (chkBTT5.Checked == true)
            BTT += ";" + "Trực tiếp tại trung tâm";



        return this.hocvienmoreinfo.kus_NewHocVienMoreInFo(HocVienID, HVGioiThieu, TDHocVAn, tentruong, CCTA, BTT);

    }

    protected void dlChoseLoaiGD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dlChoseLoaiGD.SelectedValue == "0")
        {
            panelGhiDanhMoi.Visible = true;
            panelDaGhiDanh.Visible = false;
        }
        else
        {
            panelGhiDanhMoi.Visible = false;
            panelDaGhiDanh.Visible = true;
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchHocVienCode(string prefixText, int count)
    {
        kus_HocVienBLL kus_hocvien = new kus_HocVienBLL();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["connectionStrCon"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from kus_HocVien where HocVienCode like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> lstHVCode = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lstHVCode.Add(sdr["HocVienCode"].ToString());
                    }
                }
                conn.Close();
                return lstHVCode;
            }
        }
    }

    protected void btnGhiDanhAvailalble_Click(object sender, EventArgs e)
    {
        //kus_lophoc = new kus_LopHocBLL();
        nc_khoahoc = new nc_KhoaHocBLL();
        kus_hocvien = new kus_HocVienBLL();
        userprofile = new UserProfileBLL();
        employees = new EmployeesBLL();
        kus_ghidanh = new kus_GhiDanhBLL();
        string MaKhoaHoc = Request.QueryString["makhoahoc"];
        List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(MaKhoaHoc);
        nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
        List<kus_HocVien> lstHV = kus_hocvien.getHocVienWithMaHV(txtHocVienCode.Text);
        kus_HocVien hocvien = lstHV.FirstOrDefault();
        if (hocvien == null)
        {
            Response.Write("<script>alert('Mã Học Viên " + txtHocVienCode.Text + " không tồn tại. Vui lòng thử lại !')</script>");
        }
        else
        {
            //Kiem tra ghi danh
            List<kus_GhiDanh> lstCheckGD = kus_ghidanh.LstCheckHV_GhiDanh(hocvien.HocVienID, khoahoc.ID);
            kus_GhiDanh check_ghidanh = lstCheckGD.FirstOrDefault();
            if (check_ghidanh != null)
            {
                Response.Write("<script>alert('Học Viên " + hocvien.HocVienCode + " Đã Ghi Danh lớp " + MaKhoaHoc + " - " + khoahoc.TenKhoaHoc + " !')</script>");
                return;
            }
            else
            {
                List<UserProfile> lstprofile = userprofile.getUserProfileWithID(Session.GetCurrentUser().UserID);
                UserProfile userpro = lstprofile.FirstOrDefault();
                List<Employees> lstemployee = employees.getEmpWithProfileId(userpro.ProfileID);
                Employees emp = lstemployee.FirstOrDefault();
                int NVGhiDanh = emp.EmployeesID;
                string ghichu = txtGhiChuAvailable.Text;
                int datcoc = (string.IsNullOrEmpty(txtDatCocAvailable.Text) || string.IsNullOrWhiteSpace(txtDatCocAvailable.Text)) ? 0 : Convert.ToInt32(txtDatCocAvailable.Text);
                if (this.kus_ghidanh.GhiDanhMoi(hocvien.HocVienID, khoahoc.ID, NVGhiDanh, ghichu, datcoc))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/QLGhiDanh.aspx");
                }
                else
                {
                    Response.Write("<script>alert('Ghi danh thất bại ! Lỗi kết nối CSDL !')</script>");
                }
            }
        }
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPhieuCode(string prefixText, int count)
    {
        kus_HocVienBLL kus_hocvien = new kus_HocVienBLL();

        using (SqlConnection conn = new SqlConnection())
        {
            conn.ConnectionString = ConfigurationManager
                    .ConnectionStrings["connectionStrCon"].ConnectionString;
            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.CommandText = "select * from REGISTRATION_FORM_ADVISORY where FullName like @SearchText + '%'";
                cmd.Parameters.AddWithValue("@SearchText", prefixText);
                cmd.Connection = conn;
                conn.Open();
                List<string> lstHVCode = new List<string>();
                using (SqlDataReader sdr = cmd.ExecuteReader())
                {
                    while (sdr.Read())
                    {
                        lstHVCode.Add(sdr["FullName"].ToString());
                    }
                }
                conn.Close();
                return lstHVCode;
            }
        }
    }

    protected void txtPhieuTvInfor_TextChanged(object sender, EventArgs e)
    {
        try
        {
            registration_form = new REGISTRATION_FORM_ADVISORY_BLL();
            REGISTRATION_FORM_ADVISORY phieutv = registration_form.GET_REGISTRATION_FORM_ADVISORY_ByFullName(txtPhieuTvInfor.Text).FirstOrDefault();
            if (phieutv != null)
            {
                txtLastNameHV.Text = phieutv.FullName.Substring(0, phieutv.FullName.LastIndexOf(" "));
                txtFirstNameHV.Text = phieutv.FullName.Substring(phieutv.FullName.LastIndexOf(" ") + 1);
                txtNgaySinh.Text = phieutv.Birthday.ToString("dd-MM-yyyy");
                if (phieutv.Sex == 1)
                {
                    rdformnam.Checked = true;
                }
                else
                {
                    if (phieutv.Sex == 2)
                    {
                        rdformnu.Checked = true;
                    }
                    else
                    {
                        rdformnam.Checked = false;
                        rdformnu.Checked = false;
                    }
                }
                txtEmail.Text = phieutv.Email;
                txtPhoneHV.Text = phieutv.Phone;
                txtDCTamTru.Text = phieutv.Address_form;
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, "Thông tin phiếu tư vấn không tồn tại hoặc không chính xác. Vui lòng nhập lại thông tin chính xác !", alertPageValid, lblPageValid);
            //this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    private void ClearHocvienInfo()
    {
        lblMaHocVienB.Text = "";
        lblEmailHocVienB.Text = "";
        lblDienThoaiHocVienB.Text = "";
        imgHocVienB.Src = "../images/default_images.jpg";
        lblHoTenHocVienB.Text = "";
        lblBirthdayHocVienB.Text = "";
        lblSexHocVienB.Text = "";
    }
    protected void txtHocVienCode_TextChanged(object sender, EventArgs e)
    {
        try
        {

            kus_hocvien = new kus_HocVienBLL();
            images = new ImagesBLL();
            customerbasicinfo = new CustomerBasicInfoBLL();
            this.ClearHocvienInfo();
            kus_HocVien hocvien = kus_hocvien.getHocVienWithMaHV(txtHocVienCode.Text).FirstOrDefault();
            if (hocvien != null)
            {
                lblMaHocVienB.Text = hocvien.HocVienCode;
                lblEmailHocVienB.Text = hocvien.Email;
                lblDienThoaiHocVienB.Text = hocvien.DienThoai;

                Images img = images.getImagesWithId(hocvien.ImgID).FirstOrDefault();
                if(img!=null)
                {
                    imgHocVienB.Src = "../" + img.ImagesUrl;
                }
                CustomerBasicInfo basicinfo = customerbasicinfo.GetCusBasicInfoWithInfoId(hocvien.InfoID).FirstOrDefault();
                if(basicinfo != null)
                {
                    lblHoTenHocVienB.Text = basicinfo.LastName + " " + basicinfo.FirstName;
                    lblBirthdayHocVienB.Text = basicinfo.Birthday.ToString("dd-MM-yyyy");
                    lblSexHocVienB.Text = (basicinfo.Sex == 1) ? "Nam" : (basicinfo.Sex == 2) ? "Nữ" : "null";
                }
            }

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}