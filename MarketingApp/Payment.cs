using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MarketingApp
{
    public partial class Payment : Form
    {
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True;MultipleActiveResultSets=True";
        private Basket sepet;
        private decimal price;
        private string username;
        public Payment(decimal price, Basket sepet, string username)
        {
            InitializeComponent();
            this.price = price;
            this.username = username;
            textBox3.Text = price.ToString();
            textBox1.Text = username;
            this.sepet = sepet;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
         private void AddEarning()
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                DateTime time = DateTime.Now;
                string query = "select TOP 1 Kazanç from Earnings ORDER BY Tarih DESC";
                SqlCommand cmd4 = new SqlCommand(query, b);
                SqlDataReader dr2 = cmd4.ExecuteReader();
                    if (dr2.Read())
                    {
                        string query1 = "insert into Earnings values(@earn,@date)";
                        SqlCommand cmd1 = new SqlCommand(query1, b);
                        cmd1.Parameters.AddWithValue("@earn", Convert.ToDouble(dr2["Kazanç"]) + Convert.ToDouble(price));
                        cmd1.Parameters.AddWithValue("@date", time);
                        cmd1.ExecuteNonQuery();
                    }
                    else
                    {
                        string query1 = "insert into Earnings values(@earn,@date)";
                        SqlCommand cmd1 = new SqlCommand(query1, b);
                        cmd1.Parameters.AddWithValue("@earn", Convert.ToDouble(price));
                        cmd1.Parameters.AddWithValue("@date", time);
                        cmd1.ExecuteNonQuery();
                    }

                sepet.Product.Clear();
                MessageBox.Show("Ödeme Tamamlandı. Saat 19.00' da getireceğiz :)");
                b.Close();
            }
            else
            {
                MessageBox.Show("Veritabanı açılamadı!");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from Stok";
                SqlCommand cmd = new SqlCommand(query, b);
                SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        foreach (var product in sepet.Product)
                        {
                            int alinanmiktar = Convert.ToInt32(dr["Amount"]) - product.Miktar;
                            if (product.UrunId == Convert.ToInt32(dr["id"]))
                            {
                                string query2 = "update Stok set Amount=@amount where Urun=@urun";
                                SqlCommand cmd2 = new SqlCommand(query2, b);
                                cmd2.Parameters.AddWithValue("@amount", alinanmiktar);
                                cmd2.Parameters.AddWithValue("@urun",product.UrunAdi);
                                cmd2.ExecuteNonQuery();
                            }
                    }
                }
                    dr.Close();
                foreach (var product in sepet.Product)
                {
                    string query3 = "select id from usertbl where username=@username";
                    SqlCommand cmd3 = new SqlCommand(query3, b);
                    cmd3.Parameters.AddWithValue("@username", username);
                    SqlDataReader dr2 = cmd3.ExecuteReader();
                    if (dr2.Read())
                    {
                        DateTime date = DateTime.Now;
                        string query4 = "insert into OrderHistory values (@order,@userid,@date,@price,@amount)";
                        SqlCommand cmd4 = new SqlCommand(query4, b);
                        cmd4.Parameters.AddWithValue("@order", product.UrunAdi);
                        cmd4.Parameters.AddWithValue("@userid", dr2["id"].ToString());
                        cmd4.Parameters.AddWithValue("@date",date);
                        cmd4.Parameters.AddWithValue("@price", product.Fiyat);
                        cmd4.Parameters.AddWithValue("@amount", product.Miktar);
                        cmd4.ExecuteNonQuery();
                    }
                    else
                    {
                        
                    }
                    dr2.Close();
                }

                AddEarning();
                b.Close();
                this.Close();
            }else
            {
                MessageBox.Show("Veritabanı bağlantısı kapalı.");
            }

        }
    }
}
