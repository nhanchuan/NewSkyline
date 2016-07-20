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
        public List<Popups> ListAllPopups()
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sqlquery = "select * from Popups";
            DataTable tb = dt.DAtable(sqlquery);
            List<Popups> lst = new List<Popups>();
            foreach(DataRow r in tb.Rows)
            {
                Popups pop = new Popups();
                pop.ID = (int)r["ID"];
                pop.Permalink = (string.IsNullOrEmpty(r["Permalink"].ToString())) ? "" : (string)r["Permalink"];
                pop.ShortDescription = (string.IsNullOrEmpty(r["ShortDescription"].ToString())) ? "" : (string)r["ShortDescription"];
                pop.PopupUrl = (string.IsNullOrEmpty(r["PopupUrl"].ToString())) ? "" : (string)r["PopupUrl"];
                pop.ViewOnPage = (string.IsNullOrEmpty(r["ViewOnPage"].ToString())) ? "" : (string)r["ViewOnPage"];
                pop.PopupStatus = (Boolean)r["PopupStatus"];
                pop.UserUpload = (string.IsNullOrEmpty(r["UserUpload"].ToString())) ? 0 : (int)r["UserUpload"];
                pop.CreateDate = (DateTime)r["CreateDate"];
                pop.RedirectLink = (string.IsNullOrEmpty(r["RedirectLink"].ToString())) ? "" : (string)r["RedirectLink"];
                pop.PostID = (string.IsNullOrEmpty(r["PostID"].ToString())) ? 0 : (int)r["PostID"];
                lst.Add(pop);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public List<Popups> ListPopupsWithID(int ID)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sqlquery = "select * from Popups where ID=@ID";
            SqlParameter pID = new SqlParameter("@ID", ID);
            DataTable tb = dt.DAtable(sqlquery, pID);
            List<Popups> lst = new List<Popups>();
            foreach (DataRow r in tb.Rows)
            {
                Popups pop = new Popups();
                pop.ID = (int)r["ID"];
                pop.Permalink = (string.IsNullOrEmpty(r["Permalink"].ToString())) ? "" : (string)r["Permalink"];
                pop.ShortDescription = (string.IsNullOrEmpty(r["ShortDescription"].ToString())) ? "" : (string)r["ShortDescription"];
                pop.PopupUrl = (string.IsNullOrEmpty(r["PopupUrl"].ToString())) ? "" : (string)r["PopupUrl"];
                pop.ViewOnPage = (string.IsNullOrEmpty(r["ViewOnPage"].ToString())) ? "" : (string)r["ViewOnPage"];
                pop.PopupStatus = (Boolean)r["PopupStatus"];
                pop.UserUpload = (string.IsNullOrEmpty(r["UserUpload"].ToString())) ? 0 : (int)r["UserUpload"];
                pop.CreateDate = (DateTime)r["CreateDate"];
                pop.RedirectLink = (string.IsNullOrEmpty(r["RedirectLink"].ToString())) ? "" : (string)r["RedirectLink"];
                pop.PostID = (string.IsNullOrEmpty(r["PostID"].ToString())) ? 0 : (int)r["PostID"];
                lst.Add(pop);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public Boolean NewPopup(string Permalink, string ShortDescription, string PopupUrl, string ViewOnPage, Boolean PopupStatus, int UserUpload, string RedirectLink, int PostID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sqlquery = "Exec NewPopup @Permalink,@ShortDescription,@PopupUrl,@ViewOnPage,@PopupStatus,@UserUpload,@RedirectLink,@PostID";
            SqlParameter pPermalink = (Permalink == "") ? new SqlParameter("@Permalink", DBNull.Value) : new SqlParameter("@Permalink", Permalink);
            SqlParameter pShortDescription = (ShortDescription == "") ? new SqlParameter("@ShortDescription", DBNull.Value) : new SqlParameter("@ShortDescription", ShortDescription);
            SqlParameter pPopupUrl = (PopupUrl == "") ? new SqlParameter("@PopupUrl", DBNull.Value) : new SqlParameter("@PopupUrl", PopupUrl);
            SqlParameter pViewOnPage = (ViewOnPage == "") ? new SqlParameter("@ViewOnPage", DBNull.Value) : new SqlParameter("@ViewOnPage", ViewOnPage);
            SqlParameter pPopupStatus = new SqlParameter("@PopupStatus", PopupStatus);
            SqlParameter pUserUpload = new SqlParameter("@UserUpload", UserUpload);
            SqlParameter pRedirectLink = (RedirectLink == "") ? new SqlParameter("@RedirectLink", DBNull.Value) : new SqlParameter("@RedirectLink", RedirectLink);
            SqlParameter pPostID = (PostID == 0) ? new SqlParameter("@PostID", DBNull.Value) : new SqlParameter("@PostID", PostID);
            this.dt.Updatedata(sqlquery, pPermalink, pShortDescription, pPopupUrl, pViewOnPage, pPopupStatus, pUserUpload, pRedirectLink, pPostID);
            this.dt.CloseConnection();
            return true;
        }
        //Update
        public Boolean UpdatePopup(int ID, string Permalink, string ShortDescription, string PopupUrl, string ViewOnPage, Boolean PopupStatus, string RedirectLink, int PostID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sqlquery = "Exec UpdatePopup @ID,@Permalink,@ShortDescription,@PopupUrl,@ViewOnPage,@PopupStatus,@RedirectLink,@PostID";
            SqlParameter pID = new SqlParameter("@ID", ID);
            SqlParameter pPermalink = (Permalink == "") ? new SqlParameter("@Permalink", DBNull.Value) : new SqlParameter("@Permalink", Permalink);
            SqlParameter pShortDescription = (ShortDescription == "") ? new SqlParameter("@ShortDescription", DBNull.Value) : new SqlParameter("@ShortDescription", ShortDescription);
            SqlParameter pPopupUrl = (PopupUrl == "") ? new SqlParameter("@PopupUrl", DBNull.Value) : new SqlParameter("@PopupUrl", PopupUrl);
            SqlParameter pViewOnPage = (ViewOnPage == "") ? new SqlParameter("@ViewOnPage", DBNull.Value) : new SqlParameter("@ViewOnPage", ViewOnPage);
            SqlParameter pPopupStatus = new SqlParameter("@PopupStatus", PopupStatus);
            SqlParameter pRedirectLink = (RedirectLink == "") ? new SqlParameter("@RedirectLink", DBNull.Value) : new SqlParameter("@RedirectLink", RedirectLink);
            SqlParameter pPostID = (PostID == 0) ? new SqlParameter("@PostID", DBNull.Value) : new SqlParameter("@PostID", PostID);
            this.dt.Updatedata(sqlquery, pID, pPermalink, pShortDescription, pPopupUrl, pViewOnPage, pPopupStatus, pRedirectLink, pPostID);
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
        //Update popupsatatus
        public Boolean UpdateStatus(int ID, Boolean PopupStatus)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "update Popups set PopupStatus=@PopupStatus where ID=@ID";
            SqlParameter pID = new SqlParameter("@ID", ID);
            SqlParameter pPopupStatus = new SqlParameter("@PopupStatus", PopupStatus);
            this.dt.Updatedata(sql, pID, pPopupStatus);
            this.dt.CloseConnection();
            return true;
        }
        public Boolean UpdateStatusInPostID(int ID, Boolean PopupStatus, string ViewOnPage)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "update Popups set PopupStatus=@PopupStatus where ID<>@ID and ViewOnPage=@ViewOnPage";
            SqlParameter pID = new SqlParameter("@ID", ID);
            SqlParameter pPopupStatus = new SqlParameter("@PopupStatus", PopupStatus);
            SqlParameter pViewOnPage = new SqlParameter("@ViewOnPage", ViewOnPage);

            this.dt.Updatedata(sql, pID, pPopupStatus, pViewOnPage);
            this.dt.CloseConnection();
            return true;
        }
        //Delete 
        public Boolean DeletePopup(int ID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from Popups where ID=@ID";
            SqlParameter pID = new SqlParameter("@ID", ID);
            this.dt.Updatedata(sql, pID);
            this.dt.CloseConnection();
            return true;
        }

    }
}
