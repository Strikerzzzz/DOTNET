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
    public partial class FrmTimkiemMH : Form
    {
        public FrmTimkiemMH()
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

        private void btnTim_Click(object sender, EventArgs e)
        {
            if (rdoMaMH.Checked)
            {
                dataGridViewMatHang.DataSource = Database.TimKiemMatHang(txtNoiDung.Text, "MaMH");
            }
            else if (rdoTenMH.Checked)
            {
                dataGridViewMatHang.DataSource = Database.TimKiemMatHang(txtNoiDung.Text, "TenMatHang");
            }
            else
            {
                MessageBox.Show("Vui lòng chọn một tiêu chí để tìm!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
