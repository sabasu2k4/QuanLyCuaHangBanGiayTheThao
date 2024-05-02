using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;
using DTO;

namespace DAO
{
    public class NhaCungCapDAO : ConnectDB
    {
        public DataTable ListNhaCungCap()
        {
            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM NhaCC", _conn);
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
        public bool kiemtraMaNcc(string mancc)
        {
            bool exist = false; // Khởi tạo là false, chúng ta sẽ đặt lại thành true nếu tìm thấy mã nhà cung cấp trong cơ sở dữ liệu
            try
            {
                if (_conn.State == ConnectionState.Closed)
                    _conn.Open();

                SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM NhaCC WHERE MaNCC=@MaNCC", _conn);
                cmd.Parameters.AddWithValue("@MaNCC", mancc);
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                if (count > 0)
                {
                    exist = true; ///
                }

                if (_conn.State == ConnectionState.Open)
                    _conn.Close();
            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi: " + ex.Message);
            }
            return exist;
        }
        //

        //public DataTable GetMaNCC()
        //{
        //    DataTable dt = new DataTable();
        //    try
        //    {
        //        string query = "SELECT MaNCC FROM NhaCungCap";
        //        SqlCommand cmd = new SqlCommand(query, _conn);
        //        SqlDataAdapter da = new SqlDataAdapter(cmd);
        //        da.Fill(dt);
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Lỗi khi lấy dữ liệu: " + ex.Message);
        //    }
        //    return dt;
        //}

        //
        public void AddNCCDAO(NhaCungCapDTO nhaCC)
        {

            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("INSERT INTO NhaCC (MaNCC, TenNCC,SDTLH, Email) VALUES (@MaNCC, @TenNCC, @SDTLH, @Email)", _conn);
                cmd.Parameters.AddWithValue("@MaNCC", nhaCC.MaNCC);
                cmd.Parameters.AddWithValue("@TenNCC", nhaCC.TenNCC);
                cmd.Parameters.AddWithValue("@SDTLH", nhaCC.SDTLH);
                cmd.Parameters.AddWithValue("@Email", nhaCC.Email);

                cmd.ExecuteNonQuery();
            }
            catch
            {
                _conn.Close();
            }


        }
        //xoa
        public void DeleteSanPhamDAO(string nhaCC)
        {
            try
            {
                if (_conn.State == ConnectionState.Closed)
                {
                    _conn.Open();
                }

                SqlCommand deleteNhaCC = new SqlCommand("DELETE FROM NhaCC WHERE MaNCC=@MaNCC", _conn);
                deleteNhaCC.Parameters.AddWithValue("@MaNCC", nhaCC);
                deleteNhaCC.ExecuteNonQuery();



            }
            catch (Exception ex)
            {

                Console.WriteLine("Lỗi khi xóa nhà cung cấp: " + ex.Message);
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
        //sua
        public void UpdateNhaCungCapDAO(NhaCungCapDTO nhacc)
        {

            try
            {
                _conn.Open();
                SqlCommand cmd = new SqlCommand("UPDATE NhaCC SET  TenNCC = @TenNCC, Email = @Email, SDTLH = @SDTLH WHERE MaNCC = @MaNCC", _conn);

                cmd.Parameters.AddWithValue("@MaNCC", nhacc.MaNCC);
                cmd.Parameters.AddWithValue("@TenNCC", nhacc.TenNCC);
                cmd.Parameters.AddWithValue("@Email", nhacc.Email);
                cmd.Parameters.AddWithValue("@SDTLH", nhacc.SDTLH);

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



        //tìm kiếm
        public DataTable TimKiemNhaCCDAO(string nhaCC)
        {
            try
            {
                _conn.Open();
                SqlCommand searchNhaCC = new SqlCommand("SELECT * FROM NhaCC WHERE MaNCC LIKE @Search OR TenNCC LIKE @Search OR SDTLH LIKE @Search OR Email LIKE @Search", _conn);
                searchNhaCC.Parameters.AddWithValue("@Search", "%" + nhaCC + "%"); // Thay thế 'nhaCC' bằng 'keyword'
                SqlDataReader rdr = searchNhaCC.ExecuteReader();
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



    }
}
