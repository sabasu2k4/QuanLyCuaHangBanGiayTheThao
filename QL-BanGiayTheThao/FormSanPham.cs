using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAO;
using DTO;
namespace QL_BanGiayTheThao
{
    public partial class FormSanPham : Form
    {
        SanPhamBUS sanphambus = new SanPhamBUS();
        string maspcu = "";
        public FormSanPham()
        {
            InitializeComponent();
        }
        void loadsp()
        {
            dtgrvHienThiListSP.DataSource = sanphambus.listofsanpham();

        }
        private void SanPham_Load(object sender, EventArgs e)
        {
            try
            {
                dtgrvHienThiListSP.DataSource = sanphambus.listofsanpham();
                LoadComboBox();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi :  " + ex.Message, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
        }

        private void dtgrvHienThiListSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgrvHienThiListSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            try
            {
                txtMaSP.Enabled = false;
                maspcu = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[0].Value.ToString();
                txtMaSP.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[0].Value.ToString();
                cbbNCC.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[1].Value.ToString();
                txtTenSP.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[2].Value.ToString();
                cbbHangSP.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[3].Value.ToString();
                cbbXuatxu.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[4].Value.ToString();
                txtGiaBan.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[5].Value.ToString();
                cbbTheloai.Text = dtgrvHienThiListSP.Rows[e.RowIndex].Cells[6].Value.ToString();

            }
            catch
            {
                MessageBox.Show("Có lỗi khi thực hiện chức năng này !");
            }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            txtMaSP.Enabled = true;
            txtMaSP.Text = "";
            cbbNCC.Text = "  Chọn nhà cung cấp";
            cbbHangSP.Text = "  Chọn hãng sản phẩm";
            cbbXuatxu.Text = "  Chọn xuất xứ";
            cbbTheloai.Text = "Chọn thể loại";
            txtGiaBan.Text = "";
            txtTenSP.Text = "";
            txtTimKiem.Text = "";
            SanPham_Load(sender, e);
        }

        private void btn_them_Click(object sender, EventArgs e)
        {
            SanPhamDTO sanPham = new SanPhamDTO();
            sanPham.MaSP = txtMaSP.Text;
            sanPham.MaNCC = cbbNCC.Text;
            sanPham.TenSP = txtTenSP.Text;
            sanPham.HangSP = cbbHangSP.Text;
            sanPham.XuatXu = cbbXuatxu.Text;
            sanPham.GiaBan = txtGiaBan.Text;
            sanPham.TheLoai = cbbTheloai.Text;

            // Kiểm tra xem tất cả các trường thông tin đã được nhập đầy đủ
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(cbbNCC.Text) || string.IsNullOrWhiteSpace(txtTenSP.Text) || string.IsNullOrWhiteSpace(cbbHangSP.Text) || string.IsNullOrWhiteSpace(cbbXuatxu.Text) || string.IsNullOrWhiteSpace(txtGiaBan.Text) || string.IsNullOrWhiteSpace(cbbTheloai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // Kiểm tra xem giá bán nhập vào có đúng định dạng số không
            if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một đối tượng SanPhamDAO và gọi phương thức thêm sản phẩm từ lớp DAO
            SanPhamDAO daoSanPham = new SanPhamDAO();

            // Kiểm tra xem mã sản phẩm đã tồn tại hay chưa
            if (daoSanPham.kiemTraMaSP(sanPham.MaSP))
            {
                MessageBox.Show("Mã sản phẩm đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một đối tượng SanPhamBUS và gọi phương thức thêm sản phẩm từ lớp BUS
            SanPhamBUS sanPhamBUS = new SanPhamBUS();
            sanPhamBUS.AddSanPham(sanPham);

            // Hiển thị thông báo thành công
            MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

            btn_reset_Click(sender, e);

            // Refresh lại danh sách sản phẩm
            loadsp();

        }

        private void btn_sua_Click(object sender, EventArgs e)
        {

            SanPhamDTO sanPham = new SanPhamDTO();
            sanPham.MaSP = txtMaSP.Text;
            sanPham.MaNCC = cbbNCC.Text;
            sanPham.TenSP = txtTenSP.Text;
            sanPham.HangSP = cbbHangSP.Text;
            sanPham.XuatXu = cbbXuatxu.Text;
            sanPham.GiaBan = txtGiaBan.Text;
            sanPham.TheLoai = cbbTheloai.Text;

            // Kiểm tra xem tất cả các trường thông tin đã được nhập đầy đủ
            if (string.IsNullOrWhiteSpace(txtMaSP.Text) || string.IsNullOrWhiteSpace(cbbNCC.Text) || string.IsNullOrWhiteSpace(txtTenSP.Text) || string.IsNullOrWhiteSpace(cbbHangSP.Text) || string.IsNullOrWhiteSpace(cbbXuatxu.Text) || string.IsNullOrWhiteSpace(txtGiaBan.Text) || string.IsNullOrWhiteSpace(cbbTheloai.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra xem giá bán nhập vào có đúng định dạng số không
            if (!decimal.TryParse(txtGiaBan.Text, out decimal giaBan))
            {
                MessageBox.Show("Giá bán không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Tạo một đối tượng SanPhamDAO và gọi phương thức thêm sản phẩm từ lớp DAO
            SanPhamDAO daoSanPham = new SanPhamDAO();

            // Tạo một đối tượng SanPhamBUS và gọi phương thức thêm sản phẩm từ lớp BUS
            SanPhamBUS sanPhamBUS = new SanPhamBUS();
            sanPhamBUS.UpdateSanPham(sanPham, maspcu);

            // Hiển thị thông báo thành công
            MessageBox.Show("Sủa sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btn_reset_Click(sender, e);
            // Refresh lại danh sách sản phẩm
            loadsp();

        }

        private void btn_xoa_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult del = MessageBox.Show("Bạn có chắc chắn muốn xóa không ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (del == DialogResult.Yes)
                {
                    sanphambus.DeleteSanPham(txtMaSP.Text);
                    btn_reset_Click(sender, e);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi xóa san pham: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            Excel obj = new Excel();
            SanPhamDAO dataService = new SanPhamDAO();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = dataService.ListSanPhamDAO();

                // Kiểm tra xem có dữ liệu để xuất không
                if (dataTable != null && dataTable.Rows.Count > 0)
                {
                    bool export = obj.ToExcel(dataTable, saveFileDialog1.FileName);
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
            string searchSanPham = txtTimKiem.Text.Trim();

            try
            {
                DataTable dt = sanphambus.TimKiemNhaCCBUS(searchSanPham);

                dtgrvHienThiListSP.DataSource = dt;

                if (dt.Rows.Count == 0)
                {
                    MessageBox.Show("Không tìm thấy sản phẩm bạn mong muốn.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi khi tìm kiếm nhà cung cấp: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadComboBox()
        {
            try
            {
                DataTable nhaSanXuatTable = sanphambus.loadNhaCC();

                cbbNCC.DataSource = nhaSanXuatTable;
                //cbbNCC.DisplayMember = "Ten";
                cbbNCC.ValueMember = "MaNCC";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
