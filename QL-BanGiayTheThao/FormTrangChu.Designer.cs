namespace QL_BanGiayTheThao
{
    partial class FormTrangChu
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTrangChu));
            this.panelTrangChu = new System.Windows.Forms.Panel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // panelTrangChu
            // 
            this.panelTrangChu.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panelTrangChu.BackgroundImage")));
            this.panelTrangChu.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panelTrangChu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTrangChu.Location = new System.Drawing.Point(0, 0);
            this.panelTrangChu.Name = "panelTrangChu";
            this.panelTrangChu.Size = new System.Drawing.Size(1236, 609);
            this.panelTrangChu.TabIndex = 0;
            // 
            // FormTrangChu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1236, 609);
            this.Controls.Add(this.panelTrangChu);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormTrangChu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormTrangChu";
            this.Load += new System.EventHandler(this.FormTrangChu_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTrangChu;
        private System.Windows.Forms.Timer timer1;
    }
}