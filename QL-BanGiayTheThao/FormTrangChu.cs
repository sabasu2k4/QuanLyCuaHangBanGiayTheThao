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
    public partial class FormTrangChu : Form
    {
        private Timer imageTimer;
        private int currentImageIndex = 0;
        private string[] images = {
        @"C:\Users\nguye\source\repos\qlcuahangMain\images\Trangchu1.png",
        @"C:\Users\nguye\source\repos\qlcuahangMain\images\Trangchu2.png",
        @"C:\Users\nguye\source\repos\qlcuahangMain\images\Trangchu3.png",
        @"C:\Users\nguye\source\repos\qlcuahangMain\images\Trangchu4.png"
    };

        public FormTrangChu()
        {
            InitializeComponent();
            // Khởi tạo Timer và cấu hình
            imageTimer = new Timer();
            imageTimer.Interval = 2000; // Thời gian chuyển ảnh (đơn vị: milliseconds)
            imageTimer.Tick += ImageTimer_Tick;
            imageTimer.Start();
        }

        private void ImageTimer_Tick(object sender, EventArgs e)
        {
            // Tăng chỉ số của ảnh hiện tại
            currentImageIndex++;
            // Nếu đã đến ảnh cuối cùng, quay lại ảnh đầu tiên
            if (currentImageIndex >= images.Length)
            {
                currentImageIndex = 0;
            }
            // Hiển thị ảnh mới
            ShowCurrentImage();
        }

        private void ShowCurrentImage()
        {
            if (currentImageIndex >= 0 && currentImageIndex < images.Length)
            {
                string imagePath = images[currentImageIndex];
                // Hiển thị ảnh trong panelBody hoặc PictureBox (tùy vào cách bạn thiết kế giao diện)
                panelTrangChu.BackgroundImage = Image.FromFile(imagePath);
            }
        }
        private void FormTrangChu_Load(object sender, EventArgs e)
        {

        }
    }
}
