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

public partial class kus_admin_QLGhiDanh : BasePage
{
    kus_HTChiNhanhBLL kus_htchinhanh;
    kus_CoSoBLL kus_coso;
    kus_GhiDanhBLL kus_ghidanh;
    kus_GhiDanhTiemNamgBLL ghidanhtiemnang;
    nc_KhoaHocBLL khoahoc;
    nc_LopHocBLL lophoc;
    kus_LichHocBLL lichhoc;
    WeekdaysBLL weekdays;
    public int PageSize = 20;
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
                if (!check_permiss(ac.UserID, FunctionName.QLGhiDanh))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    this.load_dlHTChiNhanh();
                    formHT_CoSO.Visible = false;
                    dlLoaiThongKe.Items.FindByValue("0").Selected = true;
                    dlCoSo.Items.Insert(0, new ListItem("------ Chọn Cơ Sở thuộc Hệ Thống Chi Nhánh -------", "0"));
                    btnEditKhoaHoc.Attributes.Add("class", "btn btn-circle btn-icon-only btn-default disabled");
                    btnPhieuGD.Attributes.Add("class", "btn btn-default disabled");
                    btnghidanhkhoa.Attributes.Add("class", "btn btn-success disabled");
                    this.load_gwListClass();
                }
            }
        }
    }
    private void load_dlHTChiNhanh()
    {
        kus_htchinhanh = new kus_HTChiNhanhBLL();
        dlHTChiNhanh.DataSource = kus_htchinhanh.getlAllHTChiNHanh();
        dlHTChiNhanh.DataTextField = "tenHTChiNhanh";
        dlHTChiNhanh.DataValueField = "hTChiNhanhID";
        dlHTChiNhanh.DataBind();
        dlHTChiNhanh.Items.Insert(0, new ListItem("-- Chọn Ht.Chi Nhánh --", "0"));
    }
    protected void dlHTChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
    {
        kus_coso = new kus_CoSoBLL();
        dlCoSo.DataSource = kus_coso.getLSTCoSoWithChiNhanhID(Convert.ToInt32(dlHTChiNhanh.SelectedValue));
        dlCoSo.DataTextField = "TenCoSo";
        dlCoSo.DataValueField = "CoSoID";
        dlCoSo.DataBind();
        dlCoSo.Items.Insert(0, new ListItem("------ Chọn Cơ Sở thuộc Hệ Thống Chi Nhánh -------", "0"));
    }
    protected void dlLoaiThongKe_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (dlLoaiThongKe.SelectedValue == "0")
        {
            formHT_CoSO.Visible = false;
        }
        else
        {
            formHT_CoSO.Visible = true;
        }
    }
    private void Getkus_HVGhiDanhPageWise(int pageIndex, DateTime startdate, DateTime enddate)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        int recordCount = 0;
        gwGhiDanhHocVien.DataSource = kus_ghidanh.kus_getHVGhiDanh(pageIndex, PageSize, startdate, enddate);
        recordCount = kus_ghidanh.Countkus_getHVGhiDanh(startdate, enddate);
        gwGhiDanhHocVien.DataBind();
        this.PopulatePager(rptPager,recordCount, pageIndex,PageSize);
    }
    
    protected void Page_Changed(object sender, EventArgs e)
    {
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
        DateTime startdate;
        if (string.IsNullOrWhiteSpace(txtStartdate.Text) || DateTime.TryParseExact(txtStartdate.Text, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out startdate) || getday(txtStartdate.Text) == "" || getmonth(txtStartdate.Text) == "" || getyear(txtStartdate.Text) == "")
        {
            lblstartdateFalse.Text = "Ngày bắt đầu không chính xác !";
            return;
        }
        else
        {
            lblstartdateFalse.Text = "";
            startdate = DateTime.ParseExact(getday(txtStartdate.Text) + "/" + getmonth(txtStartdate.Text) + "/" + getyear(txtStartdate.Text), "dd/MM/yyyy", null);
        }
        DateTime enddate;
        if (string.IsNullOrWhiteSpace(txtEnddate.Text) || DateTime.TryParseExact(txtEnddate.Text, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out enddate) || getday(txtEnddate.Text) == "" || getmonth(txtEnddate.Text) == "" || getyear(txtEnddate.Text) == "")
        {
            lblEnddatefalse.Text = "Ngày kết thúc không chính xác !";
            return;
        }
        else
        {
            lblEnddatefalse.Text = "";
            enddate = DateTime.ParseExact(getday(txtEnddate.Text) + "/" + getmonth(txtEnddate.Text) + "/" + getyear(txtEnddate.Text), "dd/MM/yyyy", null);
        }
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Getkus_HVGhiDanhPageWise(pageIndex, startdate, enddate);
        rptPager.Visible = true;
        rptcoso.Visible = false;
    }
    private void Getkus_HVGhiDanhCoSoPageWise(int pageIndex, DateTime startdate, DateTime enddate, int CoSoID)
    {
        kus_ghidanh = new kus_GhiDanhBLL();
        int recordCount = 0;
        gwGhiDanhHocVien.DataSource = kus_ghidanh.kus_getHVGhiDanhInCoSo(pageIndex, PageSize, startdate, enddate, CoSoID);
        recordCount = kus_ghidanh.CountgetHVGhiDanhInCoSo(startdate, enddate, CoSoID);
        gwGhiDanhHocVien.DataBind();
        this.PopulatePager(rptcoso, recordCount, pageIndex, PageSize);
    }
    
    protected void CSPage_Changed(object sender, EventArgs e)
    {
        string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
        DateTime startdate;
        if (DateTime.TryParseExact(txtStartdate.Text, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out startdate) || getday(txtStartdate.Text) == "" || getmonth(txtStartdate.Text) == "" || getyear(txtStartdate.Text) == "")
        {
            lblstartdateFalse.Text = "Ngày bắt đầu không chính xác !";
            return;
        }
        else
        {
            lblstartdateFalse.Text = "";
            startdate = DateTime.ParseExact(getday(txtStartdate.Text) + "/" + getmonth(txtStartdate.Text) + "/" + getyear(txtStartdate.Text), "dd/MM/yyyy", null);
        }
        DateTime enddate;
        if (DateTime.TryParseExact(txtEnddate.Text, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out enddate) || getday(txtEnddate.Text) == "" || getmonth(txtEnddate.Text) == "" || getyear(txtEnddate.Text) == "")
        {
            lblEnddatefalse.Text = "Ngày kết thúc không chính xác !";
            return;
        }
        else
        {
            lblEnddatefalse.Text = "";
            enddate = DateTime.ParseExact(getday(txtEnddate.Text) + "/" + getmonth(txtEnddate.Text) + "/" + getyear(txtEnddate.Text), "dd/MM/yyyy", null);
        }
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Getkus_HVGhiDanhCoSoPageWise(pageIndex, startdate, enddate, Convert.ToInt32(dlCoSo.SelectedValue));
        rptPager.Visible = false;
        rptcoso.Visible = true;

    }
    protected void btnSunmitGhiDanh_Click(object sender, EventArgs e)
    {
        try
        {
            kus_ghidanh = new kus_GhiDanhBLL();
            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            DateTime startdate;
            if (DateTime.TryParseExact(txtStartdate.Text, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out startdate) || getday(txtStartdate.Text) == "" || getmonth(txtStartdate.Text) == "" || getyear(txtStartdate.Text) == "")
            {
                lblstartdateFalse.Text = "Ngày bắt đầu không chính xác !";
                return;
            }
            else
            {
                lblstartdateFalse.Text = "";
                startdate = DateTime.ParseExact(getday(txtStartdate.Text) + "/" + getmonth(txtStartdate.Text) + "/" + getyear(txtStartdate.Text), "dd/MM/yyyy", null);
            }
            DateTime enddate;
            if (DateTime.TryParseExact(txtEnddate.Text, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out enddate) || getday(txtEnddate.Text) == "" || getmonth(txtEnddate.Text) == "" || getyear(txtEnddate.Text) == "")
            {
                lblEnddatefalse.Text = "Ngày kết thúc không chính xác !";
                return;
            }
            else
            {
                lblEnddatefalse.Text = "";
                enddate = DateTime.ParseExact(getday(txtEnddate.Text) + "/" + getmonth(txtEnddate.Text) + "/" + getyear(txtEnddate.Text), "dd/MM/yyyy", null);
            }

            if (dlCoSo.SelectedValue == "0")
            {
                this.Getkus_HVGhiDanhPageWise(1, startdate, enddate);
                lblSumHocVien.Text = kus_ghidanh.Countkus_getHVGhiDanh(startdate, enddate).ToString();
                rptPager.Visible = true;
                rptcoso.Visible = false;
            }
            else
            {
                this.Getkus_HVGhiDanhCoSoPageWise(1, startdate, enddate, Convert.ToInt32(dlCoSo.SelectedValue));
                lblSumHocVien.Text = kus_ghidanh.CountgetHVGhiDanhInCoSo(startdate, enddate, Convert.ToInt32(dlCoSo.SelectedValue)).ToString();
                rptPager.Visible = false;
                rptcoso.Visible = true;
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    protected void btnPhieuGD_ServerClick(object sender, EventArgs e)
    {
        if(gwGhiDanhHocVien.SelectedRow==null)
        {
            Response.Write("<script>alert('Chưa chọn Học Viên !')</script>");
            return;
        }
        else
        {
            string MaGhiDanh = (gwGhiDanhHocVien.SelectedRow.FindControl("lblGhiDanhCode") as Label).Text;
            Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/PhieuGhiDanh.aspx?MaGhiDanh="+ MaGhiDanh);

        }
    }
    protected void gwGhiDanhHocVien_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnEditKhoaHoc.Attributes.Add("class", "btn btn-circle btn-icon-only btn-default");
        btnPhieuGD.Attributes.Add("class", "btn btn-default");

    }


    private void Getkus_GhiDanhTiemNamgPageWise(int pageIndex, int LopHoc)
    {
        ghidanhtiemnang = new kus_GhiDanhTiemNamgBLL();
        int recordCount = 0;
        gwGhiDanhTN.DataSource = ghidanhtiemnang.kus_getHVGhiDanhTiemNang(pageIndex, PageSize, LopHoc);
        recordCount = ghidanhtiemnang.Countkus_GhiDanhTiemNamg(LopHoc);
        gwGhiDanhTN.DataBind();
        this.PopulatePager(rptTN, recordCount, pageIndex, PageSize);
    }
    //Load gwListClass
    private void load_gwListClass()
    {
        ghidanhtiemnang = new kus_GhiDanhTiemNamgBLL();
        gwListClass.DataSource = ghidanhtiemnang.ListGhiDanhTiemNangByLopID();
        gwListClass.DataBind();
    }

    protected void gwListClass_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            int LopID = Convert.ToInt32((gwListClass.SelectedRow.FindControl("lblID") as Label).Text);
            this.Getkus_GhiDanhTiemNamgPageWise(1, LopID);
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwGhiDanhTN_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("linkBtnDel") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this ?')");
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwGhiDanhTN_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            ghidanhtiemnang = new kus_GhiDanhTiemNamgBLL();
            int GDId = Convert.ToInt32((gwGhiDanhTN.Rows[e.RowIndex].FindControl("lblID") as Label).Text);
            if (this.ghidanhtiemnang.DeleteByID(GDId))
            {
                int LopID = Convert.ToInt32((gwListClass.SelectedRow.FindControl("lblID") as Label).Text);
                this.Getkus_GhiDanhTiemNamgPageWise(1, LopID);
            }
            else
            {
                this.AlertPageValid(true, "False to connect server !", alertPageValid, lblPageValid);
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void lnkPageTN_Click(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as Button).CommandArgument);
        int LopID = Convert.ToInt32((gwListClass.SelectedRow.FindControl("lblID") as Label).Text);
        this.Getkus_GhiDanhTiemNamgPageWise(pageIndex, LopID);
    }

    protected void gwGhiDanhTN_SelectedIndexChanged(object sender, EventArgs e)
    {
        khoahoc = new nc_KhoaHocBLL();
        int numKhoa = Convert.ToInt32((gwGhiDanhTN.SelectedRow.FindControl("lblNumKhoa") as Label).Text);
        if (numKhoa > 0)
        {
            int LopID = Convert.ToInt32((gwListClass.SelectedRow.FindControl("lblID") as Label).Text);
            int cosoid = Convert.ToInt32((gwGhiDanhTN.SelectedRow.FindControl("lblCoSoID") as Label).Text);
            btnghidanhkhoa.Attributes.Add("class", "btn btn-success");

            gwKhoaHocByLop.DataSource = khoahoc.getListKhoaHocIncomingByLopHoc(LopID, cosoid);
            gwKhoaHocByLop.DataBind();
        }
        else
        {
            btnghidanhkhoa.Attributes.Add("class", "btn btn-success disabled");
        }
        string script = "window.onload = function() { callbtntabGhiDanhTNClickEvent(); };";
        ClientScript.RegisterStartupScript(this.GetType(), "callbtntabGhiDanhTNClickEvent", script, true);
    }

    protected void gwKhoaHocByLop_SelectedIndexChanged(object sender, EventArgs e)
    {
        khoahoc = new nc_KhoaHocBLL();
        lichhoc = new kus_LichHocBLL();
        weekdays = new WeekdaysBLL();
        int khoahocid = Convert.ToInt32((gwKhoaHocByLop.SelectedRow.FindControl("lblID") as Label).Text);
        nc_KhoaHoc khoc = khoahoc.getListKhoaHocWithID(khoahocid).FirstOrDefault();
        lblMaKhoaHocDetail.Text = khoc.MaKhoaHoc;
        lblTenKhoaHocDetail.Text = khoc.TenKhoaHoc;
        lblNgayKhaiGiangDetail.Text = khoc.NgayKhaiGiang.ToString("dd/MM/yyyy");
        lblNgayKetThucDetail.Text = khoc.NgayKetThuc.ToString("dd/MM/yyyy");
        lblThoiLuongDetail.Text = khoc.ThoiLuong.ToString();

        List<kus_LichHoc> lstLichHoc = lichhoc.getListLichHocWithKhoaHoc(khoahocid);
        foreach (kus_LichHoc itm in lstLichHoc)
        {
            lblLichHocDetail.Text += weekdays.ListWeekdaysByID(itm.DayID).FirstOrDefault().WeekdaysNameVN+" | ";
        }

    }

    protected void btnSaveSelectKhoaHoc_Click(object sender, EventArgs e)
    {
        try
        {
            kus_ghidanh = new kus_GhiDanhBLL();
            ghidanhtiemnang = new kus_GhiDanhTiemNamgBLL();
            lophoc = new nc_LopHocBLL();

            if(gwKhoaHocByLop.SelectedRow==null)
            {
                Response.Write("<script>alert('Chưa chọn Khóa Học !')</script>");
            }
            else
            {
                int HocVienID = Convert.ToInt32((gwGhiDanhTN.SelectedRow.FindControl("lblHocVienID") as Label).Text);
                int khoahocid = Convert.ToInt32((gwKhoaHocByLop.SelectedRow.FindControl("lblID") as Label).Text);
                int lophocid = Convert.ToInt32((gwGhiDanhTN.SelectedRow.FindControl("lblLopHoc") as Label).Text);
                int gdTNID = Convert.ToInt32((gwGhiDanhTN.SelectedRow.FindControl("lblID") as Label).Text);

                kus_GhiDanhTiemNamg gdTN = ghidanhtiemnang.ListByID(gdTNID).FirstOrDefault();
                nc_LopHoc lop = lophoc.getListLopHocWithID(lophocid).FirstOrDefault();


                if (kus_ghidanh.GhiDanhMoi(HocVienID, khoahocid, gdTN.NVGhiDanh, gdTN.GhiChu, 0, lop.MucHocPhi))
                {
                    Response.Write("<script>alert('Ghi danh thành công !')</script>");
                    this.ghidanhtiemnang.DeleteByID(gdTNID);
                    Response.Redirect(Request.Url.AbsolutePath);
                }
                else
                {
                    this.AlertPageValid(true, "Thao tác thất bại. Lỗi kết nối server !", alertPageValid, lblPageValid);
                }

            }
            

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}