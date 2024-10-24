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
    public partial class FrmKhachHang : Form
    {
        public FrmKhachHang()
        {
            InitializeComponent();
        }
        void LoadDatabase()
        {
            this.dataGridViewKhachHang.DataSource = Database.GetDataTable("SELECT * FROM tblKhachHang");
        }
        private void FrmKhachHang_Load(object sender, EventArgs e)
        {
            LoadDatabase();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (Database.ThemKhachHang(txtHoTen.Text, cbGioiTinh.Text, txtDiaChi.Text, txtDienThoai.Text))
            {
                MessageBox.Show("Thêm thành công");
            }
            else
            {
                MessageBox.Show("Thêm thất bại");
            }
            LoadDatabase();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có muốn kết thúc không ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            Database.SuaKhachHang( txtHoTen.Text, cbGioiTinh.Text, txtDiaChi.Text, txtDienThoai.Text, txtMaKH.Text);
            LoadDatabase();
        }

        private void dataGridViewKhachHang_Click(object sender, EventArgs e)
        {
            HienThi();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            Database.XoaKhachHang(txtMaKH.Text);
            LoadDatabase();
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dataGridViewKhachHang.DataSource = Database.TimKiemKhachHang(txtMaKH.Text,"MaKH","*");

            
        }
        void HienThi()
        {
            txtMaKH.Text = dataGridViewKhachHang.CurrentRow.Cells[0].Value.ToString();
            txtHoTen.Text = dataGridViewKhachHang.CurrentRow.Cells[1].Value.ToString();
            cbGioiTinh.SelectedItem = dataGridViewKhachHang.CurrentRow.Cells[2].Value.ToString();
            txtDiaChi.Text = dataGridViewKhachHang.CurrentRow.Cells[3].Value.ToString();
            txtDienThoai.Text = dataGridViewKhachHang.CurrentRow.Cells[4].Value.ToString();
        }
    }
}
