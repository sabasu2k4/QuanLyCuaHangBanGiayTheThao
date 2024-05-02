using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class ChiTietDonHangDTO
    {
        public int MaCTDH { get; set; }
        public string MaDH { get; set; }
        public string MaSP { get; set; }
        public Nullable<int> SoLuong { get; set; }
        public Nullable<double> DonGia { get; set; }

        public virtual DonHangDTO DonHang { get; set; }
        public virtual SanPhamDTO SanPham { get; set; }
    }
}
