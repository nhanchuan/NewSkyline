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

public partial class Pages_FileManager :BasePage
{
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
                if (!check_permiss(ac.UserID, FunctionName.FileExplorer))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    if(HasPermission(ac.UserID,FunctionName.FileExplorer,TypeAudit.View))
                    {
                        FileExplorer.Attributes.Add("class", "row");
                    }
                    else
                    {
                        FileExplorer.Attributes.Add("class", "row display-none");
                        this.AlertPageValid(true, "Bạn không có quyền xem nội dung này !", alertPageValid, lblPageValid);
                    }
                }
            }
        }
    }
}