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
using System.Drawing;
using System.CodeDom.Compiler;

namespace MarketingApp
{
    public partial class Stok_durum : Form
    {
        public String conString = "Data Source=DESKTOP-BFQR825\\SQLEXPRESS;Initial Catalog=MarketDb;Integrated Security=True;MultipleActiveResultSets=True";
        static int i = 1;
        public Stok_durum()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Stok_durum_Load(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
        }
        private void DrawChart(Graphics g)
        {
            SqlConnection b = new SqlConnection(conString);
            b.Open();

                if (b.State == System.Data.ConnectionState.Open)
                {
                    string query = "select Urun,Amount FROM Stok";
                    SqlCommand cmd = new SqlCommand(query, b);
                    SqlDataReader dr = cmd.ExecuteReader();
                    int x = 120;
                    int y = 300;
                    int width = 30;
                    int gap = 40;
                while (dr.Read()){
                            int height = Convert.ToInt32(dr["Amount"]);
                            Brush brush = Brushes.PowderBlue;
                            g.FillRectangle(brush, x, y - height, width, height);
                            g.DrawRectangle(Pens.Black, x, y - height, width, height);
                            string label = dr["Urun"].ToString();
                            g.DrawString(label, this.Font, Brushes.Black, x + (width / 2) - 16, y + 5);
                            g.DrawString(height.ToString(), this.Font, Brushes.Red, x+5, y - height-15);
                            x += width + gap;
                        }
                }
                else
                {
                    MessageBox.Show("Veritabanı bağlantısı kapalı.");
                }
            }

        private void Stok_durum_Paint(object sender, PaintEventArgs e)
        {
            DrawChart(e.Graphics);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (i % 2 != 0)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
            i++;
            
        }

        private void Stok_durum_Resize(object sender, EventArgs e)
        {
            int x = this.ClientSize.Width - button1.Width;
            int y = 0;
            button1.Location = new System.Drawing.Point(x,y);
        }
    }
    }
