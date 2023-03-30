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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();
        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 8);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Adana",5);

            SqlDataAdapter adapter = new SqlDataAdapter("Select UrunAd,Sum(Adet) As 'Miktar' from Tbl_URUNLER group by UrunAd", baglan.baglanti());
            DataTable table = new DataTable();
            adapter.Fill(table);
            gridControl1.DataSource = table;

            //Chart Stok Miktarı Listeleme
            SqlCommand komut = new SqlCommand("Select UrunAd,Sum(Adet) As 'Miktar' from Tbl_URUNLER group by UrunAd", baglan.baglanti());
            SqlDataReader reader = komut.ExecuteReader();
            while (reader.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(reader[0]),int.Parse(reader[1].ToString()));
            }
            baglan.baglanti().Close();
        }

        private void gridControl1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay stokdetay = new FrmStokDetay();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                stokdetay.ad = row["URUNAD"].ToString();
            }
            stokdetay.Show();
        }
       
    }
}
