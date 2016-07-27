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
    public class InteractiveHistoryBLL
    {
        DataServices dt = new DataServices();
        public DataTable DataTableInteractiveHistory()
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select ith.ID,ith.UserID,(pro.LastName+' '+pro.FirstName+' - Mã: '+emp.EmployeesCode) as UserInt, ith.InteractiveContent, ith.Createdate, ith.InteractiveLink";
            sql += " ";
            sql += "from InteractiveHistory ith full outer join UserProfile pro on ith.UserID=pro.UserID full outer join Employees emp on pro.ProfileID=emp.ProfileID where ith.ID is not null order by ith.Createdate desc";
            DataTable tb = dt.DAtable(sql);
            this.dt.CloseConnection();
            return tb;
        }
        public Boolean NewInteractiveHistory(int UserID, string InteractiveContent, string InteractiveLink)
        {
            if(!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "Exec NewInteractiveHistory @UserID,@InteractiveContent,@InteractiveLink";
            SqlParameter pUserID = new SqlParameter("@UserID", UserID);
            SqlParameter pInteractiveContent = (InteractiveContent == "") ? new SqlParameter("@InteractiveContent", DBNull.Value) : new SqlParameter("@InteractiveContent", InteractiveContent);
            SqlParameter pInteractiveLink = (InteractiveLink == "") ? new SqlParameter("@InteractiveLink", DBNull.Value) : new SqlParameter("@InteractiveLink", InteractiveLink);
            this.dt.Updatedata(sql, pUserID, pInteractiveContent, pInteractiveLink);
            this.dt.CloseConnection();
            return true;
        }
        //Delete 
        public Boolean DeleteInteractiveHistoryByCreatedate(DateTime Createdate)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from InteractiveHistory where Createdate >= @Createdate";
            SqlParameter pCreatedate = new SqlParameter("@Createdate", Createdate);
            this.dt.Updatedata(sql, pCreatedate);
            this.dt.CloseConnection();
            return true;
        }
        public Boolean DeleteAll()
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from InteractiveHistory";
            this.dt.Updatedata(sql);
            this.dt.CloseConnection();
            return true;
        }
    }
}
