using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class DonHangBUS
    {
        DonHangDAO daodh = new DAO.DonHangDAO();

        public DataTable LoadComboBoxSanPhamBUS()
        {
            return daodh.LoadComboBoxSanPhamDAO();
        }
        public DataTable HienThiDanhSachGioHangBUS()
        {
            return daodh.HienThiDanhSachGioHangDAO();
        }
        public void AddSanPhamVaoGioHangBUS(GioHangDTO giohang)
        {
            daodh.AddSanPhamVaoGioHang(giohang);
        }
        public void DeleteSanPhamBUS(string maSP)
        {
            daodh.DeleteSanPhamDAO(maSP);
        }
        public void HuyDonHangBUS()
        {
            daodh.HuyDonHangDAO();
        }
        public float getGiaBanBUS(string maSP)
        {
            return daodh.getGiaBan(maSP);
        }
        public float tinhTongTien()
        {
            return daodh.tinhTongTien();
        }
        public List<GioHangDTO> GetInvoiceItemsFromCart()
        {
            return daodh.GetInvoiceItemsFromCart();
        }
    }
}
    
