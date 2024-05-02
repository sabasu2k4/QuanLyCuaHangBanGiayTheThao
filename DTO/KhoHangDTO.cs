using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class KhoHangDTO
    {
        private string maSP;
        private string tenSP;
        private string slTon;
        private string ngayNhap;

        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public string SlTon { get; set; }
        public string NgayNhap { get; set; }

        public KhoHangDTO()
        {
        }

        public KhoHangDTO(string maSP, string tenSP, string slTon, string ngayNhap)
        {
            this.maSP = maSP;
            this.tenSP = tenSP;
            this.slTon = slTon;
            this.ngayNhap = ngayNhap;
        }
    }
}
