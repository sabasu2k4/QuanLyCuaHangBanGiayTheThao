using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DTO
{

    public class SanPhamDTO
    {
        public string MaSP { get; set; }
        public string MaNCC { get; set; }
        public string TenSP { get; set; }
        public string HangSP { get; set; }
        public string XuatXu { get; set; }
        public string GiaBan { get; set; }
        public string TheLoai { get; set; }

        public SanPhamDTO()
        {

        }

        public SanPhamDTO(string maSP, string maNCC, string tenSP, string hangSP, string xuatXu, string giaBan, string theLoai)
        {
            this.MaSP = maSP;
            this.MaNCC = maNCC;
            this.TenSP = tenSP;
            this.HangSP = hangSP;
            this.XuatXu = xuatXu;
            this.GiaBan = giaBan;
            this.TheLoai = theLoai;
        }
    }
}
