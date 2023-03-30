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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void listele()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_FATURABILGI ", baglan.baglanti());
            DataTable table = new DataTable();
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }

        void temizle()
        {
            TxtAlici.Text="";
            TxtID.Text="";
            TxtSeri.Text="";
            TxtSıraNo.Text="";
            TxtAlan.Text="";
            TxtEden.Text="";
            TxtVergiDaire.Text="";
            MskSaat.Text="";
            MskTarih.Text="";

        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
          
            if (TxtFaturaId.Text == "")
            {

                SqlCommand komut = new SqlCommand("INSERT INTO TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8)", baglan.baglanti());
                komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
                komut.Parameters.AddWithValue("@P2", TxtSıraNo.Text);
                komut.Parameters.AddWithValue("@P3", MskTarih.Text);
                komut.Parameters.AddWithValue("@P4", MskSaat.Text);
                komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
                komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
                komut.Parameters.AddWithValue("@P7", TxtEden.Text);
                komut.Parameters.AddWithValue("@P8", TxtAlan.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Fatura bilgisi sisteme kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            //Firma Carisi
            if (TxtFaturaId.Text != "" && comboBox1.Text == "Firma")
            {
                double miktar, tutar, fiyat;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();
                SqlCommand komut2 = new SqlCommand("INSERT INTO TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) VALUES (@P1,@P2,@P3,@P4,@P5)", baglan.baglanti());
                komut2.Parameters.AddWithValue("@P1", TxtUrunAd.Text);
                komut2.Parameters.AddWithValue("@P2", TxtMiktar.Text);
                komut2.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
                komut2.Parameters.AddWithValue("@P4", decimal.Parse(TxtTutar.Text));
                komut2.Parameters.AddWithValue("@P5", TxtFaturaId.Text);
                komut2.ExecuteNonQuery();
                baglan.baglanti().Close();

                //Hareket Tablosuna Veri Girişi
                SqlCommand komut3 = new SqlCommand("INSERT INTO TBL_FIRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) VALUES (@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8)", baglan.baglanti());
                komut3.Parameters.AddWithValue("@h1", TxtUrunId.Text);
                komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                komut3.Parameters.AddWithValue("@h4", TxtFirma.Text);
                komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                komut3.Parameters.AddWithValue("@h7", TxtFaturaId.Text);
                komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                komut3.ExecuteNonQuery();
                baglan.baglanti().Close();

                //Stok Sayısını Azaltma 
                SqlCommand komut4 = new SqlCommand("UPDATE TBL_URUNLER SET ADET=ADET-@S1 WHERE ID=@S2", baglan.baglanti());
                komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                komut4.Parameters.AddWithValue("@s2", TxtUrunId.Text);
                komut4.ExecuteNonQuery();
                baglan.baglanti().Close();

                MessageBox.Show("Faturaya ait ürün kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }

                //Müşteri Carisi 
                if (TxtFaturaId.Text != "" && comboBox1.Text == "Müşteri")
                {
                    double miktar, tutar, fiyat;
                    fiyat = Convert.ToDouble(TxtFiyat.Text);
                    miktar = Convert.ToDouble(TxtMiktar.Text);
                    tutar = miktar * fiyat;
                    TxtTutar.Text = tutar.ToString();
                    SqlCommand komut2 = new SqlCommand("INSERT INTO TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) VALUES (@P1,@P2,@P3,@P4,@P5)", baglan.baglanti());
                    komut2.Parameters.AddWithValue("@P1", TxtUrunAd.Text);
                    komut2.Parameters.AddWithValue("@P2", TxtMiktar.Text);
                    komut2.Parameters.AddWithValue("@P3", decimal.Parse(TxtFiyat.Text));
                    komut2.Parameters.AddWithValue("@P4", decimal.Parse(TxtTutar.Text));
                    komut2.Parameters.AddWithValue("@P5", TxtFaturaId.Text);
                    komut2.ExecuteNonQuery();
                    baglan.baglanti().Close();

                    //Hareket Tablosuna Veri Girişi
                    SqlCommand komut3 = new SqlCommand("INSERT INTO TBL_MUSTERIHAREKETLER (URUNID,ADET,PERSONEL,MUSTERI,FIYAT,TOPLAM,FATURAID,TARIH) VALUES (@H1,@H2,@H3,@H4,@H5,@H6,@H7,@H8)", baglan.baglanti());
                    komut3.Parameters.AddWithValue("@h1", TxtUrunId.Text);
                    komut3.Parameters.AddWithValue("@h2", TxtMiktar.Text);
                    komut3.Parameters.AddWithValue("@h3", TxtPersonel.Text);
                    komut3.Parameters.AddWithValue("@h4", TxtFirma.Text);
                    komut3.Parameters.AddWithValue("@h5", decimal.Parse(TxtFiyat.Text));
                    komut3.Parameters.AddWithValue("@h6", decimal.Parse(TxtTutar.Text));
                    komut3.Parameters.AddWithValue("@h7", TxtFaturaId.Text);
                    komut3.Parameters.AddWithValue("@h8", MskTarih.Text);
                    komut3.ExecuteNonQuery();
                    baglan.baglanti().Close();


                    //Stok Sayısını Azaltma 
                    SqlCommand komut4 = new SqlCommand("UPDATE TBL_URUNLER SET ADET=ADET-@S1 WHERE ID=@S2", baglan.baglanti());
                    komut4.Parameters.AddWithValue("@s1", TxtMiktar.Text);
                    komut4.Parameters.AddWithValue("@s2", TxtUrunId.Text);
                    komut4.ExecuteNonQuery();
                    baglan.baglanti().Close();

                    MessageBox.Show("Faturaya ait ürün kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    listele();
                }

            
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                TxtID.Text = row["FATURABILGIID"].ToString();
                TxtSeri.Text = row["SERI"].ToString();
                TxtSıraNo.Text = row["SIRANO"].ToString();
                MskTarih.Text = row["TARIH"].ToString();
                MskSaat.Text = row["SAAT"].ToString();
                TxtAlici.Text = row["ALICI"].ToString();
                TxtAlan.Text = row["TESLIMALAN"].ToString();
                TxtEden.Text = row["TESLIMEDEN"].ToString();
                TxtVergiDaire.Text = row["VERGIDAIRE"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM TBL_FATURABILGI WHERE FATURABILGIID=@P1", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Fatura Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Question);
            listele();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_FATURABILGI SET SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 WHERE FATURABILGIID=@P9", baglan.baglanti());
            komut.Parameters.AddWithValue("@P1", TxtSeri.Text);
            komut.Parameters.AddWithValue("@P2", TxtSıraNo.Text);
            komut.Parameters.AddWithValue("@P3", MskTarih.Text);
            komut.Parameters.AddWithValue("@P4", MskSaat.Text);
            komut.Parameters.AddWithValue("@P5", TxtVergiDaire.Text);
            komut.Parameters.AddWithValue("@P6", TxtAlici.Text);
            komut.Parameters.AddWithValue("@P7", TxtEden.Text);
            komut.Parameters.AddWithValue("@P8", TxtAlan.Text);
            komut.Parameters.AddWithValue("@P9", TxtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Fatura bilgisi güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay urundetay = new FrmFaturaUrunDetay();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(row != null)
            {
                urundetay.id = row["FATURABILGIID"].ToString();
            }
            urundetay.Show();
        }

        private void Btnbul_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT URUNAD,SATISFIYAT FROM TBL_URUNLER WHERE ID=@P1", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtUrunId.Text);
            SqlDataReader reader = komut.ExecuteReader();   
            while (reader.Read())
            {
                TxtUrunAd.Text = reader[0].ToString();
                TxtFiyat.Text = reader[1].ToString();
            }
            baglan.baglanti().Close();
        }
    }
}
