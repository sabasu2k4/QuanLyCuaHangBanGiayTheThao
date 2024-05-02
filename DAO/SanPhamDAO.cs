using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Drawing;

namespace DAO
{
    public class SanPhamDAO : ConnectDB
    {

        public DataTable ListSanPhamDAO()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM sanpham", _conn);
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


        // Kiểm tra sản phẩm có tồn tại chưa
        public bool kiemTraMaSP(string masp)
        {
            bool exist = false;
            try
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM SanPham WHERE MaSP=@MaSanPham ", _conn);
                cmd.Parameters.AddWithValue("@MaSanPham", masp);
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


        public void AddSanPhamDAO(SanPhamDTO sanPham)
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO sanpham (MaSP, MaNCC, TenSP, HangSP, XuatXu, GiaBan, TheLoai) VALUES (@MaSP, @MaNCC, @TenSP, @HangSP, @XuatXu, @GiaBan, @TheLoai)", _conn);
                cmd.Parameters.AddWithValue("@MaSP", sanPham.MaSP);
                cmd.Parameters.AddWithValue("@MaNCC", sanPham.MaNCC);
                cmd.Parameters.AddWithValue("@TenSP", sanPham.TenSP);
                cmd.Parameters.AddWithValue("@HangSP", sanPham.HangSP);
                cmd.Parameters.AddWithValue("@XuatXu", sanPham.XuatXu);
                cmd.Parameters.AddWithValue("@GiaBan", sanPham.GiaBan);
                cmd.Parameters.AddWithValue("@TheLoai", sanPham.TheLoai);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                _conn.Close();
            }

        }

        public void UpdateSanPhamDAO(SanPhamDTO sanPham, string masspcu)
        {

            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE sanpham SET MaNCC = @MaNCC, TenSP = @TenSP, HangSP = @HangSP, XuatXu = @XuatXu, GiaBan = @GiaBan, TheLoai = @TheLoai WHERE MaSP = '" + masspcu + "'", _conn);

                cmd.Parameters.AddWithValue("@MaNCC", sanPham.MaNCC);
                cmd.Parameters.AddWithValue("@TenSP", sanPham.TenSP);
                cmd.Parameters.AddWithValue("@HangSP", sanPham.HangSP);
                cmd.Parameters.AddWithValue("@XuatXu", sanPham.XuatXu);
                cmd.Parameters.AddWithValue("@GiaBan", sanPham.GiaBan);
                cmd.Parameters.AddWithValue("@TheLoai", sanPham.TheLoai);

                cmd.ExecuteNonQuery();
            }
            catch (SqlException ex)
            {
                // Bắt lỗi SQL và ghi log hoặc hiển thị thông báo
                MessageBox.Show("Đã xảy ra lỗi SQL: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Bắt các ngoại lệ khác và ghi log hoặc hiển thị thông báo
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

                _conn.Close();
            }
        }

        public void DeleteSanPhamDAO(string sanPham)
        {
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }

                SqlCommand deleteSanPham = new SqlCommand("DELETE FROM SanPham WHERE MaSP=@MaSP", _conn);
                deleteSanPham.Parameters.AddWithValue("@MaSP", sanPham);
                deleteSanPham.ExecuteNonQuery();



            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi khi xóa san pham: " + ex.Message);
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

        public DataTable TimKiemSanPhamDAO(string sanPham)
        {
            try
            {
                _conn.Open();
                SqlCommand searchSanPham = new SqlCommand("SELECT * FROM SanPham WHERE MaSP LIKE @Search OR MaNCC LIKE @Search OR TenSP LIKE @Search OR HangSP LIKE @Search", _conn);
                searchSanPham.Parameters.AddWithValue("@Search", "%" + sanPham + "%");
                SqlDataReader rdr = searchSanPham.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);

                return dt;
            }
            finally
            {
                if (_conn.State == ConnectionState.Open)
                {
                    _conn.Close();
                }
            }

        }

        public DataTable HienThiDanhSachNhaSanXuat()
        {
            try
            {
                string sql;
                sql = "SELECT MaNCC, TenNCC FROM NhaCC";
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

        public float getGiaBan(string maSP)
        {
            float giaBan = 0;
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }

                SqlCommand getGB = new SqlCommand("SELECT GiaBan FROM SanPham WHERE MaSP=@MaSP", _conn);
                getGB.Parameters.AddWithValue("@MaSP", maSP);
                giaBan = (float)getGB.ExecuteScalar();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi khi lấy giá bán: " + ex.Message);
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
    }

}
