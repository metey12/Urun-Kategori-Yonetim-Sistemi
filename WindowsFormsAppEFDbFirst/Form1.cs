using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsAppEFDbFirst
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        UrunYonetimiEntities urunYonetimi = new UrunYonetimiEntities();

        private void Form1_Load(object sender, EventArgs e)
        {
            Yukle();
        }

        void Yukle()
        {
            dgvUrunListesi.DataSource = urunYonetimi.Products.ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            urunYonetimi.Products.Add(new Products
            {
                UrunAdi = txtUrunAdi.Text,
                StokMiktari = Convert.ToInt32(txtStokMiktari.Text),
                UrunFiyati = Convert.ToDecimal(txtUrunFiyati.Text)
            });
            var sonuc = urunYonetimi.SaveChanges();
            Yukle();
            if (sonuc > 0)
            {
                MessageBox.Show("Ürün Eklendi!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvUrunListesi.CurrentRow.Cells[0].Value);
            var kayit = urunYonetimi.Products.Find(id);

            kayit.UrunFiyati=Convert.ToDecimal(txtUrunFiyati.Text);
            kayit.UrunAdi=txtUrunAdi.Text;
            kayit.StokMiktari=Convert.ToInt32(txtStokMiktari.Text);

            var sonuc = urunYonetimi.SaveChanges();
            Yukle();
            if (sonuc > 0)
            {
                MessageBox.Show("Ürün Güncellendi!");
            }
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvUrunListesi.CurrentRow.Cells[0].Value);
            var kayit = urunYonetimi.Products.Find(id);

            urunYonetimi.Products.Remove(kayit);

            var sonuc = urunYonetimi.SaveChanges();
            Yukle();
            if (sonuc > 0)
            {
                MessageBox.Show("Ürün Silindi!");
            }
        }

        private void dgvUrunListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvUrunListesi.CurrentRow.Cells[0].Value);
            var kayit = urunYonetimi.Products.Find(id);

            txtStokMiktari.Text = kayit.StokMiktari.ToString();
            txtUrunAdi.Text = kayit.UrunAdi;
            txtUrunFiyati.Text=kayit.UrunFiyati.ToString();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dgvUrunListesi.DataSource = urunYonetimi.Products.Where(u => u.UrunAdi.Contains(txtAra.Text)).ToList();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            dgvUrunListesi.DataSource = urunYonetimi.Products.Where(u => u.UrunAdi.Contains(txtAra.Text)).ToList();
        }
    }
}
