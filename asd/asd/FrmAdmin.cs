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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }

        sqlbaglantisi baglan = new sqlbaglantisi();

        private void button1_MouseHover(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.Yellow;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            BtnGirisYap.BackColor = Color.Red;
        }

        private void BtnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("SELECT * FROM TBL_ADMIN WHERE KullaniciAd=@p1 AND Sifre=@p2", baglan.baglanti());
            komut.Parameters.AddWithValue("@p1", TxtKullanıcıAd.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader reader = komut.ExecuteReader();
            if(reader.Read())
            {
                FrmAnaModul anamodul = new FrmAnaModul();
                anamodul.kullanici=TxtKullanıcıAd.Text;
                anamodul.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı ya da Şifre", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            baglan.baglanti().Close();
        }
    }
}
