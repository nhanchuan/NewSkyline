using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Popups
    {
        private int iD;
        private string permalink;
        private string shortDescription;
        private string popupUrl;
        private string viewOnPage;
        public bool PopupStatus;
        private int userUpload;
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

        public string Permalink
        {
            get
            {
                return permalink;
            }

            set
            {
                permalink = value;
            }
        }

        public string ShortDescription
        {
            get
            {
                return shortDescription;
            }

            set
            {
                shortDescription = value;
            }
        }

        public string PopupUrl
        {
            get
            {
                return popupUrl;
            }

            set
            {
                popupUrl = value;
            }
        }

        public string ViewOnPage
        {
            get
            {
                return viewOnPage;
            }

            set
            {
                viewOnPage = value;
            }
        }

        public int UserUpload
        {
            get
            {
                return userUpload;
            }

            set
            {
                userUpload = value;
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
