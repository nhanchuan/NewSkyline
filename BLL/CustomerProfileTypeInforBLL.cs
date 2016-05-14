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
    public class CustomerProfileTypeInforBLL
    {
        DataServices dt = new DataServices();
        public List<CustomerProfileTypeInfor> getListEithProfileID(int ProfileID)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from CustomerProfileTypeInfor where ProfileID=@ProfileID";
            SqlParameter pProfileID = new SqlParameter("@ProfileID", ProfileID);
            DataTable tb = dt.DAtable(sql, pProfileID);
            List<CustomerProfileTypeInfor> lst = new List<CustomerProfileTypeInfor>();
            foreach (DataRow r in tb.Rows)
            {
                CustomerProfileTypeInfor cp = new CustomerProfileTypeInfor();
                cp.TypeInforID = (int)r["TypeInforID"];
                cp.ProfileID = (string.IsNullOrEmpty(r["ProfileID"].ToString())) ? 0 : (int)r["ProfileID"];
                cp.BagProfileTypeID = (string.IsNullOrEmpty(r["BagProfileTypeID"].ToString())) ? 0 : (int)r["BagProfileTypeID"];
                cp.CountryID = (string.IsNullOrEmpty(r["CountryID"].ToString())) ? 0 : (int)r["CountryID"];
                cp.Education = (string.IsNullOrEmpty(r["Education"].ToString())) ? 0 : (int)r["Education"];
                lst.Add(cp);
            }
            this.dt.CloseConnection();
            return lst;
        }
        //New
        public Boolean NewProfileTypeInfor(int ProfileID, int BagProfileTypeID, int CountryID, int Education)
        {
            if(!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into CustomerProfileTypeInfor(ProfileID,BagProfileTypeID,CountryID,Education) values (@ProfileID,@BagProfileTypeID,@CountryID,@Education)";
            SqlParameter pProfileID = (ProfileID == 0) ? new SqlParameter("@ProfileID", DBNull.Value) : new SqlParameter("@ProfileID", ProfileID);
            SqlParameter pBagProfileTypeID = (BagProfileTypeID == 0) ? new SqlParameter("@BagProfileTypeID", DBNull.Value) : new SqlParameter("@BagProfileTypeID", BagProfileTypeID);
            SqlParameter pCountryID = (CountryID == 0) ? new SqlParameter("@CountryID", DBNull.Value) : new SqlParameter("@CountryID", CountryID);
            SqlParameter pEducation = (Education == 0) ? new SqlParameter("@Education", DBNull.Value) : new SqlParameter("@Education", Education);
            this.dt.Updatedata(sql, pProfileID, pBagProfileTypeID, pCountryID, pEducation);
            this.dt.CloseConnection();
            return true;
        }
        //Update
        public Boolean UpdateProfileTypeInfor(int ProfileID, int BagProfileTypeID, int CountryID, int Education)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "update CustomerProfileTypeInfor set BagProfileTypeID=@BagProfileTypeID,CountryID=@CountryID,Education=@Education where ProfileID=@ProfileID";
            SqlParameter pProfileID = (ProfileID == 0) ? new SqlParameter("@ProfileID", DBNull.Value) : new SqlParameter("@ProfileID", ProfileID);
            SqlParameter pBagProfileTypeID = (BagProfileTypeID == 0) ? new SqlParameter("@BagProfileTypeID", DBNull.Value) : new SqlParameter("@BagProfileTypeID", BagProfileTypeID);
            SqlParameter pCountryID = (CountryID == 0) ? new SqlParameter("@CountryID", DBNull.Value) : new SqlParameter("@CountryID", CountryID);
            SqlParameter pEducation = (Education == 0) ? new SqlParameter("@Education", DBNull.Value) : new SqlParameter("@Education", Education);
            this.dt.Updatedata(sql, pProfileID, pBagProfileTypeID, pCountryID, pEducation);
            this.dt.CloseConnection();
            return true;
        }
        //Delete 
        public Boolean DeleteWithProfileID(int ProfileID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from CustomerProfileTypeInfor where ProfileID=@ProfileID";
            SqlParameter pProfileID = (ProfileID == 0) ? new SqlParameter("@ProfileID", DBNull.Value) : new SqlParameter("@ProfileID", ProfileID);
            this.dt.Updatedata(sql, pProfileID);
            this.dt.CloseConnection();
            return true;
        }

    }
}
