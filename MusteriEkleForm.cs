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
    public partial class MusteriEkleForm : Form
    {
        public MusteriAnaForm musteriAfrm;

        public MusteriEkleForm()
        {
            InitializeComponent();
        }

        private void MusteriEkleForm_Load(object sender, EventArgs e) {        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        
        public bool durum;

       

        

        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void kayitkontrol()
        {
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select musterino from musteri", baglanti);
            SqlDataReader read;
            read = komut.ExecuteReader();
            while (read.Read())
            {
                if (txtMusteriNo.Text == read["musterino"].ToString())
                {
                    durum = false;
                }
            }
            baglanti.Close();

        }

        private void btnKaydet_Click_1(object sender, EventArgs e)
        {
            kayitkontrol();
            if (durum == true)
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("insert into musteri(musteriNo, Ad, Soyad " +
                    ", telefonNo, uyelikturu, total$, kupon,eldekalan) values(@musteriNo, @Ad, @Soyad, " +
                    "@telefonNo, @uyelikturu, @total$, @kupon, @eldekalan)", baglanti);
                komut.Parameters.AddWithValue("@musterino", txtMusteriNo.Text);
                komut.Parameters.AddWithValue("@ad", txtYetkiliAd.Text);
                komut.Parameters.AddWithValue("@soyad", txtYetkiliSoyad.Text);
                komut.Parameters.AddWithValue("@telefonno", txtTelefonNo.Text);
                komut.Parameters.AddWithValue("@uyelikturu", "Normal");
                komut.Parameters.AddWithValue("@total$", 0);
                komut.Parameters.AddWithValue("@kupon", 0);
                komut.Parameters.AddWithValue("@eldekalan", 0);


                if (txtMusteriNo.Text != "" && txtYetkiliAd.Text != "" && txtYetkiliSoyad.Text != "" &&
                     txtTelefonNo.Text != "")
                {
                    komut.ExecuteNonQuery();
                    MessageBox.Show("Müşteri Kaydı Yapıldı!");
                }
                else
                {
                    MessageBox.Show("Müşteri bilgileri eksik girildi", "Uyarı");
                }
            }

            else
            {
                MessageBox.Show("Bu müşteri numarasına ait kayıt bulunmaktadır", "Uyarı");
            }
            baglanti.Close();


            foreach (Control item in Controls)     // kayıt yaptıktan sonra tüm yazıları siler
            {                                      //foreach, control, in, is falan ne işe yarıyor öğren.
                if (item is TextBox)
                {

                    item.Text = "";


                }
            }
            
        }

        private void btnKapat_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMusteriNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtMusteriNo.Text != "")
                    txtMusteriNo.Text = (Convert.ToDouble(txtMusteriNo.Text) * 1).ToString();
            }
            catch
            {
                MessageBox.Show("Buraya yalnızca tam sayı girilebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMusteriNo.Text = "";
            }
        }

        private void txtTelefonNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (txtTelefonNo.Text != "")
                    txtTelefonNo.Text = (Convert.ToDouble(txtTelefonNo.Text) * 1).ToString();
            }
            catch
            {
                MessageBox.Show("Buraya yalnızca tam sayı girilebilir!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTelefonNo.Text = "";
            }
        }
    }
}
