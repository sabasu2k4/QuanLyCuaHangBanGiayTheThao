using BUS;
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
    public partial class FormChiTietDonHang : Form
    {
        private DonHangDTO donHang;
        private List<GioHangDTO> listSanPham;
        public FormChiTietDonHang(DonHangDTO dh, List<GioHangDTO> listsp)
        {
            InitializeComponent();
            donHang = dh;
            listSanPham = listsp;
        }
        private void ChiTietDonHang_Load(object sender, EventArgs e)
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
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading invoice items: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
