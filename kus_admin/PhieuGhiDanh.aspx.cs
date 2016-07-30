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
using System.IO;
using System.Drawing.Imaging;
using System.Drawing;

public partial class kus_admin_PhieuGhiDanh : BasePage
{
    kus_GhiDanhBLL kus_ghidanh;
    kus_LichHocBLL kus_lichhoc;
    kus_BienLaiBLL kus_bienlai;
    UserAccountsBLL useraccount;
    ImagesBLL images;
    kus_HocVienBLL kus_hocvien;
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
                if (!check_permiss(ac.UserID, FunctionName.PhieuGhiDanh))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    string MaGD = Request.QueryString["MaGhiDanh"];
                    if (!Checkquerystring(MaGD))
                    {
                        Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/QLGhiDanh.aspx");
                    }
                    else
                    {
                        this.load_PhieuGDInfor(MaGD);
                        this.load_dlUserUploadImg();
                        dlimgtype.Items.Insert(0, new ListItem("-- Hình ảnh tải lên bởi thành viên --", "0"));
                        dlimgtype.Items.FindByValue(ac.UserID.ToString()).Selected = true;
                        this.load_rpLstImg();
                        txtCash.Text = "0";
                        txtTLGiamHP.Text = "0";
                    }
                }
            }
        }
    }
    private Boolean Checkquerystring(string code)
    {
        bool check = true;
        kus_ghidanh = new kus_GhiDanhBLL();
        if (string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code))
        {
            check = false;
        }
        else
        {
            List<kus_GhiDanh> lstGD = kus_ghidanh.getListGDCode(code);
            kus_GhiDanh ghidanh = lstGD.FirstOrDefault();
            if (ghidanh == null)
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
    private void load_PhieuGDInfor(string code)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        DataTable tb = kus_ghidanh.kus_getTTPhieuGhiDanh(code);
        foreach (DataRow r in tb.Rows)
        {
            lblPhieuGhiDanh.Text = (string.IsNullOrEmpty(r["GhiDanhCode"].ToString())) ? "" : (string)r["GhiDanhCode"];
            lblMaGhiDanh.Text = (string.IsNullOrEmpty(r["GhiDanhCode"].ToString())) ? "" : (string)r["GhiDanhCode"];
            lblKhoaHoc.Text = (string.IsNullOrEmpty(r["MaKhoaHoc"].ToString())) ? "" : (string)r["MaKhoaHoc"];
            lblKhoaHoc.Text += (string.IsNullOrEmpty(r["TenKhoaHoc"].ToString())) ? "" : " - " + (string)r["TenKhoaHoc"];
            lblHoTenHV.Text = (string.IsNullOrEmpty(r["LastName"].ToString())) ? "" : (string)r["LastName"];
            lblHoTenHV.Text += (string.IsNullOrEmpty(r["FirstName"].ToString())) ? "" : " " + (string)r["FirstName"];
            lblDienThoaiHV.Text = (string.IsNullOrEmpty(r["DienThoai"].ToString())) ? "" : (string)r["DienThoai"];
            lblHocPhi.Text = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "" : ((int)r["MucHocPhi"]).ToString("C", new CultureInfo("vi-VN"));
            lblThoiLuong.Text = (string.IsNullOrEmpty(r["ThoiLuong"].ToString())) ? "" : ((int)r["ThoiLuong"]).ToString();


            lblDatCoc.Text = (string.IsNullOrEmpty(r["DatCoc"].ToString())) ? "0" : ((int)r["DatCoc"]).ToString("C", new CultureInfo("vi-VN"));

            this.load_LichHoc(string.IsNullOrEmpty(r["KhoaHoc"].ToString()) ? 0 : (int)r["KhoaHoc"]);

            lblTinhTrangHP.Text = (string.IsNullOrEmpty(r["RemainFee"].ToString())) ? 0.ToString("C", new CultureInfo("vi-VN")) : ((int)r["RemainFee"]).ToString("C", new CultureInfo("vi-VN"));
            btnmodalDongHP.Visible = true;
            btnXemBienLai.Visible = false;
            lbltitleMaHV.Text = (string.IsNullOrEmpty(r["HocVienCode"].ToString())) ? "" : (string)r["HocVienCode"];
            lbltitleTenHV.Text = (string.IsNullOrEmpty(r["LastName"].ToString())) ? "" : (string)r["LastName"];
            lbltitleTenHV.Text += (string.IsNullOrEmpty(r["FirstName"].ToString())) ? "" : " " + (string)r["FirstName"];
            txtHPThoiLuong.Text = (string.IsNullOrEmpty(r["ThoiLuong"].ToString())) ? "" : ((int)r["ThoiLuong"]).ToString();
            txtHPNgayKG.Text = (string.IsNullOrEmpty(r["NgayKhaiGiang"].ToString())) ? "" : ((DateTime)r["NgayKhaiGiang"]).ToString("dd/MM/yyyy");
            txtHPNgayKT.Text = (string.IsNullOrEmpty(r["NgayKetThuc"].ToString())) ? "" : ((DateTime)r["NgayKetThuc"]).ToString("dd/MM/yyyy");
            txtDongHP.Text = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString("C", new CultureInfo("vi-VN"));
            txtHPDatCoc.Text = (string.IsNullOrEmpty(r["DatCoc"].ToString())) ? "0" : ((int)r["DatCoc"]).ToString("C", new CultureInfo("vi-VN"));

            txtDongHPTemp.Text = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString();

            //AvailableBalances
            txtAvailableBalances.Text = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? "0" : ((int)r["AvailableBalances"]).ToString("C", new CultureInfo("vi-VN"));
            txttempAvailableBalances.Text = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? "0" : ((int)r["AvailableBalances"]).ToString();
            txtMinus.Text = "0";
            txtMinus.MaxLength = (string.IsNullOrEmpty(r["AvailableBalances"].ToString())) ? 0 : (int)r["AvailableBalances"];

            txtHPPhaiDong.Text = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString("C", new CultureInfo("vi-VN"));
            txtHPPhaiDongTemp.Text = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString();
            txtHPBangChu.Text = ReadNumber.ByWords(decimal.Parse((string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString())) + "đồng";

            txtThuKhachHang.Text = "0";
            txtThuKHByWords.Text = ReadNumber.ByWords(decimal.Parse("0")) + "đồng";

            //txtRemainFeee
            txtRemainFeee.Text= (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString("C", new CultureInfo("vi-VN"));
            txtRemainFeeeTemp.Text= (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? "0" : ((int)r["MucHocPhi"]).ToString();
        }
        this.load_ImgHocVien(code);
    }
    private void load_LichHoc(int khoahoc, int daysID, int buoiID, GridView gwLichHoc)
    {
        kus_lichhoc = new kus_LichHocBLL();
        gwLichHoc.DataSource = kus_lichhoc.getkus_LichHocWith_KhoaHoc_Day_Buoi(khoahoc, daysID, buoiID);
        gwLichHoc.DataBind();
    }
    private void load_LichHoc(int LopHocID)
    {
        this.load_LichHoc(LopHocID, 1, 1, gwThu2Sang);
        this.load_LichHoc(LopHocID, 1, 2, gwThu2Chieu);
        this.load_LichHoc(LopHocID, 1, 3, gwThu2Toi);
        this.load_LichHoc(LopHocID, 2, 1, gwThu3Sang);
        this.load_LichHoc(LopHocID, 2, 2, gwThu3Chieu);
        this.load_LichHoc(LopHocID, 2, 3, gwThu3Toi);
        this.load_LichHoc(LopHocID, 3, 1, gwThu4Sang);
        this.load_LichHoc(LopHocID, 3, 2, gwThu4Chieu);
        this.load_LichHoc(LopHocID, 3, 3, gwThu4Toi);
        this.load_LichHoc(LopHocID, 4, 1, gwThu5Sang);
        this.load_LichHoc(LopHocID, 4, 2, gwThu5Chieu);
        this.load_LichHoc(LopHocID, 4, 3, gwThu5Toi);
        this.load_LichHoc(LopHocID, 5, 1, gwThu6Sang);
        this.load_LichHoc(LopHocID, 5, 2, gwThu6Chieu);
        this.load_LichHoc(LopHocID, 5, 3, gwThu6Toi);
        this.load_LichHoc(LopHocID, 6, 1, gwThu7Sang);
        this.load_LichHoc(LopHocID, 6, 2, gwThu7Chieu);
        this.load_LichHoc(LopHocID, 6, 3, gwThu7Toi);
        this.load_LichHoc(LopHocID, 7, 1, gwChuNhatSang);
        this.load_LichHoc(LopHocID, 7, 2, gwChuNhatChieu);
        this.load_LichHoc(LopHocID, 7, 3, gwChuNhatToi);
    }

    private Boolean DiscountPercent(string code)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        int HP = 0;
        int tlg = (txtTLGiamHP.Text == "") ? 0 : Convert.ToInt32(txtTLGiamHP.Text);

        DataTable tb = kus_ghidanh.kus_getTTPhieuGhiDanh(code);
        foreach (DataRow r in tb.Rows)
        {
            HP = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? 0 : ((int)r["MucHocPhi"]);
        }
        if ((HP - (HP * tlg / 100)) < 0 || tlg < 0 || tlg > 100)
        {
            lblwarning.Text = "Mức giảm không âm và không vượt quá 100% . Vui lòng nhập lại ! Thanks !";
            return false;
        }
        else
        {
            lblwarning.Text = "";
            lblNumGiamHP.Text = (HP * tlg / 100).ToString("C", new CultureInfo("vi-VN"));
            txtCash.Text = (HP * tlg / 100).ToString();

            //So tien phai dong
            txtHPPhaiDong.Text = (HP - (HP * tlg / 100)).ToString("C", new CultureInfo("vi-VN"));
            txtHPPhaiDongTemp.Text = (HP - (HP * tlg / 100)).ToString();
            txtHPBangChu.Text = ReadNumber.ByWords(decimal.Parse((HP - (HP * tlg / 100)).ToString())) + "đồng";


            //txtRemainFeee
            txtRemainFeee.Text = (HP - (HP * tlg / 100)).ToString("C", new CultureInfo("vi-VN"));
            txtRemainFeeeTemp.Text = (HP - (HP * tlg / 100)).ToString();

        }
        return true;
    }
    private Boolean DiscountNumric(string code)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        int HP = 0;
        int num = (txtCash.Text == "") ? 0 : Convert.ToInt32(txtCash.Text);

        DataTable tb = kus_ghidanh.kus_getTTPhieuGhiDanh(code);
        foreach (DataRow r in tb.Rows)
        {
            HP = (string.IsNullOrEmpty(r["MucHocPhi"].ToString())) ? 0 : ((int)r["MucHocPhi"]);
        }
        if ((HP - num) < 0 || num < 0)
        {
            return false;
        }
        else
        {

            //lblNumGiamHP.Text = (HP * tlg / 100).ToString("C", new CultureInfo("vi-VN"));
            //txtCash.Text = (HP * tlg / 100).ToString();
            float a = num;
            float b = HP;
            float Percent = (a / b) * 100;
            //txtTLGiamHP.Text = String.Format("{0:0.00}", Percent);
            txtTLGiamHP.Text = Percent.ToString();
            lblNumGiamHP.Text = num.ToString("C", new CultureInfo("vi-VN"));
            //So tien phai dong
            txtHPPhaiDong.Text = (HP - num).ToString("C", new CultureInfo("vi-VN"));
            txtHPPhaiDongTemp.Text = (HP - num).ToString();
            txtHPBangChu.Text = ReadNumber.ByWords(decimal.Parse((HP - num).ToString())) + "đồng";

            //txtRemainFeee
            txtRemainFeee.Text = (HP - num).ToString("C", new CultureInfo("vi-VN"));
            txtRemainFeeeTemp.Text = (HP - num).ToString();

        }
        return true;
    }
    protected void txtTLGiamHP_TextChanged(object sender, EventArgs e)
    {
        string MaGD = Request.QueryString["MaGhiDanh"];
        this.DiscountPercent(MaGD);
    }

    protected void btnSaveBienLai_Click(object sender, EventArgs e)
    {
        try
        {
            kus_bienlai = new kus_BienLaiBLL();
            kus_ghidanh = new kus_GhiDanhBLL();
            string MaGD = Request.QueryString["MaGhiDanh"];
            kus_GhiDanh ghidanh = kus_ghidanh.getListGDCode(MaGD).FirstOrDefault();
            string lydo = txtHPLyDo.Text;
            int SoTienGiam = Convert.ToInt32(txtCash.Text);
            int SoTienDong = Convert.ToInt32(txtThuKhachHang.Text);


            //RemainFee
            int Sodu= Convert.ToInt32(txtSodu.Text);
            this.kus_ghidanh.UpdateRemainFee(ghidanh.GhiDanhID);



            int HPDong = Convert.ToInt32(txtHPPhaiDong.Text);
            string HPText = txtHPBangChu.Text;

            
            this.kus_bienlai.CreateBienLai(lydo, TLGiam, HPDong, HPText, ghidanh.GhiDanhID);


            //if (CalculatorHP(MaGD))
            //{
            //    List<kus_GhiDanh> lstGD = kus_ghidanh.getListGDCode(MaGD);
            //    kus_GhiDanh ghidanh = lstGD.FirstOrDefault();
            //    if (kus_bienlai.CreateBienLai(lydo, TLGiam, HPDong, HPText, ghidanh.GhiDanhID))
            //    {
            //        //this.kus_ghidanh.ResetDatcoc(ghidanh.GhiDanhID);
            //        Response.Redirect(Request.Url.AbsoluteUri);
            //    }
            //    else
            //    {
            //        Response.Write("<script>alert('Thu Học phí không thành công . Lỗi kết nối CSDL !')</script>");
            //        return;
            //    }
            //}
            //else
            //{
            //    this.AlertPageValid(true, "Phát sinh lỗi trong quá trình thanh toán, vui lòng kiểm tra lại !", alertPageValid, lblPageValid);
            //}
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    //------Load Images-----------------------------------------------------------------------------
    protected void load_ImgHocVien(string code)
    {
        kus_hocvien = new kus_HocVienBLL();
        kus_ghidanh = new kus_GhiDanhBLL();
        images = new ImagesBLL();
        List<kus_GhiDanh> lstGD = kus_ghidanh.getListGDCode(code);
        kus_GhiDanh ghidanh = lstGD.FirstOrDefault();
        List<kus_HocVien> lstHV = kus_hocvien.getHocVienWithID(ghidanh.HocVienID);
        kus_HocVien hocvien = lstHV.FirstOrDefault();
        List<Images> lstImg = images.getImagesWithId(hocvien.ImgID);
        Images img = lstImg.FirstOrDefault();
        imgCusprofile.Src = (img == null) ? "../images/default_images.jpg" : "../" + img.ImagesUrl;
    }
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
        rpLstImg.DataSource = (dlimgtype.SelectedValue == "0") ? images.GetImagesTypeUpload(6) : images.GetImagesTypeAndUserUpload(6, int.Parse(dlimgtype.SelectedValue));
        rpLstImg.DataBind();
    }
    protected void load_rpLstImg()
    {
        images = new ImagesBLL();
        rpLstImg.DataSource = images.GetImagesTypeAndUserUpload(6, Session.GetCurrentUser().UserID);
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
        string http = "http://" + Request.Url.Authority + "/";
        txtPostImgTemp.Text = HiddenimgSelect.Value.Remove(0, http.Length);
        imgCusprofile.Src = "../" + HiddenimgSelect.Value.Remove(0, http.Length);

        string MaGD = Request.QueryString["MaGhiDanh"];
        List<kus_GhiDanh> lstGD = kus_ghidanh.getListGDCode(MaGD);
        kus_GhiDanh ghidanh = lstGD.FirstOrDefault();
        UpdateImages(ghidanh.HocVienID, ImagesID(txtPostImgTemp.Text));
    }
    protected void btnuploadImg_ServerClick(object sender, EventArgs e)
    {
        //ImgEditPC.Src = txtuploadImgTemp.Text;
        UserAccounts ac = Session.GetCurrentUser();
        kus_ghidanh = new kus_GhiDanhBLL();
        images = new ImagesBLL();
        string MaGD = Request.QueryString["MaGhiDanh"];
        List<kus_GhiDanh> lstGD = kus_ghidanh.getListGDCode(MaGD);
        kus_GhiDanh ghidanh = lstGD.FirstOrDefault();

        string dateString = DateTime.Now.ToString("MM-dd-yyyy");
        string fileName = Path.GetFileName(fileUploadImgPost.PostedFile.FileName);
        ImageCodecInfo jgpEncoder = null;
        string str_image = "";
        string fileExtension = "";
        if (!string.IsNullOrEmpty(fileName))
        {
            fileExtension = Path.GetExtension(fileName);
            str_image = "Anh-Van-Hoi-Anh-My-" + dateString + "-" + RandomName + fileExtension;
            string pathToSave = Server.MapPath("../images/Upload/ImagesForCustomerProfile/") + str_image;
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
            this.images.InsertImages(str_image, "images/Upload/ImagesForCustomerProfile/" + str_image, 6, ac.UserID);
            txtPostImgTemp.Text = "images/Upload/ImagesForCustomerProfile/" + str_image;
            imgCusprofile.Src = "../images/Upload/ImagesForCustomerProfile/" + str_image;
            this.load_rpLstImg();
            this.UpdateImages(ghidanh.HocVienID, ImagesID(txtPostImgTemp.Text));
        }
        else
        {
            Response.Write("<script>alert('Upload Image False !')</script>");
            return;
        }
    }

    protected void btnXemBienLai_ServerClick(object sender, EventArgs e)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        kus_bienlai = new kus_BienLaiBLL();
        string MaGD = Request.QueryString["MaGhiDanh"];
        List<kus_GhiDanh> lstGD = kus_ghidanh.getListGDCode(MaGD);
        kus_GhiDanh ghidanh = lstGD.FirstOrDefault();
        List<kus_BienLai> lstBL = kus_bienlai.getListBienLaiWithID(ghidanh.GhiDanhID);
        kus_BienLai bienlai = lstBL.FirstOrDefault();
        if (bienlai != null)
        {
            Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/BienLaiHocPhi.aspx?BienLaiCode=" + bienlai.BienLaiCode);
        }
        else
        {
            return;
        }
    }

    protected void txtThuKhachHang_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int ThuKH = Convert.ToInt32(txtThuKhachHang.Text);
            int TruSoDu = Convert.ToInt32(txtMinus.Text);
            int PhaiDong = Convert.ToInt32(txtHPPhaiDongTemp.Text);
            //txtThuKHByWords.Text = ReadNumber.ByWords(txtThuKhachHang.Text) + "đồng";
            txtThuKHByWords.Text = ReadNumber.ByWords(decimal.Parse(txtThuKhachHang.Text)) + "đồng";
            if (((ThuKH + TruSoDu) - PhaiDong) > 0)
            {
                txtSodu.Text = ((ThuKH + TruSoDu) - PhaiDong).ToString();
                //txtRemainFeee
                txtRemainFeee.Text = (0).ToString("C", new CultureInfo("vi-VN"));
                txtRemainFeeeTemp.Text = "0";
            }
            else
            {
                txtSodu.Text = "0";
                //txtRemainFeee
                txtRemainFeee.Text = (Math.Abs((ThuKH + TruSoDu) - PhaiDong)).ToString("C", new CultureInfo("vi-VN"));
                txtRemainFeeeTemp.Text = (Math.Abs((ThuKH + TruSoDu) - PhaiDong)).ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + "Có lỗi xảy ra trong quá trình xử lý. Vui lòng kiểm tra lại các bước thực hiện !" + "')</script>");
        }
    }

    protected void btnGetAllSoDu_ServerClick(object sender, EventArgs e)
    {
        try
        {
            txtMinus.Text = txttempAvailableBalances.Text;
            int ThuKH = Convert.ToInt32(txtThuKhachHang.Text);
            int TruSoDu = Convert.ToInt32(txtMinus.Text);
            int PhaiDong = Convert.ToInt32(txtHPPhaiDongTemp.Text);
            //txtThuKHByWords.Text = ReadNumber.ByWords(txtThuKhachHang.Text) + "đồng";
            txtThuKHByWords.Text = ReadNumber.ByWords(decimal.Parse(txtThuKhachHang.Text)) + "đồng";
            if (((ThuKH + TruSoDu) - PhaiDong) > 0)
            {
                txtSodu.Text = ((ThuKH + TruSoDu) - PhaiDong).ToString();

                //txtRemainFeee
                txtRemainFeee.Text = (0).ToString("C", new CultureInfo("vi-VN"));
                txtRemainFeeeTemp.Text = "0";
            }
            else
            {
                txtSodu.Text = "0";
                //txtRemainFeee
                txtRemainFeee.Text = (Math.Abs((ThuKH + TruSoDu) - PhaiDong)).ToString("C", new CultureInfo("vi-VN"));
                txtRemainFeeeTemp.Text = (Math.Abs((ThuKH + TruSoDu) - PhaiDong)).ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + "Có lỗi xảy ra trong quá trình xử lý. Vui lòng kiểm tra lại các bước thực hiện !" + "')</script>");
        }
    }

    protected void txtMinus_TextChanged(object sender, EventArgs e)
    {
        try
        {
            int ThuKH = Convert.ToInt32(txtThuKhachHang.Text);
            int TruSoDu = Convert.ToInt32(txtMinus.Text);
            int PhaiDong = Convert.ToInt32(txtHPPhaiDongTemp.Text);
            //txtThuKHByWords.Text = ReadNumber.ByWords(txtThuKhachHang.Text) + "đồng";
            txtThuKHByWords.Text = ReadNumber.ByWords(decimal.Parse(txtThuKhachHang.Text)) + "đồng";
            if (((ThuKH + TruSoDu) - PhaiDong) > 0)
            {
                txtSodu.Text = ((ThuKH + TruSoDu) - PhaiDong).ToString();
                //txtRemainFeee
                txtRemainFeee.Text = (0).ToString("C", new CultureInfo("vi-VN"));
                txtRemainFeeeTemp.Text = "0";
            }
            else
            {
                txtSodu.Text = "0";
                //txtRemainFeee
                txtRemainFeee.Text = (Math.Abs((ThuKH + TruSoDu) - PhaiDong)).ToString("C", new CultureInfo("vi-VN"));
                txtRemainFeeeTemp.Text = (Math.Abs((ThuKH + TruSoDu) - PhaiDong)).ToString();
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + "Có lỗi xảy ra trong quá trình xử lý. Vui lòng kiểm tra lại các bước thực hiện !" + "')</script>");
        }
    }

    protected void txtCash_TextChanged(object sender, EventArgs e)
    {
        try
        {

            string MaGD = Request.QueryString["MaGhiDanh"];
            this.DiscountNumric(MaGD);


        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + "Có lỗi xảy ra trong quá trình xử lý. Vui lòng kiểm tra lại các bước thực hiện !" + "')</script>");
        }
    }
}