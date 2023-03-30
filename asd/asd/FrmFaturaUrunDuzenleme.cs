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

namespace asd
{
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        public string urunid;
        sqlbaglantisi baglan = new sqlbaglantisi();
        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunId.Text = urunid;

            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", urunid);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                TxtFiyat.Text = reader[3].ToString();
                TxtMiktar.Text = reader[2].ToString();
                TxtTutar.Text = reader[4].ToString();
                TxtUrunAd.Text = reader[1].ToString();

                baglan.baglanti().Close();
            }
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURADETAY SET URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 WHERE FATURAURUNID=@P5", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtMiktar.Text);
            komut.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
            komut.Parameters.AddWithValue("@P4",decimal.Parse(TxtTutar.Text));
            komut.Parameters.AddWithValue("@P5", TxtUrunId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Değişiklikler Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_FATURADETAY WHERE FATURAURUNID=@P1",baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtUrunId.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question);
        }
    }
}
