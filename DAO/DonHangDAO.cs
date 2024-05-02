using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;

namespace DAO
{
    public class DonHangDAO:ConnectDB
    {
        public DataTable HienThiDanhSachnv()
        {
            try
            {
                string sql;
                sql = "SELECT MaNV FROM NhanVien";
                _conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, _conn);
                DataTable ds = new DataTable();
                da.Fill(ds);
                return ds;
            }

            finally
            {
                _conn.Close();
            }
        }
        public DataTable LoadComboBoxSanPhamDAO()
        {
            try
            {
                string Ssql;
                Ssql = "SELECT MaSP,TenSP,SoLuong FROM KhoHang";
                _conn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(Ssql, _conn);
                DataTable dss = new DataTable();
                dap.Fill(dss);
                return dss;
            }

            finally
            {
                _conn.Close();
            }
        }
        public DataTable HienThiDanhSachGioHangDAO()
        {
            try
            {
                string Ssql;
                Ssql = "select Masp, TenSP, SoLuong, GiaBan, ThanhTien from giohang";
                _conn.Open();
                SqlDataAdapter dap = new SqlDataAdapter(Ssql, _conn);
                DataTable dss = new DataTable();
                dap.Fill(dss);
                return dss;
            }

            finally
            {
                _conn.Close();
            }
        }
        public void AddSanPhamVaoGioHang(GioHangDTO gioHang)
        {
            try
            {
                _conn.Open();

                // Kiểm tra xem sản phẩm đã tồn tại trong giỏ hàng chưa
                SqlCommand checkCmd = new SqlCommand("SELECT COUNT(*) FROM GioHang WHERE MaSP = @MaSP", _conn);
                checkCmd.Parameters.AddWithValue("@MaSP", gioHang.MaSP);
                int existingCount = (int)checkCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    // Nếu sản phẩm chưa tồn tại trong giỏ hàng, thêm mới
                    SqlCommand insertCmd = new SqlCommand("INSERT INTO GioHang (MaSP, TenSP, SoLuong, GiaBan, ThanhTien) VALUES (@MaSP, @TenSP, @SoLuong, @GiaBan, @ThanhTien)", _conn);
                    insertCmd.Parameters.AddWithValue("@MaSP", gioHang.MaSP);
                    insertCmd.Parameters.AddWithValue("@TenSP", gioHang.TenSP);
                    insertCmd.Parameters.AddWithValue("@SoLuong", gioHang.SoLuong);
                    insertCmd.Parameters.AddWithValue("@GiaBan", gioHang.GiaBan);
                    insertCmd.Parameters.AddWithValue("@ThanhTien", gioHang.ThanhTien);
                    insertCmd.ExecuteNonQuery();
                }
                else
                {
                    // Nếu sản phẩm đã tồn tại trong giỏ hàng, cập nhật số lượng
                    SqlCommand updateCmd = new SqlCommand("UPDATE GioHang SET SoLuong = SoLuong + @SoLuong, ThanhTien = ThanhTien + @ThanhTien WHERE MaSP = @MaSP", _conn);
                    updateCmd.Parameters.AddWithValue("@MaSP", gioHang.MaSP);
                    updateCmd.Parameters.AddWithValue("@SoLuong", gioHang.SoLuong);
                    updateCmd.Parameters.AddWithValue("@ThanhTien", gioHang.ThanhTien);
                    updateCmd.ExecuteNonQuery();
                }
            }
            finally
            {
                _conn.Close();
            }
        }
        public void DeleteSanPhamDAO(string maSP)
        {
            try
            {
                _conn.Open();

                string query = "DELETE FROM GioHang WHERE MaSP = @MaSP";
                SqlCommand command = new SqlCommand(query, _conn);
                command.Parameters.AddWithValue("@MaSP", maSP);

                command.ExecuteNonQuery();
            }
            finally
            {
                _conn.Close();
            }
        }
        public void HuyDonHangDAO()
        {
            try
            {
                _conn.Open();

                string query = "DELETE FROM GioHang";
                SqlCommand command = new SqlCommand(query, _conn);
                command.ExecuteNonQuery();
            }
            finally
            {
                _conn.Close();
            }
        }
        public float getGiaBan(string maSP)
        {
            float giaBan = 0;
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT GiaBan FROM SanPham WHERE MaSP = @MaSP", _conn);
                cmd.Parameters.AddWithValue("@MaSP", maSP);

                object result = cmd.ExecuteScalar();
                if (result != DBNull.Value && result != null)
                {
                    giaBan = Convert.ToSingle(result);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi lấy giá bán sản phẩm: " + ex.Message);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
            return giaBan;
        }
        public float tinhTongTien()
        {
            float tongTien = 0;
            try
            {
                _conn.Open();

                string query = "SELECT ThanhTien FROM GioHang";
                SqlCommand command = new SqlCommand(query, _conn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    float thanhTien = Convert.ToSingle(reader["ThanhTien"]);
                    tongTien += thanhTien;
                }

                reader.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error calculating total money in GioHang: " + ex.Message);
            }
            finally
            {
                _conn.Close();
            }
            return tongTien;
        }
        public List<GioHangDTO> GetInvoiceItemsFromCart()
        {
            List<GioHangDTO> invoiceItems = new List<GioHangDTO>();

            try
            {
                _conn.Open();

                string query = "SELECT MaSP, TenSP, SoLuong, GiaBan, ThanhTien FROM GioHang";
                SqlCommand command = new SqlCommand(query, _conn);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    GioHangDTO item = new GioHangDTO
                    {
                        MaSP = reader["MaSP"].ToString(),
                        TenSP = reader["TenSP"].ToString(),
                        SoLuong = Convert.ToInt32(reader["SoLuong"]),
                        GiaBan = Convert.ToSingle(reader["GiaBan"]),
                        ThanhTien = Convert.ToSingle(reader["ThanhTien"])
                    };
                    invoiceItems.Add(item);
                }
                reader.Close();
            }
            finally
            {
                _conn.Close();
            }
            
            return invoiceItems;
        }
    }
}



