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
    kus_LichHocBLL kus_lichhoc;
    kus_NgayDiemDanhBLL kus_ngaydiemdanh;
    kus_DiemDanhBLL kus_diemdanh;
    kus_GhiDanhBLL kus_ghidanh;
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
                        //panelLichDiemDanh.Visible = (!checkLichHoc()) ? false : true;
                        btnSaveDiemDanh.Visible = false;
                        btnDownloadExcel.Visible = false;
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
        catch (Exception ex)
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
    private void load_gwNgayDiemDanh(int MaKhoaHoc)
    {
        kus_ngaydiemdanh = new kus_NgayDiemDanhBLL();
        List<clsListDate> clsdate = new List<clsListDate>();
        gwNgayDiemDanh.DataSource = kus_ngaydiemdanh.getTableNgayDiemDanh(MaKhoaHoc);
        gwNgayDiemDanh.DataBind();
    }
    private Boolean checkLichHoc()
    {
        bool result = true;
        try
        {
            nc_khoahoc = new nc_KhoaHocBLL();
            kus_lichhoc = new kus_LichHocBLL();
            string MaKhoaHoc = Request.QueryString["makhoahoc"];
            List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(MaKhoaHoc);
            nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
            List<kus_LichHoc> lstLichHoc = kus_lichhoc.getListLichHocWithKhoaHoc(khoahoc.ID);
            kus_LichHoc lichhoc = lstLichHoc.FirstOrDefault();
            if (lichhoc == null)
            {
                result = false;
            }
        }
        catch (Exception ex)
        {
            lblPageisValid.Text = ex.ToString();
        }
        return result;
    }

    protected void btnLoadLichDiemDanh_ServerClick(object sender, EventArgs e)
    {
        if (!checkLichHoc())
        {
            lblMessageDiemDanh.Text = "Khóa Học chưa có lịch học. Vui lòng vào Quản lý -> Khóa học, để lên lịch học cho Khóa học này !";
            panelLichDiemDanh.Visible = false;
        }
        else
        {
            nc_khoahoc = new nc_KhoaHocBLL();
            kus_lichhoc = new kus_LichHocBLL();
            kus_ngaydiemdanh = new kus_NgayDiemDanhBLL();
            string MaKhoaHoc = Request.QueryString["makhoahoc"];
            List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(MaKhoaHoc);
            nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
            List<kus_NgayDiemDanh> lstNDD = kus_ngaydiemdanh.getListWithKhoaHoc(khoahoc.ID);
            kus_NgayDiemDanh ngayDD = lstNDD.FirstOrDefault();
            if (ngayDD == null)
            {
                //Khoi Tao va View
                this.HamTaoLichDD();
                this.load_gwNgayDiemDanh(khoahoc.ID);
            }
            else
            {
                //Chi View
                this.load_gwNgayDiemDanh(khoahoc.ID);
            }
            lblMessageDiemDanh.Text = "";
            panelLichDiemDanh.Visible = true;
        }
    }
    //Ham Tao Lich Diem danh
    private void HamTaoLichDD()
    {
        nc_khoahoc = new nc_KhoaHocBLL();
        kus_lichhoc = new kus_LichHocBLL();
        kus_ngaydiemdanh = new kus_NgayDiemDanhBLL();
        string MaKhoaHoc = Request.QueryString["makhoahoc"];
        List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(MaKhoaHoc);
        nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
        DataTable tbLichHoc = kus_lichhoc.kus_LichHocWithKhoaHocEvent(khoahoc.ID);
        //List<clsListDate> lstdate = new List<clsListDate>();
        foreach (DataRow r in tbLichHoc.Rows)
        {
            DateTime tostart = Convert.ToDateTime(r["NgayKhaiGiang"]);
            DateTime toend = Convert.ToDateTime(r["NgayKetThuc"]);
            while (tostart <= toend)
            {
                if (tostart.DayOfWeek.ToString() == "Monday" && (int)r["DayID"] == 1)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                if (tostart.DayOfWeek.ToString() == "Tuesday" && (int)r["DayID"] == 2)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                if (tostart.DayOfWeek.ToString() == "Wednesday" && (int)r["DayID"] == 3)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                if (tostart.DayOfWeek.ToString() == "Thursday" && (int)r["DayID"] == 4)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                if (tostart.DayOfWeek.ToString() == "Friday" && (int)r["DayID"] == 5)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                if (tostart.DayOfWeek.ToString() == "Saturday" && (int)r["DayID"] == 6)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                if (tostart.DayOfWeek.ToString() == "Sunday" && (int)r["DayID"] == 7)
                {
                    this.kus_ngaydiemdanh.InsertNgayDiemDanh((int)r["KhoaHoc"], new DateTime(tostart.Year, tostart.Month, tostart.Day));
                }
                tostart = tostart.AddDays(1);
            }
        }
    }

    //Diem danh
    private void load_gwDiemDanh(int ngaydiemdanh)
    {
        kus_diemdanh = new kus_DiemDanhBLL();
        gwDiemDanh.DataSource = kus_diemdanh.TBDiemDanhWithNgayID(ngaydiemdanh);
        gwDiemDanh.DataBind();
    }
    private void HamTaoDiemDanh()
    {
        kus_diemdanh = new kus_DiemDanhBLL();
        kus_ghidanh = new kus_GhiDanhBLL();
        nc_khoahoc = new nc_KhoaHocBLL();
        string MaKhoaHoc = Request.QueryString["makhoahoc"];
        List<nc_KhoaHoc> lstkh = nc_khoahoc.getListKhoaHocWithMaKhoaHoc(MaKhoaHoc);
        nc_KhoaHoc khoahoc = lstkh.FirstOrDefault();
        DataTable tbghidanh = kus_ghidanh.TbGhiDanhWithKhoaHoc(khoahoc.ID);
        int NgayID = Convert.ToInt32((gwNgayDiemDanh.SelectedRow.FindControl("lblNgayID") as Label).Text);
        foreach (DataRow r in tbghidanh.Rows)
        {
            this.kus_diemdanh.CreateDiemDanh(NgayID, (int)r["HocVienID"]);
        }
    }
    protected void gwNgayDiemDanh_SelectedIndexChanged(object sender, EventArgs e)
    {
        
        try
        {
            int sumDD = Convert.ToInt32((gwNgayDiemDanh.SelectedRow.FindControl("lblSumNgayDD") as Label).Text);
            int NgayID = Convert.ToInt32((gwNgayDiemDanh.SelectedRow.FindControl("lblNgayID") as Label).Text);
            btnSaveDiemDanh.Visible = true;
            btnDownloadExcel.Visible = true;
            if (sumDD == 0)
            {
                //Ham Tao + load
                this.HamTaoDiemDanh();
                this.load_gwDiemDanh(NgayID);
            }
            else
            {
                //Ham Load
                this.load_gwDiemDanh(NgayID);
            }
        }
        catch(Exception ex)
        {
            lblPageisValid.Text = ex.ToString();
        }
    }

    protected void gwNgayDiemDanh_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblSumNgay = e.Row.FindControl("lblSumNgayDD") as Label;
                if(lblSumNgay.Text=="0")
                {
                    LinkButton select = e.Row.FindControl("lkselect") as LinkButton;
                    select.Attributes.Add("onclick", "return confirm('Bạn chắc muốn tạo Bảng điểm danh cho ngày này ?')");
                }
            }
        }
        catch (Exception ex)
        {
            lblPageisValid.Text = ex.ToString();
        }
    }

    protected void btnSelectAllDD_ServerClick(object sender, EventArgs e)
    {
        foreach (GridViewRow r in gwDiemDanh.Rows)
        {
            CheckBox ch = (CheckBox)r.FindControl("chkrowDiemDanh");
            ch.Checked = true;
        }
    }

    protected void btnUnselectAllDD_ServerClick(object sender, EventArgs e)
    {
        foreach (GridViewRow r in gwDiemDanh.Rows)
        {
            CheckBox ch = (CheckBox)r.FindControl("chkrowDiemDanh");
            ch.Checked = false;
        }
    }
    protected void btnSaveDiemDanh_ServerClick(object sender, EventArgs e)
    {
        try
        {
            kus_diemdanh = new kus_DiemDanhBLL();
           
            foreach (GridViewRow r in gwDiemDanh.Rows)
            {
                //int DiemDanhID = Convert.ToInt32((gwDiemDanh.SelectedRow.FindControl("lblDiemDanhID") as Label).Text);
                Label lbdiemdanhid = (Label)r.FindControl("lblDiemDanhID");
                CheckBox chkDD = (CheckBox)r.FindControl("chkrowDiemDanh");
                CheckBox chkCoP = (CheckBox)r.FindControl("chkrowCoPhep");
                int DiemDanhID = Convert.ToInt32(lbdiemdanhid.Text);
                int DD = (chkDD.Checked) ? 1 : 0;
                int CoP = (chkCoP.Checked) ? 1 : 0;
                this.kus_diemdanh.UpdateDiemDanh(DiemDanhID, DD, CoP, "");
                //Response.Write("<script>alert('" + DD.ToString() + "    " + CoP.ToString() + "')</script>");
            }
            Response.Redirect(Request.Url.AbsoluteUri);
        }
        catch(Exception ex)
        {
            lblPageisValid.Text = ex.ToString();
        }
    }
}