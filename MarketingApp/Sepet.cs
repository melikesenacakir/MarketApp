using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketingApp
{
    public partial class Sepet : Form
    {
        private Basket sepet;
        public Sepet(Basket sepet)
        {
            InitializeComponent();
            this.sepet = sepet;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            decimal price = sepet.Product.Sum(p => p.Fiyat * p.Miktar);
            Payment odeme=new Payment(price,sepet);
            odeme.Show();
        }

        private void Sepet_Load(object sender, EventArgs e)
        {

            dataGridView1.DataSource = sepet.Product;
            decimal toplamFiyat = sepet.Product.Sum(p => p.Fiyat * p.Miktar);
            label1.Text = $"Toplam Fiyat: {toplamFiyat:C}";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Lütfen silmek istediğiniz satırı seçin.");
                return;
            }

            Products silinenurun = (Products)dataGridView1.SelectedRows[0].DataBoundItem;
            sepet.Product.Remove(silinenurun);
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = sepet.Product;
            decimal toplamFiyat = sepet.Product.Sum(p => p.Fiyat * p.Miktar);
            label1.Text = $"Toplam Fiyat: {toplamFiyat:C}";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
        }
    }
}
