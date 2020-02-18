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
    public partial class ryMusteriGirisForm : Form
    {
        public MusteriAnaForm musteriAfrm;
        public YeniKullaniciForm ilk;
        public ryMusteriGirisForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        Random rSayi = new Random();
        int islemSonucu;
        string islemIsareti = "+-";
        bool durum;
        public bool durum2;
        public string x;

        private void guvenlikKoduUret()
        {
            
            int sayi1 = rSayi.Next(10, 15);
            int sayi2 = rSayi.Next(0, 5);
            int islemTipi = rSayi.Next(0, 2);
            lblSayi1.Text = sayi1.ToString();
            lblSayi2.Text = sayi2.ToString();
            label4.Text = islemIsareti[islemTipi].ToString();

            if (label4.Text == "+")
                islemSonucu = sayi1 + sayi2;
            else
                islemSonucu = sayi1 - sayi2;
        }


        private void ryMusteriGirisForm_Load(object sender, EventArgs e)
        {
            guvenlikKoduUret();
        }



        private void varolankisi()
        {
            musteriAfrm = new MusteriAnaForm();
            durum = false;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from kullanici", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {

               
                if (txtAd.Text==read["kAd"].ToString() && txtSifre.Text == read["kSifre"].ToString() )
                {
                    if (read["kYetki"].ToString() == "Yönetici")
                    {
                        musteriAfrm.yonetici = true;
                    }
                    durum = true;

                }
                
            }
            baglanti.Close();


        }

        public MusteriAnaForm a;
        private void btnGiris_Click(object sender, EventArgs e)
        {
            musteriAfrm = new MusteriAnaForm();
            varolankisi();
            if (durum == true && txtSonuc.Text == islemSonucu.ToString())
            {
                x = txtAd.Text;
                musteriAfrm.txtkullanici.Text = x;
                musteriAfrm.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Kullanıcı adı, şifre veya güvenlik sorusu cevabı hatalı!",
                    "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtAd.Clear();
                txtSifre.Clear();
                txtSonuc.Clear();
                guvenlikKoduUret();
                txtAd.Select();
            }

        }


        private void btnCikis_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtAd_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select kYetki from kullanici where kAd = '" + txtAd.Text+ "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtRol.Text = read["kYetki"].ToString();
            }
            baglanti.Close();
        }

        private void txtRol_TextChanged(object sender, EventArgs e)
        {

        }
        private void lblSayi2_Click(object sender, EventArgs e)
        {

        }

        private void ryMusteriGirisForm_Load_1(object sender, EventArgs e)
        {
            guvenlikKoduUret();
        }

        private void kayitvarmi()
        {
            
            durum2 = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select kyetki from kullanici", baglanti);
            SqlDataReader read;
            read = komut.ExecuteReader();
            while (read.Read())
            {
                if (read["kYetki"].ToString() == "Yönetici")
                {
                    durum2 = false ;
                }
            }
            baglanti.Close();
            
        }


        private void btnIlkKayit_Click(object sender, EventArgs e)
        {
            
            ilk = new YeniKullaniciForm();
            kayitvarmi();
            if (durum2)
            {
                ilk.ShowDialog();                
            }
            else {
                MessageBox.Show("Kayıtlı Kullanıcı var!","Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }
    }
        

}



