using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BLL
{
    public class VideosBLL
    {
        DataServices DB = new DataServices();
        public List<Videos> getallVideo()
        {
            string sql = "select * from Videos";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            DataTable tb = DB.DAtable(sql);
            List<Videos> lst = new List<Videos>();
            foreach (DataRow r in tb.Rows)
            {
                Videos vi = new Videos();
                vi.VideoID = (int)r["VideoID"];
                vi.VideoName = (string.IsNullOrEmpty(r["VideoName"].ToString())) ? "" : (string)r["VideoName"];
                vi.VideoUrl = (string.IsNullOrEmpty(r["VideoUrl"].ToString())) ? "" : (string)r["VideoUrl"];
                vi.VideotypeID = (string.IsNullOrEmpty(r["VideotypeID"].ToString())) ? 0 : (int)r["VideotypeID"];
                vi.ShortDecsription = (string.IsNullOrEmpty(r["ShortDecsription"].ToString())) ? "" : (string)r["ShortDecsription"];
                vi.DateOfCreate = (DateTime)r["DateOfCreate"];
                vi.UserUpload = (string.IsNullOrEmpty(r["UserUpload"].ToString())) ? 0 : (int)r["UserUpload"];
                lst.Add(vi);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<Videos> getVideoWithId(int videoId)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from Videos where VideoID=@videoId";
            SqlParameter pVideoId = new SqlParameter("videoId", videoId);
            DataTable tb = DB.DAtable(sql, pVideoId);
            List<Videos> lst = new List<Videos>();
            foreach (DataRow r in tb.Rows)
            {
                Videos vi = new Videos();
                vi.VideoID = (int)r["VideoID"];
                vi.VideoName = (string.IsNullOrEmpty(r["VideoName"].ToString())) ? "" : (string)r["VideoName"];
                vi.VideoUrl = (string.IsNullOrEmpty(r["VideoUrl"].ToString())) ? "" : (string)r["VideoUrl"];
                vi.VideotypeID = (string.IsNullOrEmpty(r["VideotypeID"].ToString())) ? 0 : (int)r["VideotypeID"];
                vi.ShortDecsription = (string.IsNullOrEmpty(r["ShortDecsription"].ToString())) ? "" : (string)r["ShortDecsription"];
                vi.DateOfCreate = (DateTime)r["DateOfCreate"];
                vi.UserUpload = (string.IsNullOrEmpty(r["UserUpload"].ToString())) ? 0 : (int)r["UserUpload"];
                lst.Add(vi);
            }
            this.DB.CloseConnection();
            return lst;
        }

        //public Boolean UploadVideos(string VideoName, string VideoUrl, string ContentType, int VideotypeID, string ShortDecsription, int UserUpload)
        //{
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    string sql = "Exec UploadVideos @VideoName,@VideoUrl,@ContentType,@VideotypeID,@ShortDecsription,@UserUpload";
        //    SqlParameter pVideoName = new SqlParameter("@VideoName", VideoName);
        //    SqlParameter pVideoUrl = new SqlParameter("@VideoUrl", VideoUrl);
        //    SqlParameter pContentType = new SqlParameter("@ContentType", ContentType);
        //    SqlParameter pVideotypeID = (VideotypeID == 0) ? new SqlParameter("@VideotypeID", DBNull.Value) : new SqlParameter("@VideotypeID", VideotypeID);
        //    SqlParameter pShortDecsription = (ShortDecsription == "") ? new SqlParameter("@ShortDecsription", DBNull.Value) : new SqlParameter("@ShortDecsription", ShortDecsription);
        //    SqlParameter pUserUpload = new SqlParameter("@UserUpload", UserUpload);
        //    this.DB.Updatedata(sql, pVideoName, pVideoUrl, pContentType, pVideotypeID, pShortDecsription, pUserUpload);
        //    this.DB.CloseConnection();
        //    return true;
        //}

        //new videso
        public Boolean NewVideos(string VideoName, string VideoUrl, int VideotypeID, string ShortDecsription, int UserUpload)
        {

            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "insert into Videos(VideoName,VideoUrl,VideotypeID,ShortDecsription,UserUpload) values(@VideoName,@VideoUrl,@VideotypeID,@ShortDecsription,@UserUpload)";
            SqlParameter pVideoName = new SqlParameter("@VideoName", VideoName);
            SqlParameter pVideoUrl = new SqlParameter("@VideoUrl", VideoUrl);
            SqlParameter pVideotypeID = (VideotypeID == 0) ? new SqlParameter("@VideotypeID", DBNull.Value) : new SqlParameter("@VideotypeID", VideotypeID);
            SqlParameter pShortDecsription = (ShortDecsription == "") ? new SqlParameter("@ShortDecsription", DBNull.Value) : new SqlParameter("@ShortDecsription", ShortDecsription);
            SqlParameter pUserUpload = new SqlParameter("@UserUpload", UserUpload);
            this.DB.Updatedata(sql, pVideoName, pVideoUrl, pVideotypeID, pShortDecsription, pUserUpload);
            this.DB.CloseConnection();
            return true;
        }
        //Update Videos
        public Boolean UpdateVideos(int VideoID, string VideoName, string VideoUrl, int VideotypeID, string ShortDecsription)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "update Videos set VideoName=@VideoName,VideoUrl=@VideoUrl,VideotypeID=@VideotypeID,ShortDecsription=@ShortDecsription where VideoID=@VideoID";
            SqlParameter pVideoID = new SqlParameter("@VideoID", VideoID);
            SqlParameter pVideoName = new SqlParameter("@VideoName", VideoName);
            SqlParameter pVideoUrl = new SqlParameter("@VideoUrl", VideoUrl);
            SqlParameter pVideotypeID = (VideotypeID == 0) ? new SqlParameter("@VideotypeID", DBNull.Value) : new SqlParameter("@VideotypeID", VideotypeID);
            SqlParameter pShortDecsription = (ShortDecsription == "") ? new SqlParameter("@ShortDecsription", DBNull.Value) : new SqlParameter("@ShortDecsription", ShortDecsription);
            this.DB.Updatedata(sql, pVideoID, pVideoName, pVideoUrl, pVideotypeID, pShortDecsription);
            this.DB.CloseConnection();
            return true;
        }
        public int CountRecordVideo()
        {
            int count = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(*) from Videos";
            count = DB.GetValues(sql);
            this.DB.CloseConnection();
            return count;
        }
        //public int CountRecordVideoType(int type)
        //{
        //    int count = 0;
        //    string sql = "select COUNT(*) from Videos where VideotypeID=@type";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return 0;
        //    }
        //    SqlParameter ptype = new SqlParameter("type", type);
        //    count = DB.GetValues(sql, ptype);
        //    this.DB.CloseConnection();
        //    return count;
        //}
        //public int CountRecordVideoSearch(string keysearch)
        //{
        //    int count = 0;
        //    string sql = "select Count(*) from Videos Where VideoName like '%'+@keysearch+'%' or ShortDecsription like '%'+@keysearch+'%'";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return 0;
        //    }
        //    SqlParameter pkeysearch = new SqlParameter("keysearch", keysearch);
        //    count = DB.GetValues(sql, pkeysearch);
        //    this.DB.CloseConnection();
        //    return count;
        //}
        public DataTable GetVideoPageWise(int PageIndex, int PageSize)
        {
            string sql = "Exec GetVideoPageWise @PageIndex,@PageSize";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter paramPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter paramPageSize = new SqlParameter("PageSize", PageSize);
            DataTable tb = DB.DAtable(sql, paramPageIndex, paramPageSize);
            this.DB.CloseConnection();
            return tb;
        }
        //public DataTable GetVideoTypePageWise(int PageIndex, int PageSize, int type)
        //{
        //    string sql = "Exec GetVideoTypePageWise @PageIndex,@PageSize,@type";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return null;
        //    }
        //    SqlParameter paramPageIndex = new SqlParameter("PageIndex", PageIndex);
        //    SqlParameter paramPageSize = new SqlParameter("PageSize", PageSize);
        //    SqlParameter ptype = new SqlParameter("type", type);
        //    DataTable tb = DB.DAtable(sql, paramPageIndex, paramPageSize, ptype);
        //    this.DB.CloseConnection();
        //    return tb;
        //}
        //public DataTable GetVideoSearchPageWise(int PageIndex, int PageSize, string keysearch)
        //{
        //    string sql = "Exec GetVideoSearchPageWise @PageIndex,@PageSize,@keysearch";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return null;
        //    }
        //    SqlParameter paramPageIndex = new SqlParameter("PageIndex", PageIndex);
        //    SqlParameter paramPageSize = new SqlParameter("PageSize", PageSize);
        //    SqlParameter pkeysearch = new SqlParameter("keysearch", keysearch);
        //    DataTable tb = DB.DAtable(sql, paramPageIndex, paramPageSize, pkeysearch);
        //    this.DB.CloseConnection();
        //    return tb;
        //}
        //public Boolean UpdateVideoInfo(int videoid, string videoname, int videoType, string shortDC)
        //{
        //    string sql = "update Videos set VideoName=@videoname, VideotypeID=@videoType, ShortDecsription=@shortDC where VideoID=@videoid";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter pvideoid = new SqlParameter("@videoid", videoid);
        //    SqlParameter pvideoname = new SqlParameter("@videoname", videoname);
        //    SqlParameter pvideoType = new SqlParameter("@videoType", videoType);
        //    SqlParameter pshortDC = new SqlParameter("@shortDC", shortDC);
        //    this.DB.Updatedata(sql, pvideoid, pvideoname, pvideoType, pshortDC);
        //    this.DB.CloseConnection();
        //    return true;
        //}
        //Delete Video
        public Boolean DeleteVideo(int VideoID)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "delete from Videos where VideoID=@VideoID";
            SqlParameter pVideoID = new SqlParameter("@VideoID", VideoID);
            this.DB.Updatedata(sql, pVideoID);
            this.DB.CloseConnection();
            return true;
        }
    }
}
