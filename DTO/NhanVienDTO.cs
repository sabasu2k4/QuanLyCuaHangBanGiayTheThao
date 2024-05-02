namespace DTO
{
    public class NhanVienDTO
    {
        private string maNV;
        private string tenNV;
        private string sDT;
        private string queQuan;
        private string email;
        private string taiKhoan;
        private string matKhau;



        public string MaNV { get; set; }
        public string TenNV { get; set; }
        public string SDT { get; set; }
        public string QueQuan { get; set; }
        public string Email { get; set; }
        public string TaiKhoan { get; set; }
        public string MatKhau { get; set; }

        public NhanVienDTO()
        {

        }
        public NhanVienDTO(string maNV, string tenNV, string sDT, string queQuan, string email, string taiKhoan, string matKhau)
        {
            this.maNV = maNV;
            this.tenNV = tenNV;
            this.sDT = sDT;
            this.queQuan = queQuan;
            this.email = email;
            this.taiKhoan = taiKhoan;
            this.matKhau = matKhau;
        }
    }
    

}
