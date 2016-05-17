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

public partial class kus_admin_DiemDanh : BasePage
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
                if (!check_permiss(ac.UserID, FunctionName.DiemDanh))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    rptPager.Visible = true;
                    rptSearch.Visible = false;
                    this.Getnc_KhoaHocPageWise(1);
                    btnDiemDanh.Attributes.Add("class", "btn btn-default disabled");
                }
            }
        }
    }
    private void Getnc_KhoaHocPageWise(int pageIndex)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        int recordCount = 0;
        gwKhoaHoc.DataSource = nc_khoahoc.get_Tabel_nc_KhoaHoc(pageIndex, PageSize);
        recordCount = nc_khoahoc.Count_khoahoc();
        gwKhoaHoc.DataBind();
        this.PopulatePager(rptPager, recordCount, pageIndex, PageSize);
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.Getnc_KhoaHocPageWise(pageIndex);
        //Session["pageIndexnc_lophoc"] = pageIndex.ToString();
        rptPager.Visible = true;
        rptSearch.Visible = false;
    }
    //search
    private void GetSearchKhoaHocPageWise(int pageIndex, string keysearch)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        int recordCount = 0;
        gwKhoaHoc.DataSource = nc_khoahoc.Search_nc_KhoaHoc(pageIndex, PageSize, keysearch);
        recordCount = nc_khoahoc.Count_search_khoahoc(keysearch);
        gwKhoaHoc.DataBind();
        this.PopulatePager(rptSearch, recordCount, pageIndex, PageSize);
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

    protected void btnDiemDanh_ServerClick(object sender, EventArgs e)
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        if (gwKhoaHoc.SelectedRow == null)
        {
            Response.Write("<script>alert('Chưa chọn khóa học ! Vui lòng chọn 1 khóa học trong danh sách !')</script>");
        }
        else
        {
            string makhoahoc = (gwKhoaHoc.SelectedRow.FindControl("lblMaKhoaHoc") as Label).Text;
            Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/DiemDanhKhoaHoc.aspx?makhoahoc=" + makhoahoc);
        }
    }

    protected void gwKhoaHoc_SelectedIndexChanged(object sender, EventArgs e)
    {
        btnDiemDanh.Attributes.Add("class", "btn btn-default");
    }
}