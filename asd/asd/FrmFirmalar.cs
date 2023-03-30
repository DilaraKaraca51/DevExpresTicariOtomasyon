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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void firmalistesi()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_FIRMALAR", baglan.baglanti());
            DataTable table = new DataTable();
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }

        void sehirlistesi()
        {
            SqlCommand komut = new SqlCommand("SELECT SEHIR FROM TBL_ILLER", baglan.baglanti());
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                CmbIl.Properties.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        void carikodaciklamalar()
        {
            SqlCommand komut = new SqlCommand("SELECT FIRMAKOD1 FROM TBL_KODLAR",baglan.baglanti());
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                RchKod1.Text = reader[0].ToString();
            }
            baglan.baglanti().Close();
        }
        void temizle()
        {
            TxtAd.Text="";
            TxtID.Text="";
            TxtKod1.Text="";
            TxtKod2.Text="";
            TxtKod3.Text="";
            TxtMail.Text="";
            TxtSektor.Text="";
            TxtVergiDairesi.Text="";
            TxtYetkili.Text="";
            TxtYetkiliGorev.Text="";
            MskFax.Text="";
            MskTC.Text="";
            MskTelefon.Text="";
            MskTelefon2.Text="";
            MskTelefon3.Text="";
            RchAdres.Text="";
            TxtAd.Focus();
        }
        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();

            sehirlistesi();

            carikodaciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(row != null)
            {
                TxtID.Text = row["ID"].ToString();
                TxtAd.Text = row["AD"].ToString();
                TxtYetkiliGorev.Text = row["YETKILISTATU"].ToString();
                TxtYetkili.Text = row["YETKILIADSOYAD"].ToString();
                MskTC.Text = row["YETKILITC"].ToString();
                TxtSektor.Text = row["SEKTOR"].ToString();
                MskTelefon.Text = row["TELEFON1"].ToString();
                MskTelefon2.Text = row["TELEFON2"].ToString();
                MskTelefon3.Text = row["TELEFON3"].ToString();
                TxtMail.Text = row["MAIL"].ToString();
                MskFax.Text = row["FAX"].ToString();
                CmbIl.Text = row["IL"].ToString();
                CmbIlce.Text = row["ILCE"].ToString();
                TxtVergiDairesi.Text= row["VERGIDAIRE"].ToString();
                RchAdres.Text= row["ADRES"].ToString();
                TxtKod1.Text = row["OZELKOD1"].ToString();
                TxtKod2.Text = row["OZELKOD2"].ToString();
                TxtKod3.Text = row["OZELKOD3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) VALUES (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", MskTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", CmbIl.Text);
            komut.Parameters.AddWithValue("@p12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p14", RchAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti();Close();
            MessageBox.Show("Firma Sisteme Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
        }

        private void CmbIl_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbIlce.Properties.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT ILCE FROM TBL_ILCELER WHERE SEHIR=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbIl.SelectedIndex + 1);
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                CmbIlce.Properties.Items.Add(reader[0]);
            }
            baglan.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_FIRMALAR WHERE ID=@p1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            firmalistesi();
            MessageBox.Show("Firma Listeden Silindi", "bilgi", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FIRMALAR SET AD=@p1,YETKILISTATU=@p2,YETKILIADSOYAD=@p3,YETKILITC=@p4,SEKTOR=@p5,TELEFON=@p6,TELEFON2=@p7,TELEFON3=@p8,MAIL=@p9,IL=@p11,ILCE=@p12,FAX=@p10,VERGIDAIRE=@p13,ADRES=@p14,OZELKOD1=@p15,OZELKOD2=@p16,OZELKOD3=@p17 WHERE ID=@p18", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtYetkiliGorev.Text);
            komut.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p4", MskTC.Text);
            komut.Parameters.AddWithValue("@p5", TxtSektor.Text);
            komut.Parameters.AddWithValue("@p6", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p7", MskTelefon2.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon3.Text);
            komut.Parameters.AddWithValue("@p9", TxtMail.Text);
            komut.Parameters.AddWithValue("@p10", MskFax.Text);
            komut.Parameters.AddWithValue("@p11", CmbIl.Text);
            komut.Parameters.AddWithValue("@p12", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p13", TxtVergiDairesi.Text);
            komut.Parameters.AddWithValue("@p14", RchAdres.Text);
            komut.Parameters.AddWithValue("@p15", TxtKod1.Text);
            komut.Parameters.AddWithValue("@p16", TxtKod2.Text);
            komut.Parameters.AddWithValue("@p17", TxtKod3.Text);
            komut.Parameters.AddWithValue("@p18", TxtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti(); Close();
            MessageBox.Show("Firma Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }
    }
    
}
