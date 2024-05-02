
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using DAO;
using DTO;
using System.Windows.Forms;
//using System.Data.Common;

namespace BUS
{
    public class SanPhamBUS
    {
        SanPhamDAO daoSanPham = new DAO.SanPhamDAO();
        List<SanPhamDTO> lists;
        public DataTable listofsanpham()
        {
            return daoSanPham.ListSanPhamDAO();
        }

        public void AddSanPham(SanPhamDTO sanPham)
        {
            daoSanPham.AddSanPhamDAO(sanPham);

        }
        public void UpdateSanPham(SanPhamDTO sanPham, string maspcu)
        {
            daoSanPham.UpdateSanPhamDAO(sanPham, maspcu);
        }
        public void DeleteSanPham(string sanPham)
        {
            daoSanPham.DeleteSanPhamDAO(sanPham);

        }

        public DataTable TimKiemNhaCCBUS(string sanPham)
        {
            return daoSanPham.TimKiemSanPhamDAO(sanPham);
        }

        public DataTable loadNhaCC()
        {
            return daoSanPham.HienThiDanhSachNhaSanXuat();
        }

    }
}
