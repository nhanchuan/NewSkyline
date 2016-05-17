using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class kus_DiemDanh
    {
        private int diemDanhID;
        private int ngayDiemDanh;
        private int hocVien;
        private int diemDanh;
        private int coPhep;
        private string ghiChu;
        private DateTime dateOfCreate;

        public int DiemDanhID
        {
            get
            {
                return diemDanhID;
            }

            set
            {
                diemDanhID = value;
            }
        }

        public int NgayDiemDanh
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

        public int HocVien
        {
            get
            {
                return hocVien;
            }

            set
            {
                hocVien = value;
            }
        }

        public int DiemDanh
        {
            get
            {
                return diemDanh;
            }

            set
            {
                diemDanh = value;
            }
        }

        public int CoPhep
        {
            get
            {
                return coPhep;
            }

            set
            {
                coPhep = value;
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

        public DateTime DateOfCreate
        {
            get
            {
                return dateOfCreate;
            }

            set
            {
                dateOfCreate = value;
            }
        }
    }
}
