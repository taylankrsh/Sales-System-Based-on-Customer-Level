using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace Musteri_Bilgi_Otomasyonu
{
    public partial class Urun : Form
    {
        public Urun()
        {
            InitializeComponent();
        }
        #region Tanimlamalar
        public bool durum;
        public bool durum3456=true;
        public bool durum7;
        SqlConnection baglanti= new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        #endregion

        private void btnAnaForm_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void kayitkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select urunkodu from urun where urunkodu = '"+txtUrunKodu.Text+"' ", baglanti);
            SqlDataReader read;
            read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtUrunKodu.Text == read["urunkodu"].ToString())
                {
                    durum = false;
                }
                else { durum = true; }  //mesela urunkodu 1 olan bir ürünü ekledik. ürün kodu 11 olan bir ürünü kaydetmek istediğimizde 1 yazdığımız zaman durum otomatik olarak false oluyor ve 1 ile başlayan mesela 11 ürün kodu kaydı yapılamıyor. bu yüzden else içinde tekrar bunu true yapmamız gerekiyor ki 1 ile başlayan ürünkodlarını kaydedebilelim. 
            }
            baglanti.Close();
        }

        private void txtUrunKodu_TextChanged(object sender, EventArgs e)
        {
            durum7 = true;
            urunkodukontrol();

            if (durum7 == true && durum3456 == true) //durum3456 kaydetme işlemi başarıyla gerçekleşmesi, baglantinin açılmaması için var.
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("select uruntanimi,urunmiktari,alisfiyati$,satisfiyati$ from urun where urunkodu ='" + txtUrunKodu.Text + "'", baglanti);
                SqlDataReader read = komut.ExecuteReader();
                while (read.Read())
                {
                    txtUrunTanimi.Text = read["uruntanimi"].ToString();
                    txtMiktar.Text = read["urunmiktari"].ToString();
                    txtAlisFiyat.Text = read["alisfiyati$"].ToString();
                    txtSatisFiyat.Text = read["satisfiyati$"].ToString();
                }
            }

            baglanti.Close();
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            kayitkontrol();
            if (durum == true)
            {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("insert into urun(urunkodu,uruntanimi,urunmiktari,alisfiyati$,satisfiyati$) values(@urunkodu,@uruntanimi,@urunmiktari,@alisfiyati$,@satisfiyati$)", baglanti);
                    komut.Parameters.AddWithValue("@urunkodu", txtUrunKodu.Text);
                    komut.Parameters.AddWithValue("@uruntanimi", txtUrunTanimi.Text);
                    komut.Parameters.AddWithValue("@urunmiktari", txtMiktar.Text);
                    komut.Parameters.AddWithValue("@alisfiyati$", txtAlisFiyat.Text);
                    komut.Parameters.AddWithValue("@satisfiyati$", txtSatisFiyat.Text);
                    if (txtUrunKodu.Text != "" && txtUrunTanimi.Text != "" && txtMiktar.Text != "" && txtAlisFiyat.Text != "" && txtSatisFiyat.Text != "")
                    {
                        durum3456 = false;
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Ürün Kaydı Yapıldı!");
                        foreach (Control item in Controls)
                        {
                            if (item is TextBox)
                            {
                                item.Text = "";
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ürün bilgileri eksik girildi", "Uyarı");
                    }
            }
            else
            {
                MessageBox.Show("Bu ürün koduna ait kayıt bulunmaktadır", "Uyarı");
            }
            baglanti.Close();
            urunlistele();
            
        }

        private void dgvUrunListe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        DataSet daset;
        private void urunlistele()
        {
            daset = new DataSet();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select urunkodu as 'Ürün Kodu',uruntanimi as 'Ürün İsmi',urunmiktari as 'Ürün Miktarı',alisfiyati$ as 'Alış Fiyat($)',satisfiyati$ as 'Satış Fiyat($)' from urun", baglanti);
            adtr.Fill(daset, "urun");
            dgvUrunListe.DataSource = daset.Tables["urun"];
            baglanti.Close();
        }
        private void Urun_Load(object sender, EventArgs e)
        {
            urunlistele();
        }
        
        private void txtAlisFiyat_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtAlisFiyat.Text.Trim() != "")
                    txtSatisFiyat.Text = (Convert.ToDouble(txtAlisFiyat.Text) * 10).ToString();
            }
            catch
            {
                MessageBox.Show("Buraya yalnızca tam sayı girilebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAlisFiyat.Text = "";
            }
        }

        private void silToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialog;
            dialog = MessageBox.Show("Bu ürünü silmek istiyor musunuz?", "Sil", 
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialog == DialogResult.Yes)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("delete from urun where urunkodu=@urunkodu", baglanti);
                komut.Parameters.AddWithValue("@urunkodu", dgvUrunListe.CurrentRow.Cells["ürün kodu"].Value.ToString());
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Silme işlemi gerçekleşti");
                daset.Tables["urun"].Clear();
                baglanti.Close();
                urunlistele();
                
            }
            
        }

        private void güncelleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            txtUrunKodu.Text = dgvUrunListe.CurrentRow.Cells["Ürün Kodu"].Value.ToString();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("update urun set uruntanimi=@uruntanimi,urunmiktari=@urunmiktari,alisfiyati$=@alisfiyati$,satisfiyati$=@satisfiyati$ where urunkodu=@urunkodu", baglanti);
            komut.Parameters.AddWithValue("@urunkodu", txtUrunKodu.Text);
            komut.Parameters.AddWithValue("@uruntanimi", txtUrunTanimi.Text);
            komut.Parameters.AddWithValue("@urunmiktari", txtMiktar.Text);
            komut.Parameters.AddWithValue("@alisfiyati$", txtAlisFiyat.Text);
            komut.Parameters.AddWithValue("@satisfiyati$", txtSatisFiyat.Text);
            if (txtAlisFiyat.Text != "" && txtMiktar.Text != "" && txtUrunKodu.Text != "" && txtUrunTanimi.Text != "")
            {
                komut.ExecuteNonQuery();
                MessageBox.Show("Güncelleme işlemi gerçekleşti");
                baglanti.Close();
                daset.Tables["urun"].Clear();
                urunlistele();
                foreach (Control item in Controls)
                {
                    if (item is TextBox)
                    {
                        item.Text = "";
                    }
                }
            }
            else
            {
                MessageBox.Show("Bilgiler boş bırakılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        
        private void urunkodukontrol()
        {
            try
            {
                if (txtUrunKodu.Text.Trim() != "")
                    txtUrunKodu.Text = (Convert.ToDouble(txtUrunKodu.Text) * 1).ToString();
            }
            catch
            {
                MessageBox.Show("Buraya yalnızca tam sayı girilebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                durum7 = false;
                txtUrunKodu.Text = "";
            }
        }
        
        public UrunHareket urunhareket;
        private void hareketlerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            urunhareket = new UrunHareket();
            urunhareket.txtUrunKod.Text = dgvUrunListe.CurrentRow.Cells["Ürün Kodu"].Value.ToString();
            urunhareket.txtUrunBilgisi.Text = dgvUrunListe.CurrentRow.Cells["Ürün İsmi"].Value.ToString();
            urunhareket.ShowDialog();
        }

        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMiktar.Text.Trim() != "")
                    txtMiktar.Text = (Convert.ToDouble(txtMiktar.Text) * 1).ToString();
            }
            catch
            {
                MessageBox.Show("Buraya yalnızca tam sayı girilebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMiktar.Text = "";
            }
        }
    }
}
