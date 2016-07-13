using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class InteractiveHistory
    {
        private int iD;
        private int userID;
        private string interactiveContent;
        private DateTime createdate;
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

        public string InteractiveContent
        {
            get
            {
                return interactiveContent;
            }

            set
            {
                interactiveContent = value;
            }
        }

        public DateTime Createdate
        {
            get
            {
                return createdate;
            }

            set
            {
                createdate = value;
            }
        }
    }
}
