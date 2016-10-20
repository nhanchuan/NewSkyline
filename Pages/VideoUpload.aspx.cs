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
using System.IO;
using System.Security.Cryptography;

public partial class Pages_VideoUpload : BasePage
{
    VideosBLL videos;
    VideoTypeBLL videotype;
    public int PageSize = 20;

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
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    //this.load_dlVideoType();
                    btnEditVideo.Attributes.Add("class", "btn btn-warning disabled");
                    this.load_dlvideoscategory();
                    this.GetVideosPageWise(1);
                    //this.Load_dlEVideoCatwegory();
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
    private void Load_dlEVideoCatwegory()
    {
        videotype = new VideoTypeBLL();
        this.load_DropdownList(dlEVideoCatwegory, videotype.getallVideoType(), "TypeName", "VideotypeID");
        dlEVideoCatwegory.Items.Insert(0, new ListItem("-- Chọn mục videos --", "0"));
    }
    private void GetVideosPageWise(int pageIndex)
    {
        videos = new VideosBLL();
        int recordCount = 0;
        gwVideos.DataSource = videos.GetVideoPageWise(pageIndex, PageSize);
        recordCount = videos.CountRecordVideo();
        gwVideos.DataBind();
        this.PopulatePager(rptPager, recordCount, pageIndex, PageSize);
    }

    protected void Page_Changed(object sender, EventArgs e)
    {

        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetVideosPageWise(pageIndex);
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

    protected void btnAddVideos_Click(object sender, EventArgs e)
    {
        try
        {


            if (Session.GetCurrentUser() == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {
                videos = new VideosBLL();
                int videotype = Convert.ToInt32(dlvideoscategory.SelectedValue);
                string videosname = txtVideosname.Text;
                string videolink = txtLink.Text;
                string shortDesc = txtShortdescrition.Text;

                if (videos.NewVideos(videosname, videolink, videotype, shortDesc, Session.GetCurrentUser().UserID))
                {
                    txtLink.Text = "";
                    txtShortdescrition.Text = "";
                    txtVideosname.Text = "";
                    dlvideoscategory.ClearSelection();
                    this.GetVideosPageWise(1);
                }
                else
                {
                    this.AlertPageValid(true, "New Videos Fales ! Error to connect Server !", alertPageValid, lblPageValid);
                }
            }


            //string str = "kzjhbgkhb@zdjn";
            //Response.Write("<script>alert('" + SplitString(str, '@', 1) + "')</script>");

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    public string hinhvideo(string link)
    {
        if (link.IndexOf("youtube.com") > 0)
        {
            if (link.IndexOf("watch?v=") > 0)
            {

                link = SplitString(link, '&', 0);
                link = link + "=";
                link = SplitString(link, '=', 1);
                return "http://img.youtube.com/vi/" + link + "/0.jpg";
            }
        }
        return "../images/default.jpg";
    }

    protected void gwVideos_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("linkBtnDel") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Bạn có chắc muốn xóa video này ?')");
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwVideos_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            if (Session.GetCurrentUser() == null)
            {
                Response.Redirect("http://" + Request.Url.Authority + "/Login.aspx");
            }
            else
            {

                //if (HasPermission(Session.GetCurrentUser().UserID, FunctionName.NewVideo, TypeAudit.Delete))
                //{
                //    this.AlertPageValid(true, "Bạn không có quyền thực hiện chức năng này !", alertPageValid, lblPageValid);
                //}
                //else
                //{
                videos = new VideosBLL();
                int videoid = Convert.ToInt32((gwVideos.Rows[e.RowIndex].FindControl("lblVideoID") as Label).Text);
                if (videos.DeleteVideo(videoid))
                {
                    this.GetVideosPageWise(1);
                }
                else
                {
                    this.AlertPageValid(true, "Delete Videos False. Error to connect server  !", alertPageValid, lblPageValid);
                }
                //}
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwVideos_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            btnEditVideo.Attributes.Add("class", "btn btn-warning");
            videos = new VideosBLL();
            int videoid = Convert.ToInt32((gwVideos.SelectedRow.FindControl("lblVideoID") as Label).Text);

            Videos vid = videos.getVideoWithId(videoid).FirstOrDefault();


            if (vid != null)
            {
                txtEvideoname.Text = vid.VideoName;
                txtELink.Text = vid.VideoUrl;
                txtEShortDesc.Text = vid.ShortDecsription;
                Load_dlEVideoCatwegory();
                dlEVideoCatwegory.Items.FindByValue(vid.VideotypeID.ToString().Length == 0 ? "0" : vid.VideotypeID.ToString()).Selected = true;
            }

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void btnSeveEdit_Click(object sender, EventArgs e)
    {
        try
        {
            videos = new VideosBLL();
            int videoid = Convert.ToInt32((gwVideos.SelectedRow.FindControl("lblVideoID") as Label).Text);
            if (videos.UpdateVideos(videoid, txtEvideoname.Text, txtELink.Text, Convert.ToInt32(dlEVideoCatwegory.SelectedValue), txtEShortDesc.Text))
            {
                this.GetVideosPageWise(1);
            }
            else
            {
                this.AlertPageValid(true, "Update false !", alertPageValid, lblPageValid);
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}