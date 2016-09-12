using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAL;
using BLL;

namespace BLL
{
    public class PostBLL
    {
        DataServices DB = new DataServices();
        public string defaultdate = "12/12/1900";
        public List<POST> getallPostWithPID(int postid)
        {
            string sql = "select * from POST where PostID=@postid";
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter pId = new SqlParameter("postid", postid);
            DataTable tb = DB.DAtable(sql, pId);
            List<POST> lst = new List<POST>();

            foreach (DataRow r in tb.Rows)
            {
                POST p = new POST();
                p.PostID = (int)r["PostID"];
                p.PostTitle = (string.IsNullOrEmpty(r["PostTitle"].ToString())) ? "" : (string)r["PostTitle"];
                p.MetaKeywords = (string.IsNullOrEmpty(r["MetaKeywords"].ToString())) ? "" : (string)r["MetaKeywords"];
                p.MetaDescription = (string.IsNullOrEmpty(r["MetaDescription"].ToString())) ? "" : (string)r["MetaDescription"];
                p.PostContentVN = (string.IsNullOrEmpty(r["PostContentVN"].ToString())) ? "" : (string)r["PostContentVN"];
                p.PostContentEN = (string.IsNullOrEmpty(r["PostContentEN"].ToString())) ? "" : (string)r["PostContentEN"];
                p.PostModified = (string.IsNullOrEmpty(r["PostModified"].ToString())) ? Convert.ToDateTime(defaultdate) : (DateTime)r["PostModified"];
                p.DateOfCreate = (DateTime)r["DateOfCreate"];
                p.PostAuthor = (string.IsNullOrEmpty(r["PostAuthor"].ToString())) ? 0 : (int)r["PostAuthor"];
                p.PostStatus = (string.IsNullOrEmpty(r["PostStatus"].ToString())) ? false : (Boolean)r["PostStatus"];
                p.ViewCount = (string.IsNullOrEmpty(r["ViewCount"].ToString())) ? 0 : (int)r["ViewCount"];
                p.PostImage = (string.IsNullOrEmpty(r["PostImage"].ToString())) ? 0 : (int)r["PostImage"];
                p.PostCode = (string.IsNullOrEmpty(r["PostCode"].ToString())) ? "" : (string)r["PostCode"];
                lst.Add(p);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public List<POST> ListAllPosts()
        {

            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "select * from POST";
            DataTable tb = DB.DAtable(sql);
            List<POST> lst = new List<POST>();

            foreach (DataRow r in tb.Rows)
            {
                POST p = new POST();
                p.PostID = (int)r["PostID"];
                p.PostTitle = (string.IsNullOrEmpty(r["PostTitle"].ToString())) ? "" : (string)r["PostTitle"];
                p.MetaKeywords = (string.IsNullOrEmpty(r["MetaKeywords"].ToString())) ? "" : (string)r["MetaKeywords"];
                p.MetaDescription = (string.IsNullOrEmpty(r["MetaDescription"].ToString())) ? "" : (string)r["MetaDescription"];
                p.PostContentVN = (string.IsNullOrEmpty(r["PostContentVN"].ToString())) ? "" : (string)r["PostContentVN"];
                p.PostContentEN = (string.IsNullOrEmpty(r["PostContentEN"].ToString())) ? "" : (string)r["PostContentEN"];
                p.PostModified = (string.IsNullOrEmpty(r["PostModified"].ToString())) ? Convert.ToDateTime(defaultdate) : (DateTime)r["PostModified"];
                p.DateOfCreate = (DateTime)r["DateOfCreate"];
                p.PostAuthor = (string.IsNullOrEmpty(r["PostAuthor"].ToString())) ? 0 : (int)r["PostAuthor"];
                p.PostStatus = (string.IsNullOrEmpty(r["PostStatus"].ToString())) ? false : (Boolean)r["PostStatus"];
                p.ViewCount = (string.IsNullOrEmpty(r["ViewCount"].ToString())) ? 0 : (int)r["ViewCount"];
                p.PostImage = (string.IsNullOrEmpty(r["PostImage"].ToString())) ? 0 : (int)r["PostImage"];
                p.PostCode = (string.IsNullOrEmpty(r["PostCode"].ToString())) ? "" : (string)r["PostCode"];
                lst.Add(p);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public int PostIdWithPostCode(string postcode)
        {
            int code = 0;
            string sql = "select PostID from POST where PostCode=@postcode";
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            SqlParameter ppostcode = new SqlParameter("postcode", postcode);
            DataTable tb = DB.DAtable(sql, ppostcode);
            foreach (DataRow r in tb.Rows)
            {
                code = (string.IsNullOrEmpty(r[0].ToString())) ? 0 : (int)r[0];
            }
            this.DB.CloseConnection();
            return code;
        }
        //===================================================
        public int CountPost() //COUNT ROW IN TABLE Post
        {
            int RC = 0;
            string sql = "select COUNT(*) from POST";
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            RC = DB.GetValues(sql);
            this.DB.CloseConnection();
            return RC;
        }
        public DataTable GetAllPostPageWise(int PageIndex, int PageSize)
        {
            string sql = "Exec GetAllPostPageWise @PageIndex,@PageSize";
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
        //===================================================
        //===================================================
        public int CountPostCategory(int CategoryID) //COUNT ROW IN TABLE Post
        {
            int RC = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(*) from Post_Category_relationships where CategoryID=@CategoryID";
            SqlParameter pCategoryID = new SqlParameter("@CategoryID", CategoryID);
            RC = DB.GetValues(sql, pCategoryID);
            this.DB.CloseConnection();
            return RC;
        }
        public DataTable GetPostWithCategoryPageWise(int PageIndex, int PageSize, int CategoryID)
        {
            if (!this.DB.OpenConnection())
            {
                return null;
            }
            string sql = "Exec GetPostWithCategoryPageWise @PageIndex,@PageSize,@CategoryID";
            SqlParameter paramPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter paramPageSize = new SqlParameter("PageSize", PageSize);
            SqlParameter pCategoryID = new SqlParameter("@CategoryID", CategoryID);
            DataTable tb = DB.DAtable(sql, paramPageIndex, paramPageSize, pCategoryID);
            this.DB.CloseConnection();
            return tb;
        }
        //===================================================
        //======================================================================================================
        public Boolean NewPost(string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, DateTime PostModified, int author, Boolean status, int ViewCount, int img, string postcode)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "Exec NewPost @title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@PostModified,@author,@status,@ViewCount,@img,@postcode";
            SqlParameter ptitle = (title.Length == 0) ? new SqlParameter("title", DBNull.Value) : new SqlParameter("title", title);
            SqlParameter pmetaKeywords = (metaKeywords.Length == 0) ? new SqlParameter("metaKeywords", DBNull.Value) : new SqlParameter("metaKeywords", metaKeywords);
            SqlParameter pmetaDescription = (metaDescription.Length == 0) ? new SqlParameter("metaDescription", DBNull.Value) : new SqlParameter("metaDescription", metaDescription);
            SqlParameter pcontentVn = (contentVn.Length == 0) ? new SqlParameter("contentVn", DBNull.Value) : new SqlParameter("contentVn", contentVn);
            SqlParameter pcontentEn = (contentEn.Length == 0) ? new SqlParameter("contentEn", DBNull.Value) : new SqlParameter("contentEn", contentEn);
            SqlParameter pPostModified = new SqlParameter("@PostModified", PostModified);
            SqlParameter pauthor = new SqlParameter("author", author);
            SqlParameter pstatus = new SqlParameter("status", status);
            SqlParameter pViewCount = new SqlParameter("@ViewCount", ViewCount);
            SqlParameter pimg = (img == 0) ? new SqlParameter("img", DBNull.Value) : new SqlParameter("img", img);
            SqlParameter ppostcode = new SqlParameter("postcode", postcode);
            this.DB.Updatedata(sql, ptitle, pmetaKeywords, pmetaDescription, pcontentVn, pcontentEn, pPostModified, pauthor, pstatus, pViewCount, pimg, ppostcode);
            this.DB.CloseConnection();
            return true;
        }
        //public Boolean NewPostWithoutImg(string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, int author, Boolean status, string postcode)
        //{
        //    string sql = "Exec NewPostWithoutImg @title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@author,@status,@postcode";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter ptitle = new SqlParameter("title", title);
        //    SqlParameter pmetaKeywords = new SqlParameter("metaKeywords", metaKeywords);
        //    SqlParameter pmetaDescription = new SqlParameter("metaDescription", metaDescription);
        //    SqlParameter pcontentVn = new SqlParameter("contentVn", contentVn);
        //    SqlParameter pcontentEn = new SqlParameter("contentEn", contentEn);
        //    SqlParameter pauthor = new SqlParameter("author", author);
        //    SqlParameter pstatus = new SqlParameter("status", status);
        //    SqlParameter ppostcode = new SqlParameter("postcode", postcode);
        //    this.DB.Updatedata(sql, ptitle, pmetaKeywords, pmetaDescription,pcontentVn, pcontentEn, pauthor, pstatus, ppostcode);
        //    this.DB.CloseConnection();
        //    return true;
        //}
        //public Boolean NewPostWithPostModified(string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, DateTime modified, int author, Boolean status, int img, string postcode)
        //{
        //    string sql = "Exec NewPostWithPostModified @title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@modified,@author,@status,@img,@postcode";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter ptitle = new SqlParameter("title", title);
        //    SqlParameter pmetaKeywords = new SqlParameter("metaKeywords", metaKeywords);
        //    SqlParameter pmetaDescription = new SqlParameter("metaDescription", metaDescription);
        //    SqlParameter pcontentVn = new SqlParameter("contentVn", contentVn);
        //    SqlParameter pcontentEn = new SqlParameter("contentEn", contentEn);
        //    SqlParameter pmodified = new SqlParameter("modified", modified);
        //    SqlParameter pauthor = new SqlParameter("author", author);
        //    SqlParameter pstatus = new SqlParameter("status", status);
        //    SqlParameter pimg = new SqlParameter("img", img);
        //    SqlParameter ppostcode = new SqlParameter("postcode", postcode);
        //    this.DB.Updatedata(sql, ptitle, pmetaKeywords, pmetaDescription,pcontentVn, pcontentEn, pmodified, pauthor, pstatus, pimg, ppostcode);
        //    this.DB.CloseConnection();
        //    return true;
        //}
        //public Boolean NewPostWithPostModifiedWithoutImg(string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, DateTime modified, int author, Boolean status, string postcode)
        //{
        //    string sql = "Exec NewPostWithPostModifiedWithoutImg @title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@modified,@author,@status,@postcode";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter ptitle = new SqlParameter("title", title);
        //    SqlParameter pmetaKeywords = new SqlParameter("metaKeywords", metaKeywords);
        //    SqlParameter pmetaDescription = new SqlParameter("metaDescription", metaDescription);
        //    SqlParameter pcontentVn = new SqlParameter("contentVn", contentVn);
        //    SqlParameter pcontentEn = new SqlParameter("contentEn", contentEn);
        //    SqlParameter pmodified = new SqlParameter("modified", modified);
        //    SqlParameter pauthor = new SqlParameter("author", author);
        //    SqlParameter pstatus = new SqlParameter("status", status);
        //    SqlParameter ppostcode = new SqlParameter("postcode", postcode);
        //    this.DB.Updatedata(sql, ptitle, pmetaKeywords, pmetaDescription,pcontentVn, pcontentEn, pmodified, pauthor, pstatus, ppostcode);
        //    this.DB.CloseConnection();
        //    return true;
        //}

        //=======================================================================================================

        public Boolean UpdatePost(int postId, string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, DateTime PostModified, int author, Boolean status, int ViewCount, int img)
        {
            
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "Exec UpdatePost @postId,@title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@PostModified,@author,@status,@ViewCount,@img";
            SqlParameter ppostId = new SqlParameter("postId", postId);
            SqlParameter ptitle = (title.Length == 0) ? new SqlParameter("title", DBNull.Value) : new SqlParameter("title", title);
            SqlParameter pmetaKeywords = (metaKeywords.Length == 0) ? new SqlParameter("metaKeywords", DBNull.Value) : new SqlParameter("metaKeywords", metaKeywords);
            SqlParameter pmetaDescription = (metaDescription.Length == 0) ? new SqlParameter("metaDescription", DBNull.Value) : new SqlParameter("metaDescription", metaDescription);
            SqlParameter pcontentVn = (contentVn.Length == 0) ? new SqlParameter("contentVn", DBNull.Value) : new SqlParameter("contentVn", contentVn);
            SqlParameter pcontentEn = (contentEn.Length == 0) ? new SqlParameter("contentEn", DBNull.Value) : new SqlParameter("contentEn", contentEn);
            SqlParameter pPostModified = new SqlParameter("@PostModified", PostModified);
            SqlParameter pauthor = new SqlParameter("author", author);
            SqlParameter pstatus = new SqlParameter("status", status);
            SqlParameter pViewCount = new SqlParameter("@ViewCount", ViewCount);
            SqlParameter pimg = (img == 0) ? new SqlParameter("img", DBNull.Value) : new SqlParameter("img", img);
            this.DB.Updatedata(sql, ppostId, ptitle, pmetaKeywords, pmetaDescription, pcontentVn, pcontentEn, pPostModified, pauthor, pstatus, pViewCount, pimg);
            this.DB.CloseConnection();
            return true;
        }
        //public Boolean UpdatePostWithoutImg(int postId, string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, int author, Boolean status)
        //{
        //    string sql = "Exec UpdatePostWithoutImg @postId,@title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@author,@status";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter ppostId = new SqlParameter("postId", postId);
        //    SqlParameter ptitle = new SqlParameter("title", title);
        //    SqlParameter pmetaKeywords = new SqlParameter("metaKeywords", metaKeywords);
        //    SqlParameter pmetaDescription = new SqlParameter("metaDescription", metaDescription);
        //    SqlParameter pcontentVn = new SqlParameter("contentVn", contentVn);
        //    SqlParameter pcontentEn = new SqlParameter("contentEn", contentEn);
        //    SqlParameter pauthor = new SqlParameter("author", author);
        //    SqlParameter pstatus = new SqlParameter("status", status);
        //    this.DB.Updatedata(sql, ppostId, ptitle, pmetaKeywords, pmetaDescription, pcontentVn, pcontentEn, pauthor, pstatus);
        //    this.DB.CloseConnection();
        //    return true;
        //}
        //public Boolean UpdatePostWithPostModified(int postId, string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, DateTime modified, int author, Boolean status, int img)
        //{
        //    string sql = "Exec UpdatePostWithPostModified @postId,@title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@modified,@author,@status,@img";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter ppostId = new SqlParameter("postId", postId);
        //    SqlParameter ptitle = new SqlParameter("title", title);
        //    SqlParameter pmetaKeywords = new SqlParameter("metaKeywords", metaKeywords);
        //    SqlParameter pmetaDescription = new SqlParameter("metaDescription", metaDescription);
        //    SqlParameter pcontentVn = new SqlParameter("contentVn", contentVn);
        //    SqlParameter pcontentEn = new SqlParameter("contentEn", contentEn);
        //    SqlParameter pmodified = new SqlParameter("modified", modified);
        //    SqlParameter pauthor = new SqlParameter("author", author);
        //    SqlParameter pstatus = new SqlParameter("status", status);
        //    SqlParameter pimg = new SqlParameter("img", img);
        //    this.DB.Updatedata(sql, ppostId, ptitle, pmetaKeywords, pmetaDescription, pcontentVn, pcontentEn, pmodified, pauthor, pstatus, pimg);
        //    this.DB.CloseConnection();
        //    return true;
        //}
        //public Boolean UpdatePostWithPostModifiedWithoutImg(int postId, string title, string metaKeywords, string metaDescription, string contentVn, string contentEn, DateTime modified, int author, Boolean status)
        //{
        //    string sql = "Exec NewPostWithPostModifiedWithoutImg @title,@metaKeywords,@metaDescription,@contentVn,@contentEn,@modified,@author,@status";
        //    if (!this.DB.OpenConnection())
        //    {
        //        return false;
        //    }
        //    SqlParameter ppostId = new SqlParameter("postId", postId);
        //    SqlParameter ptitle = new SqlParameter("title", title);
        //    SqlParameter pmetaKeywords = new SqlParameter("metaKeywords", metaKeywords);
        //    SqlParameter pmetaDescription = new SqlParameter("metaDescription", metaDescription);
        //    SqlParameter pcontentVn = new SqlParameter("contentVn", contentVn);
        //    SqlParameter pcontentEn = new SqlParameter("contentEn", contentEn);
        //    SqlParameter pmodified = new SqlParameter("modified", modified);
        //    SqlParameter pauthor = new SqlParameter("author", author);
        //    SqlParameter pstatus = new SqlParameter("status", status);
        //    this.DB.Updatedata(sql, ppostId, ptitle, pmetaKeywords, pmetaDescription, pcontentVn, pcontentEn, pmodified, pauthor, pstatus);
        //    this.DB.CloseConnection();
        //    return true;
        //}
        //Delete POst With ID
        public Boolean DeleteWithPostID(int PostID)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "delete from POST where PostID=@PostID";
            SqlParameter pPostID = new SqlParameter("@PostID", PostID);
            this.DB.Updatedata(sql, pPostID);
            this.DB.CloseConnection();
            return true;
        }
    }
}
