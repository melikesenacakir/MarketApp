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

namespace MarketingApp
{
    public partial class Register_Page : Form
    {
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True";

        public Register_Page()
        {
            InitializeComponent();
          
        }

        private void login_buton_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {

                if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "")
                {
                    MessageBox.Show("Bilgiler boş geçilemez");
                }
                else
                {
                    string query = "select COUNT(*) AS kontrol from usertbl where username=@username";
                    SqlCommand cmd = new SqlCommand(query, b);
                    cmd.Parameters.AddWithValue("@username", textBox2.Text);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.Read())
                    {
                        if (Convert.ToInt32(dr["kontrol"].ToString()) > 0)
                        {
                            MessageBox.Show("Kullanıcı adı kullanımda!");
                        }
                        else
                        {
                            string query2 = "insert into usertbl values(@namesurname,@username,@birth,@pass,@role,@tel,@adres)";
                            SqlCommand cmd2 = new SqlCommand(query2, b);
                            cmd2.Parameters.AddWithValue("@namesurname", textBox1.Text);
                            cmd2.Parameters.AddWithValue("@username", textBox2.Text);
                            cmd2.Parameters.AddWithValue("@birth", dateTimePicker1.Value);
                            cmd2.Parameters.AddWithValue("@pass", textBox3.Text);
                            cmd2.Parameters.AddWithValue("@role", "user");
                            cmd2.Parameters.AddWithValue("@tel", maskedTextBox1.Text.ToString());
                            cmd2.Parameters.AddWithValue("@adres", textBox4.Text);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Kayıt gerçekleşti");
                            b.Close();
                            this.Close();
                        }
                    }
                    b.Close();
                }

            }
            else
            {
                MessageBox.Show("Veritabanı açılamadı hata!");
            }
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            textBox3.PasswordChar = '*';
        }

      void button1_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
