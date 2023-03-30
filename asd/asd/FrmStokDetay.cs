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
    public partial class FrmStokDetay : Form
    {
        public FrmStokDetay()
        {
            InitializeComponent();
        }
        sqlbaglantisi baglan = new sqlbaglantisi();
        public string ad;
        private void FrmStokDetay_Load(object sender, EventArgs e)
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM TBL_URUNLER WHERE URUNAD='"+ad+"'", baglan.baglanti());
            adapter.Fill(table);
            gridControl1.DataSource = table;
        }
    }
}
