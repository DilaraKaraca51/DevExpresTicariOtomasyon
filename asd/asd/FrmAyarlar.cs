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
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void listele()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_ADMIN",baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            TxtKullanıcıAd.Text="";
            TxtSifre.Text="";
        }

        private void Btnİslem_Click_1(object sender, EventArgs e)
        {
            if (Btnİslem.Text == "Kaydet")
            {
                SqlCommand komut = new SqlCommand("INSERT INTO TBL_ADMIN VALUES (@p1,@p2)", baglan.baglanti());
                komut.Parameters.AddWithValue("@p1", TxtKullanıcıAd.Text);
                komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Yeni Admin Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if (Btnİslem.Text == "Güncelle")
            {
                SqlCommand komut1 = new SqlCommand("UPDATE TBL_ADMIN SET Sifre=@p2 WHERE KullaniciAd=@p1", baglan.baglanti());
                komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
                komut1.Parameters.AddWithValue("@P1", TxtKullanıcıAd.Text);
                komut1.ExecuteNonQuery();
                baglan.baglanti().Close();
                MessageBox.Show("Kayıt Güncellendi", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if ( row != null)
            {
                TxtKullanıcıAd.Text = row["KullaniciAd"].ToString();
                TxtSifre.Text=row["Sifre"].ToString();
            }
        }

        private void TxtKullanıcıAd_TextChanged(object sender, EventArgs e)
        {
            if(TxtKullanıcıAd.Text != "")
            {
                Btnİslem.Text = "Güncelle";
                Btnİslem.BackColor=Color.Red;
            }
            else
            {
                Btnİslem.Text= "Kaydet";
                Btnİslem.BackColor=Color.Green;
            }
        }

       
    }
}
