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

public partial class QuanLyHoSo_QLHoSoDuHoc : BasePage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        this.setcurenturl();
        if(!IsPostBack)
        {
            UserAccounts ac = Session.GetCurrentUser();
            if (ac == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {
                if (!check_permiss(ac.UserID, FunctionName.NewUser))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    //
                }
            }
        }
    }



    protected void btnCreateNewDoc_ServerClick(object sender, EventArgs e)
    {

        Response.Redirect("http://" + Request.Url.Authority + "/QuanLyHoSo/BangKeKhaiThongTin.aspx");
    }
}