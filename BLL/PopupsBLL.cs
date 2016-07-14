using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using System.Data;
using System.Data.SqlClient;

namespace BLL
{
    public class PopupsBLL
    {
        DataServices dt = new DataServices();
        public Boolean NewPopup(string Permalink, string ShortDescription, string PopupUrl, string ViewOnPage, Boolean PopupStatus, int UserUpload)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sqlquery = "Exec NewPopup @Permalink,@ShortDescription,@PopupUrl,@ViewOnPage,@PopupStatus,@UserUpload";
            SqlParameter pPermalink = (Permalink == "") ? new SqlParameter("@Permalink", DBNull.Value) : new SqlParameter("@Permalink", Permalink);
            SqlParameter pShortDescription = (ShortDescription == "") ? new SqlParameter("@ShortDescription", DBNull.Value) : new SqlParameter("@ShortDescription", ShortDescription);
            SqlParameter pPopupUrl = (PopupUrl == "") ? new SqlParameter("@PopupUrl", DBNull.Value) : new SqlParameter("@PopupUrl", PopupUrl);
            SqlParameter pViewOnPage = (ViewOnPage == "") ? new SqlParameter("@ViewOnPage", DBNull.Value) : new SqlParameter("@ViewOnPage", ViewOnPage);
            SqlParameter pPopupStatus = new SqlParameter("@PopupStatus", PopupStatus);
            SqlParameter pUserUpload = new SqlParameter("@UserUpload", UserUpload);
            this.dt.Updatedata(sqlquery, pPermalink, pShortDescription, pPopupUrl, pViewOnPage, pPopupStatus, pUserUpload);
            this.dt.CloseConnection();
            return true;
        }
        public DataTable GetPopupsPageWise(int PageIndex, int PageSize)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "Exec GetPopupsPageWise @PageIndex,@PageSize";
            SqlParameter paramPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter paramPageSize = new SqlParameter("PageSize", PageSize);
            DataTable tb = dt.DAtable(sql, paramPageIndex, paramPageSize);
            this.dt.CloseConnection();
            return tb;
        }
        public int RecordCountPopups() //COUNT ROW PPopup
        {
            int RC = 0;
            
            if (!this.dt.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(*) from Popups";
            RC = dt.GetValues(sql);
            this.dt.CloseConnection();
            return RC;
        }
    }
}
