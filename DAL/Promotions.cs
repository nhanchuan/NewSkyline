using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Promotions
    {
        private int promotionID;
        private string promCode;
        private string promName;
        private int reducedRate;
        private int discount;
        private DateTime startDate;
        private DateTime endDate;
        private bool isActive;
        private DateTime createDate;
        public int PromotionID
        {
            get
            {
                return promotionID;
            }

            set
            {
                promotionID = value;
            }
        }

        public string PromCode
        {
            get
            {
                return promCode;
            }

            set
            {
                promCode = value;
            }
        }

        public string PromName
        {
            get
            {
                return promName;
            }

            set
            {
                promName = value;
            }
        }

        public int ReducedRate
        {
            get
            {
                return reducedRate;
            }

            set
            {
                reducedRate = value;
            }
        }

        public int Discount
        {
            get
            {
                return discount;
            }

            set
            {
                discount = value;
            }
        }

        public DateTime StartDate
        {
            get
            {
                return startDate;
            }

            set
            {
                startDate = value;
            }
        }

        public DateTime EndDate
        {
            get
            {
                return endDate;
            }

            set
            {
                endDate = value;
            }
        }

        public bool IsActive
        {
            get
            {
                return isActive;
            }

            set
            {
                isActive = value;
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
