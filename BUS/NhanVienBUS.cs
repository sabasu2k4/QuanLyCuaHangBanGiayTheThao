using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DAO;
using DTO;

namespace BUS
{
    public class NhanVienBUS
    {
        NhanVienDAO daoNhanVien = new DAO.NhanVienDAO();
        //List<NhanVienDTO> lists;

        public DataTable listOfNhanVienBUS()
        {
            return daoNhanVien.ListNhanVienDAO();
        }

        public void addNhanVienBUS(NhanVienDTO nhanVien)
        {
             daoNhanVien.AddNhanVienDAO(nhanVien);
        }
        // Kiểm tra nhân viên đã tồn tại chưa bằng mã nhân viên
        public bool kiemTraMaNVBUS(string maNV)
        {
            return daoNhanVien.kiemTraMaNVDAO(maNV);
        }
        // Kiểm tra tài khoản tồn tại
        public bool kiemTraTaiKhoanDAO(string TaiKhoan)
        {           
            return daoNhanVien.kiemTraTaiKhoanDAO(TaiKhoan);
        }
        // Kiểm tra định dạng số điện thoại
        public bool kiemTraSDTHopLe(string sdt)
        {
            return !string.IsNullOrEmpty(sdt) && sdt.Length >= 10 && sdt.Length <= 11 && sdt.All(char.IsDigit);
        }
        // Kiểm tra định dạng email
        public bool kiemTraEmailHopLe(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public void DeleteNhanVienDAO(string nhanVien)
        {
            daoNhanVien.DeleteNhanVienDAO(nhanVien);
        }

        public void UpdateNhanVienDAO(NhanVienDTO nhanVien)
        {
            daoNhanVien.UpdateNhanVienDAO(nhanVien);
        }

        public DataTable TimKiemNhanVienBUS(string nhanVien)
        {
            return daoNhanVien.TimKiemNhanVienDAO(nhanVien);
        }
    }
}
