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

public partial class ChuongTrinhHoc_KhoaHoc : BasePage
{
    nc_LoaiCTDaoTaoBLL nc_loaictdaotao;
    nc_ChuongTrinhDaoTaoBLL nc_chuongtrinhdaotao;
    nc_LopHocBLL nc_lophoc;
    nc_KhoaHocBLL nc_khoahoc;
    kus_HTChiNhanhBLL kus_htchinhanh;
    kus_CoSoBLL kus_coso;
    kus_BooksBLL kus_books;
    nc_KhoaHoc_BooksBLL nc_khoahoc_books;
    kus_LichHocBLL kus_lichhoc;
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
                if (!check_permiss(ac.UserID, FunctionName.KhoaHoc))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    this.load_dlLoaiChuongTrinh();
                    this.load_dlHTChiNhanh();
                    dlChuongTrinh.Items.Insert(0, new ListItem("-- Chọn chương trình --", "0"));
                    dlLopHoc.Items.Insert(0, new ListItem("-- Chọn lớp học --", "0"));
                    dlCoSo.Items.Insert(0, new ListItem("------ Chọn Cơ Sở thuộc Hệ Thống Chi Nhánh -------", "0"));
                    if(Session["pageIndexnc_lophoc"]==null)
                    {
                        this.Getnc_KhoaHocPageWise(1);
                    }
                    else
                    {
                        int pageIndex = Convert.ToInt32(Session["pageIndexnc_lophoc"].ToString());
                        this.Getnc_KhoaHocPageWise(pageIndex);
                    }
                    btnEditKhoaHoc.Attributes.Add("class", "btn btn-circle btn-icon-only btn-default disabled");
                    btnAddBooks.Attributes.Add("class", "btn btn-default disabled");
                    btnLenlichhoc.Attributes.Add("class", "btn btn-default disabled");
                    btnXemLichHoc.Attributes.Add("class", "btn btn-circle btn-icon-only btn-default disabled");
                    rptPager.Visible = true;
                    rptSearch.Visible = false;
                }
            }
        }
    }
    private void load_dlLoaiChuongTrinh()
    {
        nc_loaictdaotao = new nc_LoaiCTDaoTaoBLL();
        dlLoaiChuongTrinh.DataSource = nc_loaictdaotao.getListLoaiCTDaoTao();
        dlLoaiChuongTrinh.DataTextField = "TenChuongTrinh";
        dlLoaiChuongTrinh.DataValueField = "ID";
        dlLoaiChuongTrinh.DataBind();
        dlLoaiChuongTrinh.Items.Insert(0, new ListItem("-- Chọn loại chương trình --", "0"));
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
    protected void dlLoaiChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        nc_chuongtrinhdaotao = new nc_ChuongTrinhDaoTaoBLL();
        dlChuongTrinh.DataSource = nc_chuongtrinhdaotao.GetChuongTrinhDaoTaoWithLoai(Convert.ToInt32(dlLoaiChuongTrinh.SelectedValue));
        dlChuongTrinh.DataTextField = "TenChuongTrinh";
        dlChuongTrinh.DataValueField = "ID";
        dlChuongTrinh.DataBind();
        dlChuongTrinh.Items.Insert(0, new ListItem("-- Chọn chương trình --", "0"));
    }
    protected void dlChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        nc_lophoc = new nc_LopHocBLL();
        dlLopHoc.DataSource = nc_lophoc.getListLopHocWithChuongTrinh(Convert.ToInt32(dlChuongTrinh.SelectedValue));
        dlLopHoc.DataTextField = "TenLopHoc";
        dlLopHoc.DataValueField = "ID";
        dlLopHoc.DataBind();
        dlLopHoc.Items.Insert(0, new ListItem("-- Chọn lớp học --", "0"));
    }
    private void load_dlHTChiNhanh()
    {
        kus_htchinhanh = new kus_HTChiNhanhBLL();
        dlHTChiNhanh.DataSource = kus_htchinhanh.getAllTBChiNhanh();
        dlHTChiNhanh.DataTextField = "tenHTChiNhanh";
        dlHTChiNhanh.DataValueField = "hTChiNhanhID";
        dlHTChiNhanh.DataBind();
        dlHTChiNhanh.Items.Insert(0, new ListItem("------- Chọn Hệ Thống Chi Nhánh -------", "0"));
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
    protected void dlHTChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
    {
        kus_coso = new kus_CoSoBLL();
        dlCoSo.DataSource = kus_coso.getLSTCoSoWithChiNhanhID(Convert.ToInt32(dlHTChiNhanh.SelectedValue));
        dlCoSo.DataTextField = "TenCoSo";
        dlCoSo.DataValueField = "CoSoID";
        dlCoSo.DataBind();
        dlCoSo.Items.Insert(0, new ListItem("------ Chọn Cơ Sở thuộc Hệ Thống Chi Nhánh -------", "0"));
    }
    private void Getnc_KhoaHocPageWise(int pageIndex)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        int recordCount = 0;
        gwKhoaHoc.DataSource = nc_khoahoc.get_Tabel_nc_KhoaHoc(pageIndex, PageSize);
        recordCount = nc_khoahoc.Count_khoahoc();
        gwKhoaHoc.DataBind();
        this.PopulatePager(recordCount, pageIndex);
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the First Page Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("First", "1"));
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }

        //Add the Last Button.
        if (currentPage != pageCount)
        {
            pages.Add(new ListItem("Last", pageCount.ToString()));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Getnc_KhoaHocPageWise(pageIndex);
        Session["pageIndexnc_lophoc"] = pageIndex.ToString();
        rptPager.Visible = true;
        rptSearch.Visible = false;
    }
    protected void btnSaveNew_Click(object sender, EventArgs e)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        try
        {
            string tenkhoahoc = txtTenKhoaHoc.Text;
            int soluong = (txtSoLuong.Text == "") ? 0 : Convert.ToInt32(txtSoLuong.Text);
            int thoiluong = (txtThoiLuong.Text == "") ? 0 : Convert.ToInt32(txtThoiLuong.Text);
            string ngaykhaigiang = txtNgayKG.Text;
            string ngayketthuc = txtNgayKT.Text;
            DateTime NgayKG;
            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            if (string.IsNullOrWhiteSpace(ngaykhaigiang) || DateTime.TryParseExact(ngaykhaigiang, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayKG) || getday(ngaykhaigiang) == "" || getmonth(ngaykhaigiang) == "" || getyear(ngaykhaigiang) == "")
            {
                NgayKG = Convert.ToDateTime("12/12/1900");
            }
            else
            {
                NgayKG = DateTime.ParseExact(getday(ngaykhaigiang) + "/" + getmonth(ngaykhaigiang) + "/" + getyear(ngaykhaigiang), "dd/MM/yyyy", null);
            }
            DateTime NgayKT;
            if (string.IsNullOrWhiteSpace(ngayketthuc) || DateTime.TryParseExact(ngayketthuc, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayKT) || getday(ngayketthuc) == "" || getmonth(ngayketthuc) == "" || getyear(ngayketthuc) == "")
            {
                NgayKT = Convert.ToDateTime("12/12/1900");
            }
            else
            {
                NgayKT = DateTime.ParseExact(getday(ngayketthuc) + "/" + getmonth(ngayketthuc) + "/" + getyear(ngayketthuc), "dd/MM/yyyy", null);
            }
            int loaichuongtrinh = Convert.ToInt32(dlLoaiChuongTrinh.SelectedValue);
            int chuongtrinh = Convert.ToInt32(dlChuongTrinh.SelectedValue);
            int lophoc = Convert.ToInt32(dlLopHoc.SelectedValue);
            int coso = Convert.ToInt32(dlCoSo.SelectedValue);
            if (nc_khoahoc.New_nc_KhoaHoc(tenkhoahoc, soluong, NgayKG, thoiluong, NgayKT, loaichuongtrinh, chuongtrinh, lophoc, coso))
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write("<script>alert('Thêm thất bại. Lỗi kết nối cơ sở dữ liệu !')</script>");
            }
        }
        catch(Exception ex)
        {
            Response.Write("<script>alert('" + ex.ToString() + "')</script>");
        }
    }
    protected void gwKhoaHoc_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("linkBtnDel") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Bạn chắc chắn muốn xóa ?')");
            }
        }
        catch (Exception)
        {

        }
    }
    protected void gwKhoaHoc_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        Response.Write("<script>alert('This Process is Building ! !')</script>");
    }
    protected void gwKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        kus_coso = new kus_CoSoBLL();
        btnAddBooks.Attributes.Add("class", "btn btn-default");
        btnEditKhoaHoc.Attributes.Add("class", "btn btn-circle btn-icon-only btn-default");
        btnLenlichhoc.Attributes.Add("class", "btn btn-default");
        btnXemLichHoc.Attributes.Add("class", "btn btn-circle btn-icon-only btn btn-default");
        this.load_dlELoaiChuongTrinh();
        this.load_dlEHTChiNhanh();
        this.load_dlECoSo();
        int khoahocID = Convert.ToInt32((gwKhoaHoc.SelectedRow.FindControl("lblID") as Label).Text);
        List<nc_KhoaHoc> lst = nc_khoahoc.getListKhoaHocWithID(khoahocID);
        nc_KhoaHoc khoahoc = lst.FirstOrDefault();
        if (khoahoc != null)
        {
            this.load_dlEChuongTrinh(khoahoc.LoaiChuongTrinh);
            this.load_dlELopHoc(khoahoc.ChuongTrinh);
            txtETenKhoaHoc.Text = khoahoc.TenKhoaHoc;
            txtESoLuong.Text = khoahoc.SoLuong.ToString();
            txtENgayKhaiGiang.Text = (khoahoc.NgayKhaiGiang.Year <= 1900) ? "" : khoahoc.NgayKhaiGiang.ToString("dd-MM-yyyy");
            txtENgayKetThuc.Text = (khoahoc.NgayKetThuc.Year <= 1900) ? "" : khoahoc.NgayKetThuc.ToString("dd-MM-yyyy");
            txtEThoiLuong.Text = khoahoc.ThoiLuong.ToString();
            dlELoaiChuongTrinh.Items.FindByValue((khoahoc.LoaiChuongTrinh == 0) ? "0" : khoahoc.LoaiChuongTrinh.ToString()).Selected = true;
            dlEChuongTrinh.Items.FindByValue((khoahoc.ChuongTrinh == 0) ? "0" : khoahoc.ChuongTrinh.ToString()).Selected = true;
            dlELopHoc.Items.FindByValue((khoahoc.LopHoc == 0) ? "0" : khoahoc.LopHoc.ToString()).Selected = true;
            dlECoSo.Items.FindByValue((khoahoc.CoSoID == 0) ? "0" : khoahoc.CoSoID.ToString()).Selected = true;

            List<kus_CoSo> lstCoSo = kus_coso.getLSTCoSoWithID(khoahoc.CoSoID);
            kus_CoSo coso = lstCoSo.FirstOrDefault();
            dlEHTChiNhanh.Items.FindByValue((coso == null) ? 0.ToString() : coso.HTChiNhanhID.ToString()).Selected = true;

            //books
            this.load_dlAddBooks();
            this.load_gwkus_KhoaHoc_Books(khoahocID);
            //modal lich hoc
            lblchoseMaKhoa.Text = khoahoc.MaKhoaHoc;
            lblchoseTenKhoa.Text = khoahoc.TenKhoaHoc;
            lblchoseNgayKG.Text= (khoahoc.NgayKhaiGiang.Year <= 1900) ? "" : khoahoc.NgayKhaiGiang.ToString("dd-MM-yyyy");
            lblchoseNgayKT.Text= (khoahoc.NgayKetThuc.Year <= 1900) ? "" : khoahoc.NgayKetThuc.ToString("dd-MM-yyyy");
            this.load_LichHoc(khoahocID);
            //click create lich hoc 
            Session.SetCurrentCoSoID(khoahoc.CoSoID.ToString());
        }
        else
        {
            return;
        }
    }
    protected void btnSaveEditKhoaHoc_Click(object sender, EventArgs e)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        try
        {
            int khoahocID = Convert.ToInt32((gwKhoaHoc.SelectedRow.FindControl("lblID") as Label).Text);
            string tenkhoahoc = txtETenKhoaHoc.Text;
            int soluong = (txtESoLuong.Text == "") ? 0 : Convert.ToInt32(txtESoLuong.Text);
            int thoiluong = (txtEThoiLuong.Text == "") ? 0 : Convert.ToInt32(txtEThoiLuong.Text);
            string ngaykhaigiang = txtENgayKhaiGiang.Text;
            string ngayketthuc = txtENgayKetThuc.Text;
            DateTime NgayKG;
            string[] formats = { "dd/MM/yyyy", "d/M/yyyy", "dd/M/yyyy", "d/MM/yyyy" };
            if (string.IsNullOrWhiteSpace(ngaykhaigiang) || DateTime.TryParseExact(ngaykhaigiang, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayKG) || getday(ngaykhaigiang) == "" || getmonth(ngaykhaigiang) == "" || getyear(ngaykhaigiang) == "")
            {
                NgayKG = Convert.ToDateTime("12/12/1900");
            }
            else
            {
                NgayKG = DateTime.ParseExact(getday(ngaykhaigiang) + "/" + getmonth(ngaykhaigiang) + "/" + getyear(ngaykhaigiang), "dd/MM/yyyy", null);
            }
            DateTime NgayKT;
            if (string.IsNullOrWhiteSpace(ngayketthuc) || DateTime.TryParseExact(ngayketthuc, formats, new CultureInfo("vi-VN"), DateTimeStyles.None, out NgayKT) || getday(ngayketthuc) == "" || getmonth(ngayketthuc) == "" || getyear(ngayketthuc) == "")
            {
                NgayKT = Convert.ToDateTime("12/12/1900");
            }
            else
            {
                NgayKT = DateTime.ParseExact(getday(ngayketthuc) + "/" + getmonth(ngayketthuc) + "/" + getyear(ngayketthuc), "dd/MM/yyyy", null);
            }
            int loaichuongtrinh = Convert.ToInt32(dlELoaiChuongTrinh.SelectedValue);
            int chuongtrinh = Convert.ToInt32(dlEChuongTrinh.SelectedValue);
            int lophoc = Convert.ToInt32(dlELopHoc.SelectedValue);
            int coso = Convert.ToInt32(dlECoSo.SelectedValue);
            if (nc_khoahoc.Update_nc_KhoaHoc(khoahocID, tenkhoahoc, soluong, NgayKG, thoiluong, NgayKT, loaichuongtrinh, chuongtrinh, lophoc, coso))
            {
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write("<script>alert('Cập nhật thất bại. Lỗi kết nối cơ sở dữ liệu !')</script>");
            }
            //------------------------------------------------------------------------
            
        }
        catch(Exception ex)
        {
            Response.Write("<script>alert('" + ex.ToString() + "')</script>");
        }
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
    protected void dlELoaiChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        nc_chuongtrinhdaotao = new nc_ChuongTrinhDaoTaoBLL();
        dlEChuongTrinh.DataSource = nc_chuongtrinhdaotao.GetChuongTrinhDaoTaoWithLoai(Convert.ToInt32(dlELoaiChuongTrinh.SelectedValue));
        dlEChuongTrinh.DataTextField = "TenChuongTrinh";
        dlEChuongTrinh.DataValueField = "ID";
        dlEChuongTrinh.DataBind();
        dlEChuongTrinh.Items.Insert(0, new ListItem("-- Chọn chương trình --", "0"));
    }
    protected void dlEChuongTrinh_SelectedIndexChanged(object sender, EventArgs e)
    {
        nc_lophoc = new nc_LopHocBLL();
        dlELopHoc.DataSource = nc_lophoc.getListLopHocWithChuongTrinh(Convert.ToInt32(dlEChuongTrinh.SelectedValue));
        dlELopHoc.DataTextField = "TenLopHoc";
        dlELopHoc.DataValueField = "ID";
        dlELopHoc.DataBind();
        dlELopHoc.Items.Insert(0, new ListItem("-- Chọn lớp học --", "0"));
    }
    protected void dlEHTChiNhanh_SelectedIndexChanged(object sender, EventArgs e)
    {
        kus_coso = new kus_CoSoBLL();
        dlCoSo.DataSource = kus_coso.getLSTCoSoWithChiNhanhID(Convert.ToInt32(dlEHTChiNhanh.SelectedValue));
        dlCoSo.DataTextField = "TenCoSo";
        dlCoSo.DataValueField = "CoSoID";
        dlCoSo.DataBind();
        dlCoSo.Items.Insert(0, new ListItem("------ Chọn Cơ Sở thuộc Hệ Thống Chi Nhánh -------", "0"));
    }

    //search
    private void GetSearchKhoaHocPageWise(int pageIndex, string keysearch)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        int recordCount = 0;
        gwKhoaHoc.DataSource = nc_khoahoc.Search_nc_KhoaHoc(pageIndex, PageSize, keysearch);
        recordCount = nc_khoahoc.Count_search_khoahoc(keysearch);
        gwKhoaHoc.DataBind();
        this.PopulateSearch(recordCount, pageIndex);
    }
    private void PopulateSearch(int recordCount, int currentPage)
    {
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the First Page Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("First", "1"));
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }

        //Add the Last Button.
        if (currentPage != pageCount)
        {
            pages.Add(new ListItem("Last", pageCount.ToString()));
        }
        rptSearch.DataSource = pages;
        rptSearch.DataBind();
    }
    protected void Search_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetSearchKhoaHocPageWise(pageIndex, txtsearch.Value);
        rptPager.Visible = false;
        rptSearch.Visible = true;
    }
    protected void btnSearchKhoaHoc_ServerClick(object sender, EventArgs e)
    {
        this.GetSearchKhoaHocPageWise(1, txtsearch.Value);
        rptPager.Visible = false;
        rptSearch.Visible = true;
    }

    protected void btnRefreshLstKhoaHoc_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    // Books
    private void load_gwkus_KhoaHoc_Books(int khoahoc)
    {
        nc_khoahoc_books = new nc_KhoaHoc_BooksBLL();
        gwkus_KhoaHoc_Books.DataSource = nc_khoahoc_books.getTBnc_KhoaHoc_Books(khoahoc);
        gwkus_KhoaHoc_Books.DataBind();
    }
    private void load_dlAddBooks()
    {
        kus_books = new kus_BooksBLL();
        dlAddBooks.DataSource = kus_books.GetTBBoook();
        dlAddBooks.DataTextField = "BookNames";
        dlAddBooks.DataValueField = "BookID";
        dlAddBooks.DataBind();
        dlAddBooks.Items.Insert(0, new ListItem("--> Chọn Sách - Giáo Trình <--", "0"));
    }

    private Boolean CheckAvailableBook(int khoahoc, int bookid)
    {
        nc_khoahoc_books = new nc_KhoaHoc_BooksBLL();
        int khoahocID = Convert.ToInt32((gwKhoaHoc.SelectedRow.FindControl("lblID") as Label).Text);
        List<nc_KhoaHoc_Books> lst = nc_khoahoc_books.ListAvailableBook(khoahoc, bookid);
        nc_KhoaHoc_Books kb = lst.FirstOrDefault();
        return (kb != null) ? false : true;
    }
    protected void btnAddBook_ServerClick(object sender, EventArgs e)
    {
        nc_khoahoc_books = new nc_KhoaHoc_BooksBLL();
        if (gwKhoaHoc.SelectedRow == null)
        {
            lblvalidAddSach.Text = "Chưa chọn Lớp Học !";
        }
        else
        {
            if (dlAddBooks.SelectedValue == "0")
            {
                lblvalidAddSach.Text = "Chưa chọn Sách - Giáo trình !";
            }
            else
            {
                int khoahocID = Convert.ToInt32((gwKhoaHoc.SelectedRow.FindControl("lblID") as Label).Text);
                if (!CheckAvailableBook(khoahocID, Convert.ToInt32(dlAddBooks.SelectedValue)))
                {
                    lblvalidAddSach.Text = "Lớp Học hiện đã có sách - giáo trình này !";
                }
                else
                {
                    if (!this.nc_khoahoc_books.InsertBook(khoahocID, Convert.ToInt32(dlAddBooks.SelectedValue)))
                    {
                        lblvalidAddSach.Text = "Thêm sách - giáo trình False ! Lỗi kết nối DB";
                    }
                    else
                    {
                        lblvalidAddSach.Text = "";
                        this.load_gwkus_KhoaHoc_Books(khoahocID);
                    }
                }
            }

        }
    }

    protected void gwkus_KhoaHoc_Books_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("btnDelBook") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Bạn chắc chắn muốn xóa ?')");
            }
        }
        catch (Exception ex)
        {
            lblvalidAddSach.Text = ex.ToString();
        }
    }

    protected void gwkus_KhoaHoc_Books_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        nc_khoahoc_books = new nc_KhoaHoc_BooksBLL();
        int khoahocID = Convert.ToInt32((gwKhoaHoc.SelectedRow.FindControl("lblID") as Label).Text);
        int bookid = Convert.ToInt32((gwkus_KhoaHoc_Books.Rows[e.RowIndex].FindControl("lblBookID") as Label).Text);
        if (this.nc_khoahoc_books.DeletetBook(khoahocID, bookid))
        {
            this.load_gwkus_KhoaHoc_Books(khoahocID);
        }
        else
        {
            Response.Write("<script>alert('Xóa Sách thất bại. Lỗi kết nối csdl !')</script>");
        }
    }
    // Load Modal Lịch Học
    private void load_LichHoc(int khoahoc, int daysID, int buoiID, GridView gwLichHoc)
    {
        kus_lichhoc = new kus_LichHocBLL();
        gwLichHoc.DataSource = kus_lichhoc.getkus_LichHocWith_KhoaHoc_Day_Buoi(khoahoc, daysID, buoiID);
        gwLichHoc.DataBind();
    }
    private void load_LichHoc(int khoahocid)
    {
        this.load_LichHoc(khoahocid, 1, 1, gwThu2Sang);
        this.load_LichHoc(khoahocid, 1, 2, gwThu2Chieu);
        this.load_LichHoc(khoahocid, 1, 3, gwThu2Toi);
        this.load_LichHoc(khoahocid, 2, 1, gwThu3Sang);
        this.load_LichHoc(khoahocid, 2, 2, gwThu3Chieu);
        this.load_LichHoc(khoahocid, 2, 3, gwThu3Toi);
        this.load_LichHoc(khoahocid, 3, 1, gwThu4Sang);
        this.load_LichHoc(khoahocid, 3, 2, gwThu4Chieu);
        this.load_LichHoc(khoahocid, 3, 3, gwThu4Toi);
        this.load_LichHoc(khoahocid, 4, 1, gwThu5Sang);
        this.load_LichHoc(khoahocid, 4, 2, gwThu5Chieu);
        this.load_LichHoc(khoahocid, 4, 3, gwThu5Toi);
        this.load_LichHoc(khoahocid, 5, 1, gwThu6Sang);
        this.load_LichHoc(khoahocid, 5, 2, gwThu6Chieu);
        this.load_LichHoc(khoahocid, 5, 3, gwThu6Toi);
        this.load_LichHoc(khoahocid, 6, 1, gwThu7Sang);
        this.load_LichHoc(khoahocid, 6, 2, gwThu7Chieu);
        this.load_LichHoc(khoahocid, 6, 3, gwThu7Toi);
        this.load_LichHoc(khoahocid, 7, 1, gwChuNhatSang);
        this.load_LichHoc(khoahocid, 7, 2, gwChuNhatChieu);
        this.load_LichHoc(khoahocid, 7, 3, gwChuNhatToi);
    }
    protected void btnLenlichhoc_ServerClick(object sender, EventArgs e)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        if (gwKhoaHoc.SelectedRow == null)
        {
            Response.Write("<script>alert('Chưa chọn khóa học ! Vui lòng chọn 1 khóa học trong danh sách !')</script>");
        }
        else
        {
            string makhoahoc = (gwKhoaHoc.SelectedRow.FindControl("lblMaKhoaHoc") as Label).Text;
            Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/CreateSchedule.aspx?makhoahoc=" + makhoahoc);
        }
    }
}