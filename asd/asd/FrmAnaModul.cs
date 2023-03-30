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
    public partial class FrmAnaModul : Form
    {
        public FrmAnaModul()
        {
            InitializeComponent();
        }

        FrmUrunler urun;

        private void BtnUrunler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (urun == null || urun.IsDisposed)
            {
                urun = new FrmUrunler();
                urun.MdiParent = this;
                urun.Show();
            }
        }

        FrmMusteriler musteriler;
        private void BtnMusterıler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (musteriler == null || musteriler.IsDisposed)
            {
                musteriler = new FrmMusteriler();
                musteriler.MdiParent = this;
                musteriler.Show();
            }
        }

        FrmFirmalar firma;
        private void BtnFırmalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(firma == null || firma.IsDisposed)
            {
                firma = new FrmFirmalar();
                firma.MdiParent = this;
                firma.Show();
            }
        }

        FrmPersonel personel;
        private void BtnPersoneller_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(personel == null || personel.IsDisposed)
            {
                personel = new FrmPersonel();
                personel.MdiParent = this;
                personel.Show();
            }
        }

        FrmRehber rehber;
        private void BtnRehber_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(rehber == null || rehber.IsDisposed)
            {
                rehber = new FrmRehber();
                rehber.MdiParent =this;
                rehber.Show();
            }
        }

        FrmGiderler giderler;
        private void BtnGıderler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(giderler == null || giderler.IsDisposed)
            {
                giderler= new FrmGiderler();
                giderler.MdiParent=this;
                giderler.Show();
            }
        }

        FrmBankalar banka;
        private void BtnBankalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(banka == null || banka.IsDisposed)
            {
                banka=  new FrmBankalar();
                banka.MdiParent = this;
                banka.Show();
            }
        }

        FrmFaturalar fatura;
        private void BtnFaturalar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(fatura == null || fatura.IsDisposed)
            {
                fatura= new FrmFaturalar();
                fatura.MdiParent = this;
                fatura.Show();
            }
        }

        FrmNotlar not;
        private void BtnNotlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(not == null || not.IsDisposed)
            {
                not = new FrmNotlar();
                not.MdiParent = this;
                not.Show();
            }
        }

        FrmHareketler hareket;
        private void BtnHareketler_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(hareket == null || hareket.IsDisposed)
            {
                hareket = new FrmHareketler();
                hareket.MdiParent = this;
                hareket.Show();
            }
        }

        FrmRaporlar rapor;
        private void BtnRaporlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (rapor == null || rapor.IsDisposed)
            {
                rapor = new FrmRaporlar();
                rapor.MdiParent = this;
                rapor.Show();
            }
        }

        FrmStoklar stok;
        private void BtnStoklar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(stok == null || stok.IsDisposed)
            {
                stok = new FrmStoklar();
                stok.MdiParent = this;
                stok.Show();
            }
        }

        FrmAyarlar ayarlar;
        private void BtnAyarlar_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if(ayarlar==null || ayarlar.IsDisposed)
            {
                ayarlar = new FrmAyarlar();
                ayarlar.Show();
            }
        }

        FrmKasa kasa;
        private void BtnKasa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (kasa == null || kasa.IsDisposed)
            {
                kasa = new FrmKasa();
                kasa.ad=kullanici;
                kasa.MdiParent = this;
                kasa.Show();
            }
        }

        FrmAnaSayfa anasayfa;
        private void BtnAnaSayfa_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (anasayfa == null || anasayfa.IsDisposed)
            {
                anasayfa = new FrmAnaSayfa();
                anasayfa.MdiParent = this;
                anasayfa.Show();
            }
        }

        public string kullanici;
        private void FrmAnaModul_Load(object sender, EventArgs e)
        {
            if (anasayfa == null  )
            {
                anasayfa = new FrmAnaSayfa();
                anasayfa.MdiParent = this;
                anasayfa.Show();
            }
        }
    }
}
