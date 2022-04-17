using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _20300031029
{
    internal class Kullanici
    {
        public static int KullaniciID = 0;
        public static bool durum=false;
        public static SqlDataReader KullaniciGirisi(TextBox KullaniciAdi,TextBox Sifre)
        {
            Veritabanı.baglanti.Open();
            SqlCommand cmd = new SqlCommand("select *from tblkullanıcı where KullaniciAdi='"+KullaniciAdi.Text+"'and Sifre='"+Sifre.Text+"'",Veritabanı.baglanti);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                durum = true;
                KullaniciID=int.Parse(dr["KullaniciID"].ToString());
            }
            else
            {
                durum=false;
            }
            Veritabanı.baglanti.Close();
            return dr;

        }
    }
}
