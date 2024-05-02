using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class GioHangDTO
    {
        public string MaSP { get; set; }
        public string TenSP { get; set; }
        public int SoLuong { get; set; }
        public float GiaBan { get; set; }
        public float ThanhTien { get; set; }
        public GioHangDTO() { }
        public GioHangDTO(string maSP, string tenSP, int soLuong, float giaBan, float thanhTien)
        {
            MaSP = maSP;
            TenSP = tenSP;
            SoLuong = soLuong;
            GiaBan = giaBan;
            ThanhTien = thanhTien;
        }
    }
}
