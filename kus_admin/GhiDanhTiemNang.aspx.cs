using System;
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

public partial class kus_admin_GhiDanhTiemNang : BasePage
{
    REGISTRATION_FORM_ADVISORY_BLL registration_form;
    nc_LoaiCTDaoTaoBLL nc_loaictdaotao;
    nc_ChuongTrinhDaoTaoBLL nc_chuongtrinhdaotao;
    nc_LopHocBLL lophocs;
    nc_KhoaHocBLL nc_khoahoc;
    kus_GhiDanhBLL kus_ghidanh;
    kus_GhiDanhTiemNamgBLL ghidanhtiemnang;
    kus_HocVienBLL kus_hocvien;
    kus_HocVienMoreInFoBLL hocvienmoreinfo;
    ImagesBLL images;
    CustomerBasicInfoBLL customerbasicinfo;

    UserProfileBLL userprofile;
    EmployeesBLL employees;

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
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    this.load_dlThuocLoaiChuongTrinh();
                    panelGhiDanhMoi.Visible = true;
                    panelDaGhiDanh.Visible = false;
                    btnAddNewHV.CssClass = "btn btn-primary";
                    btnAddOldHV.CssClass = "btn btn-primary display-none";
                }
            }
        }
    }
    private void load_dlThuocLoaiChuongTrinh()
    {
        nc_loaictdaotao = new nc_LoaiCTDaoTaoBLL();
        this.load_DropdownList(dlThuocLoaiChuongTrinh, nc_loaictdaotao.getListLoaiCTDaoTao(), "TenChuongTrinh", "ID");
        dlThuocLoaiChuongTrinh.Items.Insert(0, new ListItem("-- Chọn loại chương trình --", "0"));
    }
    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> SearchPhieuTuVanInfo(string prefixText, int count)
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

    [System.Web.Script.Services.ScriptMethod()]
    [System.Web.Services.WebMethod]
    public static List<string> AutoCompleteCode(string prefixText, int count)
    {
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
    protected void txtPhieuTvInfor_TextChanged(object sender, EventArgs e)
    {
        try
        {
            registration_form = new REGISTRATION_FORM_ADVISORY_BLL();
            REGISTRATION_FORM_ADVISORY phieutv = registration_form.GET_REGISTRATION_FORM_ADVISORY_ByFullName(txtPhieuTvInfor.Text).FirstOrDefault();
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
            }
            txtEmail.Text = phieutv.Email;
            txtPhoneHV.Text = phieutv.Phone;

            txtDCTamTru.Text = phieutv.Address_form;
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, "Thông tin phiếu tư vấn không tồn tại hoặc không chính xác. Vui lòng nhập lại thông tin chính xác !", alertPageValid, lblPageValid);
        }
    }
    protected void dlThuocLoaiChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        nc_chuongtrinhdaotao = new nc_ChuongTrinhDaoTaoBLL();
        this.load_DropdownList(dlChuongTrinh, nc_chuongtrinhdaotao.GetChuongTrinhDaoTaoWithLoai(Convert.ToInt32(dlThuocLoaiChuongTrinh.SelectedValue)), "TenChuongTrinh", "ID");
        dlChuongTrinh.Items.Insert(0, new ListItem("-- Chọn chương trình --", "0"));
    }
    protected void dlChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        lophocs = new nc_LopHocBLL();
        gwSelectClass.DataSource = lophocs.GetTBByChuongTtrinh(Convert.ToInt32(dlChuongTrinh.SelectedValue));
        gwSelectClass.DataBind();
    }
    protected void dlChoseLoaiGD_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dlChoseLoaiGD.SelectedValue == "0")
        {
            panelGhiDanhMoi.Visible = true;
            panelDaGhiDanh.Visible = false;
            btnAddNewHV.CssClass = "btn btn-primary";
            btnAddOldHV.CssClass = "btn btn-primary display-none";
        }
        else
        {
            panelGhiDanhMoi.Visible = false;
            panelDaGhiDanh.Visible = true;
            btnAddNewHV.CssClass = "btn btn-primary display-none";
            btnAddOldHV.CssClass = "btn btn-primary";
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
            kus_ghidanh = new kus_GhiDanhBLL();
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
                if (img != null)
                {
                    imgHocVienB.Src = "../" + img.ImagesUrl;
                }
                CustomerBasicInfo basicinfo = customerbasicinfo.GetCusBasicInfoWithInfoId(hocvien.InfoID).FirstOrDefault();
                if (basicinfo != null)
                {
                    lblHoTenHocVienB.Text = basicinfo.LastName + " " + basicinfo.FirstName;
                    lblBirthdayHocVienB.Text = basicinfo.Birthday.ToString("dd-MM-yyyy");
                    lblSexHocVienB.Text = (basicinfo.Sex == 1) ? "Nam" : (basicinfo.Sex == 2) ? "Nữ" : "null";
                }

                DataTable tb = kus_ghidanh.TBGhiDanhByHocVienID(hocvien.HocVienID);
                if (tb.Rows.Count <= 0)
                {
                    lblMessageKhoaHoc.CssClass = "label label-warning";
                    gwChonLopGhiDanh.DataSource = tb;
                    gwChonLopGhiDanh.DataBind();
                }
                else
                {
                    lblMessageKhoaHoc.CssClass = "label label-warning display-none";
                    gwChonLopGhiDanh.DataSource = tb;
                    gwChonLopGhiDanh.DataBind();
                }
            }

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
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
    protected void btnAddNewHV_Click(object sender, EventArgs e)
    {
        try
        {
            customerbasicinfo = new CustomerBasicInfoBLL();
            kus_hocvien = new kus_HocVienBLL();
            kus_ghidanh = new kus_GhiDanhBLL();
            ghidanhtiemnang = new kus_GhiDanhTiemNamgBLL();
            userprofile = new UserProfileBLL();
            employees = new EmployeesBLL();

            lophocs = new nc_LopHocBLL();

            string FileCode = RandomName + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

            string firstname = txtFirstNameHV.Text;
            string lastname = txtLastNameHV.Text;
            string ngaysinh = txtNgaySinh.Text;
            DateTime birthday;
            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            if (string.IsNullOrWhiteSpace(ngaysinh) || DateTime.TryParseExact(ngaysinh, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out birthday) || getday(ngaysinh) == "" || getmonth(ngaysinh) == "" || getyear(ngaysinh) == "")
            {
                birthday = Convert.ToDateTime("12/12/1900");
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
                NgayCapCMND = Convert.ToDateTime("12/12/1900");
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
            UserProfile userpro = userprofile.getUserProfileWithID(Session.GetCurrentUser().UserID).FirstOrDefault();
            Employees emp = employees.getEmpWithProfileId(userpro.ProfileID).FirstOrDefault();
            kus_HocVien hocvien = kus_hocvien.getHVWithCode(HocvienCode).FirstOrDefault();
            int datcoc = (txtDatCoc.Text == "") ? 0 : Convert.ToInt32(txtDatCoc.Text);

            if (gwSelectClass.SelectedRow == null)
            {
                this.AlertPageValid(true, "Chưa chọn Lớp Ghi Danh !", alertPageValid, lblPageValid);
            }
            else
            {
                if (this.kus_hocvien.kus_UpdateHocVen(hocvien.HocVienID, basicinfo.InfoID, diachithuongtru, diachitamtru, email, dienthoai, hotenPH, nghenghiep, phonePH, 1, datcoc))
                {
                    if (NewHocVienMore(hocvien.HocVienID))
                    {
                        string ghichu = txtGhiChu.Text;


                        int LopID = Convert.ToInt32((gwSelectClass.SelectedRow.FindControl("lblID") as Label).Text);
                        if (this.ghidanhtiemnang.Newkus_GhiDanhTiemNamg(hocvien.HocVienID, LopID, emp.EmployeesID, ghichu, true))
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
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void btnAddOldHV_Click(object sender, EventArgs e)
    {
        try
        {
            ghidanhtiemnang = new kus_GhiDanhTiemNamgBLL();
            kus_hocvien = new kus_HocVienBLL();

            string HocVienCode = txtHocVienCode.Text;

            lophocs = new nc_LopHocBLL();
            kus_hocvien = new kus_HocVienBLL();
            userprofile = new UserProfileBLL();
            employees = new EmployeesBLL();


            kus_HocVien hocvien = kus_hocvien.getHocVienWithMaHV(txtHocVienCode.Text).FirstOrDefault();



            if (hocvien == null)
            {
                Response.Write("<script>alert('Mã Học Viên " + txtHocVienCode.Text + " không tồn tại. Vui lòng thử lại !')</script>");
            }
            else
            {
                if (gwChonLopGhiDanh.SelectedRow != null)
                {
                    this.AlertPageValid(true, "Chưa chọn Lớp Ghi Danh !", alertPageValid, lblPageValid);
                }
                else
                {
                    //Kiem tra ghi danh
                    int LopID = Convert.ToInt32((gwSelectClass.SelectedRow.FindControl("lblID") as Label).Text);
                    kus_GhiDanhTiemNamg check_ghidanh = ghidanhtiemnang.ListByHocVienIDandLopHoc(hocvien.HocVienID, LopID).FirstOrDefault();
                    if (check_ghidanh != null)
                    {
                        this.AlertPageValid(true, "Học Viên" + hocvien.HocVienCode + " Đã Ghi Danh lớp này ! ", alertPageValid, lblPageValid);
                        return;
                    }
                    else
                    {
                        UserProfile userpro = userprofile.getUserProfileWithID(Session.GetCurrentUser().UserID).FirstOrDefault();
                        Employees emp = employees.getEmpWithProfileId(userpro.ProfileID).FirstOrDefault();
                        int NVGhiDanh = emp.EmployeesID;
                        string ghichu = txtGhiChuAvailable.Text;
                        int datcoc = (string.IsNullOrEmpty(txtDatCocAvailable.Text) || string.IsNullOrWhiteSpace(txtDatCocAvailable.Text)) ? 0 : Convert.ToInt32(txtDatCocAvailable.Text);



                        //Update Hoc vien AvailableBalances
                        this.kus_hocvien.UpdateAvailableBalancesByHocVienID(hocvien.HocVienID, hocvien.AvailableBalances + datcoc);


                        if (this.ghidanhtiemnang.Newkus_GhiDanhTiemNamg(hocvien.HocVienID, LopID, NVGhiDanh, ghichu, true))
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
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}