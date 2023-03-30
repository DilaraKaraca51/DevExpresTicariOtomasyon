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
    public partial class FrmUrunler : Form
    {
        public FrmUrunler()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void listele()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_URUNLER", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource=table;
        }

        void temizle()
        {
            adtxt.Text="";
            TxtAlis.Text="";
            txtID.Text="";
            TxtMarka.Text="";
            TxtModel.Text="";
            TxtSatis.Text="";
            maskedYil.Text="";
            numericAdet.Value=0;
            richTextBoxDetay.Text="";
        }
        private void FrmUrunler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click_1 (object sender, EventArgs e)
        {
            //Verileri Kaydetme
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_URUNLER (URUNAD,MARKA,MODEL,YIL,ADET,ALISFIYAT,SATISFIYAT,DETAY) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", baglan.baglanti());
            komut.Parameters.AddWithValue("p1", adtxt.Text);
            komut.Parameters.AddWithValue("p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("p3", TxtModel.Text);
            komut.Parameters.AddWithValue("p4", maskedYil.Text);
            komut.Parameters.AddWithValue("p5", int.Parse((numericAdet.Value).ToString()));
            komut.Parameters.AddWithValue("p6", decimal.Parse(TxtAlis.Text).ToString());
            komut.Parameters.AddWithValue("p7", decimal.Parse(TxtSatis.Text).ToString());
            komut.Parameters.AddWithValue("p8", richTextBoxDetay.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("DELETE FROM TBL_URUNLER WHERE ID=@p1", baglan.baglanti());
            komutsil.Parameters.AddWithValue("@p1", txtID.Text);
            komutsil.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            txtID.Text=row["ID"].ToString();
            adtxt.Text=row["URUNAD"].ToString();
            TxtMarka.Text=row["MARKA"].ToString();
            TxtModel.Text=row["MODEL"].ToString();
            maskedYil.Text=row["YIL"].ToString();
            numericAdet.Value=decimal.Parse(row["ADET"].ToString());
            TxtAlis.Text=row["ALISFIYAT"].ToString();
            TxtSatis.Text=row["SATISFIYAT"].ToString();
            richTextBoxDetay.Text=row["DETAY"].ToString();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_URUNLER SET URUNAD=@p1,MARKA=@p2,MODEL=@p3,YIL=@p4,ADET=@p5,ALISFIYAT=@p6,SATISFIYAT=@p7,DETAY=@p8 WHERE ID=@p9", baglan.baglanti());
            komut.Parameters.AddWithValue("p1", adtxt.Text);
            komut.Parameters.AddWithValue("p2", TxtMarka.Text);
            komut.Parameters.AddWithValue("p3", TxtModel.Text);
            komut.Parameters.AddWithValue("p4", maskedYil.Text);
            komut.Parameters.AddWithValue("p5", int.Parse((numericAdet.Value).ToString()));
            komut.Parameters.AddWithValue("p6", decimal.Parse(TxtAlis.Text));
            komut.Parameters.AddWithValue("p7", decimal.Parse(TxtSatis.Text));
            komut.Parameters.AddWithValue("p8", richTextBoxDetay.Text);
            komut.Parameters.AddWithValue("p9", txtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Ürün Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
}
