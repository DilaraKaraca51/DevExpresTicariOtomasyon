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
    public partial class FrmMusteriler : Form
    {
        public FrmMusteriler()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan = new sqlbaglantisi();
        void listele()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_MUSTERILER",baglan.baglanti());
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
            adTxt.Text="";
            TXTID.Text="";
            TxtMail.Text="";
            TxtSoyad.Text="";
            TxtVergiDairesi.Text="";
            MskTC.Text="";
            MskTelefon.Text="";
            MskTelefon2.Text="";
            CmbIl.Text="";
            CmbIlce.Text="";
            RchAdres.Text="";
        }
        private void FrmMusteriler_Load(object sender, EventArgs e)
        {
            listele();
            sehirliStesi();
            temizle();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE Sehır=@p1",baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                CmbIlce.Properties.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_MUSTERILER(AD,SOYAD,TELEFON,TELEFON2,TC,MAIL,IL,ILCE,ADRES,VERGIDAIRE) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10)",baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", adTxt.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", CmbIl.Text);
            komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p9", RchAdres.Text);
            komut.Parameters.AddWithValue("@p10", TxtVergiDairesi.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri Sisteme Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(row != null)
            {
                TXTID.Text=row["ID"].ToString();
                adTxt.Text=row["AD"].ToString();
                TxtSoyad.Text=row["SOYAD"].ToString();
                MskTelefon.Text=row["TELEFON"].ToString();
                MskTelefon2.Text=row["TELEFON2"].ToString();
                MskTC.Text=row["TC"].ToString();
                TxtMail.Text=row["MAIL"].ToString();
                CmbIl.Text=row["IL"].ToString();
                CmbIlce.Text=row["ILCE"].ToString();
                RchAdres.Text=row["ADRES"].ToString();
                TxtVergiDairesi.Text=row["VERGIDAIRE"].ToString();
            }
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_MUSTERILER WHERE ID=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TXTID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_MUSTERILER SET AD=@p1,SOYAD=@p2,TELEFON=@p3,TELEFON2=@p4,TC=@p5,MAIL=@p6,IL=@p7,ILCE=@p8,VERGIDAIRE=@p9,ADRES=@p10 WHERE ID=@p11", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", adTxt.Text);
            komut.Parameters.AddWithValue("@p2", TxtSoyad.Text);
            komut.Parameters.AddWithValue("@p3", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p4", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p5", MskTC.Text);
            komut.Parameters.AddWithValue("@p6", TxtMail.Text);
            komut.Parameters.AddWithValue("@p7", CmbIl.Text);
            komut.Parameters.AddWithValue("@p8", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p9", TxtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p10", RchAdres.Text);
            komut.Parameters.AddWithValue("@p11", TXTID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Müşteri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
           temizle();
        }
    }
}
