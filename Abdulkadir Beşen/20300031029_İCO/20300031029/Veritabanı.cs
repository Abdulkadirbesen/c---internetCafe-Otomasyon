using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Forms;

namespace _20300031029
{
    internal class Veritabanı
    {
      public static SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KPCTGUG;Initial Catalog=İnternetCafeOtomasyon;Integrated Security=True;Pooling=False");
      public static DataTable SepetListele(DataGridView gridView)
        {
            SqlDataAdapter adtr = new SqlDataAdapter("select *from tblsepet", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            gridView.DataSource = tbl;
            baglanti.Close();
            return tbl;
        }
        public static DataTable ComboyaBosMasaGetir(ComboBox combo)
        {

            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select * from tblmasalar where durumu='BOŞ' ", baglanti);
            DataTable tbl = new DataTable();
            adtr.Fill(tbl);
            combo.DataSource = tbl;
            combo.DisplayMember = "Masalar";
            combo.ValueMember = "MasaID";
            baglanti.Close();
            return tbl;
        }   
        public static void ESG(SqlCommand command , string sorgu)
        {
            baglanti.Open();
            command.Connection = baglanti;
            command.CommandText = sorgu;
            command.ExecuteNonQuery();
            baglanti.Close();

        }
        public static SqlDataReader ListviewdeKayitlariGoster(ListView list)
        {
            list.Items.Clear();

            baglanti.Open();
            SqlCommand cmd = new SqlCommand("select *from tblhareketler",baglanti);
            SqlDataReader dr=cmd.ExecuteReader();
            while (dr.Read())
            {
                ListViewItem ekle = new ListViewItem();
                ekle.Text = dr[0].ToString();
                ekle.SubItems.Add(dr[1].ToString());
                ekle.SubItems.Add(dr[2].ToString());
                ekle.SubItems.Add(dr[3].ToString());
                ekle.SubItems.Add(dr[4].ToString());
                ekle.SubItems.Add(dr[5].ToString());
                ekle.SubItems.Add(dr[6].ToString());
                list.Items.Add(ekle);
            }
            baglanti.Close();
            return dr;
        }
    }
}
