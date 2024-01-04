using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace test
{
    internal class DBConnection
    {
        string StrCon = @"Data Source=PHAM_TUAN_ANH\SQLEXPRESS;Initial Catalog=QLSP;Integrated Security=True";
        SqlConnection conn = null;
        public void moketnoi()
        {
            if (conn == null)
            {
                conn = new SqlConnection(StrCon);
            }
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public void hienthidanhsach(string query,ListView lsvdanhsach)
        {
            moketnoi();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = conn;
            SqlDataReader reader = cmd.ExecuteReader();
            lsvdanhsach.Items.Clear();
            while (reader.Read())
            {
                string masp = reader.GetString(0);
                string tensp = reader.GetString(1);
                string kieudang = reader.GetString(2);
                string tinhtrang = reader.GetString(3);
                string slnhap = reader.GetInt32(4).ToString();
                string gianhap = reader.GetFloat(5).ToString();
                string hangsanxuat = reader.GetString(6);

                ListViewItem lvi = new ListViewItem(masp);
                lvi.SubItems.Add(tensp);
                lvi.SubItems.Add(kieudang);
                lvi.SubItems.Add(tinhtrang);
                lvi.SubItems.Add(slnhap);
                lvi.SubItems.Add(gianhap);
                lvi.SubItems.Add(hangsanxuat);

                lsvdanhsach.Items.Add(lvi);

            }
            reader.Close();

        }

        public int Command(string query)
        {
            moketnoi();

            using(SqlCommand cmd = new SqlCommand(query,conn))
            {
                return cmd.ExecuteNonQuery();
            }
        }
    }
}
