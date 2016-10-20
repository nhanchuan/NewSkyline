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
    public class PromotionsBLL
    {
        DataServices dt = new DataServices();

        public List<Promotions> ListByPromotionID(int PromotionID)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from Promotions where PromotionID=@PromotionID";
            SqlParameter pPromotionID = new SqlParameter("@PromotionID", PromotionID);
            DataTable tb = dt.DAtable(sql, pPromotionID);
            List<Promotions> lst = new List<Promotions>();
            foreach (DataRow r in tb.Rows)
            {
                Promotions pr = new Promotions();
                pr.PromotionID = (int)r["PromotionID"];
                pr.PromCode = (string.IsNullOrEmpty(r["PromCode"].ToString())) ? "" : (string)r["PromCode"];
                pr.PromName = (string.IsNullOrEmpty(r["PromName"].ToString())) ? "" : (string)r["PromName"];
                pr.ReducedRate = (string.IsNullOrEmpty(r["ReducedRate"].ToString())) ? 0 : (int)r["ReducedRate"];
                pr.Discount = (string.IsNullOrEmpty(r["Discount"].ToString())) ? 0 : (int)r["Discount"];
                pr.StartDate = (string.IsNullOrEmpty(r["StartDate"].ToString())) ? Convert.ToDateTime("01/01/1900") : (DateTime)r["StartDate"];
                pr.EndDate = (string.IsNullOrEmpty(r["EndDate"].ToString())) ? Convert.ToDateTime("01/01/1900") : (DateTime)r["EndDate"];
                pr.IsActive = (Boolean)r["IsActive"];
                lst.Add(pr);
            }
            this.dt.CloseConnection();
            return lst;
        }
        public List<Promotions> ListByInComing(Boolean IsActive)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "select * from Promotions where (getdate() between StartDate and EndDate) and IsActive=@IsActive";
            SqlParameter pIsActive = new SqlParameter("@IsActive", IsActive);
            DataTable tb = dt.DAtable(sql, pIsActive);
            List<Promotions> lst = new List<Promotions>();
            foreach (DataRow r in tb.Rows)
            {
                Promotions pr = new Promotions();
                pr.PromotionID = (int)r["PromotionID"];
                pr.PromCode = (string.IsNullOrEmpty(r["PromCode"].ToString())) ? "" : (string)r["PromCode"];
                pr.PromName = (string.IsNullOrEmpty(r["PromName"].ToString())) ? "" : (string)r["PromName"];
                pr.ReducedRate = (string.IsNullOrEmpty(r["ReducedRate"].ToString())) ? 0 : (int)r["ReducedRate"];
                pr.Discount = (string.IsNullOrEmpty(r["Discount"].ToString())) ? 0 : (int)r["Discount"];
                pr.StartDate = (string.IsNullOrEmpty(r["StartDate"].ToString())) ? Convert.ToDateTime("01/01/1900") : (DateTime)r["StartDate"];
                pr.EndDate = (string.IsNullOrEmpty(r["EndDate"].ToString())) ? Convert.ToDateTime("01/01/1900") : (DateTime)r["EndDate"];
                pr.IsActive = (Boolean)r["IsActive"];
                lst.Add(pr);
            }
            this.dt.CloseConnection();
            return lst;
        }
        //NEW
        public Boolean NewPromotions(string PromCode, string PromName, int ReducedRate, int Discount, DateTime StartDate, DateTime EndDate, Boolean IsActive)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into Promotions(PromCode,PromName,ReducedRate,Discount,StartDate,EndDate,IsActive) values(@PromCode,@PromName,@ReducedRate,@Discount,@StartDate,@EndDate,@IsActive)";
            SqlParameter pPromCode = (PromCode.Length == 0) ? new SqlParameter("@PromCode", DBNull.Value) : new SqlParameter("@PromCode", PromCode);
            SqlParameter pPromName = (PromName.Length == 0) ? new SqlParameter("@PromName", DBNull.Value) : new SqlParameter("@PromName", PromName);
            SqlParameter pReducedRate = (ReducedRate == 0) ? new SqlParameter("@ReducedRate", DBNull.Value) : new SqlParameter("@ReducedRate", ReducedRate);
            SqlParameter pDiscount = (Discount == 0) ? new SqlParameter("@Discount", DBNull.Value) : new SqlParameter("@Discount", Discount);
            SqlParameter pStartDate = (StartDate.Year <= 1900) ? new SqlParameter("@StartDate", DBNull.Value) : new SqlParameter("@StartDate", StartDate);
            SqlParameter pEndDate = (EndDate.Year <= 1900) ? new SqlParameter("@EndDate", DBNull.Value) : new SqlParameter("@EndDate", EndDate);
            SqlParameter pIsActive = new SqlParameter("@IsActive", IsActive);
            this.dt.Updatedata(sql, pPromCode, pPromName, pReducedRate, pDiscount, pStartDate, pEndDate, pIsActive);
            this.dt.CloseConnection();
            return true;
        }
        //UPDATE
        public Boolean UpdatePromotions(int PromotionID, string PromCode, string PromName, int ReducedRate, int Discount, DateTime StartDate, DateTime EndDate, Boolean IsActive)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "update Promotions set PromCode=@PromCode,PromName=@PromName,ReducedRate=@ReducedRate,Discount=@Discount,StartDate=@StartDate,EndDate=@EndDate,IsActive=@IsActive where PromotionID=@PromotionID";
            SqlParameter pPromotionID = new SqlParameter("@PromotionID", PromotionID);
            SqlParameter pPromCode = (PromCode.Length == 0) ? new SqlParameter("@PromCode", DBNull.Value) : new SqlParameter("@PromCode", PromCode);
            SqlParameter pPromName = (PromName.Length == 0) ? new SqlParameter("@PromName", DBNull.Value) : new SqlParameter("@PromName", PromName);
            SqlParameter pReducedRate = (ReducedRate == 0) ? new SqlParameter("@ReducedRate", DBNull.Value) : new SqlParameter("@ReducedRate", ReducedRate);
            SqlParameter pDiscount = (Discount == 0) ? new SqlParameter("@Discount", DBNull.Value) : new SqlParameter("@Discount", Discount);
            SqlParameter pStartDate = (StartDate.Year <= 1900) ? new SqlParameter("@StartDate", DBNull.Value) : new SqlParameter("@StartDate", StartDate);
            SqlParameter pEndDate = (EndDate.Year <= 1900) ? new SqlParameter("@EndDate", DBNull.Value) : new SqlParameter("@EndDate", EndDate);
            SqlParameter pIsActive = new SqlParameter("@IsActive", IsActive);
            this.dt.Updatedata(sql, pPromotionID, pPromCode, pPromName, pReducedRate, pDiscount, pStartDate, pEndDate, pIsActive);
            this.dt.CloseConnection();
            return true;
        }
        //DELETE
        public Boolean DeleteByPromotionID(int PromotionID)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "delete from Promotions where PromotionID=@PromotionID";
            SqlParameter pPromotionID = new SqlParameter("@PromotionID", PromotionID);
            this.dt.Updatedata(sql, pPromotionID);
            this.dt.CloseConnection();
            return true;
        }
        //===================================================
        public int CountPromotions() //COUNT ROW IN TABLE Promotions
        {
            int RC = 0;
            if (!this.dt.OpenConnection())
            {
                return 0;
            }
            string sql = "select COUNT(*) from Promotions";
            RC = dt.GetValues(sql);
            this.dt.CloseConnection();
            return RC;
        }
        public DataTable GetPromotionsPageWise(int PageIndex, int PageSize)
        {
            if (!this.dt.OpenConnection())
            {
                return null;
            }
            string sql = "Exec GetPromotionsPageWise @PageIndex,@PageSize";
            SqlParameter paramPageIndex = new SqlParameter("PageIndex", PageIndex);
            SqlParameter paramPageSize = new SqlParameter("PageSize", PageSize);
            DataTable tb = dt.DAtable(sql, paramPageIndex, paramPageSize);
            this.dt.CloseConnection();
            return tb;
        }
        //===================================================   



    }
}
