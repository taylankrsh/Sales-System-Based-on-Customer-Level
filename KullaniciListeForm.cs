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
    public partial class KullaniciListeForm : Form
    {
        public KullaniciListeForm()
        {
            InitializeComponent();
        }


        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from  kullanici where kad ='" + txtAd.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                cmbYetki.Text = read["kyetki"].ToString();
            }
            if (txtkullanici.Text == txtAd.Text)
                cmbYetki.Enabled = false;
            else { cmbYetki.Enabled = true; }
            baglanti.Close();
        }

        private void btnİptal_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataSet daset = new DataSet();

        private void txtAdAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            daset.Tables["kullanici"].Clear();
            if (txtYetkiAra.Text == "")
            {
                SqlDataAdapter adtr = new SqlDataAdapter("select kad as 'Kullanıcı Adı',kyetki as 'Yetkisi' from kullanici where kad like '%" + txtAdAra.Text + "%'", baglanti);
                adtr.Fill(daset, "kullanici");
            }
            if (txtYetkiAra.Text != "")
            {
                SqlDataAdapter adtr = new SqlDataAdapter("select kad as 'Kullanıcı Adı',kyetki as 'Yetkisi' from kullanici where kad like '%" + txtAdAra.Text + "%' and kyetki like '%" + txtYetkiAra.Text + "%'   ", baglanti);
                adtr.Fill(daset, "kullanici");
            }
            dataGridView1.DataSource = daset.Tables["kullanici"];
            baglanti.Close();
        }
        private void kullanicilistele()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select kad as 'Kullanıcı Adı',kyetki as 'Yetkisi' from kullanici", baglanti);
            adtr.Fill(daset, "kullanici");
            dataGridView1.DataSource = daset.Tables["kullanici"];
            baglanti.Close();
        }
        public ryMusteriGirisForm kullanici;
        public MusteriAnaForm anaform;
        private void KullaniciListeForm_Load(object sender, EventArgs e)
        {
            kullanicilistele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update kullanici set kyetki=@kyetki where kad=@kad", baglanti);
            komut.Parameters.AddWithValue("@kad", txtAd.Text);
            komut.Parameters.AddWithValue("@kyetki", cmbYetki.Text);
            if (cmbYetki.Text == "Yönetici" || cmbYetki.Text == "Çalışan")
            {
                if (txtAd.Text != "")
                {
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Güncelleme işlemi gerçekleşti");
                }
                else { MessageBox.Show("Kullanıcı adı boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                
            }
            else { MessageBox.Show("Geçersiz yetki girildi", "Uyarı", MessageBoxButtons.OK,MessageBoxIcon.Warning); }
            daset.Tables["kullanici"].Clear();
            baglanti.Close();
            kullanicilistele();
            txtAd.Text = "";
            cmbYetki.Text = "";
            
        }
        
        private void btnSil_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu kaydı silmek mi istiyorsunuz?", "Sil", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from kullanici where kad=@kad", baglanti);
                komut.Parameters.AddWithValue("@kad", dataGridView1.CurrentRow.Cells["kullanıcı adı"].Value.ToString());
                if (txtkullanici.Text != dataGridView1.CurrentRow.Cells["kullanıcı adı"].Value.ToString())
                {
                    komut.ExecuteNonQuery();
                    
                    MessageBox.Show("Silme işlemi gerçekleşti");
                    
                }
                else { MessageBox.Show("Kullanıcı kendini silemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                baglanti.Close();
                daset.Tables["kullanici"].Clear();
                kullanicilistele();
                txtAd.Text = "";
                cmbYetki.Text = "";

            }
        }

        private void txtYetkiAra_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
                daset.Tables["kullanici"].Clear();
                if (txtAdAra.Text == "")
                {
                    SqlDataAdapter adtr = new SqlDataAdapter("select kad as 'Kullanıcı Adı',kyetki as 'Yetkisi' from kullanici where kyetki like '%" + txtYetkiAra.Text + "%'", baglanti);
                    adtr.Fill(daset, "kullanici");
                }
                if (txtAdAra.Text != "")
                {
                    SqlDataAdapter adtr = new SqlDataAdapter("select kad as 'Kullanıcı Adı',kyetki as 'Yetkisi' from kullanici where kad like '%" + txtAdAra.Text + "%' and kyetki like '%" + txtYetkiAra.Text + "%'   ", baglanti);
                    adtr.Fill(daset, "kullanici");
                }
                    dataGridView1.DataSource = daset.Tables["kullanici"];
            baglanti.Close();
        }

        private void cmbYetki_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }
    }
}
