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

public partial class Pages_History : BasePage
{
    HistoryLoginBLL historylogin;
    InteractiveHistoryBLL interactiveHistory;
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setcurenturl();
        if (!IsPostBack)
        {
            if (Session.GetCurrentUser() == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {
                if (!check_permiss(Session.GetCurrentUser().UserID, FunctionName.History))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    this.load_gwHistoryLogin();
                    this.load_rpInteractiveHistory();
                }
            }
        }
    }
    private void load_gwHistoryLogin()
    {
        try
        {
            historylogin = new HistoryLoginBLL();
            gwHistoryLogin.DataSource = historylogin.getTbHistoryLogin();
            gwHistoryLogin.DataBind();
        }
        catch (Exception ex)
        {

            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    //rpInteractiveHistory
    private void load_rpInteractiveHistory()
    {
        interactiveHistory = new InteractiveHistoryBLL();
        rpInteractiveHistory.DataSource = interactiveHistory.DataTableInteractiveHistory();
        rpInteractiveHistory.DataBind();
    }

    private Boolean DeleteHistoryLogin(DateTime time)
    {
        historylogin = new HistoryLoginBLL();
        return this.historylogin.DeleteByDate(time);
    }
    private Boolean DeleteInteractiveHistory(DateTime time)
    {
        interactiveHistory = new InteractiveHistoryBLL();
        return this.interactiveHistory.DeleteInteractiveHistoryByCreatedate(time);
    }
    private Boolean DeleteAllHistoryLogin()
    {
        historylogin = new HistoryLoginBLL();
        return this.historylogin.DeleteAll();
    }
    private Boolean DeleteAllInteractiveHistory()
    {
        interactiveHistory = new InteractiveHistoryBLL();
        return this.interactiveHistory.DeleteAll();
    }

    protected void btnDeleteHistory_Click(object sender, EventArgs e)
    {
        try
        {
            bool result = HasPermission(Session.GetCurrentUser().UserID, FunctionName.History, TypeAudit.Delete);
            if (result == false)
            {
                this.AlertPageValid(true, "Bạn không có quyền thực hiện chức năng này !", alertPageValid, lblPageValid);
            }
            else
            {
                int selecttype = Convert.ToInt32(dlItemsFrom.SelectedValue);
                DateTime dtime = DateTime.Now;
                bool ck1;
                bool ck2;
                switch (selecttype)
                {
                    case 1:
                        dtime = dtime.AddHours(-1);
                        ck1 = (chkLoginHistory.Checked) ? DeleteHistoryLogin(dtime) : false;
                        ck2 = (chkInteractiveHistory.Checked) ? DeleteInteractiveHistory(dtime) : false;
                        this.load_gwHistoryLogin();
                        this.load_rpInteractiveHistory();
                        break;
                    case 2:
                        dtime = dtime.AddHours(-24);
                        ck1 = (chkLoginHistory.Checked) ? DeleteHistoryLogin(dtime) : false;
                        ck2 = (chkInteractiveHistory.Checked) ? DeleteInteractiveHistory(dtime) : false;
                        this.load_gwHistoryLogin();
                        this.load_rpInteractiveHistory();
                        break;
                    case 3:
                        dtime = dtime.AddHours(-168);
                        ck1 = (chkLoginHistory.Checked) ? DeleteHistoryLogin(dtime) : false;
                        ck2 = (chkInteractiveHistory.Checked) ? DeleteInteractiveHistory(dtime) : false;
                        this.load_gwHistoryLogin();
                        this.load_rpInteractiveHistory();
                        break;
                    case 4:
                        dtime = dtime.AddHours(-672);
                        ck1 = (chkLoginHistory.Checked) ? DeleteHistoryLogin(dtime) : false;
                        ck2 = (chkInteractiveHistory.Checked) ? DeleteInteractiveHistory(dtime) : false;
                        this.load_gwHistoryLogin();
                        this.load_rpInteractiveHistory();
                        break;
                    case 5:
                        ck1 = (chkLoginHistory.Checked) ? DeleteAllHistoryLogin() : false;
                        ck2 = (chkInteractiveHistory.Checked) ? DeleteAllInteractiveHistory() : false;
                        this.load_gwHistoryLogin();
                        this.load_rpInteractiveHistory();
                        break;
                }
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}