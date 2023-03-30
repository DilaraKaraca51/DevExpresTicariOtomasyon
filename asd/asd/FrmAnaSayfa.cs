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
using System.Xml;

namespace asd
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();
        void stoklar()
        {
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter("Select URUNAD,Sum(ADET) as 'ADET' From TBL_URUNLER group by URUNAD\r\nhaving Sum(ADET)<=20 order by Sum(Adet)", baglan.baglanti());
            adapter.Fill(table);
            gridControlStoklar.DataSource = table;
        }

        void ajanda()
        {
            DataTable table2 = new DataTable();
            SqlDataAdapter adapter2 = new SqlDataAdapter("SELECT TOP 5 TARIH,SAAT,BASLIK FROM TBL_NOTLAR ORDER BY ID DESC", baglan.baglanti());
            adapter2.Fill(table2);
            gridControlAjanda.DataSource = table2;
        }

        void hareketler()
        {
            DataTable table3 = new DataTable();
            SqlDataAdapter adapter3 = new SqlDataAdapter(" EXEC FİRMAHAREKET2", baglan.baglanti());
            adapter3.Fill(table3);
            gridControlHareketler.DataSource = table3;
        }

        void fihrist()
        {
            DataTable table4 = new DataTable();
            SqlDataAdapter adapter4 = new SqlDataAdapter("SELECT AD,TELEFON1 FROM TBL_FIRMALAR",baglan.baglanti());
            adapter4.Fill(table4);
            gridControlFihrist.DataSource = table4;
        }

        void haberler()
        {
            XmlTextReader xmloku = new XmlTextReader("https://www.hurriyet.com.tr/rss/anasayfa");
            while(xmloku.Read())
            {
                if(xmloku.Name == "title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        }

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            stoklar();
            ajanda();
            hareketler();
            fihrist();
            webBrowser1.Navigate("https://www.tcmb.gov.tr/wps/wcm/connect/tr/tcmb+tr/main+page+site+area/bugun");
            haberler();
        }

        
    }
}
