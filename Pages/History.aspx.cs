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
}