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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void giderlistesi()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_GIDERLER ", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource=table;
        }

        void temizle()
        {
            TxtID.Text="";
            CmbAy.Text="";
            CmbYıl.Text="";
            TxtDogalgaz.Text="";
            TxtEkstra.Text="";
            TxtElektrik.Text="";
            TxtInternet.Text="";
            TxtMaaslar.Text="";
            TxtSu.Text="";
            RchNotlar.Text="";
        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            giderlistesi();

            temizle();
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO TBL_GIDERLER  (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) VALUES (@P1,@P2,@P3,@P4,@P5,@P6,@P7,@P8,@P9)",baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Gider tabloya eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            giderlistesi();
            temizle();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(row != null)
            {
                TxtID.Text=row["ID"].ToString();
                CmbAy.Text=row["AY"].ToString();
                CmbYıl.Text=row["YIL"].ToString();
                TxtElektrik.Text=row["ELEKTRIK"].ToString();
                TxtSu.Text=row["SU"].ToString();
                TxtDogalgaz.Text=row["DOGALGAZ"].ToString();
                TxtInternet.Text=row["INTERNET"].ToString();
                TxtMaaslar.Text=row["MAASLAR"].ToString();
                TxtEkstra.Text=row["EKSTRA"].ToString();
                RchNotlar.Text=row["NOTLAR"].ToString();
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand sil = new SqlCommand("DELETE FROM TBL_GIDERLER WHERE ID=@P1", baglan.baglanti());
            sil.Parameters.AddWithValue("@P1", TxtID.Text);
            sil.ExecuteNonQuery();
            baglan.baglanti().Close();
            giderlistesi();
            MessageBox.Show("Gider silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Stop);
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE TBL_GIDERLER SET AY=@P1,YIL=@P2,ELEKTRIK=@P3,SU=@P4,DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 WHERE ID=@P10", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYıl.Text);
            komut.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            komut.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6", decimal.Parse(TxtInternet.Text));
            komut.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaaslar.Text));
            komut.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNotlar.Text);
            komut.Parameters.AddWithValue("@P10", TxtID.Text);
            komut.ExecuteNonQuery();
            baglan.baglanti().Close();
            MessageBox.Show("Gider güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            giderlistesi();
            temizle();
        }
    }
}
