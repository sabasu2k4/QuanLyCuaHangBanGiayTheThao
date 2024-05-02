using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAO;
using DTO;

namespace BUS
{
    public class KhoHangBUS
    {
        KhoHangDAO khoHangDAO = new DAO.KhoHangDAO();

        public DataTable ListOfKhoHang()
        {
            return khoHangDAO.ListKhoHangDAO();
        }

        public void DeleteSPOfKhoHangBUS(string maSP)
        {
            khoHangDAO.DeleteSPOfKhoHangDAO(maSP);
        }

        public void NhapHangOfKhoHangBUS(string maSP, string tenSP, int soLuong)
        {
            if (khoHangDAO.kiemSanPhamTonTaiDAO(maSP))
            {
                khoHangDAO.UpdateSoLuongKhoHangDAO(maSP, soLuong);
            }
            else
            {
                khoHangDAO.AddSPKhoHangDAO(maSP , tenSP, soLuong);
            }
        }
    }
}
