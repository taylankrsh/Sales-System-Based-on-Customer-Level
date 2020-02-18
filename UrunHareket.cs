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
    public partial class UrunHareket : Form
    {
        public UrunHareket()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");

        DataSet daset = new DataSet();
        private void urunhareketliste()
        {
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select h.musterino as 'Müşteri No',Ad,Soyad,Tarih from hareket h inner join musteri m on h.musterino=m.musterino  where urunkodu = '" + txtUrunKod.Text + "'", baglanti);
            adtr.Fill(daset, "hareket");
            dgvUrunHareketListe.DataSource = daset.Tables["hareket"];
            baglanti.Close();
        }
        private void UrunHareket_Load(object sender, EventArgs e)
        {
            urunhareketliste();
        }
    }
}
