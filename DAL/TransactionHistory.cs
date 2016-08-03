using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class TransactionHistory
    {
        private int iD;
        private int infoID;
        private string transactionContent;
        private int paymentAmount;
        private int emplPay;
        private DateTime createDate;
        public int ID
        {
            get
            {
                return iD;
            }

            set
            {
                iD = value;
            }
        }

        public int InfoID
        {
            get
            {
                return infoID;
            }

            set
            {
                infoID = value;
            }
        }

        public string TransactionContent
        {
            get
            {
                return transactionContent;
            }

            set
            {
                transactionContent = value;
            }
        }

        public int PaymentAmount
        {
            get
            {
                return paymentAmount;
            }

            set
            {
                paymentAmount = value;
            }
        }

        public int EmplPay
        {
            get
            {
                return emplPay;
            }

            set
            {
                emplPay = value;
            }
        }

        public DateTime CreateDate
        {
            get
            {
                return createDate;
            }

            set
            {
                createDate = value;
            }
        }
    }
}
