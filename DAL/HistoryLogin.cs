using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HistoryLogin
    {
        private long historyID;
        private int userID;
        private DateTime dateOfLogin;
        public long HistoryID
        {
            get
            {
                return historyID;
            }

            set
            {
                historyID = value;
            }
        }

        public int UserID
        {
            get
            {
                return userID;
            }

            set
            {
                userID = value;
            }
        }

        public DateTime DateOfLogin
        {
            get
            {
                return dateOfLogin;
            }

            set
            {
                dateOfLogin = value;
            }
        }
    }
}
