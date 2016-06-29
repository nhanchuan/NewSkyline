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

public partial class Pages_ImagesManager : BasePage
{
    ImagesTypeBLL imagestype;
    ImagesBLL images;
    private int PageSize = 20;
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
                if (!check_permiss(user.UserID, FunctionName.ImagesManager))
                {
                    Response.Redirect("http://" + Request.Url.Authority + "/Extra/access_denied.aspx");
                }
                else
                {
                    this.AlertPageValid(false, "", alertPageValid, lblPageValid);
                    if (HasPermission(user.UserID, FunctionName.ImagesManager, TypeAudit.View))
                    {
                        ImageslibraryPanel.Attributes.Add("class", "row");
                    }
                    else
                    {
                        ImageslibraryPanel.Attributes.Add("class", "row display-none");
                        this.AlertPageValid(true, "Bạn không có quyền xem nội dung này !", alertPageValid, lblPageValid);
                    }
                    this.load_dlImagestype();
                    dlImagestype.Items.Insert(0, new ListItem("-- Hiển thị tất cả --", "0"));
                    this.GetImagesTypePageWise(1, 0);
                    rptPager.Visible = true;
                    Repeatersearch.Visible = false;
                    if (HasPermission(user.UserID, FunctionName.ImagesManager, TypeAudit.AddNew))
                    {
                        btnNewImages.Attributes.Add("class", "btn green");
                    }
                    else
                    {
                        btnNewImages.Attributes.Add("class", "btn green disabled");
                    }
                }
            }
        }
    }
    public void load_dlImagestype()
    {
        imagestype = new ImagesTypeBLL();
        dlImagestype.DataSource = imagestype.getallImagesType();
        dlImagestype.DataTextField = "ImagesTypeName";
        dlImagestype.DataValueField = "ImagesTypeID";
        dlImagestype.DataBind();
    }
    protected void btnsearchimg_ServerClick(object sender, EventArgs e)
    {
        try
        {
            this.GetImagesSearchPageWise(1, txtsearchImages.Value);
            rptPager.Visible = false;
            //divrownumber.Visible = false;
            Repeatersearch.Visible = true;
            //divsearch.Visible = true;
            //lblsearchstart.Text = ((1 - 1) * PageSize + 1).ToString();
            //lblsearchend.Text = ((((1 - 1) * PageSize + 1) + PageSize) - 1).ToString();
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
    }
    private void GetImagesTypePageWise(int pageIndex, int ImgType)
    {

        try
        {
            images = new ImagesBLL();
            rpLstImg.DataSource = new ObjectDataSource();
            int recordCount = 0;
            if (ImgType <= 0)
            {
                rpLstImg.DataSource = images.GetImagesPageWise(pageIndex, PageSize);
                recordCount = images.RecordCountImages();
            }
            else
            {
                rpLstImg.DataSource = images.GetImagesTypePageWise(pageIndex, PageSize, ImgType);
                recordCount = images.RecordCountImagesType(ImgType);
            }
            rpLstImg.DataBind();
            this.PopulatePager(recordCount, pageIndex);
            //lbltotalProduct.Text = recordCount.ToString();
        }
        catch (Exception ex)
        {
            this.AlertPageValid(true, ex.ToString(), alertPageValid, lblPageValid);
        }
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
        this.GetImagesTypePageWise(pageIndex, int.Parse(dlImagestype.SelectedValue));
        //lblstartindex.Text = ((pageIndex - 1) * PageSize + 1).ToString();
        //lblendindex.Text = ((((pageIndex - 1) * PageSize + 1) + PageSize) - 1).ToString();
    }
    private void GetImagesSearchPageWise(int pageIndex, string keysearch)
    {
        images = new ImagesBLL();

        rpLstImg.DataSource = new ObjectDataSource();
        int recordCount = 0;
        if (string.IsNullOrEmpty(keysearch) || string.IsNullOrWhiteSpace(keysearch))
        {
            rpLstImg.DataSource = images.GetImagesPageWise(pageIndex, PageSize);
            recordCount = images.RecordCountImages();
        }
        else
        {
            rpLstImg.DataSource = images.GetImagesSearchPageWise(pageIndex, PageSize, keysearch);
            recordCount = images.RecordCountSearchImages(keysearch);
        }
        rpLstImg.DataBind();
        this.PopulateSearchPager(recordCount, pageIndex);
        //lbltotalSearchProduct.Text = recordCount.ToString();

    }
    private void PopulateSearchPager(int recordCount, int currentPage)
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
        Repeatersearch.DataSource = pages;
        Repeatersearch.DataBind();
    }
    protected void Page_SearchChanged(object sender, EventArgs e)
    {
        int pageIndex = int.Parse((sender as LinkButton).CommandArgument);
        this.GetImagesSearchPageWise(pageIndex, txtsearchImages.Value.ToString());
        //lblsearchstart.Text = ((pageIndex - 1) * PageSize + 1).ToString();
        //lblsearchend.Text = ((((pageIndex - 1) * PageSize + 1) + PageSize) - 1).ToString();
    }



    protected void dlImagestype_SelectedIndexChanged(object sender, EventArgs e)
    {
        this.GetImagesTypePageWise(1, int.Parse(dlImagestype.SelectedValue));
        txtsearchImages.Value = "";
        rptPager.Visible = true;
        //divrownumber.Visible = true;
        Repeatersearch.Visible = false;
        //divsearch.Visible = false;
    }

    protected void btnreload_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }

    protected void btndelete_ServerClick(object sender, EventArgs e)
    {
        bool result = HasPermission(Session.GetCurrentUser().UserID, FunctionName.ImagesManager, TypeAudit.Delete);
        if (result == false)
        {
            Response.Write("<script>alert('Bạn không có quyền thực hiện chức năng này !')</script>");
        }
        else
        {
            images = new ImagesBLL();

            try
            {
                this.images.DeleteImages(HiddenImages.Value);
                Response.Redirect(Request.Url.AbsoluteUri);
            }
            catch (Exception ex)
            {
                Response.Write("<script>alert('" + ex.ToString() + "')</script>");
            }
        }
    }
}