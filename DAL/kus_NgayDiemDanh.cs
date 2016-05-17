using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class kus_NgayDiemDanh
    {
        private int ngayID;
        private int khoaHoc;
        private DateTime ngayDiemDanh;
        public int NgayID
        {
            get
            {
                return ngayID;
            }

            set
            {
                ngayID = value;
            }
        }

        public int KhoaHoc
        {
            get
            {
                return khoaHoc;
            }

            set
            {
                khoaHoc = value;
            }
        }

        public DateTime NgayDiemDanh
        {
            get
            {
                return ngayDiemDanh;
            }

            set
            {
                ngayDiemDanh = value;
            }
        }
    }
}
