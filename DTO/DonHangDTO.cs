using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class DonHangDTO
    {
        private string maDH;
        private string tenKH;
        private string sDT;
        private string diaChi;
        private string maNV;
        public DateTime ThoiGianTao { get; set; }
        public string MaDH { get; set; }
        public string TenKH { get; set; }
        public string SDTKH { get; set; }
        public string DiaChi { get; set; }
        public string MaNV { get; set; }

        public List<GioHangDTO> GioHangDTO { get; set; }

        public DonHangDTO() { }

        public DonHangDTO(string maDH, string tenKH, string sDT, string diaChi, string maNV, DateTime thoiGianTao, List<GioHangDTO> gioHangDTO)
        {
            this.maDH = maDH;
            this.tenKH = tenKH;
            this.sDT = sDT;
            this.diaChi = diaChi;
            this.maNV = maNV;
            ThoiGianTao = thoiGianTao;
            GioHangDTO = gioHangDTO;
        }
    }
}
