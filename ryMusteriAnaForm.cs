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
    public partial class ryMusteriAnaForm : Form
    {
        //1.aşama
        public ryMusteriGirisForm musteriGiris;
        
    public ryMusteriAnaForm()
        {
            InitializeComponent();
            baslat();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");
        //public ryMusteriGirisForm giris = new ryMusteriGirisForm();
        //public ryMusteriEkleForm yeniMusteri = new ryMusteriEkleForm();
        //public ryMusteriIslemForm islemler = new ryMusteriIslemForm();
        //public ryMusteriUrunTanimiForm urunler = new ryMusteriUrunTanimiForm();
        //public ryMusteriYeniKullaniciForm yeniKullanici = new ryMusteriYeniKullaniciForm();
        //public ryMusteriRaporForm rapor = new ryMusteriRaporForm();

        OleDbConnection musConn = new OleDbConnection
            (@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
            Environment.CurrentDirectory + "\\data\\MUSTERI.ACCDB;");
        public bool durum;
        public bool yonetici = false;
        public bool yonetimmenuitem;

        private void baslat()
        {
            //2.aşama


            //4.aşama
            //giris.musteriAfrm = this;
            //yeniMusteri.musteriAfrm = this;
            //islemler.musteriAfrm = this;
            //urunler.musteriAfrm = this;
            //yeniKullanici.musteriAfrm = this;
            //rapor.musteriAfrm = this;

            yonetimMenuItem.Visible = false;
            yeniMenuItem.Visible = false;
            
        }

        /*
        public DataTable veriGetir(string sorgu)
        {
            DataTable musTablo = new DataTable();
            musAdp = new OleDbDataAdapter(sorgu, musConn);
            musAdp.Fill(musTablo);
            return musTablo;
        }

        public void komutCalistir(string sorgu)
        {
            try
            {
                if (musConn.State == ConnectionState.Closed)
                    musConn.Open();
                musCmd.Connection = musConn;
                musCmd.CommandText = sorgu;
                musCmd.ExecuteNonQuery();
            }
            catch (Exception msg)
            {
                MessageBox.Show(msg.ToString(), "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            finally
            {
                musConn.Close();
                //musConn.Dispose();
            }
        }
        */
       /* private void ryMusteriAnaForm_Load(object sender, EventArgs e)
        {
            giris.ShowDialog();
            musteriGiris=new ryMusteriGirisForm();
            //if (durum == false)
            //    ryMusteriAnaForm_Load(sender, e);
            if (yonetici)
            {
                yonetimMenuItem.Visible = true;
                yeniMenuItem.Visible = true;
            }

        }*/

        private void yeniMenuItem_Click(object sender, EventArgs e)
        {
            //yeniMusteri.ShowDialog();
        }

        private void islemlerMenuItem_Click(object sender, EventArgs e)
        {
            //islemler.ShowDialog();
        }

        private void urunlerMenuItem_Click(object sender, EventArgs e)
        {
            //urunler.ShowDialog();
        }

        private void raporMenuItem_Click(object sender, EventArgs e)
        {
            //rapor.ShowDialog();
        }

        private void yeniKullaniciMenuItem_Click(object sender, EventArgs e)
        {
            //yeniKullanici.ShowDialog();
        }

        private void kapatMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istiyor musunuz?", "Bilgi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes) Application.Exit();
        }

        private void yeniToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void yonetimMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void ryMusteriAnaForm_Load_1(object sender, EventArgs e)
        {
            musteriGiris = new ryMusteriGirisForm();
            if (yonetici)
            {
                yonetimMenuItem.Visible = true;
                yeniMenuItem.Visible = true;
            }
        }

        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Çıkmak istiyor musunuz?", "Bilgi",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) ==
                DialogResult.Yes)
                this.Close();
        }

        private void işlemlerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
