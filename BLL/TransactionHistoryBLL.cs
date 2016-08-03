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
    public class TransactionHistoryBLL
    {
        DataServices dt = new DataServices();
        //New TransactionHistory
        public Boolean NewTransactionHistory(int InfoID, string TransactionContent, int PaymentAmount, int EmplPay)
        {
            if (!this.dt.OpenConnection())
            {
                return false;
            }
            string sql = "insert into TransactionHistory(InfoID,TransactionContent,PaymentAmount,EmplPay) values(@InfoID,@TransactionContent,@PaymentAmount,@EmplPay)";
            SqlParameter pInfoID = new SqlParameter("@InfoID", InfoID);
            SqlParameter pTransactionContent = (TransactionContent == "") ? new SqlParameter("@TransactionContent", DBNull.Value) : new SqlParameter("@TransactionContent", TransactionContent);
            SqlParameter pPaymentAmount = (PaymentAmount == 0) ? new SqlParameter("@PaymentAmount", DBNull.Value) : new SqlParameter("@PaymentAmount", PaymentAmount);
            SqlParameter pEmplPay = (EmplPay == 0) ? new SqlParameter("@EmplPay", DBNull.Value) : new SqlParameter("@EmplPay", EmplPay);
            this.dt.Updatedata(sql, pInfoID, pTransactionContent, pPaymentAmount, pEmplPay);
            return true;
        }
    }
}
