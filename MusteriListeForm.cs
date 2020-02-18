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

namespace Musteri_Bilgi_Otomasyonu
{
    public partial class MusteriListeForm : Form
    {
        public MusteriListeForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        public MusteriGuncelleForm guncelle;
        private void dgvMusteriListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }
        
        private void musteriListele()
        {
            daset = new DataSet();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select musterino as 'Müşteri No', Ad,Soyad," +
                "Telefonno as 'Telefon No', uyelikturu as 'Üyelik Türü',Total$ as 'Total($)', " +
                "Kupon,Eldekalan as 'Elde Kalan' from musteri", baglanti);
            adtr.Fill(daset, "musteri");
            dgvMusteriListe.DataSource = daset.Tables["musteri"];
            

            baglanti.Close();

        }
        private void MusteriListeForm_Load(object sender, EventArgs e)
        {
            musteriListele();
        }
        public bool durum;
        DataSet daset = new DataSet();

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı silmek mi istiyorsunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from musteri where musterino=@musterino", baglanti);
                komut.Parameters.AddWithValue("@musterino", dgvMusteriListe.CurrentRow.Cells["Müşteri No"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi gerçekleşti");
                daset.Tables["musteri"].Clear();
                musteriListele();
                foreach(Control item in Controls)
                {
                    if(item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
        }

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            guncelle = new MusteriGuncelleForm();
            guncelle.txtMusteriNo.Text = dgvMusteriListe.CurrentRow.Cells["Müşteri No"].Value.ToString();
            guncelle.ShowDialog();
            musteriListele();
        }
        public Satış satis;
        private void satışYapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            satis = new Satış();
            satis.txtMusteriNo.Text = dgvMusteriListe.CurrentRow.Cells["Müşteri No"].Value.ToString();
            satis.txtad.Text = dgvMusteriListe.CurrentRow.Cells["ad"].Value.ToString();
            satis.txtsoyad.Text = dgvMusteriListe.CurrentRow.Cells["soyad"].Value.ToString();
            satis.txttotal.Text = dgvMusteriListe.CurrentRow.Cells["total($)"].Value.ToString();
            satis.txtKupon.Text = dgvMusteriListe.CurrentRow.Cells["kupon"].Value.ToString();
            satis.txtelde.Text = dgvMusteriListe.CurrentRow.Cells["elde kalan"].Value.ToString();
            satis.ShowDialog();
            musteriListele();
        }
        public musterihareket hareket;
        private void hareketlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            hareket = new musterihareket();
            hareket.txtMusteriNo.Text = dgvMusteriListe.CurrentRow.Cells["Müşteri No"].Value.ToString();
            hareket.ShowDialog();
            
        }
        private void txtUyelikTuru_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["musteri"].Clear();
            baglanti.Open();
            if (txtAd.Text == "")
            {
                SqlDataAdapter adtr = new SqlDataAdapter("select musterino as 'Müşteri No', Ad,Soyad, telefonno as 'Telefon No', uyelikturu as 'Üyelik Türü', total$ as 'Total($)', Kupon, eldekalan as 'Elde Kalan' from musteri where uyelikturu like '%" + txtUyelikTuru.Text + "%'", baglanti);
                adtr.Fill(daset, "musteri");
            }
            if (txtAd.Text != "")
            {
                SqlDataAdapter adtr2 = new SqlDataAdapter("select musterino as 'Müşteri No', Ad,Soyad, telefonno as 'Telefon No', uyelikturu as 'Üyelik Türü', total$ as 'Total($)', Kupon, eldekalan as 'Elde Kalan' from musteri where ad like '%" + txtAd.Text + "%' and uyelikturu like '%" + txtUyelikTuru.Text + "%'  ", baglanti);
                adtr2.Fill(daset, "musteri");

            }
            dgvMusteriListe.DataSource = daset.Tables["musteri"];
            baglanti.Close();
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            daset.Tables["musteri"].Clear();
            baglanti.Open();
            if (txtUyelikTuru.Text == "")
            {
                SqlDataAdapter adtr = new SqlDataAdapter("select musterino as 'Müşteri No', Ad,Soyad, telefonno as 'Telefon No', uyelikturu as 'Üyelik Türü', total$ as 'Total($)', Kupon, eldekalan as 'Elde Kalan' from musteri where ad like '%" + txtAd.Text + "%'", baglanti);
                adtr.Fill(daset, "musteri");
            }
            if (txtUyelikTuru.Text != "")
            {
                SqlDataAdapter adtr2 = new SqlDataAdapter("select musterino as 'Müşteri No', Ad,Soyad, telefonno as 'Telefon No', uyelikturu as 'Üyelik Türü', total$ as 'Total($)', Kupon, eldekalan as 'Elde Kalan' from musteri where uyelikturu like '%" + txtUyelikTuru.Text + "%' and ad like '%" + txtAd.Text + "%'  ", baglanti);
                adtr2.Fill(daset, "musteri");

            }
            dgvMusteriListe.DataSource = daset.Tables["musteri"];
            baglanti.Close();
        }
    }

        
}
