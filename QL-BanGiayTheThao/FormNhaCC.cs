using BUS;
using DAO;
using DTO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace QL_BanGiayTheThao
{
    public partial class FormNhaCC : Form
    {
        NhaCungCapBUS nhacungcapbus = new NhaCungCapBUS();

        public FormNhaCC()
        {
            InitializeComponent();
        }
        void loadncc()
        {
            dtgrvHienThiListNCC.DataSource = nhacungcapbus.ListOfNhaCungCap();

        }
        private void NhaCC_Load(object sender, EventArgs e)
        {
            try
            {
                dtgrvHienThiListNCC.DataSource = nhacungcapbus.ListOfNhaCungCap();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            dtgrvHienThiListNCC.CellEndEdit += dtgrvHienThiListNCC_CellEndEdit;
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            NhaCungCapDTO nhaCC = new NhaCungCapDTO();
            nhaCC.MaNCC = txtMaNhaCC.Text;
            nhaCC.TenNCC = txtTenNhaCC.Text;
            nhaCC.Email = txtEmail.Text;
            nhaCC.SDTLH = txtSDT.Text;

            // Kiểm tra xem giá bán nhập vào có đúng định dạng số không
            if (!decimal.TryParse(txtSDT.Text, out decimal giaBan))
            {
                MessageBox.Show("Số điện thoại bạn cung cấp không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem tất cả các trường thông tin đã được nhập đầy đủ
            if (string.IsNullOrWhiteSpace(txtMaNhaCC.Text) || string.IsNullOrWhiteSpace(txtTenNhaCC.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một đối tượng SanPhamDAO và gọi phương thức thêm sản phẩm từ lớp DAO
            NhaCungCapDAO daonhaCC = new NhaCungCapDAO();

            // Kiểm tra xem mã sản phẩm đã tồn tại hay chưa

            if (nhacungcapbus.addnhacungcangbus(nhaCC.MaNCC))
            {
                MessageBox.Show("Mã nhà cung cấp đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Tạo một đối tượng SanPhamBUS và gọi phương thức thêm sản phẩm từ lớp BUS
            NhaCungCapBUS nhaccBUS = new NhaCungCapBUS();
            nhaccBUS.AddNCC(nhaCC);
            btn_reset_Click(sender, e);
            // Hiển thị thông báo thành công
            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Refresh lại danh sách sản phẩm
            loadncc();

        }


        private void btn_sua_Click(object sender, EventArgs e)
        {

            NhaCungCapDTO nhacc = new NhaCungCapDTO();
            nhacc.MaNCC = txtMaNhaCC.Text;
            nhacc.TenNCC = txtTenNhaCC.Text;
            nhacc.Email = txtEmail.Text;
            nhacc.SDTLH = txtSDT.Text;
            // Kiểm tra xem tất cả các trường thông tin đã được nhập đầy đủ
            if (string.IsNullOrWhiteSpace(txtMaNhaCC.Text) || string.IsNullOrWhiteSpace(txtTenNhaCC.Text) || string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem giá bán nhập vào có đúng định dạng số không
            if (!decimal.TryParse(txtSDT.Text, out decimal giaBan))
            {
                MessageBox.Show("Hotline không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }



            // Tạo một đối tượng SanPhamBUS và gọi phương thức thêm sản phẩm từ lớp BUS
            NhaCungCapBUS nhaccbus = new NhaCungCapBUS();
            nhaccbus.UpdateNhaCungCap(nhacc);

            // Hiển thị thông báo thành công
            MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btn_reset_Click(sender, e);

            // Refresh lại danh sách sản phẩm
            loadncc();

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult del = MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (del == DialogResult.Yes)
                {
                    nhacungcapbus.DeleteNhaCC(txtMaNhaCC.Text);
                    btn_reset_Click(sender, e);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa san pham: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtMaNhaCC.Enabled = true;
            txtMaNhaCC.Text = "";
            txtTenNhaCC.Text = "";
            txtEmail.Text = "";
            txtSDT.Text = "";
            txtTimKiem.Text = "";
            NhaCC_Load(sender, e);
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            Excel obj = new Excel();
            NhaCungCapDAO dataService = new NhaCungCapDAO();

            if (xuatnhacc.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = dataService.ListNhaCungCap();

                // Kiểm tra xem có dữ liệu để xuất không
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    bool export = obj.ToExcel(dataTable, xuatnhacc.FileName);
                    if (export)
                    {
                        MessageBox.Show("Xuất dữ liệu thành công!");
                        btn_reset_Click(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu để xuất!");
                }
            }
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchNhaCC = txtTimKiem.Text;

            try
            {
                DataTable dt = nhacungcapbus.TimKiemNhaCCBUS(searchNhaCC);

                dtgrvHienThiListNCC.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy nhà cung cấp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tìm kiếm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void dtgrvHienThiListNCC_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                txtMaNhaCC.Enabled = false;
                txtMaNhaCC.Text = dtgrvHienThiListNCC.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtTenNhaCC.Text = dtgrvHienThiListNCC.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtEmail.Text = dtgrvHienThiListNCC.Rows[e.RowIndex].Cells[2].Value.ToString();
                txtSDT.Text = dtgrvHienThiListNCC.Rows[e.RowIndex].Cells[3].Value.ToString();


            }
            catch
            {
                MessageBox.Show("Có lỗi khi thực hiện chức năng này !");
            }
        }

        private void dtgrvHienThiListNCC_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dtgrvHienThiListNCC.Rows[e.RowIndex];

                // Lấy tên cột của ô dữ liệu vừa chỉnh sửa
                string columnName = dtgrvHienThiListNCC.Columns[e.ColumnIndex].Name;

                if (columnName == "TenNCC")
                {
                    // Lấy giá trị của trường "mancc" sau khi chỉnh sửa
                    string newValue = row.Cells["TenNCC"].Value.ToString();
                    MessageBox.Show($"Bạn đã sửa mã nhà cung cấp thành: {newValue}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (columnName == "hotline")
                {
                    // Lấy giá trị của trường "hotline" sau khi chỉnh sửa
                    string newValue = row.Cells["hotline"].Value.ToString();
                    MessageBox.Show($"Bạn đã sửa hotline thành: {newValue}", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
