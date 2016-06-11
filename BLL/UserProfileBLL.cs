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
    public class UserProfileBLL
    {
        DataServices DB = new DataServices();
        public string defaultdate = "12/12/1900";
        public List<UserProfile> getUserProfileWithID(int userId)
        {
            string sql = "select * from UserProfile where UserID=@userId";
            if(!this.DB.OpenConnection())
            {
                return null;
            }
            SqlParameter puserId = new SqlParameter("userId", userId);
            DataTable tb = DB.DAtable(sql, puserId);
            List<UserProfile> lst = new List<UserProfile>();
            foreach(DataRow r in tb.Rows)
            {
                UserProfile us = new UserProfile();
                us.ProfileID = (int)r["ProfileID"];
                us.UserID = (string.IsNullOrEmpty(r["UserID"].ToString())) ? 0 : (int)r["UserID"];
                us.FirstName = (string.IsNullOrEmpty(r["FirstName"].ToString())) ? "" : (string)r["FirstName"];
                us.LastName = (string.IsNullOrEmpty(r["LastName"].ToString())) ? "" : (string)r["LastName"];
                us.Sex = (string.IsNullOrEmpty(r["Sex"].ToString())) ? 0 : (int)r["Sex"];
                us.Birthday = (string.IsNullOrEmpty(r["Birthday"].ToString())) ? Convert.ToDateTime(defaultdate) : (DateTime)r["Birthday"];
                us.CountryID = (string.IsNullOrEmpty(r["CountryID"].ToString())) ? 0 : (int)r["CountryID"];
                us.ProvinceID = (string.IsNullOrEmpty(r["ProvinceID"].ToString())) ? 0 : (int)r["ProvinceID"];
                us.DistrictID = (string.IsNullOrEmpty(r["DistrictID"].ToString())) ? 0 : (int)r["DistrictID"];
                us.Address_ui = (string.IsNullOrEmpty(r["Address_ui"].ToString())) ? "" : (string)r["Address_ui"];
                us.MobileNumber = (string.IsNullOrEmpty(r["MobileNumber"].ToString())) ? "" : (string)r["MobileNumber"];
                us.Interests = (string.IsNullOrEmpty(r["Interests"].ToString())) ? "" : (string)r["Interests"];
                us.Occupation = (string.IsNullOrEmpty(r["Occupation"].ToString())) ? "" : (string)r["Occupation"];
                us.About = (string.IsNullOrEmpty(r["About"].ToString())) ? "" : (string)r["About"];
                us.WebsiteUrl = (string.IsNullOrEmpty(r["WebsiteUrl"].ToString())) ? "" : (string)r["WebsiteUrl"];
                us.Img_id = (string.IsNullOrEmpty(r["Img_id"].ToString())) ? 0 : (int)r["Img_id"];
                us.DateOfStart = (DateTime)r["DateOfStart"];
                us.UserStatus = (string.IsNullOrEmpty(r["UserStatus"].ToString())) ? 0 : (int)r["UserStatus"];
                lst.Add(us);
            }
            this.DB.CloseConnection();
            return lst;
        }
        public Boolean UpdatePersonalInfo(int uid, string fname, string lname, int sex, DateTime bday, int cid, int pid, int did, string address, string phone, string Interests, string Occupation, string about, string url)
        {
            string sql = "Exec UpdatePersonalInfo @uid,@fname,@lname,@sex,@bday,@cid,@pid,@did,@address,@phone,@Interests,@Occupation,@about,@url";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter paramuid = new SqlParameter("uid", uid);
            SqlParameter paramfname = new SqlParameter("fname", fname);
            SqlParameter paramlname = new SqlParameter("lname", lname);
            SqlParameter paramsex = new SqlParameter("sex", sex);
            SqlParameter parambday = new SqlParameter("bday", bday);
            SqlParameter paramcid = new SqlParameter("cid", cid);
            SqlParameter parampid = new SqlParameter("pid", pid);
            SqlParameter paramdid = new SqlParameter("did", did);
            SqlParameter paramaddress = new SqlParameter("address", address);
            SqlParameter paramphone = new SqlParameter("phone", phone);
            SqlParameter paramInterests = new SqlParameter("Interests", Interests);
            SqlParameter paramOccupation = new SqlParameter("Occupation", Occupation);
            SqlParameter paramabout = new SqlParameter("about", about);
            SqlParameter paramurl = new SqlParameter("url", url);
            this.DB.Updatedata(sql, paramuid, paramfname, paramlname, paramsex, parambday, paramcid, parampid, paramdid, paramaddress, paramphone, paramInterests, paramOccupation, paramabout, paramurl);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean UpdatePersonalInfoWithoutDistrict(int uid, string fname, string lname, int sex, DateTime bday, int cid, int pid, string address, string phone, string Interests, string Occupation, string about, string url)
        {
            string sql = "Exec UpdatePersonalInfoWithoutDistrict @uid,@fname,@lname,@sex,@bday,@cid,@pid,@address,@phone,@Interests,@Occupation,@about,@url";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter paramuid = new SqlParameter("uid", uid);
            SqlParameter paramfname = new SqlParameter("fname", fname);
            SqlParameter paramlname = new SqlParameter("lname", lname);
            SqlParameter paramsex = new SqlParameter("sex", sex);
            SqlParameter parambday = new SqlParameter("bday", bday);
            SqlParameter paramcid = new SqlParameter("cid", cid);
            SqlParameter parampid = new SqlParameter("pid", pid);
            SqlParameter paramaddress = new SqlParameter("address", address);
            SqlParameter paramphone = new SqlParameter("phone", phone);
            SqlParameter paramInterests = new SqlParameter("Interests", Interests);
            SqlParameter paramOccupation = new SqlParameter("Occupation", Occupation);
            SqlParameter paramabout = new SqlParameter("about", about);
            SqlParameter paramurl = new SqlParameter("url", url);
            this.DB.Updatedata(sql, paramuid, paramfname, paramlname, paramsex, parambday, paramcid, parampid, paramaddress, paramphone, paramInterests, paramOccupation, paramabout, paramurl);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean UpdatePersonalInfoWithoutProvinceAndDistrict(int uid, string fname, string lname, int sex, DateTime bday, int cid, string address, string phone, string Interests, string Occupation, string about, string url)
        {
            string sql = "Exec UpdatePersonalInfoWithoutProvinceAndDistrict @uid,@fname,@lname,@sex,@bday,@cid,@address,@phone,@Interests,@Occupation,@about,@url";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter paramuid = new SqlParameter("uid", uid);
            SqlParameter paramfname = new SqlParameter("fname", fname);
            SqlParameter paramlname = new SqlParameter("lname", lname);
            SqlParameter paramsex = new SqlParameter("sex", sex);
            SqlParameter parambday = new SqlParameter("bday", bday);
            SqlParameter paramcid = new SqlParameter("cid", cid);
            SqlParameter paramaddress = new SqlParameter("address", address);
            SqlParameter paramphone = new SqlParameter("phone", phone);
            SqlParameter paramInterests = new SqlParameter("Interests", Interests);
            SqlParameter paramOccupation = new SqlParameter("Occupation", Occupation);
            SqlParameter paramabout = new SqlParameter("about", about);
            SqlParameter paramurl = new SqlParameter("url", url);
            this.DB.Updatedata(sql, paramuid, paramfname, paramlname, paramsex, parambday, paramcid, paramaddress, paramphone, paramInterests, paramOccupation, paramabout, paramurl);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean UpdatePersonalInfoWithoutCountryProvinceDistrict(int uid, string fname, string lname, int sex, DateTime bday, string address, string phone, string Interests, string Occupation, string about, string url)
        {
            string sql = "Exec UpdatePersonalInfoWithoutCountryProvinceDistrict @uid,@fname,@lname,@sex,@bday,@address,@phone,@Interests,@Occupation,@about,@url";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter paramuid = new SqlParameter("uid", uid);
            SqlParameter paramfname = new SqlParameter("fname", fname);
            SqlParameter paramlname = new SqlParameter("lname", lname);
            SqlParameter paramsex = new SqlParameter("sex", sex);
            SqlParameter parambday = new SqlParameter("bday", bday);
            SqlParameter paramaddress = new SqlParameter("address", address);
            SqlParameter paramphone = new SqlParameter("phone", phone);
            SqlParameter paramInterests = new SqlParameter("Interests", Interests);
            SqlParameter paramOccupation = new SqlParameter("Occupation", Occupation);
            SqlParameter paramabout = new SqlParameter("about", about);
            SqlParameter paramurl = new SqlParameter("url", url);
            this.DB.Updatedata(sql, paramuid, paramfname, paramlname, paramsex, parambday, paramaddress, paramphone, paramInterests, paramOccupation, paramabout, paramurl);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean SetNullImages(int uid)
        {
            string sql = "Update UserProfile set Img_id=null where UserID=@uid";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter paramuid = new SqlParameter("uid", uid);
            this.DB.Updatedata(sql, paramuid);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean UpdateImages(int uid, int imgid)
        {
            string sql = "Update UserProfile set Img_id=@imgid where UserID=@uid";
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            SqlParameter paramuid = new SqlParameter("uid", uid);
            SqlParameter pimgid = new SqlParameter("imgid", imgid);
            this.DB.Updatedata(sql, paramuid, pimgid);
            this.DB.CloseConnection();
            return true;
        }
        public Boolean CreateUserProfile(int UserID, string FirstName, string LastName, int UserStatus)
        {
            if (!this.DB.OpenConnection())
            {
                return false;
            }
            string sql = "insert into UserProfile(UserID,FirstName,LastName,UserStatus) values(@UserID,@FirstName,@LastName,@UserStatus)";
            SqlParameter pUserID = new SqlParameter("@UserID", UserID);
            SqlParameter pFirstName = (FirstName == "") ? new SqlParameter("@FirstName", DBNull.Value) : new SqlParameter("@FirstName", FirstName);
            SqlParameter pLastName = (LastName == "") ? new SqlParameter("@LastName", DBNull.Value) : new SqlParameter("@LastName", LastName);
            SqlParameter pUserStatus = new SqlParameter("@UserStatus", UserStatus);
            this.DB.Updatedata(sql, pUserID, pFirstName, pLastName, pUserStatus);
            this.DB.CloseConnection();
            return true;
        }
        public int getProfileID(int UserID)
        {
            int proID = 0;
            if (!this.DB.OpenConnection())
            {
                return 0;
            }
            string sql = "select ProfileID from UserProfile where UserID=@UserID";
            SqlParameter pUserID = new SqlParameter("@UserID", UserID);
            DataTable tb = DB.DAtable(sql, pUserID);
            foreach(DataRow r in tb.Rows)
            {
                proID = (string.IsNullOrEmpty(r[0].ToString())) ? 0 : (int)r[0];
            }
            this.DB.CloseConnection();
            return proID;
        }
    }
}
