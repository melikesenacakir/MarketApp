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

namespace MarketingApp
{
    public partial class Login_Page : Form

    {
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True";
        
        public Login_Page()
        {
            InitializeComponent();
           
        }

        private void login_buton_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query = "select * from usertbl where username=@user AND password=@pass AND role=@rol";
                SqlCommand cmd = new SqlCommand(query, b);
                cmd.Parameters.AddWithValue("@user", textBox1.Text);
                cmd.Parameters.AddWithValue("@pass", textBox2.Text);
                if (radioButton1.Checked)
                {
                    cmd.Parameters.AddWithValue("@rol",radioButton1.Text);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@rol", radioButton2.Text);
                }
                
                SqlDataReader dr = cmd.ExecuteReader();

                if (dr.Read())
                {
                    String temp = textBox1.Text;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    if (radioButton1.Checked)
                    {
                        İndex index = new İndex(temp);
                        index.Show();
                        radioButton1.Checked = false;
                    }
                    else
                    {
                        admin index = new admin();
                        index.Show();
                        radioButton2.Checked = false;
                    }
                }
                else
                {
                    MessageBox.Show("Geçerli bilgi giriniz!", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                b.Close();

            }
            else
            {
                MessageBox.Show("Veritabanı açılamadı hata!");
            }
        }

        private void Login_Page_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            textBox2.PasswordChar = '*';
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Register_Page register = new Register_Page();
            register.Show();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
