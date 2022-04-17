using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _20300031029
{
    public partial class FrmMasaKapat : Form
    {
        public FrmMasaKapat()
        {
            InitializeComponent();
        }

        private void btnMasaKpt_Click(object sender, EventArgs e)
        {
            string sqlsorgu = "update tblmasalar set durumu = 'BOŞ' where masaID = '"+txtMasaID.Text+"'";
            SqlCommand komut=new SqlCommand();
            Veritabanı.ESG(komut,sqlsorgu); 
            string sqlsorgu2="delete from tblsepet where SepetID='"+txtID.Text+"'";
            SqlCommand komut2 = new SqlCommand();
            Veritabanı.ESG(komut2,sqlsorgu2);
            this.Close();
            Form1 frm1 = (Form1)Application.OpenForms["Form1"];
            frm1.Yenile();

        }

        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
