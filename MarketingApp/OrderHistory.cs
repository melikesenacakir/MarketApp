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
    public partial class OrderHistory : Form
    {
        private string username;
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True;MultipleActiveResultSets=True";
        public OrderHistory(string username)
        {
            InitializeComponent();
            this.username = username;
        }

        private void OrderHistory_Load(object sender, EventArgs e)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();
            if (b.State == System.Data.ConnectionState.Open)
            {
                string query3 = "select id from usertbl where username=@username";
                SqlCommand cmd3 = new SqlCommand(query3, b);
                cmd3.Parameters.AddWithValue("@username", username);
                SqlDataReader dr2 = cmd3.ExecuteReader();
                if (dr2.Read())
                {
                    string query = "select OrderedProducts,Order_date,price,amount from OrderHistory where UserID=@id";
                    SqlCommand cmd = new SqlCommand(query, b);
                    cmd.Parameters.AddWithValue("@id", dr2["id"].ToString());
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                    if (dataGridView1[2, 0].Value != null && dataGridView1[2, 0].Value.ToString() != String.Empty)
                    {
                        dataGridView1.Show();
                        label2.Visible = false;

                    }
                    else
                    {
                        dataGridView1.Hide();
                        label2.Visible = true;
                        
                    }

                    }
                b.Close();
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
