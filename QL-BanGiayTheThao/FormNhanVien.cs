using System;
using System.Data;
using System.Windows.Forms;
using BUS;
using DTO;
using DAO;
using System.Drawing;

namespace QL_BanGiayTheThao
{
    public partial class FormNhanVien : Form
    {
        NhanVienBUS nhanvienbus = new NhanVienBUS();
        NhanVienDTO nhanviendto = new NhanVienDTO();
        public FormNhanVien()
        {
            InitializeComponent();
        }
 
        // LOAD
        private void FormNhanVien_Load(object sender, EventArgs e)
        {            
            try
            {
                dtgrvHienThiListNV.DataSource = nhanvienbus.listOfNhanVienBUS();
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi :  " + ex.Message, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public DataTable LoadDataToDataGridView()
        {
            return nhanvienbus.listOfNhanVienBUS();
        }

        private void dtgrvHienThiListNV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtMaNV.Enabled = false;
            txtTenDangNhap.Enabled = false;

            txtMaNV.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[0].Value.ToString();
            txtHoTen.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[1].Value.ToString();
            txtSDT.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[2].Value.ToString();
            txtQueQuan.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[3].Value.ToString();
            txtEmail.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[4].Value.ToString();
            txtTenDangNhap.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[5].Value.ToString();
            txtMatKhau.Text = dtgrvHienThiListNV.Rows[e.RowIndex].Cells[6].Value.ToString();
        }

        private void btn_them_Click(object sender, EventArgs e)
        {

            try
            {
                TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO();
                taiKhoanDTO.TaiKhoan = txtTenDangNhap.Text;
                NhanVienDTO nhanviendto = new NhanVienDTO();
                nhanviendto.MaNV = txtMaNV.Text;
                nhanviendto.TenNV = txtHoTen.Text;
                nhanviendto.SDT = txtSDT.Text;
                nhanviendto.QueQuan = txtQueQuan.Text;
                nhanviendto.Email = txtEmail.Text;
                nhanviendto.TaiKhoan = txtTenDangNhap.Text;
                nhanviendto.MatKhau = txtMatKhau.Text;

                if (txtMaNV.Text == "" || txtHoTen.Text == "" || txtSDT.Text == "" || txtQueQuan.Text == "" || txtEmail.Text == ""
                    || txtTenDangNhap.Text == "" || txtMatKhau.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (nhanvienbus.kiemTraMaNVBUS(nhanviendto.MaNV))
                    {
                        MessageBox.Show("Nhân viên đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (nhanvienbus.kiemTraTaiKhoanDAO(taiKhoanDTO.TaiKhoan))
                    {
                        MessageBox.Show("Tài khoản đã tồn tại !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (!nhanvienbus.kiemTraSDTHopLe(nhanviendto.SDT))
                    {
                        MessageBox.Show("SDT bạn nhập sai định dạng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (!nhanvienbus.kiemTraEmailHopLe(nhanviendto.Email))
                    {
                        MessageBox.Show("Email bạn nhập sai định dạng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        DialogResult update = MessageBox.Show("Xác nhận để thêm nhân viên !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (update == DialogResult.Yes)
                        {
                            NhanVienBUS nhanVienBUS = new NhanVienBUS();
                            nhanVienBUS.addNhanVienBUS(nhanviendto);
                            FormNhanVien_Load(sender, e);
                            btn_reset_Click(sender, e);
                            MessageBox.Show("Thêm nhân viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }               
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Bạn phải tạo tài khoản bên quản lý tài khoản trước !!\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //MessageBox.Show("Có lỗi khi thêm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {   
                DialogResult del = MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(del == DialogResult.Yes)
                {
                    nhanvienbus.DeleteNhanVienDAO(txtMaNV.Text);
                    FormNhanVien_Load(sender, e);
                    btn_reset_Click(sender, e);
                    MessageBox.Show("Xóa nhân viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_sua_Click(object sender, EventArgs e)
        {
            try
            {
                TaiKhoanDTO taiKhoanDTO = new TaiKhoanDTO();
                taiKhoanDTO.TaiKhoan = txtTenDangNhap.Text;
                NhanVienDTO nhanviendto = new NhanVienDTO();
                nhanviendto.MaNV = txtMaNV.Text;
                nhanviendto.TenNV = txtHoTen.Text;
                nhanviendto.SDT = txtSDT.Text;
                nhanviendto.QueQuan = txtQueQuan.Text;
                nhanviendto.Email = txtEmail.Text;
                nhanviendto.TaiKhoan = txtTenDangNhap.Text;
                nhanviendto.MatKhau = txtMatKhau.Text;
                if (txtMaNV.Text == "" || txtHoTen.Text == "" || txtSDT.Text == "" || txtQueQuan.Text == "" || txtEmail.Text == ""
                    || txtTenDangNhap.Text == "" || txtMatKhau.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (!nhanvienbus.kiemTraSDTHopLe(nhanviendto.SDT))
                    {
                        MessageBox.Show("SDT bạn nhập sai định dạng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (!nhanvienbus.kiemTraEmailHopLe(nhanviendto.Email))
                    {
                        MessageBox.Show("Email bạn nhập sai định dạng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else
                    {
                        DialogResult update = MessageBox.Show("Xác nhận để cập nhật nhân viên !", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                        if (update == DialogResult.Yes)
                        {
                            NhanVienBUS nhanVienBUS = new NhanVienBUS();
                            nhanVienBUS.UpdateNhanVienDAO(nhanviendto);
                            FormNhanVien_Load(sender, e);
                            btn_reset_Click(sender, e);
                            MessageBox.Show("Cập nhật nhân viên thành công !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Có lỗi khi cập nhật nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = true;
            txtTenDangNhap.Enabled = true;
            txtMaNV.Text = "";
            txtHoTen.Text = "";
            txtSDT.Text = "";
            txtQueQuan.Text = "";
            txtEmail.Text = "";
            txtTenDangNhap.Text = "";
            txtMatKhau.Text = "";           
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string searchNhanVien = txtTimKiem.Text.Trim();

            try
            {
                if(searchNhanVien == "")
                {
                    MessageBox.Show("Vui lòng nhập nhân viên cần tìm !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    dtgrvHienThiListNV.DataSource = nhanvienbus.TimKiemNhanVienBUS(searchNhanVien);
                    if (dtgrvHienThiListNV.Rows.Count == 0)
                    {
                        MessageBox.Show("Không tìm thấy nhân viên.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Có lỗi khi tìm kiếm nhân viên: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_xuatexcel_Click(object sender, EventArgs e)
        {
            Excel obj = new Excel();
            NhanVienDAO dataService = new NhanVienDAO();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = dataService.ListNhanVienDAO();

                // Kiểm tra xem có dữ liệu để xuất không
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    bool export = obj.ToExcel(dataTable, saveFileDialog1.FileName);
                    if (export)
                    {
                        MessageBox.Show("Xuất dữ liệu thành công!");
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xuất!");
                }
            }
        }
    }
}
