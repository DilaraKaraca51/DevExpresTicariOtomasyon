using DevExpress.Utils.CodedUISupport;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace asd
{
    public partial class FrmPersonel : Form
    {
        public FrmPersonel()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void personelliste()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_PERSONELLER", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }

        void sehirliStesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", baglan.baglanti());
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                CmbIl.Properties.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        void temizle()
        {
            TxtID.Text="";
            TxtAd.Text="";
            TxtSoyad.Text="";
            MskTelefon.Text="";
            MskTC.Text="";
            TxtMail.Text="";
            CmbIl.Text="";
            CmbIlce.Text="";
            RchAdres.Text="";
            TxtGorev.Text="";
        }
     
        private void FrmPersonel_Load(object sender, EventArgs e)
        {
            personelliste();

            sehirliStesi();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_PERSONELLER (AD,SOYAD,TELEFON,TC,MAIL,IL,ILCE,ADRES,GOREV) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", CmbIl.Text);
            komut.Parameters.AddWithValue("@P7", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            personelliste();

        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE Sehır=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                CmbIlce.Properties.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                TxtID.Text = row["ID"].ToString();
                TxtAd.Text = row["AD"].ToString();
                TxtSoyad.Text = row["SOYAD"].ToString();
                MskTelefon.Text = row["TELEFON"].ToString();
                MskTC.Text = row["TC"].ToString();
                TxtMail.Text = row["MAIL"].ToString();
                CmbIl.Text = row["IL"].ToString();
                CmbIlce.Text = row["ILCE"].ToString();
                RchAdres.Text = row["ADRES"].ToString();
                TxtGorev.Text = row["GOREV"].ToString();

            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komutsil = new SqlCommand("DELETE FROM TBL_PERSONELLER WHERE ID=@P1", baglan.baglanti());
            komutsil.Parameters.AddWithValue("@P1", TxtID.Text);
            komutsil.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Personel Listeden Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.None);
            personelliste();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_PERSONELLER SET AD=@P1,SOYAD=@P2,TELEFON=@P3,TC=@P4,MAIL=@P5,IL=@P6,ILCE=@P7,ADRES=@P8,GOREV=@P9 WHERE ID=@P10", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtAd.Text);
            komut.Parameters.AddWithValue("@P2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@P3", MskTelefon.Text);
            komut.Parameters.AddWithValue("@P4", MskTC.Text);
            komut.Parameters.AddWithValue("@P5", TxtMail.Text);
            komut.Parameters.AddWithValue("@P6", CmbIl.Text);
            komut.Parameters.AddWithValue("@P7", CmbIlce.Text);
            komut.Parameters.AddWithValue("@P8", RchAdres.Text);
            komut.Parameters.AddWithValue("@P9", TxtGorev.Text);
            komut.Parameters.AddWithValue("@P10", TxtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Personel Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            personelliste();
        }
    }
}
