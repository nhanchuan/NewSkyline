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

public partial class kus_admin_DiemDanhKhoaHoc : BasePage
{
    nc_LopHocBLL nc_lophoc;
    nc_KhoaHocBLL nc_khoahoc;
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
                    string MaKhoaHoc = Request.QueryString["makhoahoc"];
                    if (!CheckQuerystring(MaKhoaHoc))
                    {
                        Response.Redirect("http://" + Request.Url.Authority + "/kus_admin/DiemDanh.aspx");
                    }
                    else
                    {
                        lblPageisValid.Text = "";
                        this.Load_TitlePage(MaKhoaHoc);
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
    private void Load_TitlePage(string makhoahoc)
    {
        try
        {
            nc_khoahoc = new nc_KhoaHocBLL();
            List<nc_KhoaHoc> lstKH = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(makhoahoc);
            nc_KhoaHoc khoahoc = lstKH.FirstOrDefault();
            lblTitlekhoahoc.Text = khoahoc.TenKhoaHoc + " - " + makhoahoc;
            lblTitleNKG.Text = khoahoc.NgayKhaiGiang.ToString("dd-MM-yyyy");
        }
        catch(Exception ex)
        {
            lblPageisValid.Text = ex.ToString();
        }
    }
    [Serializable()]
    public class clsListDate
    {
        public string Num { get; set; }
        public DateTime StrDate { get; set; }
        public clsListDate(string num, DateTime strdate)
        {
            Num = num;
            StrDate = strdate;
        }
    }
    private void load_gwNgayDiemDanh()
    {
        List<clsListDate> clsdate = new List<clsListDate>();
        gwNgayDiemDanh.DataSource = clsdate;
        gwNgayDiemDanh.DataBind();
    }
}