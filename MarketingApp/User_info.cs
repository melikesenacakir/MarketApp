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
    public partial class User_info : Form
    {
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True;MultipleActiveResultSets=True";
        public User_info()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query = "select name_surname,username,birthdate,TelefonNumarasi,Adres from usertbl where role=@rol";
                SqlCommand cmd = new SqlCommand(query, b);
                cmd.Parameters.AddWithValue("@rol", "User");
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                b.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                DialogResult durum = MessageBox.Show("Kullanıcının kaydını silmek istediğinizden emin misiniz?", "Silme Onayı", MessageBoxButtons.YesNo);
                if (DialogResult.Yes == durum)
                {
                    string kayit = "Delete from usertbl WHERE username=@user";
                    SqlCommand cmd = new SqlCommand(kayit, b);
                    cmd.Parameters.AddWithValue("@user", textBox2.Text);
                    cmd.ExecuteNonQuery();
                    b.Close();
                    MessageBox.Show("Kullanıcı silindi");
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string kayit = "update usertbl set name_surname=@namesurname,username=@username,birthdate=@birth,password=@pass,TelefonNumarasi=@telno,Adres=@adres WHERE username=@u";
                SqlCommand cmd= new SqlCommand(kayit, b);
                cmd.Parameters.AddWithValue("@namesurname", textBox1.Text);
                cmd.Parameters.AddWithValue("@username", textBox2.Text);
                cmd.Parameters.AddWithValue("@birth", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@pass", textBox3.Text);
                cmd.Parameters.AddWithValue("@u", textBox2.Text);
                cmd.Parameters.AddWithValue("@telno", maskedTextBox1.Text.ToString());
                cmd.Parameters.AddWithValue("@adres", richTextBox1.Text);
                cmd.ExecuteNonQuery();
                b.Close();
                MessageBox.Show("Kullanıcı Bilgileri Güncellendi.");
            }
        }

        private void button1_Click(object sender, EventArgs e)
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
                        if (Convert.ToInt32(dr["kontrol"].ToString())>0)
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
                            cmd2.Parameters.AddWithValue("@adres", richTextBox1.Text);
                            cmd2.ExecuteNonQuery();
                            MessageBox.Show("Kayıt gerçekleşti");
                        }
                    }

                        
                }

                b.Close();

            }
            else
            {
                MessageBox.Show("Veritabanı açılamadı hata!");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[2].Value.ToString());
            richTextBox1.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            maskedTextBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query = "select password from usertbl where username=@u";
                SqlCommand cmd = new SqlCommand(query, b);
                cmd.Parameters.AddWithValue("@u", textBox2.Text);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    textBox3.Text = dr["password"].ToString();
                }
                else
                {
                    MessageBox.Show("Kullanıcı Bulunamadı!");
                }
                }
            else
            {

                MessageBox.Show("Veritabanı açılamadı hata!");
            }

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void User_info_Load(object sender, EventArgs e)
        {
            textBox3.PasswordChar = '*';
        }
    }
}
