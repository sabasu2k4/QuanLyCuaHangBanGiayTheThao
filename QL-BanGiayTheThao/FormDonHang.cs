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

namespace QL_BanGiayTheThao
{
    public partial class FormDonHang : Form
    {
        DonHangBUS donhangbus = new DonHangBUS();

        public FormDonHang()
        {
            InitializeComponent();
        }


        private void DonHang_Load(object sender, EventArgs e)
        {
            try
            {
                btnXuatHD.Visible = false;
                btnXemCTDH.Visible = false;
                numericUpDownSoLuong.Value = 0;
                txtThanhTien.Text = "0";
                LoadComboBoxsp();
                
                txtTaiKhoanXuLy.Text = FormLogin.GetDataUser.taiKhoan;
                cboSanPham.SelectedIndexChanged += cboSanPham_SelectedIndexChanged;
                LoadDaTaGioHang();
                // Hiển thị thông tin sản phẩm đầu tiên (nếu có) khi Form được tải
                if (cboSanPham.SelectedIndex != -1)
                {
                    string maSP = cboSanPham.SelectedValue?.ToString();
                    txtGiaBan.Text = donhangbus.getGiaBanBUS(maSP).ToString();
                }
                lblTongTien1.Text = donhangbus.tinhTongTien().ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi :  " + ex.Message, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.None);

            }
        }
        private void LoadComboBoxsp()
        {
            try
            {
                DataTable sanphamTable = donhangbus.LoadComboBoxSanPhamBUS();

                sanphamTable.Columns.Add("MaTenSPLuong", typeof(string), "masp + ' | ' + TenSP + ' | Số lượng còn:' + Soluong");

                cboSanPham.DataSource = sanphamTable;


                cboSanPham.ValueMember = "masp";
                cboSanPham.DisplayMember = "MaTenSPLuong";
                cboSanPham.Tag = "Soluong"; // Lưu trữ tên cột số lượng để sử dụng sau này
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void LoadDaTaGioHang()
        {
            try
            {
                dtgrvHienThiListSPGioHang.DataSource = donhangbus.HienThiDanhSachGioHangBUS();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maSP = cboSanPham.SelectedValue?.ToString();
                string tenSP = cboSanPham.Text;
                int soLuong = (int)numericUpDownSoLuong.Value; // Lấy số lượng từ NumericUpDown

                if (string.IsNullOrEmpty(maSP) || string.IsNullOrEmpty(tenSP) || soLuong <= 0)
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm và nhập số lượng hợp lệ.");
                    return;
                }

                float giaBan = donhangbus.getGiaBanBUS(maSP);
                float thanhTien = soLuong * giaBan;

                GioHangDTO gioHang = new GioHangDTO
                {
                    MaSP = maSP,
                    TenSP = tenSP,
                    SoLuong = soLuong,
                    GiaBan = giaBan,
                    ThanhTien = thanhTien
                };
                donhangbus.AddSanPhamVaoGioHangBUS(gioHang);
                LoadDaTaGioHang();
                lblTongTien.Text = donhangbus.tinhTongTien().ToString();

                MessageBox.Show("Thêm sản phẩm vào giỏ hàng thành công!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm sản phẩm vào giỏ hàng: " + ex.Message);
            }
        }
        private void cboSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (cboSanPham.SelectedIndex != -1) // Kiểm tra xem có mục nào được chọn hay không
                {
                    // Lấy giá trị mã sản phẩm được chọn
                    string maSP = cboSanPham.SelectedValue?.ToString();
                    float giaBan = donhangbus.getGiaBanBUS(maSP);
                    //int soLuong = Convert.ToInt32(txtSoLuong.Text);

                    txtGiaBan.Text = giaBan.ToString();
                    TinhTien();
                    //txtThanhTien.Text = (giaBan*soLuong).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
        private void TinhTien()
        {
            try
            {
                int soLuong = (int)numericUpDownSoLuong.Value; // Lấy giá trị số lượng từ NumericUpDown

                float giaBan = 0;
                if (!float.TryParse(txtGiaBan.Text, out giaBan))
                {
                    txtThanhTien.Text = ""; // Đặt thành tiền về rỗng nếu giá bán không hợp lệ
                    return;
                }

                float thanhTien = soLuong * giaBan;
                txtThanhTien.Text = thanhTien.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính toán thành tiền: " + ex.Message);
            }
        }
        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtgrvHienThiListSPGioHang.SelectedRows.Count > 0)
                {
                    string maSP = dtgrvHienThiListSPGioHang.SelectedRows[0].Cells["MaSP"].Value.ToString();
                    donhangbus.DeleteSanPhamBUS(maSP);

                    LoadDaTaGioHang();
                    lblTongTien.Text = donhangbus.tinhTongTien().ToString();

                    MessageBox.Show("Xóa sản phẩm khỏi giỏ hàng thành công!");
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn sản phẩm cần xóa từ giỏ hàng.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa sản phẩm khỏi giỏ hàng: " + ex.Message);
            }
        }
        private void numericUpDownSoLuong_ValueChanged(object sender, EventArgs e)
        {
            TinhTien();
        }
        private void btnHuyDH_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult huy = MessageBox.Show("Chọn Yes để hủy đơn hàng ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if(huy == DialogResult.Yes)
                {
                    lblTongTien.Text = "0";
                    donhangbus.HuyDonHangBUS();
                    btnRefesh_Click(sender,e);
                }
            }catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi hủy đơn hàng: " + ex.Message,"Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
        private void btnRefesh_Click(object sender, EventArgs e)
        {
            txtMaDH.Text = "";
            txtTenKH.Text = "";
            txtSDTKH.Text = "";
            txtDiaChi.Text = "";
            dateTaoDH.Value = DateTime.Now;
            numericUpDownSoLuong.Value = 0;
            LoadDaTaGioHang();
        }

        public class Inhoadon
        {
            static public DonHangDTO dh;
            static public List<GioHangDTO> listsp;
        }
        public List<ChiTietDonHangDTO> listctdh = new List<ChiTietDonHangDTO>();
        public List<GioHangDTO> listctsp = new List<GioHangDTO>();
        public bool TestTextBox()
        {
            if (txtMaDH.Text == "" || txtTenKH.Text == "" && txtDiaChi.Text == "" || txtSDTKH.Text == "" || txtTaiKhoanXuLy.Text == "")
            {
                return false;
            }
            return true;
        }
        public DonHangDTO GetDataTextBox()
        {
            DonHangDTO dhnew = new DonHangDTO();
            if (TestTextBox())
            {
                dhnew.MaDH = txtMaDH.Text;
                dhnew.TenKH = txtTenKH.Text;
                dhnew.SDTKH = txtSDTKH.Text;
                dhnew.DiaChi = txtDiaChi.Text;
                dhnew.MaNV = txtTaiKhoanXuLy.Text;
                dhnew.ThoiGianTao = dateTaoDH.Value;
                return dhnew;
            }
            else
            {
                return null;
            }
        }
        private void btnXuatHD_Click(object sender, EventArgs e)
        {
            try
            {
                // Tạo một đối tượng của FormXuatHoaDon
                Inhoadon.dh = GetDataTextBox();
                Inhoadon.listsp = listctsp;
                FormXuatHoaDon a = new FormXuatHoaDon();
                // Gọi phương thức để lấy danh sách sản phẩm từ giỏ hàng và gán cho DataGridView trên FormXuatHoaDon
                a.LoadDataGridView(donhangbus.GetInvoiceItemsFromCart());
                a.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error exporting invoice: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        public bool kiemTraSDTHopLe(string sdt)
        {
            return !string.IsNullOrEmpty(sdt) && sdt.Length >= 10 && sdt.Length <= 11 && sdt.All(char.IsDigit);
        }
        private void btnTaoDH_Click(object sender, EventArgs e)
        {
            DialogResult create = MessageBox.Show("Chọn yes để tạo đơn hàng ", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(create == DialogResult.Yes)
            {
                if (txtMaDH.Text == "" || txtDiaChi.Text == "" || txtSDTKH.Text == "" || txtDiaChi.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin !!!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if (!kiemTraSDTHopLe(txtSDTKH.Text))
                {
                    MessageBox.Show("SDT bạn nhập sai định dạng !", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                btnXuatHD.Visible = true;
                btnXemCTDH.Visible = true;
            }
        }

        private void btnXemCTDH_Click(object sender, EventArgs e)
        {
            Inhoadon.dh = GetDataTextBox();
            Inhoadon.listsp = listctsp;
            FormChiTietDonHang a = new FormChiTietDonHang(Inhoadon.dh, Inhoadon.listsp);
            a.LoadDataGridView(donhangbus.GetInvoiceItemsFromCart());
            a.Show();
        }
    }   
}
