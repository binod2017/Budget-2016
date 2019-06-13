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
    public partial class HRAExemption : Form
    {
        // add a delegate
        public delegate void HraUpdateHandler(object sender, HraUpdateEventArgs e);
        // add an event of the delegate type
        public event HraUpdateHandler hraUpdated;
        public HRAExemption()
        {
            InitializeComponent();
            this.Text = "HRA Exemption Calculator";
            txt1.Focus();
        }

        string livein;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem.ToString() != "Select")
            {
                livein = comboBox1.SelectedItem.ToString();
                CalculateHra();
            }
        }

        private void CalculateHra()
        {
            _txt1 = int.Parse(txt1.Text);
            _txt2 = int.Parse(txt2.Text);
            _txt3 = int.Parse(txt3.Text);

            txt11.Text = (_txt1 * 12).ToString();
            txt22.Text = (_txt2 * 12).ToString();
            txt33.Text = (_txt3 * 12).ToString();

            if (livein == "Metro")
            {
                _txt44 = (_txt1 * 12 * 50) / 100;
                txt44.Text = _txt44.ToString();
            }
            else if (livein == "Non-Metro")
            {
                _txt44 = (_txt1 * 12 * 40) / 100;
                txt44.Text = _txt44.ToString();
            }
            _txt55 = _txt3 * 12 - ((10 * _txt1 * 12) / 100);
            txt55.Text = _txt55.ToString();
            //Minimum of txt22, txt44, txt55
            _txt66 = (Math.Min(Math.Min(_txt2 * 12, _txt44), _txt55));
            txt66.Text = _txt66.ToString();
        }

        private void txt1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsDigit(e.KeyChar) || e.KeyChar == 8)
            {
                //MessageBox.Show(e.KeyChar.ToString());
            }
            else { e.Handled = true; txt1.Focus(); }
        }

        private void txt1_Leave(object sender, EventArgs e)
        {
            CalculateHra();
        }
        int _txt1, _txt2, _txt3, _txt44, _txt55, _txt66;
        
        private void button2_Click(object sender, EventArgs e)
        {
            hraexemption = int.Parse(txt66.Text);
            // instance the event args and pass it each value
            HraUpdateEventArgs args = new HraUpdateEventArgs(hraexemption);
            // raise the event with the updated arguments
            hraUpdated(this, args);
            this.Close();
        }
        
        public int hraexemption { get; set; }
    }

    #region Events
    public class HraUpdateEventArgs : System.EventArgs
    {
        // add local member variable to hold text
        private int _hraexemption;

        // class constructor
        public HraUpdateEventArgs(int hraexemption)
        {
            this._hraexemption = hraexemption;
        }
        // Properties - Accessible by the listener
        public int Hraexemption
        {
            get
            {
                return _hraexemption;
            }
        }
    }
    #endregion
}
