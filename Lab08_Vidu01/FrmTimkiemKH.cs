using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace Lab08_Vidu01
{
    public partial class FrmTimkiemKH : Form
    {
        public FrmTimkiemKH()
        {
            InitializeComponent();
        }

        private void FrmTimkiemKH_Load(object sender, EventArgs e)
        {

        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có muốn kết thúc không ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (rdoMaKH.Checked)
            {
                dataGridViewKhachHang.DataSource = Database.TimKiemKhachHang(txtNoiDung.Text, "MaKH", "*");
            }
            else if(rdoTenKH.Checked)
            {
                dataGridViewKhachHang.DataSource = Database.TimKiemKhachHang(txtNoiDung.Text, "HoTen", "*");
            }
            else if (rdoSDT.Checked)
            {
                dataGridViewKhachHang.DataSource = Database.TimKiemKhachHang(txtNoiDung.Text, "DienThoai", "*");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tiêu chí để tìm!", "Lỗi",MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                
        }
    }
