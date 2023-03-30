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
using DevExpress.Charts;

namespace asd
{
    public partial class FrmKasa : Form
    {
        public FrmKasa()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void musterihareket()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Execute MusteriHareket", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }

        void firmahareket()
        {
            DataTable table2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter("Execute FirmaHareketler", baglan.baglanti());
            adapter2.Fill(table2);
            gridControl5.DataSource=table2;
        }

        void listele()
        {
            SqlDataAdapter adapter3 = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER", baglan.baglanti());
            DataTable table3 = new DataTable();
            adapter3.Fill(table3);
            gridControl2.DataSource=table3;
        }

        public string ad;
        private void FrmKasa_Load(object sender, EventArgs e)
        {
            lblaktifkullanıcı.Text=ad;
            musterihareket();
            firmahareket();
            listele();

            //Toplam Tutarı Hesaplama
            SqlCommand komut1 = new SqlCommand("SELECT SUM(TUTAR) FROM TBL_FATURADETAY", baglan.baglanti());
            SqlDataReader reader1 = komut1.ExecuteReader();
            while (reader1.Read())
            {
                lbltoplamtutar.Text = reader1[0].ToString() + " TL";
            }
            baglan.baglanti().Close();

            //Son Ayın Faturaları
            SqlCommand komut2 = new SqlCommand("SELECT (ELEKTRIK+SU+DOGALGAZ+INTERNET+EKSTRA) FROM TBL_GIDERLER ORDER BY ID ASC", baglan.baglanti());
            SqlDataReader reader2 = komut2.ExecuteReader();
            while (reader2.Read())
            {
                lblodemeler.Text = reader2[0].ToString() + " TL";
            }
            baglan.baglanti().Close();

            //Son Ayın Personel Maaşları
            SqlCommand komut3 = new SqlCommand("SELECT MAASLAR FROM TBL_GIDERLER ORDER BY ID ASC", baglan.baglanti());
            SqlDataReader reader3 = komut3.ExecuteReader();
            while (reader3.Read())
            {
                lblpersonelmaas.Text = reader3[0].ToString() + " TL";
            }
            baglan.baglanti().Close();

            //Toplam Müşteri Sayısı
            SqlCommand komut4 = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", baglan.baglanti());
            SqlDataReader reader4 = komut4.ExecuteReader();
            while (reader4.Read())
            {
                lblmusterisayısı.Text = reader4[0].ToString() ;
            }
            baglan.baglanti().Close();

            //Toplam Firma Sayısı 
            SqlCommand komut5 = new SqlCommand("SELECT COUNT(*) FROM TBL_MUSTERILER", baglan.baglanti());
            SqlDataReader reader5 = komut5.ExecuteReader();
            while (reader5.Read())
            {
                lblfirmasayısı.Text = reader5[0].ToString() ;
            }
            baglan.baglanti().Close();

            //Toplam Firma Şehir Sayısı
            SqlCommand komut6 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_FIRMALAR", baglan.baglanti());
            SqlDataReader reader6 = komut6.ExecuteReader();
            while (reader6.Read())
            {
                lblfirmasehirsayısı.Text = reader6[0].ToString();
            }
            baglan.baglanti().Close();

            //Toplam Müşteri Şehir Sayısı
            SqlCommand komut7 = new SqlCommand("SELECT COUNT(DISTINCT(IL)) FROM TBL_MUSTERILER", baglan.baglanti());
            SqlDataReader reader7 = komut7.ExecuteReader();
            while (reader7.Read())
            {
                lblmusterisehirsayisi.Text = reader7[0].ToString();
            }
            baglan.baglanti().Close();

            //Toplam Personel Sayısı
            SqlCommand komut8 = new SqlCommand("SELECT COUNT(*) FROM TBL_PERSONELLER", baglan.baglanti());
            SqlDataReader reader8 = komut8.ExecuteReader();
            while (reader8.Read())
            {
                lblpersonelsayısı.Text = reader8[0].ToString();
            }
            baglan.baglanti().Close();

            //Toplam Ürün Sayısı
            SqlCommand komut9 = new SqlCommand("SELECT SUM(ADET) FROM TBL_URUNLER", baglan.baglanti());
            SqlDataReader reader9 = komut9.ExecuteReader();
            while (reader9.Read())
            {
                lblstoksayısı.Text = reader9[0].ToString();
            }
            baglan.baglanti().Close();
        }

        int sayac = 0;
        private void timer1_Tick(object sender, EventArgs e)
        {
            sayac++;
            //ELEKTRİK
            if (sayac > 0 && sayac <= 5)
            {
                groupControl16.Text="ELEKTRİK";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader10 = komut10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                baglan.baglanti().Close();
            }
            //SU
            if (sayac > 5 && sayac <=10)
            {
                groupControl16.Text="SU";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader10 = komut10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                baglan.baglanti().Close();
            }
            //DOĞALGAZ
            if (sayac > 10 && sayac <=15)
            {
                groupControl16.Text="DOĞALGAZ";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader10 = komut10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                baglan.baglanti().Close();
            }
            //INTERNET
            if (sayac > 15 && sayac <=20)
            {
                groupControl16.Text="İNTERNET";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader10 = komut10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                baglan.baglanti().Close();
            }
            //EKSTRA
            if (sayac > 20 && sayac <=25)
            {
                groupControl16.Text="EKSTRA";
                chartControl1.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader10 = komut10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl1.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                baglan.baglanti().Close();
            }
            if(sayac == 26)
            {
                sayac = 0;
            }
        }

        int sayac2;
        private void timer2_Tick(object sender, EventArgs e)
        {
            sayac2++;
            //ELEKTRİK
            if (sayac2 > 0 && sayac2 <= 5)
            {
                groupControl17.Text="ELEKTRİK";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut10 = new SqlCommand("SELECT TOP 4 AY,ELEKTRIK FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader10 = komut10.ExecuteReader();
                while (reader10.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader10[0], reader10[1]));
                }
                baglan.baglanti().Close();
            }
            //SU
            if (sayac2 > 5 && sayac2 <=10)
            {
                groupControl17.Text="SU";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut11 = new SqlCommand("SELECT TOP 4 AY,SU FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader11 = komut11.ExecuteReader();
                while (reader11.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader11[0], reader11[1]));
                }
                baglan.baglanti().Close();
            }
            //DOĞALGAZ
            if (sayac2 > 10 && sayac2 <=15)
            {
                groupControl17.Text="DOĞALGAZ";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut12 = new SqlCommand("SELECT TOP 4 AY,DOGALGAZ FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader12 = komut12.ExecuteReader();
                while (reader12.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader12[0], reader12[1]));
                }
                baglan.baglanti().Close();
            }
            //INTERNET
            if (sayac2 > 15 && sayac2 <=20)
            {
                groupControl17.Text="İNTERNET";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut13 = new SqlCommand("SELECT TOP 4 AY,INTERNET FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader13 = komut13.ExecuteReader();
                while (reader13.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader13[0], reader13[1]));
                }
                baglan.baglanti().Close();
            }
            //EKSTRA
            if (sayac2 > 20 && sayac2 <=25)
            {
                groupControl17.Text="EKSTRA";
                chartControl2.Series["AYLAR"].Points.Clear();
                SqlCommand komut14 = new SqlCommand("SELECT TOP 4 AY,EKSTRA FROM TBL_GIDERLER ORDER BY ID DESC", baglan.baglanti());
                SqlDataReader reader14 = komut14.ExecuteReader();
                while (reader14.Read())
                {
                    chartControl2.Series["AYLAR"].Points.Add(new DevExpress.XtraCharts.SeriesPoint(reader14[0], reader14[1]));
                }
                baglan.baglanti().Close();
            }
            if (sayac2 == 26)
            {
                sayac2 = 0;
            }
        }
    }
}
