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
using DevExpress.Data.Linq.Helpers;

namespace asd
{
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void listele()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_BANKALAR ",baglan.baglanti());
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

        void firmalistesi()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT ID,AD FROM TBL_FIRMALAR", baglan.baglanti());
            adapter.Fill(table);
            lookUpEditFirma.Properties.NullText = "Lütfen bir ad seçiniz";
            lookUpEditFirma.Properties.ValueMember = "ID";
            lookUpEditFirma.Properties.DisplayMember = "AD";
            lookUpEditFirma.Properties.DataSource = table;

        }

        void temizle()
        {
            TxtBankaAd.Text="";
            TxtHesapTuru.Text="";
            TxtHesapNo.Text="";
            TxtIban.Text="";
            TXTID.Text="";
            TxtSube.Text="";
            TxtYetkili.Text="";
            MskTarih.Text="";
            MskTelefon.Text="";
            lookUpEditFirma.Text="";
        }
        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            firmalistesi();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_BANKALAR(BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TELEFON,TARIH,HESAPTURU,FIRMAID) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9,@P10,@P11)", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", CmbIl.Text);
            komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEditFirma.EditValue);
            komut.ExecuteNonQuery();
            listele();
            baglan.baglanti().Close();
            MessageBox.Show("Banka bilgisi kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                TXTID.Text=row["ID"].ToString();
                TxtBankaAd.Text=row["BANKAADI"].ToString();
                CmbIl.Text=row["IL"].ToString();
                CmbIlce.Text=row["ILCE"].ToString();
                TxtSube.Text=row["SUBE"].ToString();
                TxtIban.Text=row["IBAN"].ToString();
                TxtHesapNo.Text=row["HESAPNO"].ToString();
                TxtYetkili.Text=row["YETKILI"].ToString();
                MskTelefon.Text=row["TELEFON"].ToString();
                MskTarih.Text=row["TARIH"].ToString();
                TxtHesapTuru.Text=row["HESAPTURU"].ToString();
               
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("DELETE FROM TBL_BANKALAR WHERE ID=@P1", baglan.baglanti());
            sil.Parameters.AddWithValue("@P1", TXTID.Text);
            sil.ExecuteNonQuery();
            baglan.baglanti().Close();
            temizle();
            MessageBox.Show("Banka bilgisi silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_BANKALAR SET BANKAADI=@P1,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TELEFON=@P8,TARIH=@P9,HESAPTURU=@P10,FIRMAID=@P11 WHERE ID=@P12", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            komut.Parameters.AddWithValue("@p2", CmbIl.Text);
            komut.Parameters.AddWithValue("@p3", CmbIlce.Text);
            komut.Parameters.AddWithValue("@p4", TxtSube.Text);
            komut.Parameters.AddWithValue("@p5", TxtIban.Text);
            komut.Parameters.AddWithValue("@p6", TxtHesapNo.Text);
            komut.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            komut.Parameters.AddWithValue("@p8", MskTelefon.Text);
            komut.Parameters.AddWithValue("@p9", MskTarih.Text);
            komut.Parameters.AddWithValue("@p10", TxtHesapTuru.Text);
            komut.Parameters.AddWithValue("@p11", lookUpEditFirma.EditValue);
            komut.Parameters.AddWithValue("@p12", TXTID.EditValue);
            komut.ExecuteNonQuery();
            listele();
            baglan.baglanti().Close();
            MessageBox.Show("Banka bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        
        }
    }
}
