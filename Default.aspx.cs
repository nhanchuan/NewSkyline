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
using System.Web.Services;

public partial class _Default : BasePage
{
    CustomerProfilePrivateBLL customerProPri;
    kus_GhiDanhBLL kus_ghidanh;
    InteractiveHistoryBLL interactiveHistory;
    PostBLL posts;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setcurenturl();
        if (!this.IsPostBack)
        {
            UserAccounts user = Session.GetCurrentUser();
            if (user == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {
                //do something
                this.load_Number();
                this.load_rptfeedssystem();
                this.load_rptTopNewPost();
            }
        }
    }
    private void load_Number()
    {
        customerProPri = new CustomerProfilePrivateBLL();
        kus_ghidanh = new kus_GhiDanhBLL();
        //lblSumBoHoSo.Text = customerProPri.CounThuLyHoSoPageWise().ToString();
        //lblNumGhiDanh.Text = kus_ghidanh.CountHocVien().ToString();
    }

    private void load_rptfeedssystem()
    {
        interactiveHistory = new InteractiveHistoryBLL();
        rptfeedssystem.DataSource = interactiveHistory.DataTableInteractiveHistory();
        rptfeedssystem.DataBind();
    }
    private void load_rptTopNewPost()
    {
        posts = new PostBLL();
        rptTopNewPost.DataSource = posts.TBTopNewPost();
        rptTopNewPost.DataBind();
    }
}