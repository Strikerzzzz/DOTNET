using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Lab08_Vidu01
{
    public partial class FrmBanHang : Form
    {
        Dictionary<string, string> dictKH = new Dictionary<string, string>();
        Dictionary<string, string> dictMH = new Dictionary<string, string>();
        public FrmBanHang()
        {
            InitializeComponent();
        }

        private void FrmBanHang_Load(object sender, EventArgs e)
        {
            try
            {
                LoadDatabase();

                DataTable dt = Database.GetDataTable("SELECT MaKH, HoTen FROM tblKhachHang ");
                if (dt != null && dt.Rows.Count > 0)
                {
                    LoadListDB(dt, dictKH);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                dt = Database.GetDataTable("SELECT MaMH, TenMatHang FROM tblMatHang ");
                if (dt != null && dt.Rows.Count > 0)
                {
                    LoadListDB(dt, dictMH);
                }
                else
                {
                    MessageBox.Show("Không có dữ liệu mặt hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                cbTenMH.DataSource = new BindingSource(dictMH, null);
                cbTenMH.DisplayMember = "Value";
                cbTenMH.ValueMember = "Key";

                cbTenKH.DataSource = new BindingSource(dictKH, null);
                cbTenKH.DisplayMember = "Value";
                cbTenKH.ValueMember = "Key";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            if (DialogResult.Yes == MessageBox.Show("Bạn có muốn kết thúc không ?", "Thoát", MessageBoxButtons.YesNo, MessageBoxIcon.Question))
            {
                this.Close();
            }
        }
        void LoadListDB(DataTable dt, Dictionary<string, string> dict)
        {
            try
            {
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string ma = row[0].ToString();
                        string ten = row[1].ToString();

                        if (!dict.ContainsKey(ma))
                        {
                            dict.Add(ma, ten);
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Dữ liệu không tồn tại hoặc rỗng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        void HienThi()
        {
            try
            {
                if (dataGridViewChiTiet.CurrentRow != null)
                {
                    txtSoHieuHD.Text = dataGridViewChiTiet.CurrentRow.Cells[0].Value?.ToString();
                    txtSoLuong.Text = dataGridViewChiTiet.CurrentRow.Cells[4].Value?.ToString();
                    txtDonGia.Text = dataGridViewChiTiet.CurrentRow.Cells[5].Value?.ToString();

                    string tenMatHang = dataGridViewChiTiet.CurrentRow.Cells[3].Value?.ToString();
                    string maMatHang = dictMH.FirstOrDefault(x => x.Value == tenMatHang).Key;

                    if (!string.IsNullOrEmpty(maMatHang))
                    {
                        cbTenMH.SelectedValue = maMatHang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã mặt hàng cho tên: " + tenMatHang);
                    }

                    string tenKhachHang = dataGridViewChiTiet.CurrentRow.Cells[1].Value?.ToString();
                    string maKhachHang = dictKH.FirstOrDefault(x => x.Value == tenKhachHang).Key;

                    if (!string.IsNullOrEmpty(maKhachHang))
                    {
                        cbTenKH.SelectedValue = maKhachHang;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy mã khách hàng cho tên: " + tenKhachHang);
                    }

                    dateTimePickerNgayMua.Value = DateTime.Parse(dataGridViewChiTiet.CurrentRow.Cells[2].Value?.ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi hiển thị dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        void LoadDatabase()
        {
            this.dataGridViewChiTiet.DataSource = Database.GetDataTable("SELECT bh.SoHieuHD, kh.HoTen, bh.NgayMuaBan, mh.TenMatHang ,ct.Soluong,ct.DonGia,(ct.Soluong * ct.DonGia) as ThanhTien FROM tblBanHang bh LEFT JOIN tblChiTietBanHang ct ON bh.SoHieuHD = ct.SoHieuHD LEFT JOIN tblKhachHang kh ON kh.MaKH = bh.MaKH LEFT JOIN tblMatHang mh ON ct.MaMH = mh.MaMH ");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtSoHieuHD.Text))
                {
                    MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.");
                    return;
                }

                // Hộp thoại xác nhận trước khi xóa
                DialogResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn này?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa nếu người dùng chọn Yes
                    Database.XoaBanHang(txtSoHieuHD.Text);
                    MessageBox.Show("Xóa thành công");
                    LoadDatabase();
                }
                else
                {
                    // Hủy bỏ thao tác xóa nếu người dùng chọn No
                    MessageBox.Show("Đã hủy xóa.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
            }
        }



        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbTenKH.SelectedValue?.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(cbTenMH.SelectedValue?.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn mặt hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtDonGia.Text) || !decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đơn giá hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện sửa
                Database.SuaBanHang(cbTenKH.SelectedValue.ToString(), dateTimePickerNgayMua.Value, cbTenMH.SelectedValue.ToString(), txtSoLuong.Text, txtDonGia.Text, txtSoHieuHD.Text);
                MessageBox.Show("Sửa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadDatabase();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(cbTenKH.SelectedValue?.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn khách hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(cbTenMH.SelectedValue?.ToString()))
                {
                    MessageBox.Show("Vui lòng chọn mặt hàng.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtSoLuong.Text) || !int.TryParse(txtSoLuong.Text, out int soLuong) || soLuong <= 0)
                {
                    MessageBox.Show("Vui lòng nhập số lượng hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrEmpty(txtDonGia.Text) || !decimal.TryParse(txtDonGia.Text, out decimal donGia) || donGia <= 0)
                {
                    MessageBox.Show("Vui lòng nhập đơn giá hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Thực hiện thêm hóa đơn
                if (Database.ThemBanHang(cbTenKH.SelectedValue.ToString(), dateTimePickerNgayMua.Value, cbTenMH.SelectedValue.ToString(), txtSoLuong.Text, txtDonGia.Text))
                {
                    MessageBox.Show("Thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadDatabase();
                }
                else
                {
                    MessageBox.Show("Thêm thất bại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void dataGridViewChiTiet_Click(object sender, EventArgs e)
        {
            HienThi();
        }
    }
}
