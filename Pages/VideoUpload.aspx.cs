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
using System.IO;
using System.Security.Cryptography;

public partial class Pages_VideoUpload : BasePage
{
    VideosBLL video;
    VideoTypeBLL videotype;

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
                if (!check_permiss(Session.GetCurrentUser().UserID, FunctionName.NewVideo))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    //this.load_dlVideoType();
                    this.load_dlvideoscategory();
                }
            }
        }
    }
    private void load_dlvideoscategory()
    {
        videotype = new VideoTypeBLL();
        dlvideoscategory.DataSource = videotype.getallVideoType();
        dlvideoscategory.DataTextField = "TypeName";
        dlvideoscategory.DataValueField = "VideotypeID";
        dlvideoscategory.DataBind();
        dlvideoscategory.Items.Insert(0, new ListItem("-- Chọn mục videos --", "0"));
    }
    
    //protected void load_dlVideoType()
    //{
    //    videotype = new VideoTypeBLL();
    //    dlVideoType.DataSource = videotype.getallVideoType();
    //    dlVideoType.DataTextField = "TypeName";
    //    dlVideoType.DataValueField = "VideotypeID";
    //    dlVideoType.DataBind();
    //    dlVideoType.Items.Insert(0, new ListItem("-- Chọn danh mục Videos --", "0"));
    //}

    //protected void UploaderVideo_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
    //{
    //    video = new VideosBLL();
    //    UserAccounts ad = Session.GetCurrentUser();
    //    string dateString = DateTime.Now.ToString("MM-dd-yyyy");
    //    string fileExtension = Path.GetExtension(e.FileName).ToLower();
    //    string fileName = Path.GetFileNameWithoutExtension(e.FileName);

    //    string nameRandom = "kus-edu-vn" + "-" + dateString + "-" + XoaKyTuDacBiet(fileName) + "-" + RandomName;
    //    string contentType = "";
    //    switch (fileExtension)
    //    {
    //        case ".mp4":
    //            contentType = "video/mp4";
    //            break;
    //        case ".flv":
    //            contentType = "video/x-flv";
    //            break;
    //        case ".3gp":
    //            contentType = "video/3gpp";
    //            break;
    //        case ".avi":
    //            contentType = "video/x-msvideo";
    //            break;
    //        case ".wmv":
    //            contentType = "video/x-ms-wmv";
    //            break;
    //    }
    //    if (this.video.UploadVideos(fileName, "videos/uploads/" + nameRandom + fileExtension, contentType, 0, "", ad.UserID))
    //    {
    //        UploaderVideo.SaveAs(Server.MapPath("../videos/uploads/" + nameRandom + fileExtension));
    //    }
    //    else
    //    {
    //        Response.Write("<script>alert('Upload Video False !')</script>");
    //        return;
    //    }

    //}
}