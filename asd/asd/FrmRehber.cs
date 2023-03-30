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
    public partial class FrmRehber : Form
    {
        public FrmRehber()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();
        private void FrmRehber_Load(object sender, EventArgs e)
        { 
            //Müşteri
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT AD,SOYAD,TELEFON,TELEFON2,MAIL FROM TBL_MUSTERILER",baglan.baglanti());
            adapter.Fill(table);
            gridControl4.DataSource = table;

            //Firma
            DataTable table2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT Ad,YETKILIADSOYAD,telefon1,telefon2,telefon3,MAIL,FAX FROM TBL_FIRMALAR",baglan.baglanti());
            adapter2.Fill(table2);
            gridControl3.DataSource=table2;
        }

        private void gridView4_DoubleClick(object sender, EventArgs e)
        {
            FrmMail mail = new FrmMail();
            DataRow row = gridView4.GetDataRow(gridView4.FocusedRowHandle);
            if (row != null)
            {
                mail.mail=row["MAIL"].ToString();
            }
            mail.Show();
        }

        private void gridView3_DoubleClick(object sender, EventArgs e)
        {
            FrmMail mail = new FrmMail();
            DataRow row = gridView3.GetDataRow(gridView3.FocusedRowHandle);
            if (row != null)
            {
                mail.mail=row["MAIL"].ToString();
            }
            mail.Show();
        }
    }
}
