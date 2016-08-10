﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading;
using System.Globalization;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;
using System.Drawing.Imaging;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Text;


public class BasePage : System.Web.UI.Page
{
    SecuriryServices services;
    UserPermissBLL userpermiss;
    InteractiveHistoryBLL interactiveHistory;
    public BasePage()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public void setcurenturl()
    {
        string url = "";
        url = Request.Url.AbsoluteUri;
        Session.SetCurrentURL(url);
    }
    protected override void InitializeCulture()
    {
        string language = "en-US";
        //Detect User's Language.
        if (Request.UserLanguages != null)
        {
            //Set the Language.
            language = Request.UserLanguages[0];
        }
        //Check if PostBack is caused by Language DropDownList.
        string sesionlang = Session.GetCurrentLang();
        if (sesionlang != null)
        {
            //Set the Language.
            language = sesionlang;
        }
        //Set the Culture.
        //Thread.CurrentThread.CurrentCulture = new CultureInfo(language);
        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(language);
        Thread.CurrentThread.CurrentUICulture = new CultureInfo(language);
        base.InitializeCulture();
    }
    //value Salt pasword
    protected string SaltPassword()
    {
        return "AS@2d23";
    }
    protected string RandomName
    {
        get
        {
            return new string(
                Enumerable.Repeat("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789", 6)
                    .Select(s =>
                    {
                        var cryptoResult = new byte[6];
                        new RNGCryptoServiceProvider().GetBytes(cryptoResult);
                        return s[new Random(BitConverter.ToInt32(cryptoResult, 0)).Next(s.Length)];
                    })
                    .ToArray());
        }
    }
    public bool IsNumber(string pText)
    {
        Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
        return regex.IsMatch(pText);
    }
    protected ImageCodecInfo GetEncoder(ImageFormat format)
    {
        ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
        foreach (ImageCodecInfo codec in codecs)
        {
            if (codec.FormatID == format.Guid)
            {
                return codec;
            }
        }
        return null;
    }

    public bool check_permiss(int uid, string FCode)
    {
        services = new SecuriryServices();
        bool checkp = services.Check_PermissionFunct(uid, FCode);
        return checkp;
    }
    //Kiểm tra quyền
    public bool HasPermission(int uid, string Fcode, TypeAudit audit)
    {
        services = new SecuriryServices();
        userpermiss = new UserPermissBLL();
        List<UserPermiss> lstP = userpermiss.lstPermissWithCode(uid, Fcode);
        UserPermiss up = lstP.FirstOrDefault();
        return services.HasPermission(audit, up.PermisstionNumber);
    }
    // LOAD DROPDOWNLIST()
    public void load_DropdownList(DropDownList dl, Object obj, string textfield, string valuefield)
    {
        dl.DataSource = obj;
        dl.DataTextField = textfield;
        dl.DataValueField = valuefield;
        dl.DataBind();
    }
    //Chuyển tiêu đề tiếng việt có dấu sang không dấu dạng URL abc-def-ghi
    public string XoaKyTuDacBiet(string str)
    {
        string title_url = "";
        str = str.Replace(" ", "-");
        Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
        string temp = str.Normalize(NormalizationForm.FormD);
        title_url = regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        return title_url;
    }
    //=======================================================================
    public void PopulatePager(Repeater rptPager, int recordCount, int currentPage, int PageSize)
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
    //========================================================================
    public string getday(string str)
    {
        string day = "";
        if (!IsNumber(str.Substring(0, 2)))
        {
            return "";
        }
        else
        {
            day = str.Substring(0, 2);
        }
        return day;
    }
    public string getmonth(string str)
    {
        string month = "";
        if (!IsNumber(str.Substring(3, 2)))
        {
            return "";
        }
        else
        {
            month = str.Substring(3, 2);
        }
        return month;
    }
    public string getyear(string str)
    {
        string year = "";
        if (str.Length != 10)
        {
            return "";
        }
        else
        {
            if (!IsNumber(str.Substring(6, 4)))
            {
                return "";
            }
            else
            {
                year = str.Substring(6, 4);
            }
        }
        return year;
    }
    //============================================================================================================

    /// <summary>
    /// Hiện thông báo
    /// </summary>
    /// <param name="isvalid">Bật/Tắt thông báo</param>
    /// <param name="validString">Chuỗi thông báo</param>
    /// <param name="divValid">Điều khiển html thông báo</param>
    /// <param name="lblvalid">Label nhận chuỗi thông báo</param>
    public void AlertPageValid(bool isvalid, string validString, System.Web.UI.HtmlControls.HtmlGenericControl divValid, Label lblvalid)
    {
        if (isvalid)
        {
            divValid.Attributes.Add("class", "alert alert-danger");
            lblvalid.Text = "<strong>Error!</strong>" + " " + validString.ToString();
        }
        else
        {
            divValid.Attributes.Add("class", "alert alert-danger display-none");
            lblvalid.Text = "";
        }
    }
    //G3et Ip Address
    public string GetIPAddress()
    {
        System.Web.HttpContext context = System.Web.HttpContext.Current;
        string ipAddress = context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

        if (!string.IsNullOrEmpty(ipAddress))
        {
            string[] addresses = ipAddress.Split(',');
            if (addresses.Length != 0)
            {
                return addresses[0];
            }
        }

        return context.Request.ServerVariables["REMOTE_ADDR"];
    }
    //===============
    public string SUSerectKey()//ilovemyfamily + Salt
    {
        string serectkey = "RBlpsQq68lgl2a7Rc8Fe0lgl7MWrmrH3XkYdTtOLiPhnZCrKzbmyXkZ7DFmpBZrPASpBFcEDmemD0i3Cn0hPfQ==";
        return serectkey;
    }

    /// <summary>
    /// Save thi Interactive History
    /// </summary>
    /// <param name="userid">User ID</param>
    /// <param name="content">Interactive Content </param>
    /// <param name="link">Interactive Link</param>
    public void InteractiveHistory(int userid, string content, string link)
    {
        interactiveHistory = new InteractiveHistoryBLL();
        this.interactiveHistory.NewInteractiveHistory(userid, content, link);
    }
    ///HasOutdate
    public Boolean HasOutdate(string date)
    {
        DateTime dtime = Convert.ToDateTime(date);
        if (dtime <= DateTime.Now)
        {
            return true;
        }
        return false;
    }
}

