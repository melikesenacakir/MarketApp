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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace MarketingApp
{
    public partial class Hesap : Form
    {
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True";
        private String username;
        Random random = new Random();
        private int randomnum=21345;
        public Hesap(String username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                if (randomnum.ToString() == textBox3.Text)
                {
                    string kayit = "update usertbl set username=@username,password=@pass,TelefonNumarasi=@telno,Adres=@adres WHERE username=@username";
                    SqlCommand cmd2 = new SqlCommand(kayit, b);
                    cmd2.Parameters.AddWithValue("@username", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@pass", textBox2.Text);
                    cmd2.Parameters.AddWithValue("@telno", maskedTextBox1.Text.ToString());
                    cmd2.Parameters.AddWithValue("@adres", richTextBox1.Text);
                    cmd2.ExecuteNonQuery();
                    MessageBox.Show("Kullanıcı Bilgileri Güncellendi.", "Başarılı", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Hatalı Doğrulama Kodu", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("veritabanı açılamadı");
            }
            b.Close();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                randomnum = random.Next(1000, 9999);
                MessageBox.Show(randomnum.ToString());
            }
        }

        private void Hesap_Load(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from usertbl where username=@user";
                SqlCommand cmd = new SqlCommand(query, b);
                cmd.Parameters.AddWithValue("@user", username);
                cmd.ExecuteNonQuery();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    textBox1.Text = reader["username"].ToString();
                    textBox2.Text = reader["password"].ToString();
                    maskedTextBox1.Text = reader["TelefonNumarasi"].ToString();
                    richTextBox1.Text = reader["Adres"].ToString();
                }
                textBox2.PasswordChar = '*';

                b.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
