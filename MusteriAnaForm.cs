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
    public partial class MusteriAnaForm : Form
    {
        /*
        public ryMusteriEkleForm yeniMusteri;
        public ryMusteriIslemForm islemler;
        public ryMusteriUrunTanimiForm urunler;
        public ryMusteriYeniKullaniciForm yeniKullanici;
        public ryMusteriRaporForm rapor;
        public ryMusteriAnaForm()
        */
        

        public MusteriAnaForm()
        {
            InitializeComponent();
            grpYonetim.Visible = false;
        }

        
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        public Urun urun;
        public MusteriEkleForm yeniMusteri;
        public YeniKullaniciForm yeniKullanici;
        public KullaniciListeForm kullaniciliste;
        public MusteriListeForm musteriliste;
        public ryMusteriGirisForm giris;
        public bool yonetici = false;
        public bool yonetimmenuitem;

        private void btnMusteriEkle_Click(object sender, EventArgs e)
        {
            yeniMusteri = new MusteriEkleForm();
            yeniMusteri.ShowDialog();
        }
        private void MusteriAnaForm_Load(object sender, EventArgs e)
        {
            if (yonetici)
            {
                grpYonetim.Visible = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istiyor musunuz?", "Bilgi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
                this.Close();
        }

        

        private void btnYeniKullanici_Click(object sender, EventArgs e)
        {
            yeniKullanici = new YeniKullaniciForm();
            yeniKullanici.ShowDialog();
        }
        public string x;
        private void btnKullaniciListe_Click(object sender, EventArgs e)
        {
            kullaniciliste = new KullaniciListeForm();
            x = txtkullanici.Text;
            kullaniciliste.txtkullanici.Text = x;
            kullaniciliste.ShowDialog();
        }

        private void btnUrun_Click(object sender, EventArgs e)
        {
            urun = new Urun();
            urun.ShowDialog();
        }

        private void btnMusteriİslem_Click(object sender, EventArgs e)
        {
            musteriliste = new MusteriListeForm();
            musteriliste.ShowDialog();
        }
    }
}
