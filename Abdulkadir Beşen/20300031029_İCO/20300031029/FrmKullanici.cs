using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace _20300031029
{
    public partial class FrmKullanici : Form
    {
        public FrmKullanici()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            Kullanici.KullaniciGirisi(txtKullaniciAdi, txtSifre);
            if (Kullanici.durum==true)
            {
                Form1 frm = new Form1();
                frm.Show();
                this.Hide();
            }
            else if(Kullanici.durum==false)
            {
                MessageBox.Show("Kullanıcıadı veya şifre Hatalı Kontrol ediniz..!","Uyarı",
                    MessageBoxButtons.OK,MessageBoxIcon.Warning);

            }
        }
    }
}
