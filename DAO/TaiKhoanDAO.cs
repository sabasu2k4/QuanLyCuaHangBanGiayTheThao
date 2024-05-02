using DTO;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DAO
{
    public class TaiKhoanDAO:ConnectDB
    {
        public TaiKhoanDTO TimKiemTaiKhoanDAO(string tenTK)
        {
            try
            {
                _conn.Open();
                SqlCommand timKiem = new SqlCommand("SELECT * FROM TaiKhoan WHERE TaiKhoan = @TaiKhoan", _conn);
                timKiem.Parameters.AddWithValue("@TaiKhoan", tenTK);
                SqlDataReader reader = timKiem.ExecuteReader();

                if (reader.Read()) // Kiểm tra xem có kết quả từ truy vấn không
                {
                    // Trích xuất giá trị từ kết quả truy vấn và gán cho đối tượng TaiKhoanDTO
                    TaiKhoanDTO taiKhoan = new TaiKhoanDTO();
                    taiKhoan.TaiKhoan = reader["TaiKhoan"].ToString();
                    taiKhoan.MatKhau = reader["MatKhau"].ToString();
                    taiKhoan.PhanQuyen = reader["PhanQuyen"].ToString();

                    return taiKhoan;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tìm kiếm tài khoản: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return null;
        }
    }
}
