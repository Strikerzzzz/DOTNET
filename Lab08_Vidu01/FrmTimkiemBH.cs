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
    public partial class FrmTimkiemBH : Form
    {
        public FrmTimkiemBH()
        {
            InitializeComponent();
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            string sql = "bh.SoHieuHD, mh.TenMatHang, mh.DVT, bh.NgayMuaBan, ctbh.SoLuong, ctbh.DonGia, (ctbh.Soluong * ctbh.DonGia) as ThanhTien  FROM tblBanHang bh JOIN tblChiTietBanHang ctbh ON bh.SoHieuHD = ctbh.SoHieuHD JOIN tblMatHang mh ON ctbh.MaMH = mh.MaMH WHERE bh.MaKH ";
            DataTable dt = Database.TimKiemBanHang(txtMaKH.Text, sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                dt.Columns.Add("STT", typeof(int));

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["STT"] = i + 1; 
                }
                dataGridView.DataSource = dt;
            }
            else
            {
                MessageBox.Show("Không tìm thấy thông tin ");
            }
        }

        private void txtMaKH_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DataTable dt = Database.TimKiemKhachHang(txtMaKH.Text, "MaKH", "HoTen");
                if (dt != null && dt.Rows.Count > 0 )
                {
                    txtTenKH.Text = dt.Rows[0]["HoTen"].ToString();
                }
                else
                {
                    txtTenKH.Text = "Không tìm thấy khách hàng.";
                }
            }
        }

        private void btnKetThuc_Click(object sender, EventArgs e)
        {
            if(DialogResult.Yes == MessageBox.Show("Bạn có muốn kết thúc không ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }
    }
}
