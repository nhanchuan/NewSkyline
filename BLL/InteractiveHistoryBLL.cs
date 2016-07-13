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
        public Boolean NewInteractiveHistory(int UserID, string InteractiveContent)
        {
            if(!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "Exec NewInteractiveHistory @UserID,@InteractiveContent";
            SqlParameter pUserID = new SqlParameter("@UserID", UserID);
            SqlParameter pInteractiveContent = (InteractiveContent == "") ? new SqlParameter("@InteractiveContent", DBNull.Value) : new SqlParameter("@InteractiveContent", InteractiveContent);
            this.dt.Updatedata(sql, pUserID, pInteractiveContent);
            this.dt.CloseConnection();
            return true;
        }
    }
}
