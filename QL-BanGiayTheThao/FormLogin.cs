using System;
using System.Data;
using System.Windows.Forms;
using BUS;
using DTO;
using DAO;

namespace QL_BanGiayTheThao
{
    public partial class FormLogin : Form
    {

        public class GetDataUser
        {
            static public string taiKhoan;
            static public string phanquyen;

        }

        TaiKhoanBUS taiKhoanBUS = new TaiKhoanBUS();

        public FormLogin()
        {
            InitializeComponent();
        }

        private void checkViewPassWord_CheckedChanged(object sender, EventArgs e)
        {
            if (txtMatKhau.UseSystemPasswordChar)
                txtMatKhau.UseSystemPasswordChar = false;
            else
                txtMatKhau.UseSystemPasswordChar = true;
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtTaiKhoan.Text == "" || txtMatKhau.Text == "")
                {
                    MessageBox.Show("Thông tin tài khoản hoặc mật khẩu không được để trống !");
                }
                else
                {
                    TaiKhoanBUS tk = new TaiKhoanBUS();
                    TaiKhoanDTO pq = tk.TimKiemTaiKhoanBUS(txtTaiKhoan.Text);
                    if (taiKhoanBUS.KiemTraTKMKBUS(txtTaiKhoan.Text, txtMatKhau.Text))
                    {
                        FormMenu formMenu = new FormMenu();
                        GetDataUser.taiKhoan = pq.TaiKhoan;
                        GetDataUser.phanquyen = pq.PhanQuyen;
                        MessageBox.Show("Đăng nhập thành công !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        this.Hide();
                        formMenu.ShowDialog();
                        formMenu.BringToFront();
                    }
                    else
                    {
                        MessageBox.Show("Mật khẩu không chính xác hãy nhập lại !", "Thông Báo", MessageBoxButtons.OK, MessageBoxIcon.Question);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi :  " + ex.Message, "THÔNG BÁO", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void FormLogin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult close = MessageBox.Show("Bạn có chắc muốn thoát ?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(close == DialogResult.Yes)
            {
                Close();
            }
        }
    }
}
