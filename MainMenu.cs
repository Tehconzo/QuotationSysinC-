using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            InitializeComponent();
        }

        private void btn_quote_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Quote = new Form1();
            Quote.Closed += (s, args) => this.Close();
            Quote.Show();
        }

        private void btn_cutlist_Click(object sender, EventArgs e)
        {
            this.Hide();
            var Cutlist = new Cutlist();
            Cutlist.Closed += (s, args) => this.Close();
            Cutlist.Show();
        }

        private void btn_exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void MainMenu_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            //  btn_cutlist.Enabled = false;
            btn_despatch.Enabled = false;
        }

        private void btn_despatch_Click(object sender, EventArgs e)
        {
            
        }  
    }
}
