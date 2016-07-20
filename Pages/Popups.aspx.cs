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
using System.Drawing.Imaging;
using System.Drawing;

public partial class Pages_Popups : BasePage
{
    PopupsBLL popups;
    PostBLL posts;
    ImagesBLL images;
    public int PageSize = 10;

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
                    this.GetPopupsPageWise(1);
                    btnfixPopupInfor.Attributes.Add("class", "btn btn-default disabled");
                    this.load_dlforPost();
                    
                }
            }
        }
    }
    private void load_dlforPost()
    {
        posts = new PostBLL();
        this.load_DropdownList(dlforPost, posts.ListAllPosts(), "PostTitle", "PostID");
        dlforPost.Items.Insert(0, new ListItem("-- Chọn Bài Viết --", "0"));
    }
    private void load_dlEForPost()
    {
        posts = new PostBLL();
        this.load_DropdownList(dlEForPost, posts.ListAllPosts(), "PostTitle", "PostID");
        dlEForPost.Items.Insert(0, new ListItem("-- Chọn Bài Viết --", "0"));
    }
    protected void btnNewPopup_Click(object sender, EventArgs e)
    {
        try
        {
            popups = new PopupsBLL();
            UserAccounts ac = Session.GetCurrentUser();
            string dateString = DateTime.Now.ToString("MM-dd-yyyy");
            string dirFullPath = HttpContext.Current.Server.MapPath("../images/Popups/" + dateString + "/");

            if (!Directory.Exists(dirFullPath))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
            {
                Directory.CreateDirectory(dirFullPath);
            }
            string fileName = Path.GetFileName(FileImgUpload.PostedFile.FileName);
            ImageCodecInfo jgpEncoder = null;
            string str_image = "";
            string fileExtension = "";
            if (!string.IsNullOrEmpty(fileName))
            {
                fileExtension = Path.GetExtension(fileName);
                str_image = dateString + "-" + RandomName + fileExtension;
                string pathToSave = HttpContext.Current.Server.MapPath("../images/Popups/" + dateString + "/") + str_image;
                //file.SaveAs(pathToSave);
                System.Drawing.Image image = System.Drawing.Image.FromStream(FileImgUpload.FileContent);
                if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                    jgpEncoder = GetEncoder(ImageFormat.Gif);
                else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                    jgpEncoder = GetEncoder(ImageFormat.Jpeg);
                else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Bmp.Guid)
                    jgpEncoder = GetEncoder(ImageFormat.Bmp);
                else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                    jgpEncoder = GetEncoder(ImageFormat.Png);
                else
                    throw new System.ArgumentException("Invalid File Type");
                Bitmap bmp1 = new Bitmap(FileImgUpload.FileContent);
                Encoder myEncoder = Encoder.Quality;
                EncoderParameters myEncoderParameters = new EncoderParameters(1);
                EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 30L);
                myEncoderParameters.Param[0] = myEncoderParameter;
                bmp1.Save(pathToSave, jgpEncoder, myEncoderParameters);

                this.popups.NewPopup(txtPermalink.Text, txtShortDescription.Text, "images/Popups/" + dateString + "/" + str_image, txtViewOnPage.Text, false, Session.GetCurrentUser().UserID, txtRedirectLink.Text, Convert.ToInt32(dlforPost.SelectedValue));

                Response.Redirect(Request.Url.AbsoluteUri);
            }
            else
            {
                Response.Write("<script>alert('Upload Image False !')</script>");
                return;
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    private void GetPopupsPageWise(int pageIndex)
    {
        popups = new PopupsBLL();
        int recordCount = 0;
        gwPopups.DataSource = popups.GetPopupsPageWise(pageIndex, PageSize);
        recordCount = popups.RecordCountPopups();
        gwPopups.DataBind();
        this.PopulatePager(recordCount, pageIndex);
    }
    private void PopulatePager(int recordCount, int currentPage)
    {
        List<ListItem> pages = new List<ListItem>();
        int startIndex, endIndex;
        int pagerSpan = 5;

        //Calculate the Start and End Index of pages to be displayed.
        double dblPageCount = (double)((decimal)recordCount / Convert.ToDecimal(PageSize));
        int pageCount = (int)Math.Ceiling(dblPageCount);
        startIndex = currentPage > 1 && currentPage + pagerSpan - 1 < pagerSpan ? currentPage : 1;
        endIndex = pageCount > pagerSpan ? pagerSpan : pageCount;
        if (currentPage > pagerSpan % 2)
        {
            if (currentPage == 2)
            {
                endIndex = 5;
            }
            else
            {
                endIndex = currentPage + 2;
            }
        }
        else
        {
            endIndex = (pagerSpan - currentPage) + 1;
        }

        if (endIndex - (pagerSpan - 1) > startIndex)
        {
            startIndex = endIndex - (pagerSpan - 1);
        }

        if (endIndex > pageCount)
        {
            endIndex = pageCount;
            startIndex = ((endIndex - pagerSpan) + 1) > 0 ? (endIndex - pagerSpan) + 1 : 1;
        }

        //Add the First Page Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("First", "1"));
        }

        //Add the Previous Button.
        if (currentPage > 1)
        {
            pages.Add(new ListItem("<<", (currentPage - 1).ToString()));
        }

        for (int i = startIndex; i <= endIndex; i++)
        {
            pages.Add(new ListItem(i.ToString(), i.ToString(), i != currentPage));
        }

        //Add the Next Button.
        if (currentPage < pageCount)
        {
            pages.Add(new ListItem(">>", (currentPage + 1).ToString()));
        }

        //Add the Last Button.
        if (currentPage != pageCount)
        {
            pages.Add(new ListItem("Last", pageCount.ToString()));
        }
        rptPager.DataSource = pages;
        rptPager.DataBind();
    }
    protected void Page_Changed(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetPopupsPageWise(pageIndex);
    }
    protected void gwPopups_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                LinkButton del = e.Row.FindControl("linkBtnDel") as LinkButton;
                del.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this ?')");
            }
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.ToString() + "')</script>");
        }
    }

    protected void gwPopups_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        try
        {
            popups = new PopupsBLL();
            int pID = Convert.ToInt32((gwPopups.Rows[e.RowIndex].FindControl("lblID") as Label).Text);
            string filename = Server.MapPath("../" + popups.ListPopupsWithID(pID).FirstOrDefault().PopupUrl);
            if (popups.DeletePopup(pID))
            {
                if (!string.IsNullOrWhiteSpace(filename))
                {
                    if ((System.IO.File.Exists(filename)))
                    {
                        System.IO.File.Delete(filename);
                        this.GetPopupsPageWise(1);
                    }
                }
            }
            else
            {
                this.AlertPageValid(true, "False to connect server !", alertPageValid, lblPageValid);
            }
        }
        catch (IOException ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }

    protected void gwPopups_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            popups = new PopupsBLL();
            this.load_dlEForPost();
            btnfixPopupInfor.Attributes.Add("class", "btn btn-default");
            int ID = Convert.ToInt32((gwPopups.SelectedRow.FindControl("lblID") as Label).Text);
            Popups popup = popups.ListPopupsWithID(ID).FirstOrDefault();
            txtEPermalink.Text = popup.Permalink;
            txtEShortDescription.Text = popup.ShortDescription;
            txtEVireOnPage.Text = popup.ViewOnPage;
            txtERedirectLink.Text = popup.RedirectLink;
            dlEForPost.Items.FindByValue(popup.PostID.ToString()).Selected = true;
            ImageUpdate.ImageUrl = "../" + popup.PopupUrl;
            chkStatus.Checked = popup.PopupStatus;
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    protected void chkShow_CheckedChanged(object sender, EventArgs e)
    {
        try
        {
            CheckBox chk = (sender as CheckBox);
            GridViewRow row = (GridViewRow)(((CheckBox)sender).NamingContainer);
            HiddenField hdnCheck = (HiddenField)row.Cells[4].FindControl("hiddenField1");
            popups = new PopupsBLL();
            if (popups.UpdateStatus(Convert.ToInt32(hdnCheck.Value), chk.Checked))
            {
                this.popups.UpdateStatusInPostID(Convert.ToInt32(hdnCheck.Value), false, popups.ListPopupsWithID(Convert.ToInt32(hdnCheck.Value)).FirstOrDefault().ViewOnPage);
            }
            this.GetPopupsPageWise(1);

        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    private string UrluploadPopup()
    {
        popups = new PopupsBLL();
        string dateString = DateTime.Now.ToString("MM-dd-yyyy");
        string dirFullPath = HttpContext.Current.Server.MapPath("../images/Popups/" + dateString + "/");

        if (!Directory.Exists(dirFullPath))   // CHECK IF THE FOLDER EXISTS. IF NOT, CREATE A NEW FOLDER.
        {
            Directory.CreateDirectory(dirFullPath);
        }
        string fileName = Path.GetFileName(FileImgUpload.PostedFile.FileName);
        ImageCodecInfo jgpEncoder = null;
        string str_image = "";
        string fileExtension = "";
        if (!string.IsNullOrEmpty(fileName))
        {
            fileExtension = Path.GetExtension(fileName);
            str_image = dateString + "-" + RandomName + fileExtension;
            string pathToSave = HttpContext.Current.Server.MapPath("../images/Popups/" + dateString + "/") + str_image;
            //file.SaveAs(pathToSave);
            System.Drawing.Image image = System.Drawing.Image.FromStream(FileImgUpload.FileContent);
            if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Gif.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Gif);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Jpeg.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Jpeg);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Bmp.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Bmp);
            else if (image.RawFormat.Guid == System.Drawing.Imaging.ImageFormat.Png.Guid)
                jgpEncoder = GetEncoder(ImageFormat.Png);
            else
                throw new System.ArgumentException("Invalid File Type");
            Bitmap bmp1 = new Bitmap(FileImgUpload.FileContent);
            Encoder myEncoder = Encoder.Quality;
            EncoderParameters myEncoderParameters = new EncoderParameters(1);
            EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, 30L);
            myEncoderParameters.Param[0] = myEncoderParameter;
            bmp1.Save(pathToSave, jgpEncoder, myEncoderParameters);
        }
        return (str_image == "") ? "" : "images/Popups/" + dateString + "/" + str_image;
    }
    protected void btnUpdatePop_Click(object sender, EventArgs e)
    {
        try
        {
            popups = new PopupsBLL();
            int PopID = Convert.ToInt32((gwPopups.SelectedRow.FindControl("lblID") as Label).Text);
            Popups pop = popups.ListPopupsWithID(PopID).FirstOrDefault();
            if (popups.UpdatePopup(PopID, txtEPermalink.Text, txtEShortDescription.Text, (UrluploadPopup() == "") ? pop.PopupUrl : UrluploadPopup(), txtEVireOnPage.Text, chkStatus.Checked, txtERedirectLink.Text, Convert.ToInt32(dlEForPost.SelectedValue)))
            {
                if (chkStatus.Checked)
                {
                    this.popups.UpdateStatusInPostID(PopID, false, txtEVireOnPage.Text);
                }
                this.GetPopupsPageWise(1);
            }
            else
            {
                this.AlertPageValid(true, "False to Connect server !", alertPageValid, lblPageValid);
            }
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
}