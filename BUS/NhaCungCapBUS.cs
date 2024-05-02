using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using DAO;
using System.Data;

namespace BUS
{
    public class NhaCungCapBUS
    {
        NhaCungCapDAO nhacungcapDAO = new NhaCungCapDAO();
        List<NhaCungCapDTO> lists;

        public DataTable ListOfNhaCungCap()
        {
            return nhacungcapDAO.ListNhaCungCap();
        }
        public void AddNCC(NhaCungCapDTO nhaCC)
        {
            nhacungcapDAO.AddNCCDAO(nhaCC);

        }
        public bool addnhacungcangbus(string mancc)
        {
            return nhacungcapDAO.kiemtraMaNcc(mancc);
        }
        public void UpdateNhaCungCap(NhaCungCapDTO nhacc)
        {
            nhacungcapDAO.UpdateNhaCungCapDAO(nhacc);
        }
        public void DeleteNhaCC(string nhaCC)
        {
            nhacungcapDAO.DeleteSanPhamDAO(nhaCC);
        }
        public DataTable TimKiemNhaCCBUS(string nhaCC)
        {
            return nhacungcapDAO.TimKiemNhaCCDAO(nhaCC);
        }

    }
}
