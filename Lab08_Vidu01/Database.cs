using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab08_Vidu01
{
    internal class Database
    {
        static SqlConnection conn;
        public static void OpenConnect()
        {
            conn = new SqlConnection($@"Data Source=(LocalDB)\MSSQLLocalDB;Initial Catalog=Lab08;Integrated Security=True");
            conn.Open();
        }
        public static void CloseConnect()
        {
            conn.Close();
        }
        public SqlConnection GetConnection()
        {
            return conn;
        }
        public static bool ThemKhachHang(string hoTen, string gioiTinh, string diaChi, string dienThoai)
        {
            try
            {
                OpenConnect();
                string sql = "INSERT INTO tblKhachHang (HoTen, GioiTinh, DiaChi, DienThoai) VALUES (@hoTen, @gioiTinh, @diaChi, @dienThoai)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@hoTen", hoTen);
                    cmd.Parameters.AddWithValue("@gioiTinh", gioiTinh);
                    cmd.Parameters.AddWithValue("@diaChi", diaChi);
                    cmd.Parameters.AddWithValue("@dienThoai", dienThoai);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnect();

            }
        }
        public static bool ThemMatHang(string TenMh, string DVT)
        {
            try
            {
                OpenConnect();
                string sql = "INSERT INTO tblMatHang (TenMatHang, DVT) VALUES (@TenMh, @DVT)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("@TenMh", TenMh);
                    cmd.Parameters.AddWithValue("@DVT", DVT);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnect();

            }
        }
        public static bool ThemBanHang(string MaKH, DateTime NgayMua, string MaMH, string SoLuong, string DonGia)
        {
            try
            {
                OpenConnect();
                string sql = "INSERT INTO tblBanHang (MaKH, NgayMuaBan) VALUES (@MaKH, @NgayMua)"
                + "INSERT INTO tblChiTietBanHang (MaMH, SoLuong, DonGia) VALUES (@MaMH, @SoLuong, @DonGia)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("MaKH", MaKH);
                    cmd.Parameters.AddWithValue("NgayMua", NgayMua);
                    cmd.Parameters.AddWithValue("MaMH", MaMH);
                    cmd.Parameters.AddWithValue("SoLuong", SoLuong);
                    cmd.Parameters.AddWithValue("DonGia", DonGia);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return (rowsAffected > 0);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
                return false;
            }
            finally
            {
                CloseConnect();

            }
        }
        public static DataTable GetDataTable(string sql)
        {
            DataTable dataTable = new DataTable();
            try
            {
                OpenConnect();
                using (SqlCommand command = new SqlCommand(sql, conn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        dataTable.Load(reader);
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
            return dataTable;
        }
        public static void SuaMatHang(string TenMH, string DVT, string maMH)
        {
            try
            {
                OpenConnect();
                string sqlUpdate = "UPDATE tblMatHang SET TenMatHang = @TenMatHang, DVT = @DVT WHERE MaMH = @maMH";
                using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("TenMatHang", TenMH);
                    cmd.Parameters.AddWithValue("DVT", DVT);
                    cmd.Parameters.AddWithValue("maMH", maMH);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
        }

        public static void SuaBanHang(string MaKH, DateTime NgayMua, string MaMH, string SoLuong, string DonGia, string SoHieuHD)
        {
            try
            {
                OpenConnect();
                string sqlUpdate = "UPDATE tblChiTietBanHang SET MaMH = @MaMH, SoLuong = @SoLuong, DonGia = @DonGia WHERE SoHieuHD = @SoHieuHD \n"
                                 + "UPDATE tblBanHang SET MaKH = @MaKH, NgayMuaBan = @NgayMua WHERE SoHieuHD = @SoHieuHD";
                using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("MaKH", MaKH);
                    cmd.Parameters.AddWithValue("NgayMua", NgayMua);
                    cmd.Parameters.AddWithValue("MaMH", MaMH);
                    cmd.Parameters.AddWithValue("SoLuong", SoLuong);
                    cmd.Parameters.AddWithValue("DonGia", DonGia);
                    cmd.Parameters.AddWithValue("SoHieuHD", SoHieuHD);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
        }

        public static void SuaKhachHang(string hoTen, string gioiTinh, string diaChi, string dienThoai, string maKH)
        {
            try
            {
                OpenConnect();
                string sqlUpdate = "UPDATE tblKhachHang SET HoTen = @ht, GioiTinh = @gt, DiaChi = @dc, DienThoai = @dt WHERE MaKH = @makh";
                using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
                {
                    cmd.Parameters.AddWithValue("ht", hoTen);
                    cmd.Parameters.AddWithValue("gt", gioiTinh);
                    cmd.Parameters.AddWithValue("dc", diaChi);
                    cmd.Parameters.AddWithValue("dt", dienThoai);
                    cmd.Parameters.AddWithValue("makh", maKH);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
        }

        public static void XoaKhachHang(string maKH)
        {
            try
            {
                OpenConnect();
                string sql = "DELETE FROM tblKhachHang WHERE MaKH = @ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("ma", maKH);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
        }

        public static void XoaMatHang(string maMH)
        {
            try
            {
                OpenConnect();
                string sql = "DELETE FROM tblMatHang WHERE MaMH = @ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("ma", maMH);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
        }

        public static void XoaBanHang(string maKH)
        {
            try
            {
                OpenConnect();
                string sql = "DELETE FROM tblChiTietBanHang WHERE SoHieuHD = @ma\n" +
                             "DELETE FROM tblBanHang WHERE SoHieuHD = @ma";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.Parameters.AddWithValue("ma", maKH);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Có lỗi xảy ra: " + ex.Message);
            }
            finally
            {
                CloseConnect();
            }
        }


        //Phương thức tìm kiếm thông tin khách hàng
        public static DataTable TimKiemKhachHang(string value,string Loai,string kieu)
        {
            OpenConnect();
            DataTable bang = new DataTable();
            string sqlTimkiem = $"SELECT {kieu} FROM tblKhachHang WHERE {Loai} = @value";
            SqlCommand cmd = new SqlCommand(sqlTimkiem, conn);
            cmd.Parameters.AddWithValue("value", value);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) { bang.Load(reader); }
            else bang = null;
            CloseConnect();
            return bang;
        }
        public static DataTable TimKiemMatHang(string value, string Loai)
        {
            OpenConnect();
            DataTable bang = new DataTable();
            string sqlTimkiem = $"SELECT * FROM tblMatHang WHERE {Loai} = @value";
            SqlCommand cmd = new SqlCommand(sqlTimkiem, conn);
            cmd.Parameters.AddWithValue("value", value);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) { bang.Load(reader); }
            else bang = null;
            CloseConnect();
            return bang;
        }
        public static DataTable TimKiemBanHang(string value, string Loai)
        {
            OpenConnect();
            DataTable bang = new DataTable();
            string sqlTimkiem = $"SELECT {Loai} = @value";
            SqlCommand cmd = new SqlCommand(sqlTimkiem, conn);
            cmd.Parameters.AddWithValue("value", value);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows) { bang.Load(reader); }
            else bang = null;
            CloseConnect();
            return bang;
        }

        public static Boolean KiemTraMaKhachHang(int maKH)
        {
            Boolean kiemTra = false;
            OpenConnect();
            string sql = "SELECT * FROM tblKhachHang WHERE MaKH = @maKh";
            SqlCommand cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("maKh", maKH);
            SqlDataReader reader = cmd.ExecuteReader();
            if (reader.HasRows)
            {
                kiemTra = true;
            }
            CloseConnect();
            return kiemTra;
        }
    }
}