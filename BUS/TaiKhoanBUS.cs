using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace BUS
{
    public class TaiKhoanBUS
    {
        TaiKhoanDAO taiKhoanDAO = new TaiKhoanDAO();

        public bool KiemTraTKMKBUS(string tentk, string mk)
        {
            TaiKhoanDTO tk = taiKhoanDAO.TimKiemTaiKhoanDAO(tentk);
            if (tk != null)
            {
                if (tk.MatKhau == mk)
                {
                    return true;
                }
                return false;
            }
            return false;
        }

        public TaiKhoanDTO TimKiemTaiKhoanBUS(string tenTK)
        {
            return taiKhoanDAO.TimKiemTaiKhoanDAO(tenTK);
        }
    }
}
