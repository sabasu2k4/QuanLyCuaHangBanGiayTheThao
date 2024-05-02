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

namespace QL_BanGiayTheThao
{
    public partial class FormXuatHoaDon : Form
    {
        public FormXuatHoaDon()
        {
            InitializeComponent();
        }

        private void FormXuatHoaDon_Load(object sender, EventArgs e)
        {
            var ttnv = FormDonHang.Inhoadon.dh;
            var listsps = FormDonHang.Inhoadon.listsp;
            lblMaHD.Text = ttnv.MaDH.ToString();
            lblTKXL.Text = ttnv.MaNV.ToString();
            lblTenKH.Text = ttnv.TenKH.ToString();
            lblSDTKH.Text = ttnv.SDTKH.ToString();
            lblDiachi.Text = ttnv.DiaChi.ToString();
            lblNgaymua.Text = ttnv.ThoiGianTao.ToString();
        }

        public void LoadDataGridView(List<GioHangDTO> invoiceItems)
        {
            float tong = 0;
            dtgrvHienThiListSPGioHang1.Rows.Clear();

            try
            {
                // Hiển thị danh sách sản phẩm trong giỏ hàng trên DataGridView
                foreach (var item in invoiceItems)
                {
                    dtgrvHienThiListSPGioHang1.Rows.Add(
                        item.MaSP,
                        item.TenSP,
                        item.SoLuong,
                        item.GiaBan,
                        item.ThanhTien
                    );
                    tong = tong + item.ThanhTien;
                }
                string epkieuTiente = tong.ToString("#,##0") + " VNĐ";
                lblTongtien.Text = epkieuTiente;
                lblPhaitra.Text = epkieuTiente;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
