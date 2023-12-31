using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;

namespace MarketingApp
{
    public partial class İndex : Form
    {
        private String username;
        private Basket sepet = new Basket();
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True";
        public İndex(String username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void Sepetim(String amount, String product_name)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from Stok where Urun=@name";
                SqlCommand cmd = new SqlCommand(query, b);
                cmd.Parameters.AddWithValue("@name", product_name);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    Products product = new Products
                    {
                        UrunId = Convert.ToInt32(dr["id"]),
                        UrunAdi = dr["Urun"].ToString(),
                        Fiyat = Convert.ToInt32(dr["Price"]),
                        Miktar = Convert.ToInt32(amount),
                    };
                    if (product.Miktar > Convert.ToInt32(dr["Amount"]))
                    {
                        MessageBox.Show("En fazla " + dr["Amount"].ToString() + " ürün alınabilir!");
                    }
                    else
                    {
                        sepet.AddOrUpdateProduct(product);
                        MessageBox.Show("Ürün sepete eklendi");
                    }

                }
                else
                {
                    MessageBox.Show("Ürün eklenemedi!");
                }
            }
        }


        private void Form3_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oturumunuz sonlandırılmıştır!");
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Sepet sepett = new Sepet(sepet);
            sepett.Show();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Hesap hesap = new Hesap(username);
            hesap.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Sepetim(textBox1.Text, label2.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Sepetim(textBox6.Text, label3.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Sepetim(textBox3.Text, label4.Text);
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Sepetim(textBox4.Text, label5.Text);
        }
        private void button11_Click(object sender, EventArgs e)
        {
            Sepetim(textBox9.Text, label6.Text);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sepetim(textBox5.Text, label7.Text);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Sepetim(textBox2.Text, label8.Text);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Sepetim(textBox7.Text, label9.Text);
        }
        private void button10_Click(object sender, EventArgs e)
        {
            Sepetim(textBox8.Text, label10.Text);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Sepetim(textBox10.Text, label11.Text);
        }
    }
}