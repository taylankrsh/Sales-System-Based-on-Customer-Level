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
    public partial class MusteriGuncelleForm : Form
    {
        public MusteriGuncelleForm()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");


        private void btnKapat_Click(object sender, EventArgs e)
        {
            this.Close();
           
        }

        private void txtMusteriNo_TextChanged(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("select * from  musteri where musterino ='" + txtMusteriNo.Text + "'", baglanti);
            SqlDataReader read = komut.ExecuteReader();
            while (read.Read())
            {
                txtYetkiliAd.Text = read["ad"].ToString();
                txtYetkiliSoyad.Text = read["soyad"].ToString();
                txtTelefonNo.Text = read["telefonno"].ToString();
            }
            baglanti.Close();
        }
        DataSet daset = new DataSet();
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
           baglanti.Open();
            SqlCommand komut = new SqlCommand("update musteri set musterino=@musterino ,ad=@ad,soyad=@soyad, telefonno=@telefonno where musterino=@musterino", baglanti);
            komut.Parameters.AddWithValue("@musterino", txtMusteriNo.Text);
            komut.Parameters.AddWithValue("@ad", txtYetkiliAd.Text);
            komut.Parameters.AddWithValue("@soyad", txtYetkiliSoyad.Text);
            komut.Parameters.AddWithValue("@telefonno", txtTelefonNo.Text);
            if (txtYetkiliAd.Text != "" && txtYetkiliSoyad.Text != "" && txtTelefonNo.Text != "")
            {
                komut.ExecuteNonQuery();
                baglanti.Close();
                MessageBox.Show("Güncelleme işlemi gerçekleşti");
            }
            else { MessageBox.Show("Bilgiler boş bırakılamaz!", "Uyarı"); }
            
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
