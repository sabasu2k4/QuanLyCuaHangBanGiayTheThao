using System;
using System.Data.SqlClient;
using System.Data;
using DTO;
using System.Windows.Forms;

namespace DAO
{
    public class KhoHangDAO : ConnectDB
    {
        public DataTable ListKhoHangDAO()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM KhoHang", _conn);
                SqlDataReader rdr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(rdr);
                return dt;
            }finally
            {
                _conn.Close();
            }
        }

        public void DeleteSPOfKhoHangDAO(string maSP)
        {
            try
            {
                _conn.Open();
                SqlCommand deleteSP = new SqlCommand("DELETE FROM KhoHang WHERE MaSP=@MaSP", _conn);
                deleteSP.Parameters.AddWithValue("@MaSP", maSP);
                deleteSP.ExecuteNonQuery();

            }catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm trong kho hàng !\n" +  ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { _conn.Close(); }
        }

        public bool kiemSanPhamTonTaiDAO(string maSP)
        {
            _conn.Open();
            SqlCommand kt = new SqlCommand("SELECT COUNT(*) FROM KhoHang WHERE MaSP=@MaSP", _conn);
            kt.Parameters.AddWithValue("@MaSP", maSP);
            int count = (int)kt.ExecuteScalar();
            _conn.Close();
            return count > 0;
        }
        public void UpdateSoLuongKhoHangDAO(string maSP, int soLuong)
        {
            try
            {
                _conn.Open();
                SqlCommand updateSL = new SqlCommand("UPDATE KhoHang SET SoLuong = SoLuong + @SoLuong WHERE MaSP = @MaSP", _conn);
                updateSL.Parameters.AddWithValue("@MaSP", maSP);
                updateSL.Parameters.AddWithValue("@SoLuong", soLuong);
                updateSL.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally 
            { _conn.Close(); }
        }
        public void AddSPKhoHangDAO(string maSP, string tenSP , int soLuong)
        {
            try
            {
                _conn.Open();
                SqlCommand addSP = new SqlCommand("INSERT INTO KhoHang (MaSP, TenSP, SoLuong, NgayNhap) VALUES (@MaSP, @TenSP, @SoLuong, GETDATE())", _conn);
                addSP.Parameters.AddWithValue("@MaSP", maSP);
                addSP.Parameters.AddWithValue("@TenSP", tenSP);
                addSP.Parameters.AddWithValue("@SoLuong", soLuong);
                //addSP.Parameters.AddWithValue("@NgayNhap", DateTime.Now);
                addSP.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            { _conn.Close(); }
        }
    }
}
