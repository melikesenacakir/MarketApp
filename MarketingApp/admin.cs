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
    public partial class admin : Form
    {
        public admin()
        {
            InitializeComponent();
        }

        private void admin_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Oturumunuz sonlandırılmıştır!");
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            User_info kullanici=new User_info();
            kullanici.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stok_durum stok = new Stok_durum();
            stok.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            EarningChart earn = new EarningChart();
            earn.Show();
        }
    }
}
