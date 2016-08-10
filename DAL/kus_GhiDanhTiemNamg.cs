using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class kus_GhiDanhTiemNamg
    {
        private int iD;
        private int hocVienID;
        private int lopHoc;
        private int nVGhiDanh;
        private DateTime ngayGD;
        private string ghiChu;
        public bool GDStatus;
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

        public int HocVienID
        {
            get
            {
                return hocVienID;
            }

            set
            {
                hocVienID = value;
            }
        }

        public int LopHoc
        {
            get
            {
                return lopHoc;
            }

            set
            {
                lopHoc = value;
            }
        }

        public int NVGhiDanh
        {
            get
            {
                return nVGhiDanh;
            }

            set
            {
                nVGhiDanh = value;
            }
        }

        public DateTime NgayGD
        {
            get
            {
                return ngayGD;
            }

            set
            {
                ngayGD = value;
            }
        }

        public string GhiChu
        {
            get
            {
                return ghiChu;
            }

            set
            {
                ghiChu = value;
            }
        }
    }
}
