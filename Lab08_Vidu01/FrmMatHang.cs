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
    public partial class FrmMatHang : Form
    {
        public FrmMatHang()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có muốn kết thúc không ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }
        void LoadDatabase()
        {
            this.dataGridViewMatHang.DataSource = Database.GetDataTable("SELECT * FROM tblMatHang");
        }
        private void FrmMatHang_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Database.ThemMatHang(txtTenMH.Text, txtDonViTinh.Text))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            LoadDatabase();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Database.SuaMatHang(txtTenMH.Text,txtDonViTinh.Text,txtMaMH.Text);
            LoadDatabase();
        }
        void HienThi()
        {
            txtMaMH.Text = dataGridViewMatHang.CurrentRow.Cells[0].Value.ToString();
            txtTenMH.Text = dataGridViewMatHang.CurrentRow.Cells[1].Value.ToString();
            txtDonViTinh.Text = dataGridViewMatHang.CurrentRow.Cells[2].Value.ToString();
        }

        private void dataGridViewMatHang_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Database.XoaMatHang(txtMaMH.Text);
            LoadDatabase();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dataGridViewMatHang.DataSource = Database.TimKiemMatHang(txtMaMH.Text,"MaMH");
        }
    }
}
