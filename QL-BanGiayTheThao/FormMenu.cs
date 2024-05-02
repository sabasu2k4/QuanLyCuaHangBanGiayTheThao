using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_BanGiayTheThao
{
    public partial class FormMenu : Form
    {
        private Timer timer;

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nwidthEllipse,
            int nHeightEllipse
        );

        public FormMenu()
        {
            InitializeComponent();

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnTrangChu.Height;
            pnlNav.Top = btnTrangChu.Top;
            pnlNav.Left = btnTrangChu.Left;

            // Khởi tạo Timer
            timer = new Timer();
            timer.Interval = 1000; // 1000 mili giây = 1 giây
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            // Cập nhật thời gian mỗi khi Timer tick
            lblDateTime.Text = GetTimeNow();
        }

        private Form formNow;
        // Dùng để tải 1 form mới vào panelbody của FormMenu 
        private void LoadForm(Form formnew)
        {
            // Kiểm tra xem có Form nào đang hiển thị trong panelbody không
            if (formNow != null)
            {
                formNow.Close();
            }
            formNow = formnew;
            //  Đặt thuộc tính TopLevel của Form mới thành false, để Form mới không được coi là một cửa sổ độc lập và được nhúng vào Panel
            formnew.TopLevel = false;
            formnew.TopMost = true;
            // Đặt kiểu viền của Form mới thành None, làm cho Form mới không có viền.
            formnew.FormBorderStyle = FormBorderStyle.None;
            // Đặt Dock của Form mới thành Fill, làm cho Form mới lấp đầy toàn bộ kích thước của Panel.
            formnew.Dock = DockStyle.Fill;
            // Thêm Form mới vào danh sách các controls của Panel (panelBody).
            panelBody.Controls.Add(formnew);
            // Lưu trữ Form mới vào thuộc tính Tag của Panel, điều này có thể hữu ích khi bạn muốn truy cập Form từ Panel sau này.
            panelBody.Tag = formnew;
            // Đưa Form mới lên phía trước của các controls khác trong Panel, đảm bảo rằng Form mới sẽ được hiển thị.
            formnew.BringToFront();
            formnew.Show();
        }

        private void btnTrangChu_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "TRANG CHỦ";
            FormTrangChu tc = new FormTrangChu();
            LoadForm(tc);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnTrangChu.Height;
            pnlNav.Top = btnTrangChu.Top;
            pnlNav.Left = btnTrangChu.Left;
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "SẢN PHẨM";
            FormSanPham sp = new FormSanPham();
            LoadForm(sp);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnSanPham.Height;
            pnlNav.Top = btnSanPham.Top;
            pnlNav.Left = btnSanPham.Left;
        }

        private void btnNhaCC_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "NHÀ CUNG CẤP";
            FormNhaCC ncc = new FormNhaCC();
            LoadForm(ncc);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnNhaCC.Height;
            pnlNav.Top = btnNhaCC.Top;
            pnlNav.Left = btnNhaCC.Left;
        }

        private void btnTaoDonHang_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "TẠO ĐƠN HÀNG";
            FormDonHang formDonHang = new FormDonHang();
            LoadForm(formDonHang);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnTaoDonHang.Height;
            pnlNav.Top = btnTaoDonHang.Top;
            pnlNav.Left = btnTaoDonHang.Left;
        }

        private void btnKhoHang_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "KHO HÀNG";
            FormKhoHang kh = new FormKhoHang();
            LoadForm(kh);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnKhoHang.Height;
            pnlNav.Top = btnKhoHang.Top;
            pnlNav.Left = btnKhoHang.Left;
        }

        private void btnNhanVien_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "NHÂN VIÊN";
            FormNhanVien nv = new FormNhanVien();
            LoadForm(nv);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnNhanVien.Height;
            pnlNav.Top = btnNhanVien.Top;
            pnlNav.Left = btnNhanVien.Left;
        }

        private void btnThongKe_Click(object sender, EventArgs e)
        {
            lblTitle.Text = "THỐNG KÊ";
            FormThongKe formThongKe = new FormThongKe();
            LoadForm(formThongKe);

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnThongKe.Height;
            pnlNav.Top = btnThongKe.Top;
            pnlNav.Left = btnThongKe.Left;
        }

        private void btnDangXuat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất tài khoản không  ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                this.Hide();
                FormLogin a = new FormLogin();
                a.ShowDialog();
            }

            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));
            pnlNav.Height = btnDangXuat.Height;
            pnlNav.Top = btnDangXuat.Top;
            pnlNav.Left = btnDangXuat.Left;
        }

        private void FormMenu_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        static string GetTimeNow()
        {
            DateTime date = DateTime.Now;

            string dayOfWeek = date.ToString("dddd");
            string dayOfMonth = date.Day.ToString();
            string month = date.Month.ToString();
            string year = date.Year.ToString();
            string hour = date.Hour.ToString("00"); // Đảm bảo hiển thị 2 chữ số cho giờ
            string minute = date.Minute.ToString("00"); // Đảm bảo hiển thị 2 chữ số cho phút
            string second = date.Second.ToString("00"); // Đảm bảo hiển thị 2 chữ số cho giây

            return $"{dayOfWeek}, ngày {dayOfMonth}/{month}/{year}, Thời gian: {hour}:{minute}:{second}";
        }

        private void FormMenu_Load(object sender, EventArgs e)
        {
            lblUserName.Text = FormLogin.GetDataUser.taiKhoan;
            lblPhanQuyen.Text = FormLogin.GetDataUser.phanquyen;
            ////Gettime
            lblDateTime.Text = GetTimeNow();
            if (lblPhanQuyen.Text == "Nhân Viên")
            {
                btnNhanVien.Visible = false;
                btnThongKe.Visible = false;
            }
            FormTrangChu tc = new FormTrangChu();
            btnTrangChu_Leave(sender, e);
            btnSanPham_Leave(sender, e);
            btnNhaCC_Leave(sender, e);
            btnKhoHang_Leave(sender, e);
            btnDangXuat_Leave(sender, e);
            btnTaoDonHang_Leave(sender, e);
            btnNhanVien_Leave(sender, e);
            btnThongKe_Leave(sender, e);

            LoadForm(tc);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult close = MessageBox.Show("Bạn có chắc muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (close == DialogResult.Yes)
            {
                Close();
            }
        }


        // Hiệu ứng button
        private void btnTrangChu_Leave(object sender, EventArgs e)
        {
            btnTrangChu.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnSanPham_Leave(object sender, EventArgs e)
        {
            btnSanPham.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnNhaCC_Leave(object sender, EventArgs e)
        {
            btnNhaCC.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnTaoDonHang_Leave(object sender, EventArgs e)
        {
            btnTaoDonHang.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnKhoHang_Leave(object sender, EventArgs e)
        {
            btnKhoHang.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnNhanVien_Leave(object sender, EventArgs e)
        {
            btnNhanVien.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnThongKe_Leave(object sender, EventArgs e)
        {
            btnThongKe.BackColor = Color.FromArgb(30, 50, 94);
        }

        private void btnDangXuat_Leave(object sender, EventArgs e)
        {
            btnDangXuat.BackColor = Color.FromArgb(30, 50, 94);
        }

    }
}
