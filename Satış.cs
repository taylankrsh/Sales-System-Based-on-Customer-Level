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
    public partial class Satış : Form
    {
        public Satış()
        {
            InitializeComponent();
            pic.Visible = false;
            dollar.Visible = false;
            btnnormalsat.Visible = false;
            lbl10.Visible = false;
            urunsec.Visible = false;
            txtMiktar.Text = "0";
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
       
        private void btnGeri_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        public bool durum9 = false;
        public bool durum1234;
        private void txtUrunKodu_TextChanged(object sender, EventArgs e)
        {
            txtMiktar.Text = "0";
            txtSatisTutari.Text = "";
            try
            {
                if (int.Parse(txttotal.Text) < 1000 && int.Parse(txtUrunKodu.Text) < 100 || int.Parse(txttotal.Text) > 1000 && int.Parse(txttotal.Text) < 2000 && int.Parse(txtUrunKodu.Text) < 200 || int.Parse(txttotal.Text) > 2000)
                {
                    baglanti.Open();
                    SqlCommand komut = new SqlCommand("select urunkodu,uruntanimi,satisfiyati$ from  urun where urunkodu ='" + txtUrunKodu.Text + "'", baglanti);
                    SqlDataReader read = komut.ExecuteReader();
                    while (read.Read())
                    {
                        txtUrunTanimi.Text = read["uruntanimi"].ToString();
                        txtSatisFiyat.Text = read["satisfiyati$"].ToString();
                    }
                }
                else
                {
                    durum1234 = false;
                    MessageBox.Show("Seviyeniz bu ürün için henüz yetersiz", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUrunKodu.Text = "";
                }
            }
            catch
            {
                if (durum1234 == true)
                {
                    MessageBox.Show("Buraya yalnızca tam sayı girilebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtUrunKodu.Text = "";
                }
            }
            baglanti.Close();
        }

        private void txtSatisTutari_TextChanged(object sender, EventArgs e) { }
        public int k;
        private void txtMiktar_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (durum9 == false)
                {
                    if (txtSatisFiyat.Text != "" && txtMiktar.Text != "")
                        txtSatisTutari.Text = (Convert.ToDouble(txtMiktar.Text) * Convert.ToDouble(txtSatisFiyat.Text)).ToString();
                }
                else
                {
                if (txtSatisFiyat.Text != "" && txtMiktar.Text != "")
                {

                    txtSatisTutari.Text = (Convert.ToDouble(txtMiktar.Text) * Convert.ToDouble(txtSatisFiyat.Text)).ToString();
                    k = int.Parse(txtSatisTutari.Text);
                    k = k * 90 / 100;
                    txtSatisTutari.Text = Convert.ToString(k);
                }
                }
            }
            catch
            {
                MessageBox.Show("Miktar bölümüne yalnızca tam sayı yazılabilir!", "Uyarı",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMiktar.Text="0";
            }
        }
        public bool durum3;
        public bool durum;
        public bool durum2;
        private void varolanurun()
        {
            durum = false;
            durum3 = true;
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select urunkodu,urunmiktari from urun", baglanti);
            SqlDataReader read = komut2.ExecuteReader();
            while (read.Read())
            {
                if (txtMiktar.Text != "" && txtMiktar.Text != "0")
                {
                    if (int.Parse(read["urunmiktari"].ToString()) >= int.Parse(txtMiktar.Text) &&
                        read["urunmiktari"].ToString() != "0" &&
                        txtUrunKodu.Text == read["urunkodu"].ToString())
                    {
                        durum3 = false;
                    }
                    if (txtUrunKodu.Text == read["urunkodu"].ToString())
                        durum = true;
                }
            }
            baglanti.Close();
        }

        
        private void tekraralinan()
        {
            durum2 = false;
            baglanti.Open();
            SqlCommand komut2 = new SqlCommand("select musterino,urunkodu from hareket", baglanti);
            SqlDataReader read = komut2.ExecuteReader();
            while (read.Read())
            {
                if (txtMusteriNo.Text == read["musterino"].ToString() && txtUrunKodu.Text == read["urunkodu"].ToString())
                {
                    durum2 = true;
                }
            }
            baglanti.Close();
        }

        DataSet daset = new DataSet();
        private void satinalinanlar()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select musterino as 'Müşteri No', urunkodu as 'Ürün Kodu', satıştutarı as 'Satış Tutarı', satilanmiktar as 'Satış Miktarı', Tarih from hareket", baglanti);
            adtr.Fill(daset, "hareket");
            dgvSatisListe.DataSource = daset.Tables["hareket"];
            baglanti.Close();

            if (int.Parse(txttotal.Text) > 2000)
                pic.Visible = true;
            if (int.Parse(txttotal.Text) > 1000 && int.Parse(txttotal.Text) < 2000)
                dollar.Visible = true;
        }

        public bool durum123;
        public string x;
        public string y;
        public string z;
        public int t;
        private void btnSat_Click(object sender, EventArgs e)
        {
            durum123 = false;
            güncelle();
            varolanurun();
            tekraralinan();
            if (durum == true)
            {
                if (cmbOdeme.Text == "Senet" || cmbOdeme.Text == "Nakit" || cmbOdeme.Text == "Kredi Kartı")
                { 
                    baglanti.Open();
                    SqlCommand komut3 = new SqlCommand("update hareket set satilanmiktar = satilanmiktar + '"+ txtMiktar.Text + "' , " +
                        "satıştutarı = satıştutarı+ '"+txtSatisTutari.Text+"', tarih = '"+ DateTime.Now.ToString() + "'" +
                        " where musterino ='" + txtMusteriNo.Text+"' and urunkodu = '"+txtUrunKodu.Text+"' ", baglanti);
                    SqlCommand komut = new SqlCommand("insert into hareket(musterino,urunkodu" +
                        ",satıştutarı,satilanmiktar,tarih) values(@musterino,@urunkodu,@satıştutarı,@satilanmiktar,@tarih)", baglanti);
                    komut.Parameters.AddWithValue("@musterino", txtMusteriNo.Text);
                    komut.Parameters.AddWithValue("@urunkodu", txtUrunKodu.Text);
                    komut.Parameters.AddWithValue("@satıştutarı", txtSatisTutari.Text);
                    komut.Parameters.AddWithValue("@satilanmiktar", txtMiktar.Text);
                    komut.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());

                    komut3.Parameters.AddWithValue("@musterino", txtMusteriNo.Text);
                    komut3.Parameters.AddWithValue("@urunkodu", txtUrunKodu.Text);
                    komut3.Parameters.AddWithValue("@satıştutarı", txtSatisTutari.Text);
                    komut3.Parameters.AddWithValue("@satilanmiktar", txtMiktar.Text);
                    komut3.Parameters.AddWithValue("@tarih", DateTime.Now.ToString());
                    if (durum2 == true)
                    {
                        if (durum3 == false)
                        {
                            if (durum9 == false)   //kupon satış aktif değilken elde olan artar ve kupona eklenir
                            {
                                komut3.ExecuteNonQuery(); //aynı ürün aynı kişi tarafından tekrar alındı
                                SqlCommand komut5 = new SqlCommand("update musteri set kupon = kupon + '" + (int.Parse(txtMiktar.Text) + int.Parse(txtelde.Text)) / 5+"' where musterino = '"+txtMusteriNo.Text+"'", baglanti);
                            komut5.ExecuteNonQuery(); //kupon eklendi şimdi elde olanı yenilememiz lazım.
                                t = (int.Parse(txtMiktar.Text) + int.Parse(txtelde.Text)) % 5;
                                txtelde.Text = Convert.ToDouble(t).ToString();
                                SqlCommand komut6 = new SqlCommand("update musteri set eldekalan = '" + txtelde.Text + "' where musterino = '" + txtMusteriNo.Text + "' ", baglanti);
                                komut6.ExecuteNonQuery(); // eldekalan update edildi.

                                SqlCommand komut2 = new SqlCommand("update urun set urunmiktari=urunmiktari-'" + txtMiktar.Text + "' where urunkodu= '" + txtUrunKodu.Text + "' ", baglanti);
                                komut2.ExecuteNonQuery();
                                SqlCommand komut4 = new SqlCommand("update musteri set total$ = total$+ '" + txtSatisTutari.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut4.ExecuteNonQuery();
                                durum123 = true;
                                MessageBox.Show("Satış Yapıldı!");
                            }

                            
                            if (durum9 == true && int.Parse(txtMiktar.Text) <= int.Parse(txtKupon.Text)) //kuponla satış aktifken kupon eksiltilir, elde olana karışılmaz.
                            {
                                komut3.ExecuteNonQuery(); //aynı ürün aynı kişi tarafından tekrar alındı
                                txtKupon.Text = Convert.ToDouble(int.Parse(txtKupon.Text) - int.Parse(txtMiktar.Text)).ToString();
                                SqlCommand komut99 = new SqlCommand("update musteri set kupon=kupon-'" + txtMiktar.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut99.ExecuteNonQuery();

                                SqlCommand komut2 = new SqlCommand("update urun set urunmiktari=urunmiktari-'" + txtMiktar.Text + "' where urunkodu= '" + txtUrunKodu.Text + "' ", baglanti);
                                komut2.ExecuteNonQuery();
                                SqlCommand komut4 = new SqlCommand("update musteri set total$ = total$+ '" + txtSatisTutari.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut4.ExecuteNonQuery();
                                durum123 = true;
                                MessageBox.Show("Satış Yapıldı!");
                                txtMiktar.Text = "0"; // kupon sayısı 3 alınan miktar 2 olsun. satış sonrası miktar 2 kupon sayısı 1 olup miktar sayısı kupondan büyük olacağından hata mesajı geliyor. 

                            }
                            if (durum9 == true && int.Parse(txtMiktar.Text) > int.Parse(txtKupon.Text)) { MessageBox.Show("Girilen miktar kupon sayısını aşıyor", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }

                        }
                        else { MessageBox.Show("İstenilen miktar stok sayısını aşıyor veya ürün stokta kalmadı ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                    }
                    else
                    {
                        if (durum3 == false) 
                        {
                            if (durum9 == false)   //kupon satış aktif değilken elde olan artar ve kupona eklenir
                            {
                                komut.ExecuteNonQuery(); //farklı ürün alındı
                                SqlCommand komut5 = new SqlCommand("update musteri set kupon = kupon + '" + (int.Parse(txtMiktar.Text) + int.Parse(txtelde.Text)) / 5 + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut5.ExecuteNonQuery(); //kupon eklendi şimdi elde olanı yenilememiz lazım.
                                t = (int.Parse(txtMiktar.Text) + int.Parse(txtelde.Text)) % 5;
                                txtelde.Text = Convert.ToDouble(t).ToString();
                                SqlCommand komut6 = new SqlCommand("update musteri set eldekalan = '" + txtelde.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut6.ExecuteNonQuery(); // eldekalan update edildi.

                                SqlCommand komut2 = new SqlCommand("update urun set urunmiktari=urunmiktari-'" + txtMiktar.Text + "' where urunkodu= '" + txtUrunKodu.Text + "' ", baglanti);
                                komut2.ExecuteNonQuery();
                                SqlCommand komut4 = new SqlCommand("update musteri set total$ = total$+ '" + txtSatisTutari.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut4.ExecuteNonQuery();
                                durum123 = true;
                                MessageBox.Show("Satış Yapıldı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            if (durum9 == true && int.Parse(txtMiktar.Text) <= int.Parse(txtKupon.Text)) //kuponla satış aktifken kupon eksiltilir, elde olana karışılmaz.
                            {
                                komut.ExecuteNonQuery(); //farklı ürün alındı
                                txtKupon.Text = Convert.ToDouble(int.Parse(txtKupon.Text) - int.Parse(txtMiktar.Text)).ToString();
                                SqlCommand komut99 = new SqlCommand("update musteri set kupon=kupon-'" + txtMiktar.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut99.ExecuteNonQuery();

                                SqlCommand komut2 = new SqlCommand("update urun set urunmiktari=urunmiktari-'" + txtMiktar.Text + "' where urunkodu= '" + txtUrunKodu.Text + "' ", baglanti);
                                komut2.ExecuteNonQuery();
                                SqlCommand komut4 = new SqlCommand("update musteri set total$ = total$+ '" + txtSatisTutari.Text + "' where musterino = '" + txtMusteriNo.Text + "'", baglanti);
                                komut4.ExecuteNonQuery();
                                durum123 = true;
                                MessageBox.Show("Satış Yapıldı!", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                txtMiktar.Text = "0"; // kupon sayısı 3 alınan miktar 2 olsun. satış sonrası miktar 2 kupon sayısı 1 olup miktar sayısı kupondan büyük olacağından hata mesajı geliyor. 
                            }
                            if(durum9==true && int.Parse(txtMiktar.Text) > int.Parse(txtKupon.Text)) { MessageBox.Show("Girilen miktar kupon sayısını aşıyor", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                            
                        }
                        else { MessageBox.Show("İstenilen miktar stok sayısını aşıyor veya ürün stokta kalmadı ", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information); }
                    }
                    baglanti.Close();
                    if (durum123 == true)
                        güncelle();
                    if (durum9 == false)
                    {
                        daset.Tables["hareket"].Clear();
                        satinalinanlar();
                    }
                    if (durum9 == true && int.Parse(txtKupon.Text) < 1)
                    {
                        MessageBox.Show("Kuponunuz kalmadı.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        normalsat();
                        smile.Visible = false;
                    }
                }
                else
                {
                    MessageBox.Show("Bilgiler eksik veya yanlış girildi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                    MessageBox.Show("Bilgiler eksik veya yanlış girildi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        public MusteriListeForm musteriliste;
        private void güncelle()
        {
            musteriliste = new MusteriListeForm();
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select total$, musterino from musteri where musterino='"+txtMusteriNo.Text+"'" , baglanti);
            SqlDataReader read;
            read = komut.ExecuteReader();
            while (read.Read())
            {
                if (durum123 == false)
                    x = read["total$"].ToString();   // update öncesi değer
                y = read["total$"].ToString(); // update sonrası değer
                txttotal.Text = y;
                
            }
            baglanti.Close();
            baglanti.Open();
            if (int.Parse(x) < 1000 && int.Parse(y) > 1000) 
            {
                SqlCommand komut2 = new SqlCommand("update musteri set uyelikturu = 'Zengin' " +
                    "where musterino= '" + txtMusteriNo.Text + "'   ", baglanti);
                komut2.ExecuteNonQuery();
                MessageBox.Show("Tebrikler artık zengin bir üyesiniz!", "Bilgi");
                dollar.Visible = true;
            }
                if(int.Parse(x) < 2000 && int.Parse(x)>1000 && int.Parse(y) > 2000 || int.Parse(x) < 1000 && int.Parse(y) > 2000)
            {
                SqlCommand komut2 = new SqlCommand("update musteri set uyelikturu = 'VIP' " +
                    "where musterino= '" + txtMusteriNo.Text + "'   ", baglanti);
                komut2.ExecuteNonQuery();
                MessageBox.Show("Tebrikler artık VIP üyesiniz!", "Bilgi");
                dollar.Visible = false;
                pic.Visible = true;
            }
            baglanti.Close();
            baglanti.Open();
            SqlCommand komut123 = new SqlCommand("select kupon from musteri where musterino ='" + txtMusteriNo.Text + "'  ",baglanti);
            SqlDataReader read2 = komut123.ExecuteReader();
            while (read2.Read())
            {
                z = read2["kupon"].ToString();
                txtKupon.Text = z;
                if (int.Parse(txtKupon.Text) > 0)
                    smile.Visible = true;
            }
            baglanti.Close();
        }
        private void txtMusteriNo_TextChanged(object sender, EventArgs e){        }
        private void Satış_Load(object sender, EventArgs e)
        {
            satinalinanlar();
            if (int.Parse(txtKupon.Text) < 1)
                smile.Visible = false;
        }

        private void btntemizle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from hareket",baglanti);
            komut.ExecuteNonQuery();
            baglanti.Close();
            daset.Tables["hareket"].Clear();
            satinalinanlar();
        }
        public Urun urun;
        private void btnUrunListe_Click(object sender, EventArgs e)
        {
            urun = new Urun();
            urun.ShowDialog();
        }
        private void button1_Click(object sender, EventArgs e){        }
        private void kuponliste()
        {  //üyelik türüne göre (total$ a göre) satın alabildiği ürünleri listeleme. 
            baglanti.Open();
            if (dollar.Visible == true) //zenginler için
            {
                SqlDataAdapter adtr = new SqlDataAdapter("select urunkodu as 'Ürün Kodu',uruntanimi as 'Ürün İsmi',urunmiktari as 'Ürün Miktarı',satisfiyati$ as 'Fiyat($)' from urun where urunkodu < '200' ", baglanti);
                adtr.Fill(daset, "urun");
                dgvSatisListe.DataSource = daset.Tables["urun"];
            }
            if (int.Parse(txttotal.Text) < 1000) //normaller için
            {
                SqlDataAdapter adtr = new SqlDataAdapter("select urunkodu as 'Ürün Kodu',uruntanimi as 'Ürün İsmi',urunmiktari as 'Ürün Miktarı',satisfiyati$ as 'Fiyat($)' from urun where urunkodu < '100'", baglanti);
                adtr.Fill(daset, "urun");
                dgvSatisListe.DataSource = daset.Tables["urun"];
            }
            if (pic.Visible == true) 
            {  //VIP ler için
                SqlDataAdapter adtr = new SqlDataAdapter("select urunkodu as 'Ürün Kodu',uruntanimi as 'Ürün İsmi',urunmiktari as 'Ürün Miktarı',satisfiyati$ as 'Fiyat($)' from urun", baglanti);
                adtr.Fill(daset, "urun");
                dgvSatisListe.DataSource = daset.Tables["urun"];
            }
            baglanti.Close();

            if (int.Parse(txttotal.Text) > 2000)
                pic.Visible = true;
            if (int.Parse(txttotal.Text) > 1000 && int.Parse(txttotal.Text) < 2000)
                dollar.Visible = true;
        }
        private void btnkupon_Click(object sender, EventArgs e)
        {
                if (int.Parse(txtKupon.Text) > 0)
                {
                    daset.Tables["hareket"].Clear();
                    kuponliste();
                    txtUrunKodu.Enabled = false;
                    btnnormalsat.Visible = true;
                    lbl10.Visible = true;
                    durum9 = true;
                    txtSatisTutari.Text = "";
                    txtMiktar.Text = "0";
                    btnkupon.Visible = false;
                    smile.Location = new Point(451, 391);
                    urunsec.Visible = true;
                    btnSat.Text = "SAT ! (KUPONLA)";
                btntemizle.Visible = false;
                }
                else { MessageBox.Show("Kuponunuz bulunmamaktadır.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
        }
        private void normalsat()
        {
            txtUrunKodu.Enabled = true;
            btnnormalsat.Visible = false;
            durum9 = false;
            lbl10.Visible = false;
            daset.Tables["hareket"].Clear();
            daset.Tables["urun"].Clear();
            satinalinanlar();
            txtSatisTutari.Text = "";
            txtMiktar.Text = "0";
            btnSat.Text = "SAT !";
            smile.Location = new Point(451, 439);
            urunsec.Visible = false;
            btnkupon.Visible = true;
            btntemizle.Visible = true;

        }
        private void btnnormalsat_Click(object sender, EventArgs e)
        {  // normal satışa dönüş
            normalsat();
        }

        private void urunsec_Click(object sender, EventArgs e)
        {
            txtUrunKodu.Text = dgvSatisListe.CurrentRow.Cells["Ürün Kodu"].Value.ToString();
        }
    }
}
