using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class CustomerProfileTypeInfor
    {
        private int typeInforID;
        private int profileID;
        private int bagProfileTypeID;
        private int countryID;
        private int education;

        public int TypeInforID
        {
            get
            {
                return typeInforID;
            }

            set
            {
                typeInforID = value;
            }
        }
        public int ProfileID
        {
            get
            {
                return profileID;
            }

            set
            {
                profileID = value;
            }
        }

        public int BagProfileTypeID
        {
            get
            {
                return bagProfileTypeID;
            }

            set
            {
                bagProfileTypeID = value;
            }
        }

        public int CountryID
        {
            get
            {
                return countryID;
            }

            set
            {
                countryID = value;
            }
        }

        public int Education
        {
            get
            {
                return education;
            }

            set
            {
                education = value;
            }
        }
    }
}
