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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void listele()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_NOTLAR", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }
        void temizle()
        {
            TxtOlusturan.Text="";
            TXTID.Text="";
            TxtHitap.Text="";
            TxtBaslik.Text="";
            RchDetay.Text="";
            MskSaat.Text="";
            MskTarih.Text="";

        }
        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) VALUES (@P1,@P2,@P3,@P4,@P5,@P6)", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", MskTarih.Text);
            komut.Parameters.AddWithValue("@P2", MskSaat.Text);
            komut.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", RchDetay.Text);
            komut.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", TxtHitap.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            listele();
            MessageBox.Show("Not Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                TXTID.Text = row["ID"].ToString();
                TxtBaslik.Text = row["BASLIK"].ToString();
                TxtHitap.Text =row["HITAP"].ToString();
                TxtOlusturan.Text=row["OLUSTURAN"].ToString();
                RchDetay.Text=row["DETAY"].ToString();
                MskSaat.Text=row["SAAT"].ToString();
                MskTarih.Text=row["TARIH"].ToString();
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_NOTLAR WHERE ID=@P1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TXTID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Not silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_NOTLAR SET TARIH=@P1,SAAT=@P2,BASLIK=@P3,DETAY=@P4,OLUSTURAN=@P5,HITAP=@P6 WHERE ID=@P7", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", MskTarih.Text);
            komut.Parameters.AddWithValue("@P2", MskSaat.Text);
            komut.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            komut.Parameters.AddWithValue("@P4", RchDetay.Text);
            komut.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            komut.Parameters.AddWithValue("@P6", TxtHitap.Text);
            komut.Parameters.AddWithValue("@p7", TXTID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            listele();
            MessageBox.Show("Not Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay detay = new FrmNotDetay();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                detay.metin=row["DETAY"].ToString();
            }
            detay.Show();
        }
    }
}
