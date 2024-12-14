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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        UrunYonetimiEntities urunYonetimi = new UrunYonetimiEntities();

        private void Form2_Load(object sender, EventArgs e)
        {
            Yukle();
        }

        void Yukle()
        {
            dgvKategoriListesi.DataSource = urunYonetimi.Categories.ToList();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            urunYonetimi.Categories.Add(
                new Categories
                {
                    KategoriAdi= txtKategoriAdi.Text
                }
                );
            var sonuc = urunYonetimi.SaveChanges();
            Yukle();
            if (sonuc > 0)
            {
                MessageBox.Show("Kategori Eklendi!");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvKategoriListesi.CurrentRow.Cells[0].Value);
            var kayit = urunYonetimi.Categories.Find(id);

            kayit.KategoriAdi = txtKategoriAdi.Text;

            var sonuc = urunYonetimi.SaveChanges();
            Yukle();
            if (sonuc > 0)
            {
                MessageBox.Show("Kategori Güncellendi!");
            }
        }

        private void dgvKategoriListesi_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int id = Convert.ToInt32(dgvKategoriListesi.CurrentRow.Cells[0].Value);
            var kayit = urunYonetimi.Categories.Find(id);

            txtKategoriAdi.Text = kayit.KategoriAdi;
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt32(dgvKategoriListesi.CurrentRow.Cells[0].Value);
            var kayit = urunYonetimi.Categories.Find(id);

            urunYonetimi.Categories.Remove(kayit);
            var sonuc = urunYonetimi.SaveChanges();
            Yukle();
            if (sonuc > 0)
            {
                MessageBox.Show("Kategori Silindi!");
            }
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            dgvKategoriListesi.DataSource = urunYonetimi.Categories.Where(c => c.KategoriAdi.Contains(txtAra.Text)).ToList();
        }

        private void txtAra_TextChanged(object sender, EventArgs e)
        {
            dgvKategoriListesi.DataSource = urunYonetimi.Categories.Where(c => c.KategoriAdi.Contains(txtAra.Text)).ToList();
        }
    }
}
