using System;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Windows.Forms;

namespace DAO
{
    public class NhanVienDAO : ConnectDB
    {
        public DataTable ListNhanVienDAO() {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NhanVien", _conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                return dt;
            }
            finally
            {
                _conn.Close();
            }
        }
        // Thêm nhân viên 
        public void AddNhanVienDAO(NhanVienDTO nhanVien)
        {
            // Tạo một transaction để đảm bảo tính nhất quán giữa việc thêm nhân viên và thêm tài khoản
            //SqlTransaction transaction = _conn.BeginTransaction();
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                
                // Kiểm tra xem tài khoản đã tồn tại chưa
                SqlCommand checkTaiKhoanCmd = new SqlCommand("SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan = @TaiKhoan", _conn);
                checkTaiKhoanCmd.Parameters.AddWithValue("@TaiKhoan", nhanVien.TaiKhoan);
                int existingCount = (int)checkTaiKhoanCmd.ExecuteScalar();

                if (existingCount == 0)
                {
                    // Tài khoản chưa tồn tại, thêm tài khoản vào bảng tài khoản
                    SqlCommand addTaiKhoan = new SqlCommand("INSERT INTO TaiKhoan (TaiKhoan, MatKhau, PhanQuyen) VALUES (@TaiKhoan, @MatKhau, @PhanQuyen)", _conn);
                    addTaiKhoan.Parameters.AddWithValue("@TaiKhoan", nhanVien.TaiKhoan);
                    addTaiKhoan.Parameters.AddWithValue("@MatKhau", nhanVien.MatKhau);
                    addTaiKhoan.Parameters.AddWithValue("@PhanQuyen", "Nhân Viên");
                    addTaiKhoan.ExecuteNonQuery();
                }
                
                SqlCommand addNhanVien = new SqlCommand("INSERT INTO nhanvien (MaNV, TenNV, SDT, QueQuan, Email, TaiKhoan, MatKhau) VALUES (@MaNV, @TenNV, @SDT, @QueQuan, @Email, @TaiKhoan, @MatKhau)", _conn);
                addNhanVien.Parameters.AddWithValue("@MaNV", nhanVien.MaNV);
                addNhanVien.Parameters.AddWithValue("@TenNV", nhanVien.TenNV);
                addNhanVien.Parameters.AddWithValue("@SDT", nhanVien.SDT);
                addNhanVien.Parameters.AddWithValue("@QueQuan", nhanVien.QueQuan);
                addNhanVien.Parameters.AddWithValue("@Email", nhanVien.Email);
                addNhanVien.Parameters.AddWithValue("@TaiKhoan", nhanVien.TaiKhoan);
                addNhanVien.Parameters.AddWithValue("@MatKhau", nhanVien.MatKhau);
                addNhanVien.ExecuteNonQuery();
                // Commit transaction khi cả hai thao tác đều thành công
                //transaction.Commit();

            }
            catch (Exception ex )
            {
                //transaction.Rollback();
                Console.WriteLine("Lỗi khi thêm nhân viên: " + ex.Message);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }            
        }

        // Kiểm tra tồn tại nhân viên 
        public bool kiemTraMaNVDAO(string maNV)
        {
            bool exist = false;
            try
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM NhanVien WHERE MaNV=@MaNV ", _conn);
                cmd.Parameters.AddWithValue("@MaNV", maNV);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    exist = true;
                }

                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exist;
        }
        // Kiểm tra tồn tại tài khoản
        public bool kiemTraTaiKhoanDAO(string TaiKhoan)
        {
            bool exist = false;
            try
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM TaiKhoan WHERE TaiKhoan=@TaiKhoan ", _conn);
                cmd.Parameters.AddWithValue("@TaiKhoan", TaiKhoan);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    exist = true;
                }

                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return exist;
        }

        // Xóa nhân viên
        public void DeleteNhanVienDAO(string nhanVien)
        {
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                // Lấy tên tài khoản của nhân viên cần xóa
                string TaiKhoan = GetTaiKhoanByMaNV(nhanVien);

                SqlCommand deleteNhanVien = new SqlCommand("DELETE FROM NhanVien WHERE MaNV=@MaNV", _conn);
                deleteNhanVien.Parameters.AddWithValue("@MaNV", nhanVien);
                deleteNhanVien.ExecuteNonQuery();

                // Xóa tài khoản
                SqlCommand deleteTaiKhoan = new SqlCommand("DELETE FROM TaiKhoan WHERE TaiKhoan=@TaiKhoan", _conn);
                deleteTaiKhoan.Parameters.AddWithValue("@TaiKhoan", TaiKhoan);
                deleteTaiKhoan.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                // In ra thông báo lỗi khi có ngoại lệ xảy ra
                Console.WriteLine("Lỗi khi xóa nhân viên: " + ex.Message);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
        }

        // Hàm để lấy tên tài khoản của nhân viên dựa trên mã nhân viên
        private string GetTaiKhoanByMaNV(string maNV)
        {
            string taiKhoan = "";
            SqlCommand cmd = new SqlCommand("SELECT TaiKhoan FROM nhanvien WHERE MaNV = @MaNV", _conn);
            cmd.Parameters.AddWithValue("@MaNV", maNV);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                taiKhoan = reader["TaiKhoan"].ToString();
            }
            reader.Close();
            return taiKhoan;
        }

        public void UpdateNhanVienDAO(NhanVienDTO nhanVien)
        {
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }
                // Thêm tài khoản vào bảng tài khoản trước khi thêm vào bảng nhân viên
                SqlCommand updateTaiKhoan = new SqlCommand("UPDATE TaiKhoan SET MatKhau = @MatKhau WHERE TaiKhoan = @TaiKhoan", _conn);
                updateTaiKhoan.Parameters.AddWithValue("@TaiKhoan", nhanVien.TaiKhoan);
                updateTaiKhoan.Parameters.AddWithValue("@MatKhau", nhanVien.MatKhau);
                updateTaiKhoan.ExecuteNonQuery();


                SqlCommand addNhanVien = new SqlCommand("UPDATE nhanvien SET TenNV = @TenNV, SDT = @SDT, QueQuan = @QueQuan, Email = @Email, MatKhau = @MatKhau WHERE MaNV = @MaNV", _conn);
                addNhanVien.Parameters.AddWithValue("@MaNV", nhanVien.MaNV);
                addNhanVien.Parameters.AddWithValue("@TenNV", nhanVien.TenNV);
                addNhanVien.Parameters.AddWithValue("@SDT", nhanVien.SDT);
                addNhanVien.Parameters.AddWithValue("@QueQuan", nhanVien.QueQuan);
                addNhanVien.Parameters.AddWithValue("@Email", nhanVien.Email);
                addNhanVien.Parameters.AddWithValue("@MatKhau", nhanVien.MatKhau);
                addNhanVien.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật nhân viên: " + ex.Message);
                throw;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }
        }

        public DataTable TimKiemNhanVienDAO(string nhanVien)
        {
            try
            {
                _conn.Open();
                SqlCommand searchNhanVien = new SqlCommand("SELECT * FROM NhanVien WHERE TenNV LIKE @Search OR MaNV LIKE @Search", _conn);
                searchNhanVien.Parameters.AddWithValue("@Search", "%" + nhanVien + "%");
                SqlDataReader rdr = searchNhanVien.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                // Kiểm tra số dòng trả về
                //if (dt.Rows.Count == 0)
                //{
                //    throw new Exception("Không tìm thấy nhân viên.");
                //}
                return dt;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}