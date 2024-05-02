using BUS;
using DAO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_BanGiayTheThao
{
    public partial class FormKhoHang : Form
    {
        KhoHangBUS khoHangBUS = new KhoHangBUS();
        public FormKhoHang()
        {
            InitializeComponent();
        }

        private void KhoHang_Load(object sender, EventArgs e)
        {
            try
            {
                dtgrvHienThiListSPKho.DataSource = khoHangBUS.ListOfKhoHang();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRefesh_Click(object sender, EventArgs e)
        {
            KhoHang_Load(sender, e);
        }

        private void btnNhapHang_Click(object sender, EventArgs e)
        {
            // Tạo một instance của form nhập hàng
            FormNhapHang formNhapHang = new FormNhapHang();

            // Hiển thị form nhập hàng
            formNhapHang.ShowDialog();
        }

        private void btnXoaSP_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có dòng nào được chọn không
                if (dtgrvHienThiListSPKho.SelectedRows.Count > 0)
                {
                    // Lấy dòng được chọn
                    DataGridViewRow selectedRow = dtgrvHienThiListSPKho.SelectedRows[0];

                    string maSP = selectedRow.Cells[0].Value.ToString();

                    DialogResult del = MessageBox.Show("Bạn có chắn chắc muốn xóa sản phẩm có mã: " + maSP + " không ? ",
                        "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (del == DialogResult.Yes)
                    {
                        khoHangBUS.DeleteSPOfKhoHangBUS(maSP);
                        MessageBox.Show("Xóa sản phẩm thành công. ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        KhoHang_Load(sender, e);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm muốn xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm: " + ex.Message,"Lỗi", MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }

        private void dtgrvHienThiListSPKho_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            Excel obj = new Excel();
            KhoHangDAO dataService = new KhoHangDAO();

            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                DataTable dataTable = dataService.ListKhoHangDAO();

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
