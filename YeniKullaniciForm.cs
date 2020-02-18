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
    public partial class YeniKullaniciForm : Form
    {
        public YeniKullaniciForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        public bool durum;
        public bool durum2; 


        private void YeniKullaniciForm_Load(object sender, EventArgs e)
        {
            kayitkontrol();
            if (durum2 == true)
            {
                cmbYetki.Visible = false;
                label3.Visible = false;
            }
            if (durum2 == false)
                lblilkkayit.Visible = false;
        }


        private void kayitkontrol()
        {
            durum2 = true;
            durum = true;
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select kad,kyetki from kullanici", baglanti);
            SqlDataReader read;
            read = komut.ExecuteReader();
            while (read.Read())
            {
                if (read["kyetki"].ToString() == "Yönetici")
                {
                    durum2 = false;
                }
                if (txtAd.Text == read["kad"].ToString())
                {
                    durum = false;
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
                SqlCommand komut = new SqlCommand("insert into kullanici(kad, ksifre, kyetki) values(@kad, @ksifre, @kyetki)", baglanti);
                komut.Parameters.AddWithValue("@kad", txtAd.Text);
                komut.Parameters.AddWithValue("@ksifre", txtSifre.Text);
                if (durum2 == false )
                {
                    if (txtAd.Text != "" && txtSifre.Text != ""
                        && cmbYetki.Text == "Yönetici" || cmbYetki.Text == "Çalışan")
                    {
                        komut.Parameters.AddWithValue("@kyetki", cmbYetki.Text);
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı Kaydı Yapıldı!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    }
                    else { MessageBox.Show("Kullanıcı bilgileri eksik veya geçersiz yetki girildi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
                
                if (durum2 == true )
                {
                    komut.Parameters.AddWithValue("@kyetki", cmbYetki.Text = "Yönetici");
                    if (txtAd.Text != "" && txtSifre.Text != "")
                    {
                        komut.ExecuteNonQuery();
                        MessageBox.Show("Kullanıcı Kaydı Yapıldı!","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        this.Close();
                    }
                    else { MessageBox.Show("Kullanıcı bilgileri eksik girildi!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
                }
            }

            else
            {

                MessageBox.Show("Bu kullanıcı adına ait kayıt bulunmaktadır", "Uyarı");
                
            }
            baglanti.Close();

            foreach (Control item in Controls)     
            {                                      
                if (item is TextBox)
                {

                    item.Text = "";


                }
            }
            
            
        }

    }
}
