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
    public partial class TaxBenefits : Form
    {
        // add a delegate
        public delegate void BenefitUpdateHandler(object sender, BenefitUpdateEventArgs e);
        // add an event of the delegate type
        public event BenefitUpdateHandler benefitUpdated;
        public TaxBenefits()
        {
            InitializeComponent();
            this.Text = "Tax Savings / Benefits";
        }

        private void s3_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //MessageBox.Show(e.KeyChar.ToString());
            }
            else { e.Handled = true; s3.Focus(); }
        }
        int _s1, _s2, _s3, _s4, _s5, _s6, _s7, _s8, _s9;
        private void s3_Leave(object sender, EventArgs e)
        {
            _s1 = int.Parse(s1.Text.Trim());
            _s2 = int.Parse(s2.Text);
            _s3 = int.Parse(s3.Text);
            _s4 = int.Parse(s4.Text);
            _s5 = int.Parse(s5.Text);
            _s6 = int.Parse(s6.Text);
            _s7 = int.Parse(s7.Text);
            _s8 = int.Parse(s8.Text);
            _s9 = int.Parse(s9.Text);

            if (_s1 > 150000)
            {
                MessageBox.Show("Must be Less than 1.5 lakh");
                _s1 = 0; s1.Text = "0"; s1.Focus();
            }
            if (_s2 > 25000)
            {
                MessageBox.Show("Must be Less than 25k");
                _s2 = 0; s2.Text = "0"; s2.Focus();
            }
            if (_s3 > 50000)
            {
                MessageBox.Show("Must be Less than 50k");
                _s3 = 0; s3.Text = "0"; s3.Focus();
            }
            if (_s4 > 35000)
            {
                MessageBox.Show("Must be Less than 35k");
                _s4 = 0; s4.Text = "0"; s4.Focus();
            }

            if (_s7 > 10000)
            {
                MessageBox.Show("Must be Less than 10k");
                _s7 = 0; s7.Text = "0"; s7.Focus();
            }
            if (_s8 > 200000)
            {
                MessageBox.Show("Must be Less than 2 lakh");
                _s8 = 0; s8.Text = "0"; s8.Focus();
            }

            CalculateBenefits();
        }

        private void CalculateBenefits()
        {
            _s9 = _s1 + _s2 + _s3 + _s4 + _s5 + _s6 + _s7 + _s8;
            s9.Text = _s9.ToString();
        }

        private void TaxBenefits_Load(object sender, EventArgs e)
        {
            CalculateBenefits();
            s1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            benefitexemption = int.Parse(s9.Text);
            // instance the event args and pass it each value
            BenefitUpdateEventArgs args = new BenefitUpdateEventArgs(benefitexemption);
            // raise the event with the updated arguments
            benefitUpdated(this, args);
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }


        public int benefitexemption { get; set; }
    }

    #region Events
    public class BenefitUpdateEventArgs : System.EventArgs
    {
        // add local member variable to hold text
        private int _benefitexemption;

        // class constructor
        public BenefitUpdateEventArgs(int benefitexemption)
        {
            this._benefitexemption = benefitexemption;
        }
        // Properties - Accessible by the listener
        public int Benefitexemption
        {
            get
            {
                return _benefitexemption;
            }
        }
    }
    #endregion
}
