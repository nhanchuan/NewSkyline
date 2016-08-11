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
using System.Globalization;
using System.Text.RegularExpressions;
using System.Drawing.Imaging;
using System.IO;
using System.Drawing;

public partial class kus_admin_ChiTietHocVien : BasePage
{
    kus_HocVienBLL kus_hocvien;
    kus_HocVienMoreInFoBLL hocvienmoreinfo;
    CustomerBasicInfoBLL customerbasicinfo;
    UserProfileBLL userprofile;
    EmployeesBLL employees;
    kus_GhiDanhBLL kus_ghidanh;
    ImagesBLL images;
    UserAccountsBLL useraccount;
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
                if (!check_permiss(ac.UserID, FunctionName.ThongTinHocVien))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    string MaHV = Request.QueryString["MaHocVien"];
                    if (!Checkquerystring(MaHV))
                    {
                        Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/QLHocVien.aspx");
                    }
                    else
                    {
                        this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                        this.loadHocVienInfor(MaHV);
                        this.load_dlUserUploadImg();
                        dlimgtype.Items.Insert(0, new ListItem("-- Hình ảnh tải lên bởi thành viên --", "0"));
                        dlimgtype.Items.FindByValue(ac.UserID.ToString()).Selected = true;
                        this.load_rpLstImg();
                    }
                }
            }
        }
    }
    private Boolean Checkquerystring(string code)
    {
        bool check = true;
        kus_hocvien = new kus_HocVienBLL();
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
        {
            check = false;
        }
        else
        {
            List<kus_HocVien> lstHV = kus_hocvien.getHocVienWithMaHV(code);
            kus_HocVien hocvien = lstHV.FirstOrDefault();
            if (hocvien == null)
            {
                check = false;
            }
            else
            {
                check = true;
            }
        }
        return check;
    }
    protected void loadHocVienInfor(string code)
    {
        kus_hocvien = new kus_HocVienBLL();
        DataTable tb = kus_hocvien.kus_HocVienInfor(code);
        foreach (DataRow r in tb.Rows)
        {
            lbltitleHocVien.Text = (string.IsNullOrEmpty(r["HocVienCode"].ToString())) ? "" : (string)r["HocVienCode"];
            txtLastNameHV.Text = (string.IsNullOrEmpty(r["LastName"].ToString())) ? "" : (string)r["LastName"];
            txtFirstNameHV.Text = (string.IsNullOrEmpty(r["FirstName"].ToString())) ? "" : (string)r["FirstName"];
            txtNgaySinh.Text = (string.IsNullOrEmpty(r["Birthday"].ToString())) ? "" : ((DateTime)r["Birthday"]).ToString("dd-MM-yyyy");
            txtNoiSinhHV.Text = (string.IsNullOrEmpty(r["BirthPlace"].ToString())) ? "" : (string)r["BirthPlace"];
            txtSoCMNDHV.Text = (string.IsNullOrEmpty(r["IdentityCard"].ToString())) ? "" : (string)r["IdentityCard"];
            txtNgayCapCMND.Text = (string.IsNullOrEmpty(r["DateOfIdentityCard"].ToString())) ? "" : ((DateTime)r["DateOfIdentityCard"]).ToString("dd-MM-yyyy");
            txtNoiCapCMND.Text = (string.IsNullOrEmpty(r["PlaceOfIdentityCard"].ToString())) ? "" : (string)r["PlaceOfIdentityCard"];
            int sex = (string.IsNullOrEmpty(r["Sex"].ToString())) ? 0 : (int)r["Sex"];
            if (sex == 1)
            {
                rdformnam.Checked = true;
            }
            else
            {
                if (sex == 2)
                {
                    rdformnu.Checked = true;
                }
                else
                {
                    rdformnam.Checked = false;
                    rdformnu.Checked = false;
                }
            }
            txtDCThuongTru.Text = (string.IsNullOrEmpty(r["DCThuongTru"].ToString())) ? "" : (string)r["DCThuongTru"];
            txtDCTamTru.Text = (string.IsNullOrEmpty(r["DCTamTru"].ToString())) ? "" : (string)r["DCTamTru"];
            txtEmail.Text = (string.IsNullOrEmpty(r["Email"].ToString())) ? "" : (string)r["Email"];
            txtPhoneHV.Text = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
            txtHoTenPH.Text = (string.IsNullOrEmpty(r["HoTenPH"].ToString())) ? "" : (string)r["HoTenPH"];
            txtNgheNghiepPH.Text = (string.IsNullOrEmpty(r["NgheNghiep"].ToString())) ? "" : (string)r["NgheNghiep"];
            txtDienThoaiPH.Text = (string.IsNullOrEmpty(r["PhonePhuHuynh"].ToString())) ? "" : (string)r["PhonePhuHuynh"];
            txtHVGioiThieu.Text = (string.IsNullOrEmpty(r["HVGioiThieu"].ToString())) ? "" : (string)r["HVGioiThieu"];

            string trinhdoHV = (string.IsNullOrEmpty(r["TrinhDoHocVan"].ToString())) ? "" : (string)r["TrinhDoHocVan"];
            switch (trinhdoHV)
            {
                case "Đại Học - Cao Đẳng":
                    chkDH.Checked = true;
                    break;
                case "Trung học phổ thông":
                    chkTHPT.Checked = true;
                    break;
                case "Trung học cơ sở":
                    chkTHCS.Checked = true;
                    break;
                case "Tiểu học":
                    chkTieuHoc.Checked = true;
                    break;
                case "Mẫu giáo":
                    chkMauGiao.Checked = true;
                    break;
            }
            txtTenTruong.Text = (string.IsNullOrEmpty(r["TenTruong"].ToString())) ? "" : (string)r["TenTruong"];
            string CCTiengAnh = (string.IsNullOrEmpty(r["CCTiengAnh"].ToString())) ? "" : (string)r["CCTiengAnh"];
            if (CCTiengAnh.Contains("Starters"))
                chkCC1.Checked = true;
            if (CCTiengAnh.Contains("Movers"))
                chkCC2.Checked = true;
            if (CCTiengAnh.Contains("Flyers"))
                chkCC3.Checked = true;
            if (CCTiengAnh.Contains("KET"))
                chkCC4.Checked = true;
            if (CCTiengAnh.Contains("PET"))
                chkCC5.Checked = true;
            if (CCTiengAnh.Contains("FCE"))
                chkCC6.Checked = true;


            string[] lstCCTiengAnh = CCTiengAnh.Split(';');
            string CCKHac = CCTiengAnh.Replace(";", "");
            for (int i = 0; i < lstCCTiengAnh.Length; i++)
            {

                if (lstCCTiengAnh[i] == "Starters")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "Movers")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "Flyers")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "KET")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "PET")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                if (lstCCTiengAnh[i] == "FCE")
                    CCKHac = CCKHac.Replace(lstCCTiengAnh[i], "");
                txtCCKHac.Text = CCKHac;
            }

            string BietTT = (string.IsNullOrEmpty(r["BietThongTin"].ToString())) ? "" : (string)r["BietThongTin"];
            if (BietTT.Contains("Báo chí"))
                chkBTT1.Checked = true;
            if (BietTT.Contains("Internet"))
                chkBTT2.Checked = true;
            if (BietTT.Contains("Bạn bè"))
                chkBTT3.Checked = true;
            if (BietTT.Contains("Website"))
                chkBTT4.Checked = true;
            if (BietTT.Contains("Trực tiếp tại trung tâm"))
                chkBTT5.Checked = true;
            string[] lstBietTT = BietTT.Split(';');
            string strBietTT = BietTT.Replace(";", "");
            for (int j = 0; j < lstBietTT.Length; j++)
            {
                if (lstBietTT[j] == "Báo chí")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Internet")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Bạn bè")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Website")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");
                if (lstBietTT[j] == "Trực tiếp tại trung tâm")
                    strBietTT = strBietTT.Replace(lstBietTT[j], "");

                txtBTTKHac.Text = strBietTT;
            }
            txtGhiChu.Text = (string.IsNullOrEmpty(r["GhiChu"].ToString())) ? "" : (string)r["GhiChu"];
            imgCusprofile.Src = (string.IsNullOrEmpty(r["ImagesUrl"].ToString())) ? "../images/default_images.jpg" : "../" + (string)r["ImagesUrl"];

            if ((int)r["HocVienStatus"] == 1)
            {
                dlTinhtrangHocVien.Items.FindByValue("1").Selected = true;
            }
            else
            {
                dlTinhtrangHocVien.Items.FindByValue("0").Selected = true;
            }
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

        return this.hocvienmoreinfo.kus_UpdateNewHocVienMoreInFo(HocVienID, HVGioiThieu, TDHocVAn, tentruong, CCTA, BTT);

    }
    protected void btnUpdateInfor_ServerClick(object sender, EventArgs e)
    {


        try
        {
            customerbasicinfo = new CustomerBasicInfoBLL();
            kus_hocvien = new kus_HocVienBLL();
            userprofile = new UserProfileBLL();
            employees = new EmployeesBLL();
            kus_ghidanh = new kus_GhiDanhBLL();
            //string FileCode = RandomName + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();

            string HVCode = Request.QueryString["MaHocVien"];
            List<kus_HocVien> lstHocVien = kus_hocvien.getHocVienWithMaHV(HVCode);
            kus_HocVien hocvien = lstHocVien.FirstOrDefault();

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

            //this.customerbasicinfo.CreateBasicCodeInfo(FileCode);
            this.customerbasicinfo.UpdateCustomerBasicInfo(hocvien.InfoID, firstname, lastname, "", birthday, noisinh, sex, socmnd, NgayCapCMND, noicap);
            //string HocvienCode = RandomName + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString();
            //this.kus_hocvien.CreateCodeHocVien(HocvienCode);

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
            int NVGoiThieu = emp.EmployeesID;
            if (this.kus_hocvien.kus_UpdateHocVen(hocvien.HocVienID, hocvien.InfoID, diachithuongtru, diachitamtru, email, dienthoai, hotenPH, nghenghiep, phonePH, Convert.ToInt32(dlTinhtrangHocVien.SelectedValue), hocvien.AvailableBalances))
            {
                if (NewHocVienMore(hocvien.HocVienID))
                {
                    string ghichu = txtGhiChu.Text;
                    //int datcoc = (string.IsNullOrEmpty(txtDatCoc.Text) || string.IsNullOrWhiteSpace(txtDatCoc.Text)) ? 0 : Convert.ToInt32(txtDatCoc.Text);
                    List<kus_GhiDanh> lstGhiDanh = kus_ghidanh.getListGDWithHocVien(hocvien.HocVienID);
                    kus_GhiDanh ghidanh = lstGhiDanh.FirstOrDefault();
                    if (ghidanh == null)
                    {
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        if (this.kus_ghidanh.UpdateGhichu(hocvien.HocVienID, ghichu))
                        {
                            Response.Redirect(Request.Url.AbsoluteUri);
                        }
                        else
                        {
                            Response.Write("<script>alert('Cập nhật thất bại ! Lỗi kết nối CSDL !')</script>");
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
    protected void btnEnableTT_ServerClick(object sender, EventArgs e)
    {
        dlTinhtrangHocVien.Enabled = true;
    }
    //=============IMAGES=========================================================================
    protected void load_dlUserUploadImg()
    {
        useraccount = new UserAccountsBLL();
        dlimgtype.DataSource = useraccount.getAllUseraccount();
        dlimgtype.DataValueField = "UserID";
        dlimgtype.DataTextField = "UserName";
        dlimgtype.DataBind();
    }
    protected void dlimgtype_SelectedIndexChanged(object sender, EventArgs e)
    {
        images = new ImagesBLL();
        rpLstImg.DataSource = new ObjectDataSource();
        rpLstImg.DataSource = (dlimgtype.SelectedValue == "0") ? images.getImagesWithType(1009) : images.GetImagesTypeAndUserUpload(1009, int.Parse(dlimgtype.SelectedValue));
        rpLstImg.DataBind();
    }
    protected void load_rpLstImg()
    {
        images = new ImagesBLL();
        rpLstImg.DataSource = images.getImagesWithType(1009);
        rpLstImg.DataBind();
    }
    protected int ImagesID(string filename)
    {
        int ImID = 0;
        images = new ImagesBLL();

        if (string.IsNullOrWhiteSpace(filename))
        {
            ImID = 0;
        }
        else
        {
            ImID = images.ImagesID(filename);
        }
        return ImID;
    }
    private void UpdateImages(int hocvienID, int ImgId)
    {
        kus_hocvien = new kus_HocVienBLL();
        if (this.kus_hocvien.UpdateHocVienImage(hocvienID, ImgId))
        {
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        else
        {
            Response.Write("<script>alert('Change Image False !')</script>");
            return;
        }
    }
    protected void btnchangeImgCusPro_Click(object sender, EventArgs e)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        kus_hocvien = new kus_HocVienBLL();
        string http = "http://" + Request.Url.Authority + "/";
        txtPostImgTemp.Text = HiddenimgSelect.Value.Remove(0, http.Length);
        imgCusprofile.Src = "../" + HiddenimgSelect.Value.Remove(0, http.Length);

        string HVCode = Request.QueryString["MaHocVien"];
        List<kus_HocVien> lstHocVien = kus_hocvien.getHocVienWithMaHV(HVCode);
        kus_HocVien hocvien = lstHocVien.FirstOrDefault();
        UpdateImages(hocvien.HocVienID, ImagesID(txtPostImgTemp.Text));
    }
    protected void btnuploadImg_ServerClick(object sender, EventArgs e)
    {
        //ImgEditPC.Src = txtuploadImgTemp.Text;
        UserAccounts ac = Session.GetCurrentUser();
        kus_hocvien = new kus_HocVienBLL();
        kus_ghidanh = new kus_GhiDanhBLL();
        images = new ImagesBLL();
        string HVCode = Request.QueryString["MaHocVien"];
        List<kus_HocVien> lstHocVien = kus_hocvien.getHocVienWithMaHV(HVCode);
        kus_HocVien hocvien = lstHocVien.FirstOrDefault();

        string dateString = DateTime.Now.ToString("MM-dd-yyyy");
        string fileName = Path.GetFileName(fileUploadImgPost.PostedFile.FileName);
        ImageCodecInfo jgpEncoder = null;
        string str_image = "";
        string fileExtension = "";
        if (!string.IsNullOrEmpty(fileName))
        {
            fileExtension = Path.GetExtension(fileName);
            str_image = "Anh-Van-Hoi-Anh-My-" + dateString + "-" + hocvien.HocVienCode + RandomName + fileExtension;
            string pathToSave = Server.MapPath("../images/Upload/Anh-Hoc-Vien/") + str_image;
            //file.SaveAs(pathToSave);
            System.Drawing.Image image = System.Drawing.Image.FromStream(fileUploadImgPost.FileContent);
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Gif);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Bmp.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Bmp);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Png);
            else
                throw new System.ArgumentException("Invalid File Type");
            Bitmap bmp1 = new Bitmap(fileUploadImgPost.FileContent);
            Encoder myEncoder = Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 50L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(pathToSave, jgpEncoder, myEncoderParameters);
            this.images.InsertImages(str_image, "images/Upload/Anh-Hoc-Vien/" + str_image, 1009, ac.UserID);
            txtPostImgTemp.Text = "images/Upload/Anh-Hoc-Vien/" + str_image;
            imgCusprofile.Src = "../images/Upload/Anh-Hoc-Vien/" + str_image;
            this.load_rpLstImg();
            this.UpdateImages(hocvien.HocVienID, ImagesID(txtPostImgTemp.Text));
        }
        else
        {
            Response.Write("<script>alert('Upload Image False !')</script>");
            return;
        }
    }
}