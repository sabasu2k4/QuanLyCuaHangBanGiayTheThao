using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class TaiKhoanDTO
    {
        private string tenDangNhap;
        private string matKhau;
        private string phanQuyen;
        public TaiKhoanDTO()
        {
        }

        public TaiKhoanDTO(string tenDangNhap, string matKhau, string phanQuyen)
        {
            this.tenDangNhap = tenDangNhap;
            this.matKhau = matKhau;
            this.phanQuyen = phanQuyen;
        }

        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }
        public string PhanQuyen { get; set; }

    }
}
