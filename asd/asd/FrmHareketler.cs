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
    public partial class FrmHareketler : Form
    {
        public FrmHareketler()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        void FirmaHareketleri()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Exec FirmaHareketler",baglan.baglanti());
            adapter.Fill(table);
            gridControl2.DataSource=table;
        }

        void MusteriHareketleri()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Exec MusteriHareket", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource=table;
        }
        private void FrmHareketler_Load(object sender, EventArgs e)
        {
            FirmaHareketleri();
            MusteriHareketleri();
        }
    }
}
