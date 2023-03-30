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
    public partial class FrmFaturaUrunDetay : Form
    {
        public FrmFaturaUrunDetay()
        {
            InitializeComponent();
        }

        public string id;
        sqlbaglantisi baglan = new sqlbaglantisi();
        
        void listele()
        {
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_FATURADETAY WHERE FATURAID= '" + id + "'", baglan.baglanti());
            DataTable table = new DataTable();
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }
        private void FrmFaturaUrunDetay_Load(object sender, EventArgs e)
        {
            label1.Text = id;
            listele();
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDuzenleme duzen = new FrmFaturaUrunDuzenleme();
            DataRow row = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (row != null)
            {
                duzen.urunid = row["FATURAURUNID"].ToString();
            }
            duzen.Show();
        }
    }
}
