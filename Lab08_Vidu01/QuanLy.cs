using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab08_Vidu01
{
    public partial class QuanLy : Form
    {
        public QuanLy()
        {
            InitializeComponent();
            thôngTinKháchHàngToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
            thôngTinMặtHàngToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
            chiTiếtBánHàngToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
            tìmKiếmKháchHàngToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
            tìmKiếmMặtHàngToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
            tìmKiếmBánHàngToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
            thoátToolStripMenuItem.MouseEnter += new EventHandler(Menu_MouseEnter);
        }
        private void Menu_MouseEnter(object sender, EventArgs e)
        {
            ToolStripMenuItem menuItem = sender as ToolStripMenuItem;

            if (menuItem != null)
            {
                // Hiển thị gợi ý tùy thuộc vào mục Menu
                switch (menuItem.Name)
                {
                    case "thôngTinKháchHàngToolStripMenuItem":
                        statusLabel.Text = "Xem thông tin khách hàng";
                        break;
                    case "thôngTinMặtHàngToolStripMenuItem":
                        statusLabel.Text = "Xem thông tin mặt hàng";
                        break;
                    case "chiTiếtBánHàngToolStripMenuItem":
                        statusLabel.Text = "Xem chi tiết bán hàng";
                        break;
                    case "tìmKiếmKháchHàngToolStripMenuItem":
                        statusLabel.Text = "Tìm kiếm khách hàng";
                        break;
                    case "tìmKiếmMặtHàngToolStripMenuItem":
                        statusLabel.Text = "Tìm kiếm mặt hàng";
                        break;
                    case "tìmKiếmBánHàngToolStripMenuItem":
                        statusLabel.Text = "Tìm kiếm thông tin bán hàng";
                        break;
                    case "thoátToolStripMenuItem":
                        statusLabel.Text = "Thoát chương trình";
                        break;
                }
            }
        }

        private void QuanLy_Load(object sender, EventArgs e)
        {

        }

        private void thôngTinKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKhachHang frm = new FrmKhachHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thôngTinMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMatHang frm = new FrmMatHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void chiTiếtBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBanHang frm = new FrmBanHang();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tìmKiếmKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTimkiemKH frm = new FrmTimkiemKH();
            frm.MdiParent = this;
            frm.Show();
        }

        private void thoátToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void tìmKiếmMặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTimkiemMH frm = new FrmTimkiemMH();
            frm.MdiParent = this;
            frm.Show();
        }

        private void tìmKiếmBánHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTimkiemBH frm = new FrmTimkiemBH();
            frm.MdiParent = this;
            frm.Show();
        }
    }
}
