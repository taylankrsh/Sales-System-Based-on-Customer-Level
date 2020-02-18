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
    public partial class musterihareket : Form
    {
        public musterihareket()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection("Data Source=desktop-5i15fkf;Initial Catalog=MBS;Integrated Security=True");

        private void btnkapat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        DataSet daset = new DataSet();
        private void musterihareketlistele()
        {
            daset = new DataSet();
            baglanti.Open();
            SqlDataAdapter adtr = new SqlDataAdapter("select h.urunkodu as 'Ürün Kodu' ,uruntanimi as 'Ürün İsmi',satıştutarı as 'Satış Tutarı',satilanmiktar as 'Satılan Miktar',Tarih from hareket h inner join urun u on h.urunkodu=u.urunkodu where musterino = '" + txtMusteriNo.Text + "'", baglanti);
            adtr.Fill(daset, "hareket");
            dgvMusteriHareketListe.DataSource = daset.Tables["hareket"];
            baglanti.Close();
        }

        private void musterihareket_Load(object sender, EventArgs e)
        {
            musterihareketlistele();
        }
    }
}
