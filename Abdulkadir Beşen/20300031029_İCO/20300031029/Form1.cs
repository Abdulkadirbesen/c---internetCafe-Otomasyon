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
    public partial class Form1 : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-KPCTGUG;Initial Catalog=İnternetCafeOtomasyon;Integrated Security=True;Pooling=False");
        public Form1()
        {
            InitializeComponent();
        }
        Button btn;

        private void SecileneGore(object sender, MouseEventArgs e)
        {
            btn = sender as Button;
            if (btn.BackColor == Color.LightGreen)
            {
                süreliMasaAçmaİsteğiGönderToolStripMenuItem.Visible = false;
                süresizMasaAçmaİsteğiGönderToolStripMenuItem.Visible = false;
            }
            if (btn.BackColor == Color.OrangeRed)
            {
                süreliMasaAçmaİsteğiGönderToolStripMenuItem.Visible = true;
                süresizMasaAçmaİsteğiGönderToolStripMenuItem.Visible = true;
            }

        }
        RadioButton radio;
        private void RadioButtonSeciliyeGore(object sender, EventArgs e)
        {
            radio = sender as RadioButton;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            this.tBLSaatUcretiTableAdapter.Fill(this.İnternetCafeOtomasyonDataSet.TBLSaatUcreti);
            radioButtonSuresiz.Checked = true;
            Yenile();
            comboSaatUcreti.Text = "";

        }
        public void Yenile()
        {
            Veritabanı.SepetListele(dataGridView1);
            Veritabanı.ListviewdeKayitlariGoster(listView1);
            foreach (Control item in Controls)
            {
                if (item is Button)
                {
                    if (item.Name != btnMasaAc.Name)
                    {
                        Veritabanı.baglanti.Open();
                        SqlCommand komut = new SqlCommand("select *from tblmasalar", Veritabanı.baglanti);
                        SqlDataReader dr = komut.ExecuteReader();
                        while (dr.Read())
                        {
                            if (dr["durumu"].ToString() == "BOŞ" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.OrangeRed;
                            }
                            if (dr["durumu"].ToString() == "DOLU" && dr["Masalar"].ToString() == item.Text)
                            {
                                item.BackColor = Color.LightGreen;
                            }
                        }
                        Veritabanı.baglanti.Close();
                    }
                }
            }
            Veritabanı.ComboyaBosMasaGetir(comboBosMasalar);
        }

        private void btnMasaAc_Click(object sender, EventArgs e)
        {
            if (Kullanici.KullaniciID == 1)
            {
                string sqlsorgu = "insert into tblsepet(masaID,masa,acilisturu,baslangıc,tarih) values ('" + comboBosMasalar.Text.Substring(5) + "','" + comboBosMasalar.Text + "','" + radio.Text + "','" + DateTime.Parse(DateTime.Now.ToString()) + "','" +DateTime.Now.ToString()+"')";
                SqlCommand komut = new SqlCommand();
                Veritabanı.ESG(komut, sqlsorgu);
                MessageBox.Show(comboBosMasalar.Text.Substring(5) + "Nolu Masa Açıldı", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Yenile();
                radioButtonSuresiz.Checked = true;

            }
            else
            {
                MessageBox.Show("Böyle Bir Yetkiniz Yok", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Environment.Exit(0);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataGridView1.Columns["Hesapla"].Index)
            {
                if (comboSaatUcreti.Text!= " ")
                {
                    DateTime BitisTarihi = DateTime.Now;
                    DateTime BaslangicTarihi = DateTime.Parse(dataGridView1.CurrentRow.Cells["Baslangıc"].Value.ToString());
                    TimeSpan saatfarki = BitisTarihi - BaslangicTarihi;
                    double saatfarki2 = saatfarki.TotalHours;
                    dataGridView1.CurrentRow.Cells["Süre"].Value = saatfarki2.ToString("0.00");
                    double toplamtutar = saatfarki2 * double.Parse(comboSaatUcreti.Text);
                    dataGridView1.CurrentRow.Cells["_Tutar"].Value = toplamtutar.ToString("0.00");
                    dataGridView1.CurrentRow.Cells["BitisSaati"].Value = BitisTarihi;
                }
                if (comboSaatUcreti.Text=="")
                {
                    MessageBox.Show("Önce Saat Ücreti Belirtilmeli","Uyarı",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }
            }
            if (e.ColumnIndex == dataGridView1.Columns["Masa_Kapat"].Index)
            {
                if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value!=null)
                {

                    FrmMasaKapat frm = new FrmMasaKapat();
                    frm.txtID.Text = dataGridView1.CurrentRow.Cells["ID"].Value.ToString();
                    frm.txtMasaID.Text = dataGridView1.CurrentRow.Cells["Masa_ID"].Value.ToString();
                    frm.txtMasa.Text = dataGridView1.CurrentRow.Cells["_Masa"].Value.ToString();
                    frm.txtAt.Text = dataGridView1.CurrentRow.Cells["Acılıs_Türü"].Value.ToString();
                    frm.txtBasSa.Text = dataGridView1.CurrentRow.Cells["Baslangıc"].Value.ToString();
                    frm.txtBitSa.Text = dataGridView1.CurrentRow.Cells["BitisSaati"].Value.ToString();
                    frm.txtSaÜc.Text = comboSaatUcreti.Text;
                    frm.txtSü.Text = dataGridView1.CurrentRow.Cells["Süre"].Value.ToString();
                    frm.txtTut.Text = dataGridView1.CurrentRow.Cells["_Tutar"].Value.ToString();
                    frm.txtTar.Text = dataGridView1.CurrentRow.Cells["_Tarih"].Value.ToString();

                    frm.ShowDialog();
                }
                else if (dataGridView1.CurrentRow.Cells["BitisSaati"].Value == null)
                {
                   MessageBox.Show("Önce Hesaplama Yapılmalı","Uyarı!",MessageBoxButtons.OK,MessageBoxIcon.Warning);

                }

               
            }
        }
    }
}

