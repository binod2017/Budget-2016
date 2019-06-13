using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Budget_2016
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUser.Text.ToLower() == "admin" && txtPassword.Text == "admin")
            {
                Earning f2 = new Earning();
                f2.ShowDialog();                 
            }
            else
            {
                DailyExpenseCalendaar f1 = new DailyExpenseCalendaar();
                f1.Show();                
            }
            this.Hide();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUser.Text = txtPassword.Text = "";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            TaxCalculator tax = new TaxCalculator();
            tax.Show();
            this.Hide();
        }
    }
}
