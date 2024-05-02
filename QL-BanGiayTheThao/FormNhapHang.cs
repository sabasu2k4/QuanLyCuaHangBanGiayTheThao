using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BUS;
using DAO;
using DTO;

namespace QL_BanGiayTheThao
{
    public partial class FormNhapHang : Form
    {
        SanPhamBUS sanPhamBUS = new SanPhamBUS();
        KhoHangBUS khoHangBUS = new KhoHangBUS();
        public FormNhapHang()
        {
            InitializeComponent();
        }

        private void FormNhapHang_Load(object sender, EventArgs e)
        {
            try
            {
                dtgrvHienThiListSP.DataSource = sanPhamBUS.listofsanpham();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dtgrvHienThiListSP.SelectedRows.Count > 0)
                {
                    // Lấy dòng được chọn
                    DataGridViewRow selectedRow = dtgrvHienThiListSP.SelectedRows[0];

                    string maSP = selectedRow.Cells[0].Value.ToString();
                    string tenSP = selectedRow.Cells[3].Value.ToString();
                    int soLuong = int.Parse(txtSL.Text);

                    if (!int.TryParse(txtSL.Text, out soLuong))
                    {
                        // Nếu chuỗi không hợp lệ, hiển thị thông báo lỗi và không tiếp tục thực hiện thêm sản phẩm vào kho
                        MessageBox.Show("Số lượng nhập không hợp lệ. Vui lòng nhập một số nguyên.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    
                    DialogResult add = MessageBox.Show("Xác nhận để thêm sản phẩm có mã: " + maSP + " với số lượng " + soLuong + " không ? ",
                        "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (add == DialogResult.Yes)
                    {
                        khoHangBUS.NhapHangOfKhoHangBUS(maSP, tenSP, soLuong);
                        MessageBox.Show("Nhập hàng thành công.\nSản phẩm " + maSP + " đã được thêm " + soLuong,
                            "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần nhập hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Có lỗi khi nhập hàng !\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormNhapHang_FormClosed(object sender, FormClosedEventArgs e)
        {
            
        }
    }
}
